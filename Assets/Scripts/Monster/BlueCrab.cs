using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster {
    public class BlueCrab : AbstractEnemy {

        public GameObject deathParticle;

        // Use this for initialization
        protected override void Start () {
            base.Start();
            this.health = 2;
            this.movementSpeed = 0.7f;
            this.damageAmount = 1;
        }
        
        // Update is called once per frame
        protected override void Update () {
            base.Update();
        }

        public override void deathAnimation()
        {
            Instantiate(this.deathParticle, transform.position, transform.rotation); 
        }
        
        public override void startAttack()
        {
        }

        public override void endAttack()
       {
       } 
    }
}
