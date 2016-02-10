// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Activateable.h"
#include "Activateable_Light.generated.h"

/**
 * 
 */
UCLASS()
class GAME_API AActivateable_Light : public AActivateable
{
	GENERATED_BODY()
	
public:

	AActivateable_Light();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		UPointLightComponent* light;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		float intensity;

	virtual void ReceivePing(AActivateable* sender) override;

	
	
};
