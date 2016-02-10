// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Activateable_Trigger_Region.h"
#include "Avatar.h"

AActivateable_Trigger_Region::AActivateable_Trigger_Region() :
ProxBox(CreateDefaultSubobject<UBoxComponent>("Proximity Box")),
hasActivated(false),
onlyOnce(false)
{
	ProxBox->OnComponentBeginOverlap.AddDynamic(this, &AActivateable_Trigger_Region::Prox);
	ProxBox->AttachTo(RootComponent);
}

void AActivateable_Trigger_Region::SendPing(){
	Super::SendPing();
	hasActivated = true;
}

void AActivateable_Trigger_Region::ReceivePing(AActivateable* sender){
	Super::ReceivePing(sender);
}

void AActivateable_Trigger_Region::Prox_Implementation(AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult){
	if (Cast<AAvatar>(OtherActor) == nullptr){
		return;
	}

	if (onlyOnce && hasActivated){
		return;
	}

	SendPing();
}


