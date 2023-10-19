using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    Material material;
    Vector2 offset;

    public float xspeed =0f, yspeed = 0.03f;

   

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        offset = new Vector2(xspeed, -yspeed);
        material.mainTextureOffset += offset * Time.deltaTime;

      
    }
}
