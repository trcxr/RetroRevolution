using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript instance;
 
    public GameObject Metal;
    public GameObject Stone;
    public GameObject Wood;
    public GameObject Snow;
    public Text CoinText;
    public int posX;
    public int posY;

    public bool magnetPower;
    
    public bool playerDeath;
    public float t = 0.0f; //renamed i to t, for convention (i is for loops, t is for lerping or easing function "time" variables
    public Text highScore;
    public float highScoreAnimationLength = 1.0f; //how long in seconds it should take to go from score 0 to the players final score

    GameObject PausePanel;

    GameObject anim1;

    public GameObject ShieldParticle;

    public float powerupTime = 1f;

    public GameObject MetalSpikes;
    public GameObject StoneSpikes;
    public GameObject WoodSpikes;
    public GameObject SnowSpikes;

    public GameObject BallPrefab;

    public Animator CameraShake;
    public float MoveUpSpeed;
    public int score;
    public int CoinCount;
    public Text scoreText;
    public Text highScoreText;

   

    public GameObject GameOverPanel;

    public GameObject BallSpeed;

    public bool ShieldTurnOn;
    public bool isRespawn;
    public bool isPaused;
    public GameObject timer;

    public float slowdownFactor; 
    public float slowdownLength;

    public bool ballSlowed;

    public Sprite[] ballSprites;


    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {


        AudioManager.instance.Stop("ShieldParticle");
        AudioManager.instance.Stop("SpeedParticle");
        AudioManager.instance.Stop("Death");
        AudioManager.instance.Stop("Time");

        slowdownLength = slowdownLength + PlayerPrefs.GetInt("Power3", 0) * 2f;

        playerDeath = false;

       
        AudioManager.instance.Stop("MenuSong");

        GameOverPanel.SetActive(false);


        PlayerPrefs.SetInt("Score", 0);

        ShieldTurnOn = false;
        GameObject.FindWithTag("Ball").GetComponent<SpriteRenderer>().sprite = ballSprites[PlayerPrefs.GetInt("Skin")];
        BallPrefab.GetComponent<SpriteRenderer>().sprite = ballSprites[PlayerPrefs.GetInt("Skin")];

        if (PlayerPrefs.GetInt("Arena", 1) == 1) {

            Metal.SetActive(true);
            MetalSpikes.SetActive(true);
            AudioManager.instance.Play("GameSong");
         

        }

        if (PlayerPrefs.GetInt("Arena") == 2) {

            Snow.SetActive(true);
            SnowSpikes.SetActive(true);
            AudioManager.instance.Play("FrozenSound");
          

        }
        if (PlayerPrefs.GetInt("Arena") == 3) {

            Wood.SetActive(true);
            WoodSpikes.SetActive(true);
            AudioManager.instance.Play("WoodSound");

        } 
        if (PlayerPrefs.GetInt("Arena") == 4) {

            Stone.SetActive(true);
            StoneSpikes.SetActive(true);
            AudioManager.instance.Play("CastleSound");
        
        }
        
    }

    private void OnApplicationPause(bool pause) {
        if (pause && !isPaused) {
            Pause.instance.PauseButton();
        }
    }

    private void OnApplicationFocus(bool focus) {
        if (!focus && !isPaused) {
            Pause.instance.PauseButton();
        }
    }

    private void Update() {
        AchievementManager.instance.GameAchivements();

        if (Input.GetKeyDown(KeyCode.Escape) && !Pause.instance.PausePanel.activeInHierarchy && !isPaused)
        {
            Pause.instance.PauseButton();
      
        }

        else if (Pause.instance.PausePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {

            Pause.instance.Resume();

        }


        if (playerDeath == false)
        {
            t = 0.0f;
            scoreText.text = score.ToString();
        }
        else if (playerDeath == true)
        {
            t = Mathf.MoveTowards(t, 1.0f, Time.deltaTime / highScoreAnimationLength); //move t closer to 1.0, we use Time.deltaTime to make it move in "realtime" rather than frame time, MoveTowards will always stop at the target value, so we never go over it.
            int previousCoins = PlayerPrefs.GetInt("Coins") - PlayerPrefs.GetInt("Score");
            int displayedCoins = (int)Mathf.Lerp(previousCoins, PlayerPrefs.GetInt("Coins"), t); //Here we use a lerp to calculate what number to display, we then cast it to an int to display it correctly (no decimals)
            CoinText.text = "" + displayedCoins.ToString();

            t = Mathf.MoveTowards(t, 1.0f, Time.deltaTime / highScoreAnimationLength); //move t closer to 1.0, we use Time.deltaTime to make it move in "realtime" rather than frame time, MoveTowards will always stop at the target value, so we never go over it.
            int displayedScore = (int) Mathf.Lerp(PlayerPrefs.GetInt("Score"), 0, t); //Here we use a lerp to calculate what number to display, we then cast it to an int to display it correctly (no decimals)
            scoreText.text = "" + displayedScore.ToString();
        }
        
        //Debug.Log("timeScale : " + Time.timeScale);
        if (Pause.instance.ManageTimer)
        {

            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        }
    }

    

    
    IEnumerator Shielddelay()
    {
        yield return new WaitForSeconds(Shield.instance.powerupTime);

        ShieldTurnOn = false;

        BallControl.instance.collisionDone = false;


    }

    IEnumerator ShielddelayGameOver()
    {
        yield return new WaitForSeconds(powerupTime);

        ShieldTurnOn = false;



    }

    IEnumerator SpeedDelay()
    {
        BallSpeed = GameObject.FindWithTag("Ball");
        yield return new WaitForSeconds(SpeedPower.instance.powerupTime);
        if (BallSpeed != null) {
            BallSpeed.GetComponent<BallControl>().speed /= 1.8f;
        }
    }

    public void SlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
        Pause.instance.ManageTimer = true;
        //ballSlowed = true;

    }


    public void GameOver() {

        magnetPower = false;

        Pause.instance.PauseButtonObject.SetActive(false);

        AudioManager.instance.Play("Death");

        Time.timeScale = Mathf.Clamp(1f, 1f, 1f);

        GameOverPanel.SetActive(true);
        isPaused = true;

     
        PlayerPrefs.SetInt("Score", score);

        LeaderboardManager.instance.AddScoreToLeaderboard();

        
       CoinText.text = "" + PlayerPrefs.GetInt("Coins");

        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);

                Debug.Log("NEW HIGHSCORE");
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        highScoreText.text = "" + PlayerPrefs.GetInt("HighScore");

        Invoke("playerDeathBool", 1f);

        AchievementManager.instance.DeathAchievements();


    }

    public void RestartButton()
    {
        if (PlayerPrefs.HasKey("Audio") && PlayerPrefs.GetInt("Audio") == 0)
        {
            AudioListener.pause = true;
        }

        else if (PlayerPrefs.GetInt("Audio", 1) == 1)
        {


            AudioListener.pause = false;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGameScene");
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        Pause.instance.ManageTimer = false;

        SceneManager.LoadScene("MainMenu");
    }


    public void SpawnBall() {
        TurnOnShield();
        Instantiate(BallPrefab, new Vector3(transform.position.x + posX, transform.position.y +posY, 0), Quaternion.identity);
        Debug.Log("BallSpawned");
    }

    public void InvokeSlowMotion() {
        Invoke("SlowMotion", 0.1f);
    }

    public void TurnOnShield()
    {
        ShieldTurnOn = true;
        slowdownLength /= 3;

        Invoke("TurnOnShieldAfterTime", 0.001f);

        Invoke("ChangeSlowMotionTime", 2f);

    }

    public void IncreaseSpeed() {
        BallSpeed = GameObject.FindWithTag("Ball");

        BallSpeed.GetComponent<BallControl>().speed *= 1.8f;


    }

    public void TurnOnShieldAfterTime()
    {
        anim1 = GameObject.Find("Timer");

        AudioManager.instance.Play("Time");
        AudioManager.instance.Play("Shield");
     

        //  PlatformSpawner.instance.PowerupCollected(powerupTime);


      
        GameObject particleObj = Instantiate(ShieldParticle, transform.position, transform.rotation * Quaternion.Euler(-90, 0, 0));


        StartCoroutine("ShielddelayGameOver");


        Animator animation1 = anim1.GetComponent<Animator>();
        animation1.SetTrigger("Shield");
        animation1.speed = 1 / (powerupTime / animation1.runtimeAnimatorController.animationClips[0].length);

    }

    public void ChangeSlowMotionTime() {


        slowdownLength *= 3;

    }

    public void playerDeathBool() {

        CoinCount = CoinCount + PlayerPrefs.GetInt("Coins");

        AudioManager.instance.Play("CoinIncrease");
        PlayerPrefs.SetInt("Coins", CoinCount);

        playerDeath = true;
       

    }

    public void MagnetPowerFalse()
    {

        Invoke("MagnetPowerFalseAfterTime", Magnet.instance.powerupTime);

    }

    public void MagnetPowerFalseAfterTime() {

        magnetPower = false;

    }


}



