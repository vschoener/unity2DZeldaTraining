using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour, WeaponInterface {

    protected int damage;

    public GameObject destroySwordEffect;

	// Use this for initialization
	void Start () {
        this.damage = 0;
	}
	
	// Update is called once per frame
	protected void Update () {

	}

    public int getDamage()
    {
        return this.damage;
    }

    abstract public void destroyWeapon();
}
