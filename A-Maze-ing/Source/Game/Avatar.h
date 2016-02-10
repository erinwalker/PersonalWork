// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Spell.h"
#include "GameFramework/Character.h"
#include "Avatar.generated.h"
class APickupItem;

UCLASS()
class GAME_API AAvatar : public ACharacter
{
	GENERATED_BODY()

public:
	// Sets default values for this character's properties
	AAvatar();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;

	// Called to bind functionality to input
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;
	void MoveForward(float amount);
	void MoveRight(float amount);
	void Yaw(float amount);
	void Pitch(float amount);

	TMap<FString, int> Backpack;
	TMap<FString, UTexture2D*> Icons;
	bool inventoryShowing;
	void Pickup(APickupItem *item);
	void ToggleInventory();
	
	void MouseClicked();

	AAvatar(const class FObjectInitializer& PCIP);

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = PlayerProperties)
		float Hp;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = PlayerProperties)
		float MaxHp;

	void CastSpell(UClass* bpSpell);

	void Drop(UClass *className);

	TMap<FString, UClass*> Classes;

	int numberOfItems;
	bool enteredWin;
	bool enteredLose;

	TMap<FString, UClass*> Spells;
	//void MouseRightClicked();
	void EClicked();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = PlayerProperties)
		int32 numOfItems;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = PlayerProperties)
		bool castSpell;
};
