using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shake camera using IEnumerable inside a coroutine.
// IEnumerable will wait a couple of seconds before switching back to the main camera
public class CameraManager : Singleton<CameraManager> {

    private CameraController cameraController;
    private Coroutine coroutine;


    
    // Getter
    public CameraController CAMERA_CONTROLLER {

        get {

            if (cameraController == null) {

                cameraController = GameObject.FindObjectOfType<CameraController>();
            }

            return cameraController;
        }
    }




    public void CameraShake(float runForSeconds) {

        // prevent duplicate coroutines running
        if (coroutine != null) {
            StopCoroutine(coroutine);
        }

        // no duplicate coroutines found. start this one
        coroutine = StartCoroutine(ICameraShake(runForSeconds));
    }




    IEnumerator ICameraShake(float secondsToRun) {

        // Trigger Shake Camera
        CAMERA_CONTROLLER.triggerCamera(CameraController.CameraTriggers.Shake);

        // wait
        yield return new WaitForSeconds(secondsToRun);

        // switch to Default Camera
        CAMERA_CONTROLLER.triggerCamera(CameraController.CameraTriggers.Default);
    }

} // class
