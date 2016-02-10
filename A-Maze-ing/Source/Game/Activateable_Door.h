// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Activateable.h"
#include "Activateable_Door.generated.h"

/**
 * 
 */

UCLASS()
class GAME_API AActivateable_Door : public AActivateable
{
	GENERATED_BODY()

public:

	AActivateable_Door();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		float Steps;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		bool onlyOnce;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		float Granularity;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		float Angle;

	FRotator fro;

	bool isOpen;
	bool moving;
	int moveCount;
	int waitCount;

	void Move(float DeltaTime);

	virtual void ReceivePing(AActivateable* sender) override;

	virtual void SendPing() override;

	virtual void Tick(float DeltaTime) override;

	UFUNCTION(BlueprintCallable, Category = Ping)
		void Ping();
	
	
	
};
