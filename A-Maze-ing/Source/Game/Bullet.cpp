// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Bullet.h"


// Sets default values
ABullet::ABullet()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

ABullet::ABullet(const class FObjectInitializer& PCIP) : Super(PCIP)
{
	Mesh = PCIP.CreateDefaultSubobject<UStaticMeshComponent>(this, TEXT("Mesh"));
	RootComponent = Mesh;
	ProxSphere = PCIP.CreateDefaultSubobject<USphereComponent>(this, TEXT("ProxSphere"));
	ProxSphere->AttachTo(RootComponent);
	
	ProxSphere->OnComponentBeginOverlap.AddDynamic(this, &ABullet::Prox);

	Damage = 1;
	
}

// Called when the game starts or when spawned
void ABullet::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ABullet::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

void ABullet::Prox_Implementation(AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult)
{
	/*if (OtherComp != OtherActor->GetRootComponent())
	{
		return;
	}

	if (Cast<AAvatar>(OtherActor)->Hp > 0)
		Cast<AAvatar>(OtherActor)->Hp -= AttackDamage + WeaponHolder->BaseAttackDamage;
	else
		Cast<AAvatar>(OtherActor)->Hp = 0;


	this->Destroy();*/
	
}

