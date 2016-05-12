using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public enum TypeOfDT { Dialogue, Trigger, PoI}

public class DeepThought : MonoBehaviour 
{
    //Public Variables
    [HideInInspector]
    public string Text;
    [HideInInspector]
    public float Value;
    public TypeOfDT TypeOfDeepThought;
    public bool IsReactableThought;
    public string ReactableConvo;
}