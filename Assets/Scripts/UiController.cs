using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Image[] hearts;

    private Player player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.initializeHeart();
    }

    // Update is called once per frame
    void Update()
    {
        this.checkHealth();
    }

    void initializeHeart()
    {
        renderHealth();
    }

    void renderHealth()
    {
        for (int i = 0; i < this.hearts.Length; i++) {
            this.hearts[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < this.player.getCurrentHeart(); i++) {
            this.hearts[i].gameObject.SetActive(true);
        }
    }

    void checkHealth()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            this.player.removeHearth();
        }
        else if (Input.GetKeyDown(KeyCode.P) && this.player.getCurrentHeart() < this.player.getMaxHeart()) {
            this.player.addHearth();
        }

        this.renderHealth();
    }
}
