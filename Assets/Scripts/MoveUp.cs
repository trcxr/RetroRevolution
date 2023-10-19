using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{

   
    public static float speed;
    private float localSpeed;

    void Update()
    {
        localSpeed = speed;
        if (GameManagerScript.instance.ballSlowed) {
            speed = .5f;
            ScrollBackground.multiplier = .7f;
        } else {

            if (GameManagerScript.instance.score <= 100) {

                speed = 1.6f;
                ScrollBackground.multiplier = 1f;
            }

            if (GameManagerScript.instance.score > 100 && GameManagerScript.instance.score <= 300) {

                speed = 2f;
                ScrollBackground.multiplier = 1.2f;
            }

            if (GameManagerScript.instance.score > 300 && GameManagerScript.instance.score <= 500) {

                speed = 2.5f;
                ScrollBackground.multiplier = 1.55f;
            }

            if (GameManagerScript.instance.score > 500) {

                speed = 2.6f;
                ScrollBackground.multiplier = 1.6f;
            }
        }

        transform.Translate(0, Time.deltaTime * speed, 0);


    }

}
