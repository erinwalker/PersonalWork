// Fill out your copyright notice in the Description page of Project Settings.

#include "Game.h"
#include "Activateable_Light.h"



AActivateable_Light::AActivateable_Light() :
light(CreateDefaultSubobject<UPointLightComponent>("Light")), intensity(5000.f)
{
}

void AActivateable_Light::ReceivePing(AActivateable* sender){
	Super::ReceivePing(sender);
	light->SetIntensity(intensity);
}
