using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    public static AchievementManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void ShowAchievement() {

        FindObjectOfType<AudioManager>().Play("Button");

        if (Social.localUser.authenticated) {

            Social.ShowAchievementsUI();

        } else {
            LeaderboardManager.instance.Login();

        }

    }

    public void GameAchivements() {
        if (!Social.localUser.authenticated) {
            return;
        }

        if (GameManagerScript.instance.score > 100) {

            Social.ReportProgress(AchievementServices.achievement_beginners_luck, 100f, (bool success) => { });

        }

        if (GameManagerScript.instance.score > 300) {

            Social.ReportProgress(AchievementServices.achievement_piece_of_cake, 100f, (bool success) => { });

        }

        if (GameManagerScript.instance.score > 500) {

            Social.ReportProgress(AchievementServices.achievement_rollin_in_the_deep, 100f, (bool success) => { });

        }

        if (GameManagerScript.instance.score > 1000) {

            Social.ReportProgress(AchievementServices.achievement_how_did_you_get_here, 100f, (bool success) => { });


        }

        if (GameManagerScript.instance.score > 5000) {

            Social.ReportProgress(AchievementServices.achievement_hackerman, 100f, (bool success) => { });


        }

        if (GameManagerScript.instance.score > 10000) {

            Social.ReportProgress(AchievementServices.achievement_please_stop_hacking_for_gods_sake, 100f, (bool success) => { });


        }

    }

    public void SkinAchievements(int i) {
        if (!Social.localUser.authenticated) {
            return;
        }

        if (i == 1 && !PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_3_pointer).IsUnlocked) {
            // Basketball
            Social.ReportProgress(AchievementServices.achievement_3_pointer, 100f, (bool success) => { });
        } else if (i == 2 && !PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_its_a_goal).IsUnlocked) {
            // Football
            Social.ReportProgress(AchievementServices.achievement_its_a_goal, 100f, (bool success) => { });
        } else if (i == 3 && !PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_strike).IsUnlocked) {
            // Bowling
            Social.ReportProgress(AchievementServices.achievement_strike, 100f, (bool success) => { });
        } else if (i == 4 && !PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_you_got_served).IsUnlocked) {
            // Tennis
            Social.ReportProgress(AchievementServices.achievement_you_got_served, 100f, (bool success) => { });
        } else if (i == 5 && !PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_howzat).IsUnlocked) {
            // Cricket
            Social.ReportProgress(AchievementServices.achievement_howzat, 100f, (bool success) => { });
        } else if (i == 6 && !PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_gold_mine).IsUnlocked) {
            // Gold
            Social.ReportProgress(AchievementServices.achievement_gold_mine, 100f, (bool success) => { });
        }
        Achievement achievementMasterAvatar = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_master_avatar);
        if (isAllSkinsBought() && !achievementMasterAvatar.IsUnlocked) {
            Social.ReportProgress(AchievementServices.achievement_master_avatar, 100f, (bool success) => { });
        }
    }

    public void ShoppingAchievments(int coins) {
        if (!Social.localUser.authenticated) {
            return;
        }
        // 50 coins = 1 step;
        int steps = coins / 50;
        Debug.Log(steps);
        Achievement achievementShopper = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_shopper);
        if (!achievementShopper.IsUnlocked) {
            Social.ReportProgress(AchievementServices.achievement_shopper, 100f, (bool success) => { });
        }

        Achievement achievementPenny = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_a_penny_spent_is_a_penny_earned);
        if (!achievementPenny.IsUnlocked) {
            PlayGamesPlatform.Instance.IncrementAchievement(
              AchievementServices.achievement_a_penny_spent_is_a_penny_earned, steps, (bool success) => {
              });
        }

        Achievement achievementLavish = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_lavish_spender);
        if (!achievementLavish.IsUnlocked) {
            PlayGamesPlatform.Instance.IncrementAchievement(
                AchievementServices.achievement_lavish_spender, steps, (bool success) => {
                });
        }

        Achievement achievementPowerHungry = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_power_hungry);
        if (isAllPowerBought() && !achievementPowerHungry.IsUnlocked) {
            Social.ReportProgress(AchievementServices.achievement_power_hungry, 100f, (bool success) => { });
        }
    }

    public void DeathAchievements() {
        if (!Social.localUser.authenticated) {
            return;
        }

        Achievement achievementDisgraceful = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_disgraceful);
        if (!achievementDisgraceful.IsUnlocked) {
            Social.ReportProgress(AchievementServices.achievement_disgraceful, 100f, (bool success) => { });
        }

        Achievement achievementDoomed = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_doomed);
        if (!achievementDoomed.IsUnlocked) {
            PlayGamesPlatform.Instance.IncrementAchievement(
                AchievementServices.achievement_doomed, 1, (bool success) => {
                });
        }

        Achievement achievementEpicFail = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_epic_fail);
        if (!achievementEpicFail.IsUnlocked) {
            PlayGamesPlatform.Instance.IncrementAchievement(
                AchievementServices.achievement_epic_fail, 1, (bool success) => {
                });
        }
    }

    private bool isAllSkinsBought() {
        if (!Social.localUser.authenticated) {
            return false;
        }

        if (!PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_3_pointer).IsUnlocked) {
            return false;
        }
        if (!PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_its_a_goal).IsUnlocked) {
            return false;
        }
        if (!PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_strike).IsUnlocked) {
            return false;
        }
        if (!PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_you_got_served).IsUnlocked) {
            return false;
        }
        if (!PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_howzat).IsUnlocked) {
            return false;
        }
        if (!PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_gold_mine).IsUnlocked) {
            return false;
        }
        return true;
    }

    private bool isAllPowerBought() {
        for (int i = 0; i < 4; i++) {
            if (PlayerPrefs.GetInt("Power" + (i + 1), 1) != 4) {
                return false;
            }
        }
        if (PlayerPrefs.GetInt("Hearts", 1) != 3) {
            return false;
        }
        return true;
    }

    public void CheckForOfflineAchievements() {
        Achievement achievementPowerHungry = PlayGamesPlatform.Instance.GetAchievement(AchievementServices.achievement_power_hungry);
        if (isAllPowerBought() && !achievementPowerHungry.IsUnlocked) {
            Social.ReportProgress(AchievementServices.achievement_power_hungry, 100f, (bool success) => { });
        }
    }
}