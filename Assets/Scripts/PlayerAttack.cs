using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject sword;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            this.Attack();
        }
    }

    void Attack()
    {
        sword = Instantiate(this.sword, transform.position, this.sword.transform.rotation);
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int swordDirection = player.animator.GetInteger("direction");

        if (swordDirection == (int)Enums.Direction.North) {
            sword.transform.Rotate(0, 0, 0);
            sword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * player.powerForce);
        }
        else if (swordDirection == (int)Enums.Direction.East) {
            sword.transform.Rotate(0, 0, -90);
            sword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * player.powerForce);
        }
        else if (swordDirection == (int)Enums.Direction.South) {
            sword.transform.Rotate(0, 0, 180);
            sword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * player.powerForce);
        }
        else if (swordDirection == (int)Enums.Direction.West) {
            sword.transform.Rotate(0, 0, 90);
            sword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * player.powerForce);
        }
    }
}
