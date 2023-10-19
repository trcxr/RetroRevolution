using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;

public class LeaderboardManager : MonoBehaviour
{

    public static LeaderboardManager instance;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

    }



    void Start()
    {
        PlayGamesPlatform.Activate();
        Login();
        if (Social.localUser.authenticated) {
            AchievementManager.instance.CheckForOfflineAchievements();
        }
    }

    

    void Update()
    {

    }

    public void Login()
    {
        if (!Social.localUser.authenticated) {
            Social.localUser.Authenticate((bool success) => {
            });
        }
    }

    public void AddScoreToLeaderboard()
    {

        if (Social.localUser.authenticated) {
            Social.ReportScore(GameManagerScript.instance.score, LeaderBoards.leaderboard_top_scores, (bool success) => {

            });
        }
    }


    public void ShowLeaderboard()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderBoards.leaderboard_top_scores);
        }
        else
        {
            Login();
        }
    }
}



