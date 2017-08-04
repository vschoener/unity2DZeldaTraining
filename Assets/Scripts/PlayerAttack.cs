using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject sword;

    private GameObject currentSword;

    private Player player;

    private PlayerMovement playerMovement;

    private float simpleaAttackAnimationTime = 0.2F;

    private float specialAttackAnimationTime = 1F;

    private float attackTimeLeft;

    private bool canAttackState;

    private bool specialAttack;

    // Use this for initialization
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        this.attackTimeLeft = this.simpleaAttackAnimationTime;
        this.canAttackState = true;
        this.specialAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.handleAttacksAnimation();
        if (Input.GetKeyDown(KeyCode.L)) {
            this.Attack();
        }
    }

    private void handleAttacksAnimation()
    {
        if (this.attackTimeLeft >= 0) {  
            this.attackTimeLeft -= Time.deltaTime;

            if (this.attackTimeLeft < 0) {
                Destroy(currentSword);
                playerMovement.setIsAbleToMove(true);
                this.setIsAbleToAttack(true);
                this.player.animator.SetInteger("attackDirection", Enums.IDLE);
                this.player.animator.speed = 0;
                // If it's a special attack, the sword animation is longer than the classic and doesn't need to block any movements
            } else if (this.specialAttack && this.specialAttackAnimationTime - this.simpleaAttackAnimationTime > this.attackTimeLeft) {
                playerMovement.setIsAbleToMove(true);
                this.player.animator.SetInteger("attackDirection", Enums.IDLE);
            }
        }
    }

    public void Attack()
    {
        if (!this.isAbleToAttack()) {
            return ;
        }

        this.player.animator.speed = 1;
        if (this.player.getCurrentHeart() == this.player.getMaxHeart()) {
            this.specialAttack = true;
            this.attackTimeLeft = this.specialAttackAnimationTime;
        } else {
            this.attackTimeLeft = this.simpleaAttackAnimationTime;
        }

        this.playerMovement.setIsAbleToMove(false);
        this.setIsAbleToAttack(false);
        currentSword = Instantiate(this.sword, transform.position, this.sword.transform.rotation);
        int swordDirection = this.player.animator.GetInteger("direction");
        this.player.animator.SetInteger("attackDirection", swordDirection);

        if (swordDirection == (int)Enums.Direction.North) {
            currentSword.transform.Rotate(0, 0, 0);
            currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * this.player.powerForce);
        } else if (swordDirection == (int)Enums.Direction.East) {
            currentSword.transform.Rotate(0, 0, -90);
            currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * this.player.powerForce);
        } else if (swordDirection == (int)Enums.Direction.South) {
            currentSword.transform.Rotate(0, 0, 180);
            currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * this.player.powerForce);
        } else if (swordDirection == (int)Enums.Direction.West) {
            currentSword.transform.Rotate(0, 0, 90);
            currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * this.player.powerForce);
        }
    }

    public PlayerAttack setIsAbleToAttack(bool state)
    {
        this.canAttackState = state;

        return this;
    }

    public bool isAbleToAttack()
    {
        return this.canAttackState;
    }
}
