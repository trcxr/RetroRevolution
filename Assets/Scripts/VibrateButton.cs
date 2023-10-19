using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateButton : MonoBehaviour {

    public GameObject stopIcon;
    public GameObject OnIcon;

    // 0 - Enable, 1 - Disable
    private void Awake() {
        if (!PlayerPrefs.HasKey("Vibrate")) {
            stopIcon.SetActive(false);
            OnIcon.SetActive(true);
            PlayerPrefs.SetInt("Vibrate", 0);
        } else if (PlayerPrefs.GetInt("Vibrate") == 1) {
            stopIcon.SetActive(true);
            OnIcon.SetActive(false);
        } else if (PlayerPrefs.GetInt("Vibrate") == 0) {
            stopIcon.SetActive(false);
            OnIcon.SetActive(true);
        }
    }

    public void ToggleButton() {
        if (PlayerPrefs.HasKey("Vibrate") && PlayerPrefs.GetInt("Vibrate") == 0) {
            stopIcon.SetActive(true);
            OnIcon.SetActive(false);
            PlayerPrefs.SetInt("Vibrate", 1);
        } else if (PlayerPrefs.GetInt("Vibrate", 1) == 1) {
            Handheld.Vibrate();
            stopIcon.SetActive(false);
            OnIcon.SetActive(true);
            PlayerPrefs.SetInt("Vibrate", 0);
        }
    }
}
