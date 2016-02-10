// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "MeleeWeapon.h"
#include "Monster.h"
#include "Avatar.h"
#include <iostream>

AMeleeWeapon::AMeleeWeapon(const class FObjectInitializer& PCIP) : Super(PCIP)
{
	AttackDamage = 1;
	Swinging = false;
	WeaponHolder = NULL;
	Mesh = PCIP.CreateDefaultSubobject<UStaticMeshComponent>(this, TEXT("Mesh"));
	RootComponent = Mesh;
	ProxBox = PCIP.CreateDefaultSubobject<UBoxComponent>(this, TEXT("ProxBox"));
	ProxBox->OnComponentBeginOverlap.AddDynamic(this, &AMeleeWeapon::Prox);
	ProxBox->AttachTo(RootComponent);
}

// Sets default values
AMeleeWeapon::AMeleeWeapon()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void AMeleeWeapon::BeginPlay()
{
	Super::BeginPlay();
}

// Called every frame
void AMeleeWeapon::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

void AMeleeWeapon::Prox_Implementation(AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult)
{
	if (OtherComp != OtherActor->GetRootComponent())
	{
		return;
	}

	if (Swinging && OtherActor != WeaponHolder && !ThingsHit.Contains(OtherActor))
	{
		//Books damage doesn't work
		//OtherActor->TakeDamage(AttackDamage + WeaponHolder->BaseAttackDamage, FDamageEvent(), NULL, this);

		if (Cast<AAvatar>(OtherActor)->Hp > 0)
			Cast<AAvatar>(OtherActor)->Hp -= AttackDamage + WeaponHolder->BaseAttackDamage;
		else
			Cast<AAvatar>(OtherActor)->Hp = 0;

		ThingsHit.Add(OtherActor);
	}
}

void AMeleeWeapon::Swing()
{
	ThingsHit.Empty();
	Swinging = true;
}

void AMeleeWeapon::Rest()
{
	ThingsHit.Empty();
	Swinging = false;
}
