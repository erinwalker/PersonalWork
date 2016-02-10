// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Avatar.h"
#include "PickupItem.h"
#include "MyHUD.h"


AAvatar::AAvatar(const class FObjectInitializer& PCIP) : Super(PCIP)
{
	inventoryShowing = false;
	Hp = MaxHp = 100;
	numberOfItems = 0;
	enteredLose = false;
	enteredWin = false;
	numOfItems = 0;
	castSpell = false;
}


// Sets default values
AAvatar::AAvatar()
{
 	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void AAvatar::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AAvatar::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );
	for (TMap<FString, int>::TIterator it = Backpack.CreateIterator(); it; ++it)
	{
		numOfItems = it->Value;
		if (numOfItems == 5 && enteredWin == false)
		{
			APlayerController* PController = GetWorld()->GetFirstPlayerController();
			AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
			hud->addMessage(Message(FString("Find the open door and exit to win!"), 50.f, FColor::Green, FColor::Black));
			enteredWin = true;
		}
	}	

	if (Hp <= 0 && enteredLose == false)
	{
		APlayerController* PController = GetWorld()->GetFirstPlayerController();
		AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
		//hud->DrawText(new FString("You Win!"), new FVector2D(0, 0), hud->hudFont, 20, FColor::Green);
		hud->addMessage(Message(FString("You're Dead!"), 50.f, FColor::Red, FColor::Black));
		enteredLose = true;
	}
}

// Called to bind functionality to input
void AAvatar::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	check(InputComponent);
	InputComponent->BindAxis("Forward", this, &AAvatar::MoveForward);
	InputComponent->BindAxis("Strafe", this, &AAvatar::MoveRight);
	InputComponent->BindAxis("Yaw", this, &AAvatar::Yaw);
	InputComponent->BindAxis("Pitch", this, &AAvatar::Pitch);
	InputComponent->BindAction("Inventory", IE_Pressed, this, &AAvatar::ToggleInventory);
	InputComponent->BindAction("MouseClickedLMB", IE_Pressed, this, &AAvatar::MouseClicked);
	//InputComponent->BindAction("MouseClickedRMB", IE_Pressed, this, &AAvatar::MouseRightClicked);
	InputComponent->BindAction("EClicked", IE_Pressed, this, &AAvatar::EClicked);
}

void AAvatar::MoveForward(float amount)
{
	if (Controller && amount)
	{
		FVector fwd = GetActorForwardVector();
		AddMovementInput(fwd, amount);
	}
}

void AAvatar::MoveRight(float amount)
{
	if (Controller && amount)
	{
		FVector right = GetActorRightVector();
		AddMovementInput(right, amount);
	}
}

void AAvatar::Yaw(float amount)
{
	if (inventoryShowing)
	{
		APlayerController* PController = GetWorld()->GetFirstPlayerController();
		AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
		hud->MouseMoved();
		return;
	}
	else
	{
		AddControllerYawInput(200.f*amount * GetWorld()->GetDeltaSeconds());
	}
}

void AAvatar::Pitch(float amount)
{
	if (inventoryShowing)
	{
		// if the button is down click drag
		APlayerController* PController = GetWorld()->GetFirstPlayerController();
		AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
		hud->MouseMoved();
	}
	else
	{
		AddControllerPitchInput(200.f*amount * GetWorld()->GetDeltaSeconds());
	}
}


void AAvatar::ToggleInventory()
{
	APlayerController* PController = GetWorld()->GetFirstPlayerController();
	AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());

	// If inventory is displayed, undisplay it.
	if (inventoryShowing)
	{
		hud->clearWidgets();
		inventoryShowing = false;
		PController->bShowMouseCursor = false;
		return;
	}

	// Otherwise, display the player's inventory
	inventoryShowing = true;
	PController->bShowMouseCursor = true;
	for (TMap<FString, int>::TIterator it = Backpack.CreateIterator(); it; ++it)
	{
		// Combine string name of the item, with qty eg Cow x 5
		FString fs = it->Key + FString::Printf(TEXT(" x %d"), it->Value);
		UTexture2D* tex = NULL;
		if (Icons.Find(it->Key))
		{
			//tex = Icons[it->Key];
			//auto icon = Icon(fs, tex);
			//auto classes = Classes[it->Key];
			//Widget w(icon, classes);
			//hud->addWidget(w);
			//hud->addWidget(Widget(Icon(fs, tex)));

			tex = Icons[it->Key];
			Widget w(Icon(fs, tex), Classes[it->Key]);
			w.bpSpell = Spells[it->Key];
			hud->addWidget(w);
		}
	}
}

void AAvatar::Pickup(APickupItem *item)
{
	if (Backpack.Find(item->Name))
	{
		Backpack[item->Name] += item->Quantity;
	}
	else
	{
		Backpack.Add(item->Name, item->Quantity);
		Icons.Add(item->Name, item->Icon);
		Classes.Add(item->Name, item->GetClass());
		Spells.Add(item->Name, item->Spell);
	}
}

void AAvatar::MouseClicked()
{
	if (inventoryShowing)
	{
		APlayerController* PController = GetWorld()->GetFirstPlayerController();
		AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
		hud->MouseClicked();
	}
}

void AAvatar::Drop(UClass *className)
{
	GetWorld()->SpawnActor<AActor>(
		className, GetActorLocation() + GetActorForwardVector() * 200 + FVector(0, 0, 200),
		GetMesh()->GetTransformMatrix().Rotator());
}

void AAvatar::CastSpell(UClass* bpSpell)
{
	// instantiate the spell and attach to character
	ASpell *spell = GetWorld()->SpawnActor<ASpell>(
		bpSpell, FVector(0), FRotator(0));

	if (spell)
	{
		spell->SetCaster(this);
		castSpell = true;
	}
	else
	{
		GEngine->AddOnScreenDebugMessage(1, 5.f, FColor::White,
			FString("can't cast ") + bpSpell->GetName());
	}
}

/*void AAvatar::MouseRightClicked()
{
	if (inventoryShowing)
	{
		APlayerController* PController = GetWorld()->GetFirstPlayerController();
		AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
		hud->MouseRightClicked();
	}
}*/
void AAvatar::EClicked()
{
	if (inventoryShowing)
	{
		APlayerController* PController = GetWorld()->GetFirstPlayerController();
		AMyHUD* hud = Cast<AMyHUD>(PController->GetHUD());
		hud->MouseRightClicked();
	}
}