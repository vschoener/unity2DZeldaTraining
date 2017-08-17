using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private int damageAmount;

    private float attackDuration;

	// Use this for initialization
	void Start () {
		this.attackDuration = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		this.attackDuration -= Time.deltaTime;

        if (this.attackDuration < 0) {
            Destroy(this.gameObject);
        }
	}

    public void setDamageAmount(int amount)
    {
        this.damageAmount = amount;
    }

    public int getDamageAmount()
    {
        return this.damageAmount;
    }
}
