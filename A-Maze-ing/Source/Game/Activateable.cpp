// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Activateable.h"


// Sets default values
AActivateable::AActivateable() : debugName("Default Name")
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void AActivateable::BeginPlay()
{
	Super::BeginPlay();

}

// Called every frame
void AActivateable::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

void AActivateable::ReceivePing(AActivateable* sender){
	if (GEngine){
		GEngine->AddOnScreenDebugMessage(1, 2.f, FColor::Red, debugName + FString(" received ping"));
	}
}

void AActivateable::SendPing(){
	if (Cast<AActivateable>(target) == nullptr)
		return;

	target->ReceivePing(this);

	if (GEngine){
		GEngine->AddOnScreenDebugMessage(0, 2.f, FColor::Blue, debugName + FString(" sent ping"));
	}
}

