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

    private bool specialAttackState;

    // Use this for initialization
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        this.attackTimeLeft = 0;
        this.canAttackState = true;
        this.specialAttackState = false;
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
        if (this.attackTimeLeft > 0) {  
            this.attackTimeLeft -= Time.deltaTime;

            if (this.attackTimeLeft < 0) {

                if (this.currentSword) {
                    this.currentSword.GetComponent<Weapon>().destroyWeapon();
                }
                this.releaseAllAction();

                // If it's a special attack, the sword animation is longer than the classic and doesn't need to block any movements
            } else if (this.specialAttackState && this.specialAttackAnimationTime - this.simpleaAttackAnimationTime > this.attackTimeLeft) {
                playerMovement.enableMovement();
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
            this.specialAttackState = true;
            this.attackTimeLeft = this.specialAttackAnimationTime;
        } else {
            this.attackTimeLeft = this.simpleaAttackAnimationTime;
            this.specialAttackState = false;
        }

        this.playerMovement.disableMovement();
        this.setIsAbleToAttack(false);
        currentSword = Instantiate(this.sword, transform.position, this.sword.transform.rotation);
        int swordDirection = this.player.animator.GetInteger("direction");
        this.player.animator.SetInteger("attackDirection", swordDirection);

        if (swordDirection == (int)Enums.Direction.North) {
            currentSword.transform.Rotate(0, 0, 0);
            if (!this.specialAttackState) {
                Vector3 newSwordPosition = currentSword.transform.position;
                newSwordPosition.y += 0.15f;
                currentSword.transform.position = newSwordPosition;
            } else {
                currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Player.powerForce);
            }
        } else if (swordDirection == (int)Enums.Direction.East) {
            currentSword.transform.Rotate(0, 0, -90);
            if (!this.specialAttackState) {
                Vector3 newSwordPosition = currentSword.transform.position;
                newSwordPosition.x += 0.15f;
                currentSword.transform.position = newSwordPosition;
            } else {
                currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Player.powerForce);
            }
        } else if (swordDirection == (int)Enums.Direction.South) {
            currentSword.transform.Rotate(0, 0, 180);
            if (!this.specialAttackState) {
                Vector3 newSwordPosition = currentSword.transform.position;
                newSwordPosition.y -= 0.15f;
                currentSword.transform.position = newSwordPosition;
            } else {
                currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * Player.powerForce);
            }
        } else if (swordDirection == (int)Enums.Direction.West) {
            currentSword.transform.Rotate(0, 0, 90);
            if (!this.specialAttackState) {
                Vector3 newSwordPosition = currentSword.transform.position;
                newSwordPosition.x -= 0.15f;
                currentSword.transform.position = newSwordPosition;
            } else {
                currentSword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Player.powerForce);
            }
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

    public bool hasSpecialAttack()
    {
        return this.specialAttackState;
    }

    public void releaseAllAction()
    {
        playerMovement.enableMovement();
        this.setIsAbleToAttack(true);
        this.player.animator.SetInteger("attackDirection", Enums.IDLE);
        this.player.animator.speed = 0;
    }
}
