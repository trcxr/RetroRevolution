using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class ButtonShareScript : MonoBehaviour {

    public static ButtonShareScript instance;

    //for android
    private bool isProcessing = false;
    public string message = "Play this awesome retro themed game now!";
    public string appUrl = "www.google.com";


    void Awake() {
        MakeInstance();
    }

    //method whihc make this object instance
    void MakeInstance() {
        if (instance == null) {
            instance = this;
        }
    }

    //function called from a button
    public void ButtonShare() {
        if (!isProcessing) {

            NativeShare nativeshare = new NativeShare();
            nativeshare.SetText(message + appUrl);
            nativeshare.Share();
                
        }
    }
}
