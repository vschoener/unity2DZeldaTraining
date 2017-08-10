using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster {
    public class BlueCrab : AbstractEnnemy {

        public GameObject deathParticle;

        // Use this for initialization
        protected override void Start () {
            base.Start();
            this.health = 2;
            this.movementSpeed = 0.7f;
            this.damageAmont = 1;
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
