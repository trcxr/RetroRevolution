using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground: MonoBehaviour
{
    Material material;
    Vector2 offset;

    public float xspeed = 0f, yspeed;

    public static float multiplier;

    public static ScrollBackground instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        material = GetComponent<Renderer>().material;

    }
    void Start()
    {

        multiplier = 1f;

        if (PlayerPrefs.GetInt("Arena", 1) == 1)
        {

           yspeed = -0.11f;
        }

        if (PlayerPrefs.GetInt("Arena") == 2)
        {

           yspeed = -0.08f;
        }

        if (PlayerPrefs.GetInt("Arena") == 3)
        {

          yspeed = -0.08f;
        }

        if (PlayerPrefs.GetInt("Arena") == 4)
        {

           yspeed = -0.09f;
        }

    }

    
    void Update()
    {
        offset = new Vector2(xspeed, yspeed  *multiplier);
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
