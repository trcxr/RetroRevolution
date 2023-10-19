using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float powerupTime = 10f;
    public bool MagnetPowerBool = true;
    public GameObject MagnetParticle;
    public static Magnet instance;
  


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
        
        anim2 = GameObject.Find("Canvas/MagnetText");
        anim1 = GameObject.Find("Timer");
        powerupTime = 10f + PlayerPrefs.GetInt("Power4", 0) * 2f;

    }

    private void Update()
    {


   

    }
    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "Ball")
        {

            AudioManager.instance.Play("Shield");
            AudioManager.instance.Play("ShieldParticle");

            PlatformSpawner.instance.PowerupCollected(powerupTime);

            GameManagerScript.instance.magnetPower = true;
            GameManagerScript.instance.MagnetPowerFalse();


            GameObject particleObj = Instantiate(MagnetParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
            Animator animation1 = anim1.GetComponent<Animator>();
            animation1.SetTrigger("Magnet");
            GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
            if (PlayerPrefs.GetInt("Vibrate") == 0) {
                Handheld.Vibrate();
            }

            animation1.speed = 1 / (powerupTime / animation1.runtimeAnimatorController.animationClips[0].length);

            Animator animation2 = anim2.GetComponent<Animator>();
            animation2.SetTrigger("MagnetText");

            Destroy(particleObj, 5.0f);
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Spike")
        {

            MagnetPowerBool = true;

        }

    }

    public void DestroyMagnet()
    {

        Destroy(gameObject);
        GameObject particleObj = Instantiate(MagnetParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));
        GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

        Destroy(particleObj, 5.0f);

    }

  


}
