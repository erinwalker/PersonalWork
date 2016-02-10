// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Activateable_Splitter.h"

AActivateable_Splitter::AActivateable_Splitter()
{

}

void AActivateable_Splitter::SendPing(){
	Super::SendPing();

	for (int i = 0; i < SecondaryTargets.Num(); i++){
		SecondaryTargets[i]->ReceivePing(this);
	}
}

void AActivateable_Splitter::ReceivePing(AActivateable* sender){
	Super::ReceivePing(sender);
	SendPing();
}


