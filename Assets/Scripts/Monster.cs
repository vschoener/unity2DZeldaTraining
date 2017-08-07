using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Monster : MonoBehaviour {

    protected int health;

	// Use this for initialization
	void Start () {
		this.health = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "sword") {

            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            
            this.health -= weapon.getDamage();

            if (this.health <= 0) {
                this.deathAnimation();
                Destroy(gameObject);
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().releaseAllAction();
            weapon.destroyWeapon();
        }
    }

    abstract public void deathAnimation();
}
