using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster {
    public abstract class AbstractEnemy : MonoBehaviour {

        protected int health;

        protected float movementSpeed;

        protected int damageAmount;

        protected int dammageOnCollision;

        protected SpriteRenderer spriteRenderer;

        public Sprite[] facingSprites;
        
        protected Animator animator;

        public int direction;

        public float movementTimer;

        public float movementTimerLeft;

        // Use this for initialization
        virtual protected void Start () {
            this.spriteRenderer = GetComponent<SpriteRenderer>();
            this.animator = GetComponent<Animator>();
            this.health = 1;
            this.movementSpeed = 1f;
            this.damageAmount = 1;
            this.movementTimerLeft = 0;
            this.movementTimer = 1.5f;
            this.direction = Random.Range(0, 3);
            this.dammageOnCollision = 1;
        }
        
        // Update is called once per frame
        virtual protected void Update() {
            
            this.movementTimerLeft -= Time.deltaTime;
            if (movementTimerLeft <= 0) {
                this.direction = Random.Range(0, 4);
                this.movementTimerLeft = this.movementTimer;

                // Use facing sprite for simple enemies
                // (Used to learn Sprite system without animation)
                if (this.facingSprites.Length > 0 && this.facingSprites.Length == 4) {
                    this.spriteRenderer.sprite = this.facingSprites[this.direction];
                } else if (this.animator != null) {
                    this.animator.SetInteger("direction", this.direction);
                }
            }
            
            this.handleMovement();
        }

        protected void handleMovement()
        {
            if (this.direction == (int)Enums.Direction.North) {
                transform.Translate(0, this.movementSpeed * Time.deltaTime, 0);
            } else if (this.direction == (int)Enums.Direction.South) {
                transform.Translate(0, -this.movementSpeed * Time.deltaTime, 0);
            } else if (this.direction == (int)Enums.Direction.West) {
                transform.Translate(-this.movementSpeed * Time.deltaTime, 0, 0);
            } else if (this.direction == (int)Enums.Direction.East) {
                transform.Translate(this.movementSpeed * Time.deltaTime, 0, 0);
            }
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

        /// <summary>
        /// Sent when an incoming collider makes contact with this object's
        /// collider (2D physics only).
        /// </summary>
        /// <param name="other">The Collision2D data associated with this collision.</param>
        void OnCollisionEnter2D(Collision2D other)
        {
            switch(other.gameObject.tag) {
                case "Player":
                    this.handlePlayerCollision(other.gameObject.GetComponent<Player>());
                break;
            }
        }

        void OnCollisionStay2D(Collision2D other)
        {
            switch(other.gameObject.tag) {
                case "Player":
                    this.handlePlayerCollision(other.gameObject.GetComponent<Player>());
                break;
            }
        }

        private void handlePlayerCollision(Player player) 
        {
            if (player.isInvincible()) {
                return ;
            }

            // Add force to push the player from the monster
            player.initializeCollisionInvincibility();
            player.decreaseLife(this.dammageOnCollision);
        }

        abstract public void deathAnimation();
    }
}
