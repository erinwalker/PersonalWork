// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "PickupItem.h"
#include "Avatar.h"
#include "MyHUD.h"

APickupItem::APickupItem(const class FObjectInitializer& PCIP) : Super(PCIP)
{
	Name = "UNKNOWN ITEM";
	Quantity = 0;

	ProxSphere = PCIP.CreateDefaultSubobject<USphereComponent>(this, TEXT("ProxSphere"));
	Mesh = PCIP.CreateDefaultSubobject<UStaticMeshComponent>(this, TEXT("Mesh"));
	//AudioComp = PCIP.CreateDefaultSubobject<UAudioComponent>(this, TEXT("AudioComp"));
	RootComponent = Mesh;
	Mesh->SetSimulatePhysics(true);
	ProxSphere->OnComponentBeginOverlap.AddDynamic(this, &APickupItem::Prox);
	ProxSphere->AttachTo(Mesh);
	//AudioComp->AttachParent = RootComponent;
	//AudioComp->bAutoActivate = false;
	//AudioComp->bStopWhenOwnerDestroyed = false;
	Hit = false;

}

// Sets default values
APickupItem::APickupItem()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void APickupItem::BeginPlay()
{
	Super::BeginPlay();
}

// Called every frame
void APickupItem::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

void APickupItem::Prox_Implementation(AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult)
{
	if (Cast<AAvatar>(OtherActor) == nullptr)
	{
		return;
	}
	Hit = true;
	AAvatar *avatar = Cast<AAvatar>(UGameplayStatics::GetPlayerPawn(GetWorld(), 0));

	avatar->Pickup(this);

	APlayerController* PController = GetWorld()->GetFirstPlayerController();
	//AudioComp->Activate(true);
	//AudioComp->Play(0.0f);
	AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
	hud->addMessage(Message(Icon, FString("Picked Up") + FString::FromInt(Quantity) + FString(" ") + Name, 5.f, FColor::White, FColor::Black));
	//this->Destroy();
	//this->SetActorEnableCollision(false);
	//this->SetActorHiddenInGame(true);
	//this->SetActorTickEnabled(false);
}
