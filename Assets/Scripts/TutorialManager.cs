using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject handLeft;
    public GameObject handRight;
    public GameObject goText;

    public GameObject tutorialUIObejct;

    private float timeValue = 1f;
    private float slowTime = 0.1f;
    private bool tutorialDone;
    private GameObject[] stockPlatform;
    private bool collisionT = false;
    private bool collisionB = false;
    private bool collisionM = false;
    private bool onPlatform = false;
    private bool collisionDone = false;

    // Start is called before the first frame update
    void Start() {
        stockPlatform = new GameObject[3];
        tutorialDone = PlayerPrefs.GetInt("TutorialDone", 1) != 1;
        BallControl.instance.tutorialTouch = tutorialDone;

        if (!tutorialDone) {
            PlatformSpawner.instance.PowerupSpawned(15f);
        }

        if (handLeft != null) {
            handLeft.SetActive(false);
        }
        if (handRight != null) {
            handRight.SetActive(false);
        }
        if (goText != null) {
            goText.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Arena", 1) == 1) {
            stockPlatform[0] = GameObject.Find("FixedGameElements/Scene/Metal/MetalStockP/MetalPlatformT");
            stockPlatform[1] = GameObject.Find("FixedGameElements/Scene/Metal/MetalStockP/MetalPlatformM");
            stockPlatform[2] = GameObject.Find("FixedGameElements/Scene/Metal/MetalStockP/MetalPlatformB");
        }

        if (PlayerPrefs.GetInt("Arena") == 2) {
            stockPlatform[0] = GameObject.Find("FixedGameElements/Scene/Snow/SnowStockP/SnowPlatformT");
            stockPlatform[1] = GameObject.Find("FixedGameElements/Scene/Snow/SnowStockP/SnowPlatformM");
            stockPlatform[2] = GameObject.Find("FixedGameElements/Scene/Snow/SnowStockP/SnowPlatformB");
        }
        if (PlayerPrefs.GetInt("Arena") == 3) {
            stockPlatform[0] = GameObject.Find("FixedGameElements/Scene/Wood/WoodStockP/WoodPlatformT");
            stockPlatform[1] = GameObject.Find("FixedGameElements/Scene/Wood/WoodStockP/WoodPlatformM");
            stockPlatform[2] = GameObject.Find("FixedGameElements/Scene/Wood/WoodStockP/WoodPlatformB");
        }
        if (PlayerPrefs.GetInt("Arena") == 4) {
            stockPlatform[0] = GameObject.Find("FixedGameElements/Scene/Stone/StoneStockP/StonePlatformT");
            stockPlatform[1] = GameObject.Find("FixedGameElements/Scene/Stone/StoneStockP/StonePlatformM");
            stockPlatform[2] = GameObject.Find("FixedGameElements/Scene/Stone/StoneStockP/StonePlatformB");
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (!tutorialDone) {
            BallControl.instance.tutorialTouch = false;
            if (Input.GetMouseButton(0)) {
                if (collisionM && Input.mousePosition.x < Screen.width / 2) {
                    Time.timeScale = timeValue;
                    Pause.instance.ManageTimer = true;
                    BallControl.instance.tutorialTouch = true;

                } else if (collisionT && Input.mousePosition.x > Screen.width / 2) {
                    Time.timeScale = timeValue;
                    Pause.instance.ManageTimer = true;
                    BallControl.instance.tutorialTouch = true;
                } else if (collisionB) {
                    tutorialDone = true;
                    BallControl.instance.tutorialTouch = true;
                }
            } else {
                BallControl.instance.smoothInput = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!tutorialDone && !onPlatform) {

            if (collision.gameObject == stockPlatform[0]) {
                // TOP
                //Debug.Log("Collision Top");
                collisionT = true;
                collisionB = false;
                collisionM = false;
                handLeft.SetActive(false);
                handRight.SetActive(true);

                Time.timeScale = slowTime;
                Pause.instance.ManageTimer = false;
                BallControl.instance.smoothInput = 0f;
            }
            if (collision.gameObject == stockPlatform[1]) {
                // Middle
                //Debug.Log("Collision Middle");
                collisionT = false;
                collisionM = true;
                collisionB = false;
                handLeft.SetActive(true);
                handRight.SetActive(false);

                Time.timeScale = slowTime;
                Pause.instance.ManageTimer = false;
                BallControl.instance.smoothInput = 0f;
            }
            if (collision.gameObject == stockPlatform[2]) {
                // Bottom
                //Debug.Log("Collision Bottom");
                collisionT = false;
                collisionM = false;
                collisionB = true;
                handLeft.SetActive(false);
                handRight.SetActive(false);
                goText.SetActive(true);


                PlayerPrefs.SetInt("TutorialDone", 0);
                BallControl.instance.smoothInput = 0f;
            }

            if(collision.gameObject.tag == "Spike" || collision.gameObject.tag == "TopSpikes") {
                BallControl.tutorialDeath = true;
                if (tutorialUIObejct != null) {
                    tutorialUIObejct.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "TutorialBottom") {
            BallControl.tutorialDeath = true;
            if (tutorialUIObejct != null) {
                tutorialUIObejct.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collisionDone) {
            onPlatform = true;
        }
        if (collision.gameObject.tag == "TutorialBottom") {
            BallControl.tutorialDeath = true;
            if (tutorialUIObejct != null) {
                tutorialUIObejct.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        onPlatform = false;
        collisionDone = false;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        collisionDone = true;
    }
}