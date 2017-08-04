using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canMove;

    // Use this for initialization
    void Start()
    {
        this.canMove = true;
    }

    void Update()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.handleMovement(player);
    }

    private void handleMovement(Player player)
    {
        Animator animator = player.animator;
        float speed = player.speed;

        if (!this.canMove) {
            return ;
        }

        if (Input.GetKey(KeyCode.Z)) {
            animator.speed = 1;
            animator.SetInteger("direction", (int)Enums.Direction.North);
            transform.Translate(0, speed * Time.deltaTime, 0);
        } else if (Input.GetKey(KeyCode.S)) {
            animator.speed = 1;
            animator.SetInteger("direction", (int)Enums.Direction.South);
            transform.Translate(0, -speed * Time.deltaTime, 0);
        } else if (Input.GetKey(KeyCode.Q)) {
            animator.speed = 1;
            animator.SetInteger("direction", (int)Enums.Direction.West);
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        } else if (Input.GetKey(KeyCode.D)) {
            animator.speed = 1;
            animator.SetInteger("direction", (int)Enums.Direction.East);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        } else {
            animator.speed = 0;
        }
    }

    public PlayerMovement setIsAbleToMove(bool state)
    {
        this.canMove = state;

        return this;
    }

    public bool isAbleToMove()
    {
        return this.canMove;
    }
}
