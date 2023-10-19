using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using GooglePlayGames;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager instance;

    public GameObject[] objectsToEnable;

    public GameObject fadeImage;

    public Text highScoreText;

    public Text CoinText;

    public GameObject AboutPanel;

    public GameObject quitobject;

    public GameObject skipButton;

    public GameObject SettingsPanel;

    public GameObject ConfirmPanel;

    public GameObject LoadingText;

    public Image playSkin;
    public Sprite[] ballSprites;

    private bool clickedBefore = false;
    private bool closeGame = false;
    private Coroutine quitTimerRoutine;
    private bool skipped = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()

    {
        playSkin.sprite = ballSprites[PlayerPrefs.GetInt("Skin")];
        AboutPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        //ConfirmPanel.SetActive(false);
        AudioManager.instance.Stop("Time");
        AudioManager.instance.Stop("Death");

        AudioManager.instance.Stop("ShieldParticle");
        AudioManager.instance.Stop("SpeedParticle");

        CoinText.text = "" + PlayerPrefs.GetInt("Coins");

        highScoreText.text = "" + PlayerPrefs.GetInt("HighScore");

        AudioManager.instance.Stop("GameSong");
        AudioManager.instance.Stop("WoodSound");
        AudioManager.instance.Stop("FrozenSound");
        AudioManager.instance.Stop("CastleSound");
        AudioManager.instance.Play("MenuSong");
        skipButton.SetActive(false);

        if (PlayerPrefs.GetInt("SkipAllowed", 1) != 1) {
            skipped = true;
        }

    }

    // Update is called once per frame
    void Update() {


    

        if (StoreManager.instance.Store.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {

            StoreManager.instance.CloseStoreButton();

        }
        else if (AboutPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {

            CloseAboutPanel();

        } else if (SettingsPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {

            CloseSettingsPanel();

        }
         //Check input for the first time
         else if ((closeGame && !clickedBefore) || (Input.GetKeyDown(KeyCode.Escape) && !clickedBefore
            && !StoreManager.instance.Store.activeInHierarchy
            && !AboutPanel.activeInHierarchy
            && !SettingsPanel.activeInHierarchy)) {
            Debug.Log("Back Button pressed for the first time");
            //Set to false so that this input is not checked again. It will be checked in the coroutine function instead
            clickedBefore = true;
            closeGame = false;
            //Activate Quit Object
            quitobject.SetActive(true);

            //Start quit timer
            quitTimerRoutine = StartCoroutine(quitingTimer());
        }
    }

    IEnumerator quitingTimer() {
        //Wait for a frame so that Input.GetKeyDown is no longer true
        yield return null;

        //3 seconds timer
        const float timerTime = 3f;
        float counter = 0;

        while (counter < timerTime) {
            //Increment counter while it is < timer time(3)
            counter += Time.deltaTime;

            //Check if Input is pressed again while timer is running then quit/exit if is
            if (closeGame || Input.GetKeyDown(KeyCode.Escape) && quitobject.activeInHierarchy) {
                Debug.Log("Back Button pressed for the second time. EXITING.....");
                Quit();
            }

            //Wait for a frame so that Unity does not freeze
            yield return null;
        }

        Debug.Log("Timer finished...Back Button was NOT pressed for the second time within: '" + timerTime + "' seconds");

        //Timer has finished and NO QUIT(NO second press) so deactivate
        quitobject.SetActive(false);
        //Reset clickedBefore so that Input can be checked again in the Update function
        clickedBefore = false;
        closeGame = false;
    }

    public void StopQuitTimer() {
        if (quitTimerRoutine != null) {
            StopCoroutine(quitTimerRoutine);
            quitobject.SetActive(false);
            clickedBefore = false;
        }
    }

    void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    //Application.Quit();
    System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }


    public void Metal()
    {
        AudioManager.instance.Play("Play");
        StartCoroutine(FadeImage());
        StartCoroutine(LoadGameScene());
    }



    IEnumerator FadeImage() {
        List<GameObject> disableList = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(disableList);
        List<GameObject> enableList = new List<GameObject>(objectsToEnable);
        disableList.RemoveAll(enableList.Contains);

        fadeImage.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        foreach (GameObject disableObj in disableList) {
            disableObj.SetActive(false);
        }
        LoadLogo();
    }

    public void SkipLoadMainScene() {
        skipped = true;
    }

    IEnumerator LoadGameScene() {
        yield return new WaitForSeconds(3.0f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainGameScene");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone) {
            //Output the current progress

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f) {
                if (skipped) {
                    asyncOperation.allowSceneActivation = true;
                    PlayerPrefs.SetInt("SkipAllowed", 0);
                  
                } else {
                    skipButton.SetActive(true);
                    LoadingText.SetActive(false);
                }
            }
            yield return null;
        }
    }

    private void LoadLogo() {
        foreach (GameObject enableObj in objectsToEnable) {
            enableObj.SetActive(true);
        }
    }

    public void OpenAboutPanel()
    {
        AudioManager.instance.Play("Button");
        StoreManager.instance.Store.SetActive(false);
        SettingsPanel.SetActive(false);
        ConfirmPanel.SetActive(false);
        AboutPanel.SetActive(true);
        StopQuitTimer();
    }

    public void CloseAboutPanel()
    {
        AudioManager.instance.Play("Close");
        AboutPanel.SetActive(false);

    }

    public void CloseSettingsPanel() {
        AudioManager.instance.Play("Close");
        SettingsPanel.SetActive(false);
        ConfirmPanel.SetActive(false);
    }

    public void CloseGame()
    {
        StoreManager.instance.Store.SetActive(false);
        SettingsPanel.SetActive(false);
        ConfirmPanel.SetActive(false);
        AboutPanel.SetActive(false);
        closeGame = true;
    }


    public void LeaderboardButton() {
        AudioManager.instance.Play("Button");

        LeaderboardManager.instance.ShowLeaderboard();

    }

    public void SettingsButton()
    {
        AudioManager.instance.Play("Button");
        SettingsPanel.SetActive(true);
        StopQuitTimer();
    }

    public void ResetProgressButton() {
        AudioManager.instance.Play("Button");
        PlayerPrefs.DeleteAll();
        ConfirmPanel.SetActive(true);
    }

    public void ConfirmPanelYes() {
        AudioManager.instance.Play("Button");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ConfirmPanelNo() {
        AudioManager.instance.Play("Close");
        ConfirmPanel.SetActive(false);
    }

    public void AchievementsButton() {
        AudioManager.instance.Play("Button");

        PlayGamesPlatform.Instance.ShowAchievementsUI();

    }
}
