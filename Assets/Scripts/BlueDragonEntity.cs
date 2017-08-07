using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDragonEntity : Monster {

    public GameObject deathParticle;

	// Use this for initialization
	void Start () {
        this.health = 2;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void deathAnimation()
    {
        Instantiate(this.deathParticle, transform.position, transform.rotation); 
    }
}
