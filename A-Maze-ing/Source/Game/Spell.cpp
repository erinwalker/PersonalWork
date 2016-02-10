// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Spell.h"
#include "Monster.h"


ASpell::ASpell(const class FObjectInitializer& PCIP) : Super(PCIP)
{
	ProxBox = PCIP.CreateDefaultSubobject<UBoxComponent>(this, TEXT("ProxBox"));
	Particles = PCIP.CreateDefaultSubobject<UParticleSystemComponent>(this, TEXT("ParticleSystem"));
	RootComponent = Particles;
	ProxBox->AttachTo(RootComponent);
	Duration = 3;
	DamagePerSeconds = 1;
	TimeAlive = 0;
	PrimaryActorTick.bCanEverTick = true;
}

// Sets default values
/*ASpell::ASpell()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}*/

// Called when the game starts or when spawned
void ASpell::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ASpell::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

	TArray<AActor*> actors;
	ProxBox->GetOverlappingActors(actors);
	for (int c = 0; c < actors.Num(); c++)
	{
		if (actors[c] != Caster)
		{
			AMonster *monster = Cast<AMonster>(actors[c]);

			if (monster && ProxBox->IsOverlappingComponent(monster->GetCapsuleComponent()))
			{
				monster->TakeDamage(DamagePerSeconds*DeltaTime, FDamageEvent(), 0, this);
			}
		}
		TimeAlive += DeltaTime;
		if (TimeAlive > Duration)
		{
			this->Destroy();
		}
	}
}

void ASpell::SetCaster(AActor *caster)
{
	Caster = caster;
	AttachRootComponentTo(caster->GetRootComponent());
}
