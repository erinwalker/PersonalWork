using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class DeepThoughtDialogue : DeepThought
{


    //Dialogue nodes call this function from their script line
    //Checks what the current weight is and chooses whether to add a positive or negative text to the list
    //Also it decreases all dialogue type deep thoughts by 1/4 if the deep though already exists in the list
    public void SetValues()
    {
        string _posDT = DialogueLua.GetVariable("PosDT").AsString;
        string _negDT = DialogueLua.GetVariable("NegDT").AsString;
        bool _reactable = DialogueLua.GetVariable("Reactable").AsBool;
        string _reactableConvo = DialogueLua.GetVariable("ReactableConvo").AsString;

        if (Text.Contains(_posDT) || Text.Contains(_negDT))
        {
            Value = DeepThoughtManager.MAX_VALUE / 4;
        }


        if (!Text.Contains(_posDT) && !Text.Contains(_posDT))
        {
            if (WorldViewScript.CurrentWorldView >= 0)
                Text = _posDT;
            else
                Text = _negDT;

            ReactableConvo = _reactableConvo;
            IsReactableThought = _reactable;
            Value = DeepThoughtManager.MAX_VALUE;
            TypeOfDeepThought = TypeOfDT.Dialogue;
        }
    }
}