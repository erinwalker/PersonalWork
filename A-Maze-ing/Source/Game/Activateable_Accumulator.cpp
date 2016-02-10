// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Activateable_Accumulator.h"


AActivateable_Accumulator::AActivateable_Accumulator() :
SignalsReceived(0), SignalsRequired(3), loop(false)
{

}

void AActivateable_Accumulator::ReceivePing(AActivateable* sender){
	Super::ReceivePing(sender);

	SignalsReceived++;

	if (SignalsReceived == SignalsRequired){
		Super::SendPing();
		if (loop)
			SignalsReceived = 0;
	}
}

