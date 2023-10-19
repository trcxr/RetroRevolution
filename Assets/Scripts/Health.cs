using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public static Health instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
     
    }
    void Start()
    {
        health = 3 + PlayerPrefs.GetInt("Hearts", 0);
        numOfHearts = health;
    }

    // Update is called once per frame
    void Update()
    {

        if (health > numOfHearts) {

            health = numOfHearts;

        }

        for (int i = 0; i <hearts.Length; i++)
        {

            if (i < health)
            {

                hearts[i].sprite = fullHeart;

            }

            else {

                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {

                hearts[i].enabled = true;

            }

            else {

                hearts[i].enabled = false;
            }


        }
    }
}
