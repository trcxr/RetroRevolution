using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public bool Heartbool=true;
    public GameObject HeartParticle;
    public static  Heart instance;


    GameObject anim;
  

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

        anim = GameObject.Find("Canvas/HeartText");
      

    }
    private void OnTriggerEnter2D(Collider2D col) {


        if (col.gameObject.tag == "Ball") {
            AudioManager.instance.Play("Heart");

            AudioManager.instance.Play("ShieldParticle");

            Health.instance.health += 1;
            
            GameObject particleObj = Instantiate(HeartParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));

            Animator animation1 = anim.GetComponent<Animator>();
            animation1.SetTrigger("HeartText");

            Destroy(particleObj, 5.0f);
            GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
            if (PlayerPrefs.GetInt("Vibrate") == 0) {
                Handheld.Vibrate();
            }

            Destroy(gameObject);
            
        }

        if (col.gameObject.tag == "Spike") {



         Heartbool = true;

         

        }

    }

    public void DestroyHeart() {
       
        Destroy(gameObject);
        GameObject particleObj = Instantiate(HeartParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));

       
        GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
      
        Destroy(particleObj, 5.0f);



    }

  
}
