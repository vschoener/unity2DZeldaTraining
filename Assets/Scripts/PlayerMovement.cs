using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canMove;

    private Player player;

    // Use this for initialization
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.canMove = true;
    }

    void Update()
    {
        this.handleMovement();
    }

    private void handleMovement()
    {
        if (!this.canMove) {
            return ;
        }

        if (Input.GetKey(KeyCode.Z)) {
            player.animator.speed = 1;
            player.animator.SetInteger("direction", (int)Enums.Direction.North);
            transform.Translate(0, Player.speed * Time.deltaTime, 0);
        } else if (Input.GetKey(KeyCode.S)) {
            player.animator.speed = 1;
            player.animator.SetInteger("direction", (int)Enums.Direction.South);
            transform.Translate(0, -Player.speed * Time.deltaTime, 0);
        } else if (Input.GetKey(KeyCode.Q)) {
            player.animator.speed = 1;
            player.animator.SetInteger("direction", (int)Enums.Direction.West);
            transform.Translate(-Player.speed * Time.deltaTime, 0, 0);
        } else if (Input.GetKey(KeyCode.D)) {
            player.animator.speed = 1;
            player.animator.SetInteger("direction", (int)Enums.Direction.East);
            transform.Translate(Player.speed * Time.deltaTime, 0, 0);
        } else {
            this.stopMovement();
        }
    }

    public PlayerMovement stopMovement()
    {
        this.player.animator.speed = 0;

        return this;
    }

    public PlayerMovement enableMovement()
    {
        this.canMove = true;

        return this;
    }

    public PlayerMovement disableMovement()
    {
        this.canMove = false;

        return this;
    }

    public bool isAbleToMove()
    {
        return this.canMove;
    }
}
