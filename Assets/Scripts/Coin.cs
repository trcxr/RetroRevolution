using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Animator anim;
    public GameObject MileStoneParticle;

    public GameObject CoinAlpha;

    GameObject mileAnim;
    GameObject plusOneAnim;

    public GameObject CoinParticle;


    bool DestroyCoin = false;
    bool coinCollected = false;
    bool ballFound = false;

    GameObject coinAnim;

    Transform CoinUI;

    [SerializeField]
    float magnetSpeed = 0f;
    bool Magnet = false;


    Transform BallObject;
  
    public float magnetSpeedpowerup = 10f;
  
    public float dist = 0.1f;
   


    // Use this for initialization
    private void Start()
    {
        plusOneAnim = GameObject.Find("Canvas/+1");
        mileAnim = GameObject.Find("MilestoneAnimations");
        coinAnim = GameObject.Find("CoinAnimator");
        CoinUI = GameObject.FindWithTag("CoinUI").transform;
    }

   
    void Update()
    {
        if (!ballFound) {
            if (GameObject.FindWithTag("Ball") != null) {
                BallObject = GameObject.FindWithTag("Ball").transform;
                ballFound = true;
            }
        }
        if (Magnet)
        {
            transform.position = Vector3.MoveTowards(transform.position, CoinUI.position, magnetSpeed * Time.deltaTime);
        }

        if (GameManagerScript.instance.magnetPower && !coinCollected && BallObject != null) {
            //Debug.Log("Magnet is working");

            if (Vector3.Distance(transform.position, BallObject.position) < dist) {
                transform.position = Vector3.MoveTowards(transform.position, BallObject.position, magnetSpeedpowerup * Time.deltaTime);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (GameManagerScript.instance.score == PlayerPrefs.GetInt("HighScore") ) {

                Animator mileanimation = mileAnim.GetComponent<Animator>();
                mileanimation.SetTrigger("HighScore");
                GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
                if (PlayerPrefs.GetInt("Vibrate") == 0) {
                    Handheld.Vibrate();
                }

            }

            DestroyCoin = true;
            coinCollected = true;
            Magnet = true;

            AudioManager.instance.Play("Coin");

            Animator plusoneanimation = plusOneAnim.GetComponent<Animator>();
            plusoneanimation.SetTrigger("+1");

            GameObject particleObj2 = Instantiate(CoinParticle, new Vector2(-2.13f, 4.03f), transform.rotation * Quaternion.Euler(-90, 0, 0));
            Destroy(particleObj2, 1f);

            if (GameManagerScript.instance.score == 99)
            {

                Animator mileanimation = mileAnim.GetComponent<Animator>();
                mileanimation.SetTrigger("100M");
                GameObject particleObj = Instantiate(MileStoneParticle, new Vector2(0.14f, 2.72f), transform.rotation * Quaternion.Euler(-90, 0, 0));
                Destroy(particleObj, 1f);
                GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

            }

            else if (GameManagerScript.instance.score == 299)
            {

                Animator mileanimation = mileAnim.GetComponent<Animator>();
                mileanimation.SetTrigger("300M");
                GameObject particleObj = Instantiate(MileStoneParticle, new Vector2(0.14f, 2.72f), transform.rotation * Quaternion.Euler(-90, 0, 0));
                Destroy(particleObj, 1f);
                GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");


            }

            else if (GameManagerScript.instance.score == 499)
            {

                Animator mileanimation = mileAnim.GetComponent<Animator>();
                mileanimation.SetTrigger("500M");
                Debug.Log("500");
                GameObject particleObj = Instantiate(MileStoneParticle, new Vector2(0.14f, 2.72f), transform.rotation * Quaternion.Euler(-90, 0, 0));
                Destroy(particleObj, 1f);
                GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

            }

            else if (GameManagerScript.instance.score == 999)
            {

                Animator mileanimation = mileAnim.GetComponent<Animator>();
                mileanimation.SetTrigger("1KM");
                Debug.Log("1000");
                GameObject particleObj = Instantiate(MileStoneParticle, new Vector2(0.14f, 2.72f), transform.rotation * Quaternion.Euler(-90, 0, 0));
                Destroy(particleObj, 1f);
                GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");
            }

            else if (GameManagerScript.instance.score == 4999)
            {

                Animator mileanimation = mileAnim.GetComponent<Animator>();
                mileanimation.SetTrigger("5KM");
                Debug.Log("5000");
                GameObject particleObj = Instantiate(MileStoneParticle, new Vector2(0.14f, 2.72f), transform.rotation * Quaternion.Euler(-90, 0, 0));
                Destroy(particleObj, 1f);
                GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

            }

            else if (GameManagerScript.instance.score == 9999)
            {

                Animator mileanimation = mileAnim.GetComponent<Animator>();
                mileanimation.SetTrigger("10KM");
                Debug.Log("10000");
                GameObject particleObj = Instantiate(MileStoneParticle, new Vector2(0.14f, 3.75f), transform.rotation * Quaternion.Euler(-90, 0, 0));
                Destroy(particleObj, 1f);
                GameManagerScript.instance.CameraShake.SetTrigger("PowerShake");

            }
            GameManagerScript.instance.score++;
            GameManagerScript.instance.CoinCount++;



            anim.SetTrigger("StopCoinMove");

            Color tmp = CoinAlpha.GetComponent<SpriteRenderer>().color;


            tmp.a = 0.7f;

            CoinAlpha.GetComponent<SpriteRenderer>().color = tmp;

        }


      if (col.gameObject.tag == "CoinUI" && DestroyCoin)
        {


            Destroy(gameObject);
            Animator animation1 = coinAnim.GetComponent<Animator>();
            animation1.SetTrigger("CoinJump");
            DestroyCoin = false;
            

        }

    }
}



