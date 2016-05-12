using UnityEngine;
using System.Collections;

public abstract class InventoryItem : MonoBehaviour {

	internal Sprite itemPicture;

	public int HealthBonus;
	public int MightBonus;
	public int MobilityBonus;
	public int FortuityBonus;
	
	public bool HasUses;
	public int Uses;
	
	public float CoolDownTime;
	public float CurrentCoolDown;
	public float FillValue;
	internal bool Ready;

	public string ItemDescription;
	public string ItemName;
	
	public virtual void Start()
	{
		Ready = true;
		SetSprite ();
	}
	
	public void SetSprite()
	{
		itemPicture = GetComponent<SpriteRenderer>().sprite;
	}
	
	public virtual void Update()
	{
		if(!Ready)
		{
			RunCoolDownTime();
		}
	}
	
	public virtual void AddBonus()
	{
		//add health to the bonus
		Player.bonusMight += MightBonus;
		Player.bonusMobility += MobilityBonus;
		Player.bonusFortuity += FortuityBonus;
		
		Player.UpdateStats();
		
		//add a heart for each of the bonus added
		//Player.playerCurrentHealth += HealthBonus * 2;
	}
	
	public virtual void RemoveBonus()
	{
		Player.bonusMight -= MightBonus;
		Player.bonusMobility -= MobilityBonus;
		Player.bonusFortuity -= FortuityBonus;
		
		Player.UpdateStats();
	}

	public void AddHealthBonus()
	{
		Player.bonusHealth += HealthBonus;
		Player.UpdateStats();
	}

	public void RemoveHealthBonus()
	{
		Player.bonusHealth -= HealthBonus;
		Player.UpdateStats();
	}
	
	public virtual void Pickup()
	{
	}
	
	public virtual void Drop()
	{
	}
	
	public virtual void Use()
	{
		if (CoolDownTime > 0) 
		{
			StartCoroutine ("Cooldown");
			
			if(HasUses)
			{
				Uses--;
			}
		}
	}
	
	public void CheckIfUsedUp()
	{
		if(Uses == 0)
		{
			GameObject.FindGameObjectWithTag("PlayerPhysics").GetComponent<InventoryManager>().DestroyCurrentItem();
		}
	}
	
	public void RunCoolDownTime()
	{
		CurrentCoolDown -= Time.deltaTime;
		
		FillValue = (CurrentCoolDown / CoolDownTime);
	}
	
	IEnumerator Cooldown()
	{
		CurrentCoolDown = CoolDownTime;
		Ready = false;
		yield return new WaitForSeconds(CoolDownTime);
		Ready = true;
	}
}