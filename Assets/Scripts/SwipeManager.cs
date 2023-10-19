using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour {
    public float maxTime = 0.5f;

    public float minSwipeDistance = 50f;

    float startTime;
    float endTime;

    Vector3 startPos;
    Vector3 endPos;

    float swipeDistance;

    float swipeTime;

    public GameObject anim;

    private void Start()
    {
    }

    void Update() {


        if (StoreManager.instance.Store.activeInHierarchy) {
            if (Input.touchCount > 0) {
                Debug.Log("touch active");
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    Debug.Log("touch Began");
                    startTime = Time.time;
                    startPos = touch.position;

                } else if (touch.phase == TouchPhase.Ended) {
                    Debug.Log("touch Ended");
                    endTime = Time.time;
                    endPos = touch.position;

                    swipeDistance = (endPos - startPos).magnitude;

                    swipeTime = endTime - startTime;

                    if (swipeTime < maxTime && swipeDistance > minSwipeDistance) {
                        Debug.Log("Swiped");
                        swipe();


                    }
                }
            }
        }
    }

    void swipe() {

        Vector2 distance = endPos - startPos;
        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y)) {

            Debug.Log("Horizontal Swipe");

            if (distance.x > 0) {
                if (StoreManager.instance.storeSelected == "Power") {
                    StoreManager.instance.OpenSkinStore();
                }

                Debug.Log("Right Swipe");

            }
            if (distance.x < 0) {
                if (StoreManager.instance.storeSelected == "Skin") {
                    StoreManager.instance.OpenPowerStore();
                }
                Debug.Log("Left Swipe");
            }



        } else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y)) {

            Debug.Log("Vertical Swipe");

            if (distance.y > 0) {

                Debug.Log("Up Swipe");

            }


            if (distance.y < 0) {

                Debug.Log("Down Swipe");

            }

        }




    }


}