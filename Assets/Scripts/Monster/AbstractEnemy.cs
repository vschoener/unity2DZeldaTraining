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

        protected int attackState;

        protected float attackTimeLeft;

        protected float attackDuration;

        protected float nextAttackTimeLeft;

        // Use this for initialization
        virtual protected void Start () {
            this.spriteRenderer = GetComponent<SpriteRenderer>();
            this.animator = GetComponent<Animator>();
            this.health = 1;
            this.movementSpeed = 1f;
            this.damageAmount = 1;
            this.movementTimerLeft = 0;
            this.movementTimer = 1.5f;
            this.direction = Random.Range(0, 4);
            this.animator.SetInteger("direction", this.direction);
            this.dammageOnCollision = 1;
            this.initializeNextAttackDuration();
        }
        
        // Update is called once per frame
        virtual protected void Update() {
            
            this.handleAttack();

            this.movementTimerLeft -= Time.deltaTime;
            if (!this.isAttacking() && this.movementTimerLeft <= 0) {
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
            if (this.attackState == (int)Enums.AttackState.On) {
                return ;
            }

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

        public bool isAttacking()
        {
            return this.attackTimeLeft >= 0.0;
        }

        protected virtual void handleAttack()
        {
            if (this.attackState == (int)Enums.AttackState.Pre) {
                this.nextAttackTimeLeft -= Time.deltaTime;
                if (this.nextAttackTimeLeft < 0) {
                    this.attackState = (int)Enums.AttackState.On;
                    this.attackTimeLeft = this.attackDuration;
                    this.startAttack();
                }
            }
            else if (this.attackState == (int)Enums.AttackState.On) {
                this.attackTimeLeft -= Time.deltaTime;
                if (this.attackTimeLeft < 0) { 
                    this.attackState = (int)Enums.AttackState.Post;
                    this.endAttack();
                }
            } else if (this.attackState == (int)Enums.AttackState.Post) {
                this.initializeNextAttackDuration();
            }
        }

        protected virtual void initializeNextAttackDuration()
        {
            this.attackState = (int)Enums.AttackState.Pre;
            this.nextAttackTimeLeft = Random.Range(1.2f, 3.5f);
        }

        abstract public void deathAnimation();

        abstract public void startAttack();
        abstract public void endAttack();
    }
}
