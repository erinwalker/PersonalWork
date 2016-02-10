// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "Activateable.generated.h"

UCLASS()
class GAME_API AActivateable : public AActor
{
	GENERATED_BODY()
	
public:	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		FString debugName;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Activateable")
		AActivateable* target;

	// Sets default values for this actor's properties
	AActivateable();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

	// Called every frame
	virtual void Tick(float DeltaSeconds) override;

	virtual void ReceivePing(AActivateable* sender);

	virtual void SendPing();
};
