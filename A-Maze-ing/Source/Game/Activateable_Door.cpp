// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Activateable_Door.h"


AActivateable_Door::AActivateable_Door() :
Steps(50.f), Granularity(10), onlyOnce(false), isOpen(false), moving(false), moveCount(0), waitCount(0), Angle(90)
{
	fro = GetActorRotation();
}

void AActivateable_Door::SendPing(){
	Super::SendPing();
}

void AActivateable_Door::ReceivePing(AActivateable* sender){
	Super::ReceivePing(sender);
	
	if (onlyOnce == true)
	{
		if (!moving){
			moving = true;
		}
		onlyOnce = false;
	}
}

void AActivateable_Door::Tick(float DeltaTime){
	Super::Tick(DeltaTime);
	
	if (moving){
		if (waitCount >= Granularity){
			waitCount = 0;
			Move(moveCount);
			moveCount++;
		}

		waitCount++;

		if (moveCount >= Steps){
			moving = false;
		}
	}
}

void AActivateable_Door::Move(float DeltaTime){
	
	fro.Yaw = (Angle / Steps);

	this->AddActorLocalRotation(fro);
}

void AActivateable_Door::Ping()
{
	ReceivePing(this);
}