using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    /* *********************** MANUAL ***************************
     * 
     * To use an AudioManager function, you need to create a reference to AudioManager Object,
     * whenever is the script you are creating de sound function. When creating the refference,
     * you need to specify what function you want do play, and in most of cases, what's the
     * array's name of the sound that you want to play (see in AudioManager Object Inspector).
     * 
     * To do that, use this code:
     * FindObjectOfType<AudioManager>().FUNCTION_NAME("ARRAY_NAME");
     * 
     * 
     * Here's the example for play the jump sound:
     * Every time the player jumps, this code also appears:
     * 
     * FindObjectOfType<AudioManager>().PlaySound("PlayerJump");
     * (Where "PlayerJump" is the array's name of the sound of jumping, see the AudioManager's Inspector)
     * 
     * Another example:
     * When pausing the game, I want to pause all sounds, and play only the sfx of pausing the game.
     * So, every time I pause the game, this code appears:
     * 
     * FindObjectOfType<AudioManager>().PlayOnly("PauseGame");
     * 
     * And when I resume the game, I want do unPause all sounds,
     * so, when i resume the game...
     * 
     * FindObjectOfType<AudioManager>().UnPauseAll();
     * 
     * ****************** ON GAME OBJECT INSPECTOR *************************
     * 
     * There some parameters in AudioManager's GameObject Inspector.
     * If you want to add a sound, just increase the number of arrays, write the name of the array(s) you've created,
     * drag the sound clip to the "Clip" attribute, and adjust the other things (volume, pitch and loop), also, 
     * drag the Audio Mixer Group on it's respective attribute and you good to go!
     * 
     * ****************** ALL FUNCTIONS **********************
     * 
     * PlaySound(ArrayName)    - To play a specific sound.
     * PlayOnly(ArrayName)     - To play a specific sound, pausing the others that was playing.
     * PauseAll()              - To Pause all sounds.
     * UnPauseAll()            - To UnPause all sounds that was been paused.
     * PauseSound(ArrayName)   - To pause a specific sound.
     * UnPauseSound(ArrayName) - To UnPause a specific sound.
     * StopSound(ArrayName)    - To stop a specific sound.
     * StopAll()               - To stop all sounds.
     * 
     * ArrayName = The name of the array sound (on AudioManager Inspector) that you will pass by parameter.
     * 
     * Code: FindObjectOfType<AudioManager>().FUNCTION_NAME("ARRAY_NAME");
     * 
     * *******************************************************
     * 
     * There it is! If you have some question, ask to Pajé.
    */

    #region Variables

    public Sound[] sounds;

    public static AudioManager instance;

    [SerializeField] AudioMixerSnapshot snapshot;

    [SerializeField] bool phase;

    #endregion

    // AWAKE ////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        if (instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
            return;
        }
        // this if and else statements is to not duplicate the AudioManager gameObject when transitioning through scenes (if the scenes have audio manager)

        //DontDestroyOnLoad(gameObject); // to not destroy the game Object when transitioning through scenes (in case of using the same song in different scenes)

        foreach (Sound s in sounds) // to loop through the list of sounds, an add an AudioSource for each (when open the game)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // storing the AudioSource in the variable "source"

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    private void Start()
    {
        if(snapshot != null)
            snapshot.TransitionTo(0.01f);
    }

    #region Sound Manipulation Functions

    // PLAY SOUND ///////////////////////////////////////////////////////////////////////
    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // find the sound of the correct name to play
        // the parameters: (the array, who to find -> in this case, its the sound where the sound name it's equals to the name)

        if (s == null) // just to prevent to not let test the game in case of something writed wrong
        {
            Debug.LogWarning("SOUND NOT FOUND! PROBABLY WRONG WRITED IN INSPECTOR OF AUDIOMANAGER OR THE FILE ITSELF!");
            return;
        }

        s.source.Play();
    }

    // PLAY ONLY ////////////////////////////////////////////////////////////////////////
    public void PlayOnly (string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name != _name)
            {
                Sound s = sounds[i];
                s.source.Pause();
            }

            else
            {
                Sound s = sounds[i];
                s.source.Play();
            }
        }
    }

    // PAUSE ALL ////////////////////////////////////////////////////////////////////////
    public void PauseAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            Sound s = sounds[i];
            s.source.Pause();
        }
    }

    // UNPAUSE ALL //////////////////////////////////////////////////////////////////////
    public void UnPauseAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            Sound s = sounds[i];
            s.source.UnPause();
        }
    }

    // PAUSE SOUND //////////////////////////////////////////////////////////////////////
    public void PauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // find the sound of the correct name to play
        // the parameters: (the array, who to find -> in this case, its the sound where the sound name it's equals to the name)

        if (s == null) // just to prevent to not let test the game in case of something writed wrong
        {
            Debug.LogWarning("SOUND NOT FOUND! PROBABLY WRONG WRITED IN INSPECTOR OF AUDIOMANAGER OR THE FILE ITSELF!");
            return;
        }

        s.source.Pause();
    }

    // UNPAUSE SOUND ////////////////////////////////////////////////////////////////////
    public void UnPauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // find the sound of the correct name to play
        // the parameters: (the array, who to find -> in this case, its the sound where the sound name it's equals to the name)

        if (s == null) // just to prevent to not let test the game in case of something writed wrong
        {
            Debug.LogWarning("SOUND NOT FOUND! PROBABLY WRONG WRITED IN INSPECTOR OF AUDIOMANAGER OR THE FILE ITSELF!");
            return;
        }

        s.source.UnPause();
    }

    // STOP SOUND ///////////////////////////////////////////////////////////////////////
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // find the sound of the correct name to play
        // the parameters: (the array, who to find -> in this case, its the sound where the sound name it's equals to the name)

        if (s == null) // just to prevent to not let test the game in case of something writed wrong
        {
            Debug.LogWarning("SOUND NOT FOUND! PROBABLY WRONG WRITED IN INSPECTOR OF AUDIOMANAGER OR THE FILE ITSELF!");
            return;
        }

        s.source.Stop();
    }

    // STOP ALL /////////////////////////////////////////////////////////////////////////
    public void StopAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            Sound s = sounds[i];
            s.source.Stop();
        }
    }

    #endregion
}
