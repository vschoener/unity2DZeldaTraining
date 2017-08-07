using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float lastTimeKeyDown = 0.0F;

    private float rateSpeedKeyDown = 0.05F;

    public float speed;

    public Animator animator;

    private int currentHeart;

    private int maxHeart;

    public float powerForce = 100.0F;

    // Use this for initialization
    void Start()
    {
        this.currentHeart = 5;
        this.maxHeart = 5;
        this.speed = 2;
        this.animator = GetComponent<Animator>();
        this.animator.SetInteger("direction", (int)Enums.Direction.South);
        //this.initializeWeapons();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Time.time > rateSpeedKeyDown + lastTimeKeyDown) {
			lastTimeKeyDown = Time.time;
		}*/
    }

    void initializeWeapons()
    {
        //GameObject gameObject = Instantiate(this.sword);
    }

    public int getCurrentHeart()
    {
        return currentHeart;
    }

    public int getMaxHeart()
    {
        return this.maxHeart;
    }

    public bool addHearth()
    {
        if (this.getCurrentHeart() < this.getMaxHeart()) {
            this.currentHeart++;

            return true;
        }

        return false;
    }

    public bool removeHearth()
    {
        if (this.getCurrentHeart() >= 0) {
            this.currentHeart--;

            return true;
        }

        return false;
    }
}
