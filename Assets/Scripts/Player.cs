using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public const float invincibilityCollisionTime = 1f;

    public const float powerForce = 100f;

    public const float speed = 2f;

    public Animator animator;

    private int currentHeart;

    private int maxHeart;

    public float currentCollisionInvincibilityTime = 0;

    public float currentMovementHitCollisionTime = 0;

    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        this.currentHeart = 5;
        this.maxHeart = 5;
        this.animator = GetComponent<Animator>();
        this.animator.SetInteger("direction", (int)Enums.Direction.South);
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.handleInvincibleEvent();
    }

    private void handleInvincibleEvent()
    {
        // Update collision invincibility time
        if (this.isInvincible()) {
            this.spriteRenderer.enabled = !this.spriteRenderer.enabled;
            this.currentCollisionInvincibilityTime -= Time.deltaTime;
        } else if (!this.spriteRenderer.enabled) {
            this.spriteRenderer.enabled = true;
        }
    }

    public int getCurrentHeart()
    {
        return currentHeart;
    }

    public int getMaxHeart()
    {
        return this.maxHeart;
    }

    public bool increaseLife(int number)
    {
        if (this.getCurrentHeart() < this.getMaxHeart()) {
            this.currentHeart += number;
            if (this.currentHeart > this.maxHeart) {
                this.currentHeart = this.maxHeart;
            }

            return true;
        }

        return false;
    }

    public bool decreaseLife(int number)
    {
        if (this.getCurrentHeart() >= 0) {
            this.currentHeart -= number;
            if (this.currentHeart < 0) {
                this.currentHeart = 0;
            }

            return true;
        }

        return false;
    }

    public Player initializeCollisionInvincibility()
    {
        this.currentCollisionInvincibilityTime = Player.invincibilityCollisionTime;

        return this;
    }

    public bool isInvincible()
    {
        return this.currentCollisionInvincibilityTime > 0;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyAttack") {
            this.initializeCollisionInvincibility();
            this.decreaseLife(other.gameObject.GetComponent<EnemyAttack>().getDamageAmount());
            Destroy(other.gameObject);
        }
    }
}
