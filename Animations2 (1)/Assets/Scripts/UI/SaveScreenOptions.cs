using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScreenOptions : MonoBehaviour
{
    #region unused content
    /*
        public GameObject buttons;
        bool slotSelected = false;
        public GameObject[] slots;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {

                if (slotSelected)
                {
                    buttons.SetActive(true);
                    slotSelected = false;
                }

                else if (!slotSelected)
                {
                    buttons.SetActive(false);
                }
            }
        }
    */
    #endregion

    [SerializeField] GameObject saveOrLoad;
    [SerializeField] GameObject emptyArea;


    public void ShowSaveLoad()
    {
        saveOrLoad.SetActive(true);
        emptyArea.SetActive(true);
    }

    public void EmptyAreaClick()
    {
        saveOrLoad.SetActive(false);
        emptyArea.SetActive(false);
    }
}
