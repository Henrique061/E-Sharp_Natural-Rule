using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CollisionTests : MonoBehaviour
{
    public AudioMixerSnapshot snapshot;
    public AudioMixer audioMixer;

    private void OnTriggerEnter2D(Collider2D collision) // detect collision with triggers
    {
        CollisionProcess(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) // detect collision with colliders
    {
        CollisionProcess(collision.gameObject);
    }

    void CollisionProcess(GameObject collider) // call the collision functions
    {
        if (collider.CompareTag("Player"))
            Tests();
    }

    void Tests()
    {
        #region Audio Manager Functions

        if (this.tag == "PauseAll")
        {
            Debug.Log("Pause All");
            FindObjectOfType<AudioManager>().PauseAll();
        }

        if (this.tag == "UnPauseAll")
        {
            Debug.Log("UnPause All");
            FindObjectOfType<AudioManager>().UnPauseAll();
        }

        if (this.tag == "StopAll")
        {
            Debug.Log("Stop All");
            FindObjectOfType<AudioManager>().StopAll();
        }

        if (this.tag == "PlayBGM")
        {
            Debug.Log("Play BGM");
            FindObjectOfType<AudioManager>().PlaySound("BGM");
        }

        if (this.tag == "Pause")
        {
            Debug.Log("Pause BGM");
            FindObjectOfType<AudioManager>().PauseSound("BGM");
        }

        if (this.tag == "UnPause")
        {
            Debug.Log("Unpause BGM");
            FindObjectOfType<AudioManager>().UnPauseSound("BGM");
        }

        if (this.tag == "Stop")
        {
            Debug.Log("Stop BGM");
            FindObjectOfType<AudioManager>().StopSound("BGM");
        }

        #endregion

        #region Audio Mixer Functions

        switch (this.tag)
        {
            case "AM Test 1":
                snapshot.TransitionTo(.01f);
                break;

            case "AM Test 2":
                snapshot.TransitionTo(.01f);
                break;

            case "AM Test 3":
                snapshot.TransitionTo(.01f);
                break;

            case "AM Test 4":
                snapshot.TransitionTo(.01f);
                break;

            case "AM Test 5":
                snapshot.TransitionTo(2f);
                break;

            case "AM Test 6":
                audioMixer.ClearFloat("bgmVol");
                break;
        }

        #endregion
    }
}
