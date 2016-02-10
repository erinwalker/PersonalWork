// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Activateable_Timer.h"

AActivateable_Timer::AActivateable_Timer() :
running(false), time(10.f)
{
	
}

void AActivateable_Timer::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

	if (running == true && time > 0.f){
		time -= GetWorld()->GetDeltaSeconds();
	}

	if (running == true && time <= 0.f){
		running = false;
		SendPing();
	}
}

void AActivateable_Timer::ReceivePing(AActivateable* sender){
	Super::ReceivePing(sender);
	running = true;
}

void AActivateable_Timer::SendPing(){
	Super::SendPing();
}


