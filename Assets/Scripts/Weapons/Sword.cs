using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

	// Use this for initialization
	void Start () {
		this.damage = 1;
	}  
    public override void destroyWeapon()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().hasSpecialAttack()) {
            Instantiate(this.destroySwordEffect, gameObject.transform.position, gameObject.transform.rotation);
        } 
        Destroy(gameObject);
    }
}
