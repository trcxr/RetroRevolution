using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BallControl : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public bool tutorialTouch;
    public static bool tutorialDeath;
    public bool tutorialDone;
    private float moveInput;

    private bool localTutDeath;
    
    public static BallControl instance;
    public float smoothInput;
    bool shake;
    bool onPlatform = false;
    //bool routineRunning = false;
    public bool collisionDone = false;
    public GameObject deathParticle;
    public GameObject Particle0;
    public GameObject Particle1;
    public GameObject Particle2;
    public GameObject Particle3;
    public GameObject Particle4;
    public GameObject Particle5;
    public GameObject Particle6;


    GameObject Trail0;
    GameObject Trail1;
    GameObject Trail2;
    GameObject Trail3;
    GameObject Trail4;
    GameObject Trail5;
    GameObject Trail6;

    Transform timer;

    GameObject anim;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        tutorialDeath = false;
        anim = GameObject.Find("Canvas/DeathText");

        Trail0 = gameObject.transform.Find("Timer/BallTrailObject/BallTrail0").gameObject;
        Trail1 = gameObject.transform.Find("Timer/BallTrailObject/BallTrail1").gameObject;
        Trail2 = gameObject.transform.Find("Timer/BallTrailObject/BallTrail2").gameObject;
        Trail3 = gameObject.transform.Find("Timer/BallTrailObject/BallTrail3").gameObject;
        Trail4 = gameObject.transform.Find("Timer/BallTrailObject/BallTrail4").gameObject;
        Trail5 = gameObject.transform.Find("Timer/BallTrailObject/BallTrail5").gameObject;
        Trail6 = gameObject.transform.Find("Timer/BallTrailObject/BallTrail6").gameObject;
        timer = gameObject.transform.Find("Timer");
     

        if (PlayerPrefs.GetInt("Skin") == 0)
        {
            deathParticle = Particle0;
            Trail0.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Skin") == 1)
        {
            deathParticle = Particle1;
            Trail1.SetActive(true);
        }  
        if (PlayerPrefs.GetInt("Skin") == 2)
        {
            deathParticle = Particle2;
            Trail2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Skin") == 3)
        {
            deathParticle = Particle3;
            Trail3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Skin") == 4)
        {
            deathParticle = Particle4;
            Trail4.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Skin") == 5)
        {
            deathParticle = Particle5;
            Trail5.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Skin") == 6)
        {
            deathParticle = Particle6;
            Trail6.SetActive(true);
        }

        tutorialDone = PlayerPrefs.GetInt("TutorialDone", 1) != 1;

    }



    private void FixedUpdate()
    {
        localTutDeath = tutorialDeath;
        if (tutorialTouch || tutorialDone || tutorialDeath) {
            float axisX = Input.GetAxis("Horizontal");
            if (Input.GetMouseButton(0)) {
                if (Input.mousePosition.x < Screen.width / 2) {
                    smoothInput = Mathf.Lerp(smoothInput, -1, Time.deltaTime * speed);
                    transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
                } else if (Input.mousePosition.x > Screen.width / 2) {
                    smoothInput = Mathf.Lerp(smoothInput, 1, Time.deltaTime * speed);
                    transform.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);
                }


            } else {
                smoothInput = Mathf.Lerp(smoothInput, 0, Time.deltaTime * speed * 2f);
            }

            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D))
                transform.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);


            transform.Translate(new Vector3(axisX, 0) * Time.deltaTime * speed, Space.World);
            transform.Translate(new Vector3(smoothInput, 0) * Time.deltaTime * speed, Space.World);
        }

        timer.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        SpikeCollisionDetection(collision);

        if (collision.gameObject.tag == "Platform" && !onPlatform)
        {
            AudioManager.instance.Play("Hit");
            GameManagerScript.instance.CameraShake.SetTrigger("PlatformShake");
            collisionDone = true;
        }

    }

    private void SpikeCollisionDetection(Collision2D collision) {

        if (collision.gameObject.tag == "Spike" && GameManagerScript.instance.ShieldTurnOn == false)
        {


            if (Health.instance.health > 0)
            {
                Animator animation = anim.GetComponent<Animator>();
                animation.SetTrigger("DeathText");
                DestroyPowerUps();
                StartCountdown();
                Health.instance.health -= 1;
                GameManagerScript.instance.CameraShake.SetTrigger("CameraShake1");
                if (PlayerPrefs.GetInt("Vibrate") == 0)
                {
                    Handheld.Vibrate();
                }

                Instantiate(deathParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));

                Destroy(gameObject);
            }

            else
            {
                Animator animation = anim.GetComponent<Animator>();
                animation.SetTrigger("DeathText");
                Health.instance.health -= 1;
                Instantiate(deathParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));

                GameManagerScript.instance.CameraShake.SetTrigger("CameraShake1");
                if (PlayerPrefs.GetInt("Vibrate") == 0)
                {
                    Handheld.Vibrate();
                }
                GameManagerScript.instance.GameOver();

                DestroyPowerUps();
                Destroy(gameObject);
            }


        }

        else if (collision.gameObject.tag == "TopSpikes")
        {

            if (Health.instance.health > 0)
            {
                Animator animation = anim.GetComponent<Animator>();
                animation.SetTrigger("DeathText");
                DestroyPowerUps();
                StartCountdown();
                Health.instance.health -= 1;
                GameManagerScript.instance.CameraShake.SetTrigger("CameraShake1");
                if (PlayerPrefs.GetInt("Vibrate") == 0)
                {
                    Handheld.Vibrate();
                }

                Instantiate(deathParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));

                Destroy(gameObject);
            }

            else
            {
                Animator animation = anim.GetComponent<Animator>();
                animation.SetTrigger("DeathText");
                Health.instance.health -= 1;

                Instantiate(deathParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));

                GameManagerScript.instance.CameraShake.SetTrigger("CameraShake1");
                if (PlayerPrefs.GetInt("Vibrate") == 0)
                {
                    Handheld.Vibrate();
                }
                GameManagerScript.instance.GameOver();

                DestroyPowerUps();
                Destroy(gameObject);
            }
        }


    }

    private void DestroyPowerUps() {
        if (GameObject.FindWithTag("PowerUp")) {
            Shield.instance.DestroyShield();
        } else if (GameObject.FindWithTag("Time")) {
            SlowTime.instance.DestroyTime();
        } else if (GameObject.FindWithTag("Speed")) {
            SpeedPower.instance.DestroySpeed();
        } else if (GameObject.FindWithTag("Heart")) {
            Heart.instance.DestroyHeart();
        } else if (GameObject.FindWithTag("Magnet")) {
            Magnet.instance.DestroyMagnet();
        }
    }

    private void StartCountdown() {
        AudioManager.instance.Play("Death");
        GameManagerScript.instance.isRespawn = true;
        GameManagerScript.instance.InvokeSlowMotion();
        GameManagerScript.instance.timer.SetActive(true);
        PlatformSpawner.instance.PowerupSpawned();
        GameManagerScript.instance.magnetPower = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collisionDone)
        {
            onPlatform = true;
        }
       
    }
  
    private void OnTriggerExit2D(Collider2D collision) {
        onPlatform = false;
        collisionDone = false;
    }

    private void OnCollisionStay2D(Collision2D collision) {

        SpikeCollisionDetection(collision);
        collisionDone = true;
    }
}