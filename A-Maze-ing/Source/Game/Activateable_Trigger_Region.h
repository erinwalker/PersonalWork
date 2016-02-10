// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Activateable.h"
#include "Activateable_Trigger_Region.generated.h"

/**
 * 
 */
UCLASS()
class GAME_API AActivateable_Trigger_Region : public AActivateable
{
	GENERATED_BODY()
public:

	AActivateable_Trigger_Region();

	UPROPERTY(VisibleDefaultsOnly, BlueprintReadOnly, Category = "Activateable")
		UBoxComponent* ProxBox;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		bool onlyOnce;

	bool hasActivated;

	UFUNCTION(BlueprintNativeEvent, Category = Collision)
		void Prox(AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult&SweepResult);


	virtual void ReceivePing(AActivateable* sender) override;

	virtual void SendPing() override;

	
};
