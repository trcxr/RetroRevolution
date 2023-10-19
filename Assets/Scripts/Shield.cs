using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public float powerupTime = 10f;

    public bool Shieldbool = true;
    public GameObject ShieldParticle;
    public static Shield instance;
   

    GameObject anim1;
    GameObject anim2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
    }
    private void Start()
    {
        
        anim1 = GameObject.Find("Timer");
        anim2 = GameObject.Find("Canvas/ShieldText");
        powerupTime = 10f + PlayerPrefs.GetInt("Power1", 0) * 2f;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "Ball")
        {

            AudioManager.instance.Play("Shield");
            AudioManager.instance.Play("ShieldParticle");
            PlatformSpawner.instance.PowerupCollected(powerupTime);


            GameManagerScript.instance.ShieldTurnOn = true;


            GameManagerScript.instance.StartCoroutine("Shielddelay");


            GameObject particleObj = Instantiate(ShieldParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
           


            Animator animation1 = anim1.GetComponent<Animator>();
            animation1.SetTrigger("Shield");
            animation1.speed = 1 / (powerupTime / animation1.runtimeAnimatorController.animationClips[0].length);
            Animator animation2 = anim2.GetComponent<Animator>();
            animation2.SetTrigger("ShieldText");
            

            GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
            if (PlayerPrefs.GetInt("Vibrate") == 0) {
                Handheld.Vibrate();
            }


            Destroy(particleObj, 5.0f);
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "Spike")
        {

            Shieldbool = true;


        }

    }

    public void DestroyShield()
    {
       
       Destroy(gameObject);
       GameObject particleObj = Instantiate(ShieldParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
       GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

       Destroy(particleObj, 5.0f);
     
    }

  

  
}
