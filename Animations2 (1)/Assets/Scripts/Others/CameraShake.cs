using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    /* ----- HOW TO USE -----
     * 
     * Drag this script on MainCamera's Inspector;
     * 
     * Create a variable on the script you want to create a shake effect, that is a instance of this script, example:
     * public CameraShake aVariableName;
     * 
     * Drag the object Main Camera to the aVariableName on Inspector of the actual script of the object;
     * 
     * So, at the line you want to put the shake effect, just write the code below:
     * StartCoroutine (aVariableName.ShakeCamera(duration, magnitude));
     * 
     * And there it is! The camera is now shaking!
     * 
     * ------------------------------------------- 
     * 
     * Example applied on PlayerCollision Script:
     * public CameraShake cameraShake;
     * 
     * CODE
     * 
     * StartCoroutine (cameraShake.ShakeCamera(.1f, .2f));
     * 
     * --------------------------------------------
     * 
     * ATENTION!!!!!!!!!!!!!!!!!!!!!!!!
     * The object MainCamera HAVE to be in an empty object with the transform reseted, for the shake effect work properly.
     * Consult the scene "Collision Tests" to see it applied.
    */

    public IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition; // to store the original camera position on the originalPosition variable;

        float elapsed = 0.0f; // variable to store how many time have passed since the shake has been called;

        while (elapsed < duration) // loop to continue until the elapsed time is equal or higher than the duration declared;
        {
            if (!PauseMenu.gamePaused)
            {
                float x = Random.Range(-1f, 1f) * magnitude; // the shake that will be applied to X posstion
                float y = Random.Range(-1f, 1f) * magnitude; // the shake that will be applied to Y posstion

                transform.localPosition = new Vector3(x, y, originalPosition.z); // the shake effect

                elapsed += Time.deltaTime; // to add the time passed since the function call to the elapsed variable
            }

            yield return null;
        }

        transform.localPosition = originalPosition; // bring the camera back to the original position
    }
}
