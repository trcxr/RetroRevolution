using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public float powerupTime = 6f;
    public bool SlowTimebool = true;
    public GameObject SlowTimeParticle;
    public static SlowTime instance;
 
    GameObject anim1;
    GameObject anim2;

    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    private void Start()
    {


        anim2 = GameObject.Find("Canvas/TimeText");

        anim1 = GameObject.Find("Timer");
        powerupTime = 6f + PlayerPrefs.GetInt("Power3", 0) * 2f;

    }
    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "Ball")
        {
            AudioManager.instance.Play("Time");
            

            PlatformSpawner.instance.PowerupCollected(powerupTime);

            GameManagerScript.instance.SlowMotion();

            Animator animation2 = anim2.GetComponent<Animator>();
            animation2.SetTrigger("TimeText");
            GameObject particleObj = Instantiate(SlowTimeParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
            Animator animation1 = anim1.GetComponent<Animator>();
            animation1.SetTrigger("Clock");
            animation1.speed = 1 / (powerupTime / animation1.runtimeAnimatorController.animationClips[0].length);

            GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
            if (PlayerPrefs.GetInt("Vibrate") == 0) {
                Handheld.Vibrate();
            }

            Destroy(particleObj, 5.0f);
            Destroy(this.gameObject);


        }

        if (col.gameObject.tag == "Spike")
        {



            SlowTimebool = true;



        }

    }
    public void DestroyTime()
    {
       
        Destroy(gameObject);
        GameObject particleObj = Instantiate(SlowTimeParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
        GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

        Destroy(particleObj, 5.0f);

    }

   
}
