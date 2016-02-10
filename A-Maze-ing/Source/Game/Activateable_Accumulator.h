// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Activateable.h"
#include "Activateable_Accumulator.generated.h"

/**
 * 
 */
UCLASS()
class GAME_API AActivateable_Accumulator : public AActivateable
{
	GENERATED_BODY()
	
public:

	AActivateable_Accumulator();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		int32 SignalsRequired;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		bool loop;

	int SignalsReceived;

	virtual void ReceivePing(AActivateable* sender) override;


	
	
};
