// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Monster.h"
#include "Avatar.h"
#include "MeleeWeapon.h"

AMonster::AMonster(const class FObjectInitializer& PCIP) : Super(PCIP)
{
	Speed = 20;
	HitPoints = 20;
	Experience = 0;
	BPLoot = NULL;
	BaseAttackDamage = 1;
	AttackTimeout = 1.5f;
	TimeSinceLastStrike = 0;
	SightSphere = PCIP.CreateDefaultSubobject<USphereComponent>(this, TEXT("SightSphere"));
	SightSphere->AttachTo(RootComponent);
	AttackRangeSphere = PCIP.CreateDefaultSubobject<USphereComponent>(this, TEXT("AttackRangeSphere"));
	AttackRangeSphere->AttachTo(RootComponent);
	IsInAttackRangeBool = false;
}

// Sets default values
AMonster::AMonster()
{
 	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void AMonster::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AMonster::Tick( float DeltaTime )
{
	Super::Tick(DeltaTime);
	AAvatar *avatar = Cast<AAvatar>(UGameplayStatics::GetPlayerPawn(GetWorld(), 0));
	if (!avatar) return;
	FVector toPlayer = avatar->GetActorLocation() - GetActorLocation();
	float distanceToPlayer = toPlayer.Size();

	if (distanceToPlayer > SightSphere->GetScaledSphereRadius())
	{
		return;
	}

	toPlayer /= distanceToPlayer;
	//AddMovementInput(toPlayer, Speed*DeltaTime);
	FRotator toPlayerRotation = toPlayer.Rotation();
	toPlayerRotation.Pitch = 0;
	RootComponent->SetWorldRotation(toPlayerRotation);

	if (isInAttackRange(distanceToPlayer))
	{
		// Perform the attack.
		if (!TimeSinceLastStrike)
		{
			// If the cooldown is over, then attack again
			// This resets the hit parameter.
			Attack(avatar);
		}

		TimeSinceLastStrike += DeltaTime;
		if (TimeSinceLastStrike > AttackTimeout)
		{
			TimeSinceLastStrike = 0;
		}
		//Mesh->AnimScriptInstance->PlaySlotAnimation(sword)
		return;	// nothing else to do.
	}
	else
	{
		if (MeleeWeapon)
		{
			// rest the melee weapon (not swinging)
			MeleeWeapon->Rest();
		}

		// not in attack range, so walk towards player
		AddMovementInput(toPlayer, Speed*DeltaTime);
	}

}

// Called to bind functionality to input
void AMonster::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	Super::SetupPlayerInputComponent(InputComponent);

}

void AMonster::PostInitializeComponents()
{
	Super::PostInitializeComponents();

	// instantiate the melee weapon if a bp was selected
	if (BPMeleeWeapon)
	{
		MeleeWeapon = GetWorld()->SpawnActor<AMeleeWeapon>(
			BPMeleeWeapon, FVector(0), FRotator(0));

		// Always check that this did not fail. If a blueprint
		// that does not derive from AMeleeWeapon is selected,
		// then the above line would fail.
		if (MeleeWeapon)
		{
			MeleeWeapon->WeaponHolder = this;
			const USkeletalMeshSocket *socket = GetMesh()->GetSocketByName("RightHandSocket");
			socket->AttachActor(MeleeWeapon, GetMesh());
		}
		else
		{
			FString msg = GetName() + FString(" cannot instantiate meleeweapon ") +
				BPMeleeWeapon->GetName();
			GEngine->AddOnScreenDebugMessage(0, 5.f, FColor::Red, msg);
		}
	}
}

bool AMonster::isInAttackRangeOfPlayer()
{
	AAvatar *avatar = Cast<AAvatar>(UGameplayStatics::GetPlayerPawn(GetWorld(), 0));
	if (!avatar) return false;
	FVector playerPos = avatar->GetActorLocation();
	FVector toPlayer = playerPos - GetActorLocation();
	float distanceToPlayer = toPlayer.Size();
	IsInAttackRangeBool = distanceToPlayer < AttackRangeSphere->GetScaledSphereRadius();
	return IsInAttackRangeBool;
}

void AMonster::SwordSwung()
{
	if (MeleeWeapon)
	{
		MeleeWeapon->Swing();
	}
}

void AMonster::Attack(AActor* thing)
{
	if (MeleeWeapon)
	{
		MeleeWeapon->Swing();
	}
}

float AMonster::TakeDamage(float Damage, struct FDamageEvent const& DamageEvent, AController* EventInstigator, AActor* DamageCauser)
{
	Super::TakeDamage(Damage, DamageEvent, EventInstigator, DamageCauser);

	HitPoints -= Damage;

	Knockback = GetActorLocation() - DamageCauser->GetActorLocation();
	Knockback.Normalize();
	Knockback *= 500;

	if (HitPoints <= 0)
	{
		// award the avatar exp
		//AAvatar *avatar = Cast<AAvatar>(UGameplayStatics::GetPlayerPawn(GetWorld(), 0));
		//avatar->Experience += Experience;
		this->Destroy();
		MeleeWeapon->Destroy();
	}

	return Damage;
}