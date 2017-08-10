using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster {
    public class BlueDragon : AbstractEnnemy {

        public GameObject deathParticle;

        // Use this for initialization
        protected override void Start () {
            this.health = 2;
            base.Start();	
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
