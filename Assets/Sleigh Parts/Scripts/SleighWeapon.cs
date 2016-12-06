using UnityEngine;
using System.Collections;

public class SleighWeapon : SleighComponent
{

	public int damageOnHit = 20;
	public int damageOnStay = 10;
	public bool onlyDamageIfMoving = true;
	public float minSpeedForMoving = 2;
	public bool canDamage = false;
	// Use this for initialization
	void Start ()
	{
		SetSleigh (GetComponentInParent<SleighController> ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (onlyDamageIfMoving == true && sleigh.actualSpeed < minSpeedForMoving) {
			canDamage = false;
		} else {
			canDamage = true;
		}
	}

	public override void EnterCollision (Elf elf)
	{
		elf.TakeDamage (sleigh.controllerNumber, damageOnHit);
	}

	public override void StayCollision (Elf elf)
	{
		elf.TakeDamage (sleigh.controllerNumber, damageOnHit);
	}
}
