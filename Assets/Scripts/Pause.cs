using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public static Pause instance;
    public GameObject PausePanel;
    public GameObject PauseButtonObject;
    public GameObject timer;

    public bool ManageTimer;

    public float TimeValue;


    // Update is called once per frame
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ManageTimer = true;

        PauseButtonObject.SetActive(true);

      
        PausePanel.SetActive(false);
    }

    void Update()
    {

       //if (Input.GetKeyDown(KeyCode.Escape))
            //Pause();


    }


    public void PauseButton()
    {


        //AudioManager.instance.Play("Pause")


                ManageTimer = false;
                PauseStart();





    }


    public void Resume()
    {

        Time.timeScale = 0f;
        timer.SetActive(true);

        //ManageTimer = true;

        AudioManager.instance.Play("Button");
        PausePanel.SetActive(false);

    }




    public  void PauseStart()
    {
        GameManagerScript.instance.isPaused = true;
        TimeValue = Time.timeScale;
        Time.timeScale = 0f;
        timer.SetActive(false);
        UpdatePauseButton(false);

        PausePanel.SetActive(true);

        AudioListener.pause = true;
    }

    public void UpdatePauseButton(bool state) {
        PauseButtonObject.GetComponent<Button>().interactable = state;
        PauseButtonObject.GetComponent<Image>().raycastTarget = state;
        PauseButtonObject.transform.Find("PauseButton").gameObject.GetComponent<Button>().interactable = state;
        PauseButtonObject.transform.Find("PauseButton").gameObject.GetComponent<Image>().raycastTarget = state;
    }

    public void ChangeTimeScale() {

        Time.timeScale = 0f;


    }




}


