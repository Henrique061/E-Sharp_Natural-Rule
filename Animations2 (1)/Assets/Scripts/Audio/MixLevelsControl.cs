using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevelsControl : MonoBehaviour
{
    public AudioMixer masterMixer;
    [SerializeField] ChangeMusic change;
    float valorMaster = 0f;

    private void Awake()
    {
        masterMixer.SetFloat("masterVol",PlayerPrefs.GetFloat("masterVol"));
        masterMixer.SetFloat("bgmVol", PlayerPrefs.GetFloat("bgmVol"));
        masterMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
    }

    private void Start()
    {
        valorMaster = GetMasterLevel();
    }

    private void Update()//(float masterLvl)
    {
        /*if (Input.GetKeyDown("O"))
        {
            Debug.Log("O: "+ valorMaster); //); // just a little and showy debug

            valorMaster = SetMasterLvlKeyDown(valorMaster-10f);
        }

        else if (Input.GetKeyDown("P"))
        {
            Debug.Log("P: "+ valorMaster);// + masterLvl); // just a little and showy debug
            valorMaster = SetMasterLvlKeyUp(valorMaster+10f);
        }*/
    }


    #region SetFloat

    public void SetMasterLvl(float masterLvl)
    {
        masterMixer.SetFloat("masterVol", masterLvl);

        PlayerPrefs.SetFloat("masterVol", masterLvl);
        PlayerPrefs.Save();

    }

    public void SetBgmLvl(float bgmLvl)
    {
        masterMixer.SetFloat("bgmVol", bgmLvl);

        PlayerPrefs.SetFloat("bgmVol", bgmLvl);
        PlayerPrefs.Save();
    }

    public void SetSfxLvl(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);

        PlayerPrefs.SetFloat("sfxVol", sfxLvl);
        PlayerPrefs.Save();
    }
    #endregion



    // Keyboard volume changer: (+) and (-):

    //   (+)
    public float SetMasterLvlKeyUp(float masterLvl)
    {
        if (masterLvl <= 1f)
        {
            masterMixer.SetFloat("masterVol", (masterLvl + .5f));
        }
        Debug.Log("master level: " + masterLvl + .5f);
        Debug.Log("master level: " + masterLvl);
        Debug.Log("valor masTEEER: " + valorMaster);
        return valorMaster;
    }

    //   (-)
    public float SetMasterLvlKeyDown(float masterLvl)
    {

        masterMixer.SetFloat("masterVol", (masterLvl-.5f));
        
        Debug.Log("master level: " + masterLvl);
        Debug.Log("master level: " + masterLvl);
        Debug.Log("valor masTEEER: " + valorMaster);
        return valorMaster;
    }



    public void ClearVolume()
    {
        masterMixer.ClearFloat("sfxVol");
    }


    // Obtains the float vol master value
    public float GetMasterLevel(){
         float value;
         bool result =  masterMixer.GetFloat("masterVol", out value);
         if(result){
             return value;
         }else{
             return -60f;
         }
     }


}
