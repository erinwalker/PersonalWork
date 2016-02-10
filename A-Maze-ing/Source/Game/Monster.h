// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
//#include "MeleeWeapon.h"
#include "GameFramework/Character.h"
#include "Monster.generated.h"

class AMeleeWeapon;

UCLASS()
class GAME_API AMonster : public ACharacter
{
	GENERATED_BODY()

public:
	// Sets default values for this character's properties
	AMonster();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;

	// Called to bind functionality to input
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		float Speed;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		float HitPoints;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		int32 Experience;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		UClass* BPLoot;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		float BaseAttackDamage;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		float AttackTimeout;

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = MonsterProperties)
		float TimeSinceLastStrike;

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = MonsterProperties)
		USphereComponent* SightSphere;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		USphereComponent* AttackRangeSphere;

	AMonster(const class FObjectInitializer& PCIP);

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		UClass* BPMeleeWeapon;

	AMeleeWeapon* MeleeWeapon;

	virtual void PostInitializeComponents() override;

	inline bool isInSightRange(float d){ return d < SightSphere->GetScaledSphereRadius(); }

	inline bool isInAttackRange(float d){ return d < AttackRangeSphere->GetScaledSphereRadius(); }

	UFUNCTION(BlueprintCallable, Category = Collision)
		bool isInAttackRangeOfPlayer();

	UFUNCTION(BlueprintCallable, Category = Collision)
		void SwordSwung();

	void Attack(AActor* thing);
	virtual float TakeDamage(float Damage, struct FDamageEvent const& DamageEvent, AController* EventInstigator, AActor* DamageCauser) override;
	FVector Knockback;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = MonsterProperties)
		bool IsInAttackRangeBool;
};
