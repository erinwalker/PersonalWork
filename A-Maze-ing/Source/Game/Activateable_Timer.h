// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Activateable.h"
#include "Activateable_Timer.generated.h"

/**
 * 
 */
UCLASS()
class GAME_API AActivateable_Timer : public AActivateable
{
	GENERATED_BODY()
public:

	AActivateable_Timer();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		bool running;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		float time;

	virtual void ReceivePing(AActivateable* sender) override;

	virtual void SendPing() override;

	virtual void Tick(float DeltaSeconds) override;
	
	
	
};
