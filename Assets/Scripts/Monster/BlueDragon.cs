using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster {
    public class BlueDragon : AbstractEnemy {

        public GameObject deathParticle;

        public GameObject fireAttack;

        public float fireAttackSpeed;

        // Use this for initialization
        protected override void Start () {
            base.Start();	
            this.health = 2;
            this.dammageOnCollision = 1;
            this.attackDuration = 1.0f;
            this.fireAttackSpeed = 100;
            this.damageAmount = 2;
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
            GameObject currentAttack = Instantiate(this.fireAttack, transform.position, transform.rotation);
            currentAttack.GetComponent<EnemyAttack>().setDamageAmount(this.damageAmount);
            Vector2 forceDirection = Vector2.down;
            switch (this.direction) {
                case (int)Enums.Direction.North:
                    forceDirection = Vector2.up * this.fireAttackSpeed;
                    break;
                case (int)Enums.Direction.East:
                    forceDirection = Vector2.right * this.fireAttackSpeed;
                    break;
                case (int)Enums.Direction.South:
                    forceDirection = Vector2.down * this.fireAttackSpeed;
                    break;
                case (int)Enums.Direction.West:
                    forceDirection = Vector2.left * this.fireAttackSpeed;
                    break;
            }
            currentAttack.GetComponent<Rigidbody2D>().AddForce(forceDirection);
        }

        public override void endAttack()
        {
        } 
    }
}
