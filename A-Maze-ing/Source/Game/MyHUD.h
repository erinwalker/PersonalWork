// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/HUD.h"
#include "MyHUD.generated.h"

struct Icon
{
	FString name;
	UTexture2D* tex;
	Icon(){ name = "UNKNOWN ICON"; tex = 0; }
	Icon(FString& iName, UTexture2D* iTex)
	{
		name = iName;
		tex = iTex;
	}
};

struct Widget
{
	Icon icon;
	// in case you need to drop an item, this is the class the item was from
	UClass *className;
	// bpSpell is the blueprint of the spell this widget casts
	UClass *bpSpell;
	FVector2D pos, size;
	Widget(Icon iicon, UClass *iClassName)
	{
		icon = iicon;
		className = iClassName;
	}
	float left(){ return pos.X; }
	float right(){ return pos.X + size.X; }
	float top(){ return pos.Y; }
	float bottom(){ return pos.Y + size.Y; }
	bool hit(FVector2D v)
	{
		// +---+ top (0)
		// |   |
		// +---+ bottom (2) (bottom > top)
		// L   R
		return v.X > left() && v.X < right() && v.Y > top() && v.Y < bottom();
	}
};

struct Message
{
	FString message;
	float time;
	FColor frontColor;
	FColor backColor;
	UTexture2D* tex;
	Message()
	{
		time = 5.f;
		time = 5.f;
		frontColor = FColor::White;
		backColor = FColor::Black;
	}
	Message(FString iMessage, float iTime, FColor iFrontColor, FColor iBackColor)
	{
		message = iMessage;
		time = iTime;
		frontColor = iFrontColor;
		backColor = iBackColor;
		tex = 0;
	}

	Message(UTexture2D* iTex, FString iMessage, float iTime, FColor iFrontColor, FColor iBackColor)
	{
		tex = iTex;
		message = iMessage;
		time = iTime;
		frontColor = iFrontColor;
		backColor = iBackColor;
	}
};

/**
 No Copyright
 */
UCLASS()
class GAME_API AMyHUD : public AHUD
{
	GENERATED_BODY()
	
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = HUDFont)
		UFont* hudFont;

	virtual void DrawHUD() override;

	TArray<Message> messages;
	void addMessage(Message msg);
	void DrawMessages();

	FVector2D dims;

	TArray<Widget> widgets;
	Widget* heldWidget;
	void DrawWidgets();
	void clearWidgets();
	void addWidget(Widget widget);

	void MouseClicked();
	void MouseMoved();
	void MouseRightClicked();

	void DrawHealthbar();
};
