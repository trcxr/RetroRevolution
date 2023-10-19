using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timeLeft;

    private float originalTimeLeft;
    private Text timerText;
    private float respawnTimeScale;
    public static Timer instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        originalTimeLeft = timeLeft;
        timerText = GetComponent<Text>();
    }

    void Update() {
        //Debug.Log(Time.timeScale + " " + Time.deltaTime + " " + timeLeft + " " + Time.unscaledDeltaTime);
        timeLeft -= Time.unscaledDeltaTime;
        timerText.text = (timeLeft).ToString("0");
        if (timeLeft > 1.5f) {
            timerText.text = "3";
        } else if (timeLeft > 1f) {
            timerText.text = "2";
        } else if (timeLeft > 0.5f) {
            timerText.text = "1";
        } else if (timeLeft < 0.5f) {
            timerText.text = "GO!";
        }
        if (timeLeft < 0f) {
            if (GameManagerScript.instance.isRespawn) {
                GameManagerScript.instance.isRespawn = false;
                GameManagerScript.instance.SpawnBall();
            }
            if (GameManagerScript.instance.isPaused) {
                GameManagerScript.instance.isPaused = false;
                Time.timeScale = Pause.instance.TimeValue;
                Pause.instance.ManageTimer = true;
                Pause.instance.UpdatePauseButton(true);
                if (PlayerPrefs.HasKey("Audio") && PlayerPrefs.GetInt("Audio") == 0) {
                    AudioListener.pause = true;
                } else if (PlayerPrefs.GetInt("Audio", 1) == 1) {
                    AudioListener.pause = false;
                }
            }
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable() {
        timeLeft = originalTimeLeft;
    }
}
