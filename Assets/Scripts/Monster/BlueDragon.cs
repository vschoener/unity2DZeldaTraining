﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster {
    public class BlueDragon : AbstractEnemy {

        public GameObject deathParticle;

        // Use this for initialization
        protected override void Start () {
            base.Start();	
            this.health = 2;
            this.dammageOnCollision = 2;
        }
        
        // Update is called once per frame
        protected override void Update () {
            base.Update();
        }

        public override void deathAnimation()
        {
            Instantiate(this.deathParticle, transform.position, transform.rotation); 
        }
    }
}
