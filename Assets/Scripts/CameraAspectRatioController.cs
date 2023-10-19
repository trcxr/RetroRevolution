using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatioController : MonoBehaviour {

    float cameraAspect;

    // Use this for initialization
    void Start() {
        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();
        cameraAspect = camera.aspect;

        if(camera.aspect < 0.51) {
            camera.orthographicSize = 5.6f;
        } else {
            camera.orthographicSize = 5f;
        }
    }
}