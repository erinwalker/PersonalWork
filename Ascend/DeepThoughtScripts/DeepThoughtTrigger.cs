using UnityEngine;
using System.Collections;

public class DeepThoughtTrigger : DeepThought {
    public string PositiveDeepThought;
    public string NegativeDeepThought;
    public int Weight;
    DeepThoughtManager DM;

    void Start()
    {
        DM = GameObject.Find("Dialogue Manager").GetComponent<DeepThoughtManager>();
    }

	void OnTriggerEnter () 
    {
        SetValues();
        DM.DeepThoughtList.Add(this);
	}
    
    void OnTriggerExit()
    {
        DM.RemoveDeepThoughtTrigger(this);
    }

    public void SetValues()
    {
        //DM.tempDeepThoughtTrig = this;

        TypeOfDeepThought = TypeOfDT.Trigger;

        if (WorldViewScript.CurrentWorldView >= 0)
            Text = PositiveDeepThought;
        else
            Text = NegativeDeepThought;

        if (Weight <= 0)
            Value = DeepThoughtManager.MAX_VALUE * 0.75f;
        else
            Value = Weight;
    }
}
