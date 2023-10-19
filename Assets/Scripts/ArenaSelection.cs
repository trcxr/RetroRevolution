using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArenaSelection : MonoBehaviour
{
    private int ArenaInt = 1;
    public Text ArenaText;

    public Animator BackAnim;

    

    private void Start()
    {
        

        if (PlayerPrefs.GetInt("Arena",1) == 1)
        {
            ArenaText.text = "Dungeonville";
            ArenaInt = 1;
            BackAnim.SetTrigger("DungeonB");
       

        }

        else if (PlayerPrefs.GetInt("Arena") == 2)
        {
            ArenaText.text = "frozen wastes";
            ArenaInt = 2;
            BackAnim.SetTrigger("SnowB");
            
        }

        else if (PlayerPrefs.GetInt("Arena") == 3)
        {
            ArenaText.text = "Nether woods";
            ArenaInt = 3;
            BackAnim.SetTrigger("WoodyB");
        }

        else if (PlayerPrefs.GetInt("Arena") == 4)
        {
            ArenaText.text = "haunted castle";
            ArenaInt = 4;
            BackAnim.SetTrigger("StoneB");
        }

        else 
        {
            ArenaText.text = "Dungeonville"; // this is for the first time when the player opens the game when no playerpref is set 
            ArenaInt = 1;
        }
    }



    public void NextArena()
    {
        AudioManager.instance.Play("Arena");
        switch (++ArenaInt)
        {

            case 1:
                ArenaText.text = "Dungeonville";
                PlayerPrefs.SetInt("Arena", 1);
                BackAnim.SetTrigger("DungeonB");

                break;
            case 2:
                ArenaText.text = "frozen wastes";
                PlayerPrefs.SetInt("Arena", 2);
                BackAnim.SetTrigger("SnowB");
                break;

            case 3:
                ArenaText.text = "Nether woods";
                PlayerPrefs.SetInt("Arena", 3);
                BackAnim.SetTrigger("WoodyB");
                break;

            case 4:
                ArenaText.text = "haunted castle";
                PlayerPrefs.SetInt("Arena", 4);
                BackAnim.SetTrigger("StoneB");
                break;
              
            default:
                ResetInt();
                NextArena();
                break;


        }


    }

    public void PrevArena()
    {
        AudioManager.instance.Play("Arena");
        switch (--ArenaInt)
        {
            case 1:
                ArenaText.text = "Dungeonville";
                PlayerPrefs.SetInt("Arena", 1);
                BackAnim.SetTrigger("DungeonA");
                break;

            case 2:
                ArenaText.text = "frozen wastes";
                PlayerPrefs.SetInt("Arena", 2);
                BackAnim.SetTrigger("SnowA");
                break;

            case 3:
                ArenaText.text = "Nether woods";
                PlayerPrefs.SetInt("Arena", 3);
                BackAnim.SetTrigger("WoodyA");
                break;

            case 4:
                ArenaText.text = "haunted castle";
                PlayerPrefs.SetInt("Arena", 4);
                BackAnim.SetTrigger("StoneA");
                break;

            default:
                ResetInt();
                PrevArena();
                break;

        }


    }

    private void ResetInt() {

        if (ArenaInt >= 4) {

            ArenaInt = 0;
        }

        else
        {

            ArenaInt = 5;
        }
    }

}
