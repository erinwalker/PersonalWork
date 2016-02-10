// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Activateable.h"
#include "Activateable_Splitter.generated.h"

/**
 * 
 */
UCLASS()
class GAME_API AActivateable_Splitter : public AActivateable
{
	GENERATED_BODY()
	
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		TArray<AActivateable*> SecondaryTargets;
	

	virtual void ReceivePing(AActivateable* sender) override;

	virtual void SendPing() override;

	AActivateable_Splitter();
	
};
