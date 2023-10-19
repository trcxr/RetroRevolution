using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    public float powerupTime = 10f;
    public bool SpeedPowerBool = true;
    public GameObject SpeedParticle;
    public static SpeedPower instance;
   

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
        anim2 = GameObject.Find("Canvas/SpeedText");
        anim1 = GameObject.Find("Timer");
        powerupTime = 10f + PlayerPrefs.GetInt("Power2", 0) * 2f;

    }
    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "Ball")
        {

            AudioManager.instance.Play("Speed");
            AudioManager.instance.Play("SpeedParticle");

            PlatformSpawner.instance.PowerupCollected(powerupTime);

            GameManagerScript.instance.IncreaseSpeed();
            GameManagerScript.instance.StartCoroutine("SpeedDelay");


            GameObject particleObj = Instantiate(SpeedParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
            Animator animation1 = anim1.GetComponent<Animator>();
            animation1.SetTrigger("Speed");
            GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
            if (PlayerPrefs.GetInt("Vibrate") == 0) {
                Handheld.Vibrate();
            }

            animation1.speed = 1 / (powerupTime / animation1.runtimeAnimatorController.animationClips[0].length);

            Animator animation2 = anim2.GetComponent<Animator>();
            animation2.SetTrigger("SpeedText");

            Destroy(particleObj, 5.0f);
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Spike")
        {



            SpeedPowerBool = true;



        }

    }

    public void DestroySpeed()
    {
       
        Destroy(gameObject);
        GameObject particleObj = Instantiate(SpeedParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
        GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

        Destroy(particleObj, 5.0f);
       
    }

  
  
}
