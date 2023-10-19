using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {
    public GameObject Store;
    public GameObject WarningPanel;
    public GameObject CongratulationsPanel;
    public GameObject particles;
    public Button skinsBtn;
    public Button powerBtn;

    public GameObject[] skinPrice;
    public GameObject[] skinGrey;
    public Button[] skinBtn;
    public GameObject[] skinTick;

    public Button starterSkinBtn;
    public GameObject starterSkinTick;

    public Button[] powerupBtn;
    public Image[] powerupBar;
    public GameObject[] powerupText;

    public Button heartBtn;
    public GameObject[] heartRed;
    public GameObject heartText;

    private int[] playerPrefSkin;
    private int[] playerPrefPower;

    private int playerPrefHearts;
    private float t = 0.0f;
    public float coinAnimationLength = 1.0f;

    public String storeSelected;
    int coinReduce;

    public float sec;
    public GameObject anim;
    public static StoreManager instance;

    private bool animateCoin = false;
    private int currentStartPrice;
    private int currentFinalPrice;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0));

        playerPrefSkin = new int[skinBtn.Length];
        playerPrefPower = new int[powerupBtn.Length];
        GetPlayerPrefs();

        UpdateSkinStore();
        UpdatePowerStore();
        Store.SetActive(false);
    }

    private void Update() {
        if (animateCoin) {
            CoinAnimate(currentStartPrice, currentFinalPrice);
        }
    }

    private void GetPlayerPrefs() {
        for (int i = 0; i < playerPrefSkin.Length; i++) {
            playerPrefSkin[i] = PlayerPrefs.GetInt("Skin" + (i + 1));
            if(playerPrefSkin[i] == 1) {
                AchievementManager.instance.SkinAchievements(i + 1);
            }
        }
        for (int i = 0; i < playerPrefPower.Length; i++) {
            playerPrefPower[i] = PlayerPrefs.GetInt("Power" + (i + 1));
        }
        playerPrefHearts = PlayerPrefs.GetInt("Hearts");
    }

    public void OpenStoreButton() {
        AudioManager.instance.Play("Button");
        Store.SetActive(true);
        skinsBtn.interactable = false;
        powerBtn.interactable = true;
        MenuUIManager.instance.StopQuitTimer();
        storeSelected = "Skin";
    }

    public void CloseStoreButton() {
        AudioManager.instance.Play("Close");
        Store.SetActive(false);
    }

    public void OpenSkinStore() {
        AudioManager.instance.Play("Button");
        Animator animation = anim.GetComponent<Animator>();
        animation.SetTrigger("Skin");
        skinsBtn.interactable = false;
        powerBtn.interactable = true;
        storeSelected = "Skin";
    }

    public void OpenPowerStore() {
        AudioManager.instance.Play("Button");
        Animator animation = anim.GetComponent<Animator>();
        animation.SetTrigger("Power");
        powerBtn.interactable = false;
        skinsBtn.interactable = true;
        storeSelected = "Power";
    }

    public void StarterSkin() {
        PlayerPrefs.SetInt("Skin", 0);
        MenuUIManager.instance.playSkin.sprite = MenuUIManager.instance.ballSprites[PlayerPrefs.GetInt("Skin")];
        UpdateSkinStore();
    }

    public void BuyPowerUpgrade(int i) {
        GetPlayerPrefs();
        i = i - 1;
        int price = Int32.Parse(powerupText[i].GetComponentInChildren<Text>().text);
        if (PlayerPrefs.GetInt("Coins") < price) {
            if (WarningPanel.activeInHierarchy) {
                WarningPanel.SetActive(false);
            }
            AudioManager.instance.Play("Warning");
            WarningPanel.SetActive(true);
        } else {
            if (CongratulationsPanel.activeInHierarchy) {
                CongratulationsPanel.SetActive(false);
            }
            if (particles.activeInHierarchy) {
                particles.SetActive(false);
            }
            AudioManager.instance.Play("Buy");
            CongratulationsPanel.SetActive(true);
            particles.SetActive(true);

            Invoke("CongratsPanel", sec);

            coinReduce = PlayerPrefs.GetInt("Coins");
            coinReduce -= price;

            AnimateCoinBool(PlayerPrefs.GetInt("Coins"), coinReduce);
            PlayerPrefs.SetInt("Coins", coinReduce);
            PlayerPrefs.SetInt("Power" + (i + 1), playerPrefPower[i] + 1);
            AchievementManager.instance.ShoppingAchievments(price);
            UpdatePowerStore();
        }
    }

    public void ButHeartUpgrade() {
        GetPlayerPrefs();
        int price = Int32.Parse(heartText.GetComponentInChildren<Text>().text);
        if (PlayerPrefs.GetInt("Coins") < price) {
            if (WarningPanel.activeInHierarchy) {
                WarningPanel.SetActive(false);
            }
            AudioManager.instance.Play("Warning");
            WarningPanel.SetActive(true);
        } else {

            if (CongratulationsPanel.activeInHierarchy) {
                CongratulationsPanel.SetActive(false);
            }
            if (particles.activeInHierarchy) {
                particles.SetActive(false);
            }
            AudioManager.instance.Play("Buy");
            CongratulationsPanel.SetActive(true);
            particles.SetActive(true);

            Invoke("CongratsPanel", sec);

            coinReduce = PlayerPrefs.GetInt("Coins");
            coinReduce -= price;

            AnimateCoinBool(PlayerPrefs.GetInt("Coins"), coinReduce);
            PlayerPrefs.SetInt("Coins", coinReduce);
            PlayerPrefs.SetInt("Hearts", playerPrefHearts + 1);
            AchievementManager.instance.ShoppingAchievments(price);
            UpdatePowerStore();
        }
    }

    public void BuySkin(int i) {
        GetPlayerPrefs();
        i = i - 1;
        int price = Int32.Parse(skinPrice[i].GetComponentInChildren<Text>().text);
        if (playerPrefSkin[i] == 1) {
            AudioManager.instance.Play("Button");
            PlayerPrefs.SetInt("Skin", (i + 1));
            MenuUIManager.instance.playSkin.sprite = MenuUIManager.instance.ballSprites[PlayerPrefs.GetInt("Skin")];
            Debug.Log(PlayerPrefs.GetInt("Skin"));
            UpdateSkinStore();
        } else {
            if (PlayerPrefs.GetInt("Coins") < price) {
                if (WarningPanel.activeInHierarchy) {
                    WarningPanel.SetActive(false);
                }
                AudioManager.instance.Play("Warning");
                WarningPanel.SetActive(true);
            } else {
                if (CongratulationsPanel.activeInHierarchy) {
                    CongratulationsPanel.SetActive(false);
                }
                if (particles.activeInHierarchy) {
                    particles.SetActive(false);
                }
                AudioManager.instance.Play("Buy");
                CongratulationsPanel.SetActive(true);
                particles.SetActive(true);

                Invoke("CongratsPanel", sec);

                coinReduce = PlayerPrefs.GetInt("Coins");
                coinReduce -= price;

                AnimateCoinBool(PlayerPrefs.GetInt("Coins"), coinReduce);
                PlayerPrefs.SetInt("Coins", coinReduce);
                PlayerPrefs.SetInt("Skin" + (i + 1), 1);
                AchievementManager.instance.SkinAchievements(i + 1);
                AchievementManager.instance.ShoppingAchievments(price);
                UpdateSkinStore();
            }
        }
    }

    private void UpdateSkinStore() {
        GetPlayerPrefs();
        for (int i = 0; i < playerPrefSkin.Length; i++) {
            if (playerPrefSkin[i] == 1) {
                skinBtn[i].GetComponentInChildren<Text>().text = "SELECT";
                skinBtn[i].interactable = true;
                skinGrey[i].SetActive(false);
                skinPrice[i].SetActive(false);
            } else {
                skinBtn[i].GetComponentInChildren<Text>().text = "BUY";
                skinBtn[i].interactable = true;
            }
            skinTick[i].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Skin") == 0) {
            starterSkinBtn.GetComponentInChildren<Text>().text = "SELECTED";
            starterSkinBtn.interactable = false;
            starterSkinTick.SetActive(true);
        } else {
            skinBtn[PlayerPrefs.GetInt("Skin") - 1].GetComponentInChildren<Text>().text = "SELECTED";
            skinBtn[PlayerPrefs.GetInt("Skin") - 1].interactable = false;
            skinTick[PlayerPrefs.GetInt("Skin") - 1].SetActive(true);
            starterSkinBtn.GetComponentInChildren<Text>().text = "SELECT";
            starterSkinBtn.interactable = true;
            starterSkinTick.SetActive(false);
        }
    }

    private void UpdatePowerStore() {
        GetPlayerPrefs();
        for (int i = 0; i < playerPrefPower.Length; i++) {
            powerupBar[i].fillAmount = playerPrefPower[i] * 0.25f;
            if(powerupBar[i].fillAmount != 0) {
                powerupText[i].GetComponentInChildren<Text>().text = "" + playerPrefPower[i] * 5 * 1000;
            }
            if (powerupBar[i].fillAmount == 1) {
                powerupBtn[i].GetComponentInChildren<Text>().text = "MAX";
                powerupBtn[i].interactable = false;
                powerupText[i].SetActive(false);
            }
            
        }
        for (int i = 0; i < heartRed.Length; i++) {
            heartRed[i].SetActive(false);
        }

        for (int i = 0; i < playerPrefHearts; i++) {
            heartRed[i].SetActive(true);
        }
        if (playerPrefHearts == 3) {
            heartText.SetActive(false);
            heartBtn.GetComponentInChildren<Text>().text = "MAX";
            heartBtn.interactable = false;
        }
        if(playerPrefHearts != 0) {
            heartText.GetComponentInChildren<Text>().text = "" + playerPrefHearts * 5 * 1000;
        }
    }

    public void CongratsPanel() {

        CongratulationsPanel.SetActive(false);
        particles.SetActive(false);

    }

    private void CoinAnimate(int startCoins, int endCoins) {
        t = Mathf.MoveTowards(t, 1.0f, Time.deltaTime / coinAnimationLength);
        int displayedCoins = (int) Mathf.Lerp(startCoins, endCoins, t);
        MenuUIManager.instance.CoinText.text = "" + displayedCoins.ToString();
    }

    public void AnimateCoinBool(int startCoin, int finalCoin) {
        AudioManager.instance.Play("CoinIncrease");
        currentStartPrice = startCoin;
        currentFinalPrice = finalCoin;
        animateCoin = true;
        t = 0.0f;
    }
}
