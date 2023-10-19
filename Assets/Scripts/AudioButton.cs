using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{



    public GameObject stopIcon;
    public GameObject OnIcon;

    private void Awake()
    {


        if (PlayerPrefs.HasKey("Audio") && PlayerPrefs.GetInt("Audio") == 0)
        {
            stopIcon.SetActive(true);
            OnIcon.SetActive(false);
            AudioListener.pause = true;
        }

        else if (PlayerPrefs.GetInt("Audio", 1) == 1)
        {
            stopIcon.SetActive(false);
            OnIcon.SetActive(true);
            AudioListener.pause = false;
        }

    }
    public void MuteButton()
    {

        AudioListener.pause = !AudioListener.pause;

        if (AudioListener.pause)
        {
            stopIcon.SetActive(true);
            OnIcon.SetActive(false);

            PlayerPrefs.SetInt("Audio", 0);
        }

        if (!AudioListener.pause)
        {
            stopIcon.SetActive(false);
            OnIcon.SetActive(true);
            PlayerPrefs.SetInt("Audio", 1);
        }


    }
}
