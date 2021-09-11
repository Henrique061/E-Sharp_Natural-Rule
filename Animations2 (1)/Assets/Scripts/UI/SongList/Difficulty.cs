using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Difficulty : MonoBehaviour
{
    [SerializeField] Button easy;
    [SerializeField] TMP_Text easyText;
    [SerializeField] Button hard;
    [SerializeField] TMP_Text hardText;

    Color easyColor, hardColor;


    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        easyColor = easy.colors.normalColor;
        hardColor = hard.colors.normalColor;

        var TEcolors = easy.colors;
        TEcolors.normalColor = new Color(easyColor.r, easyColor.g, easyColor.b, 0);
        var THcolors = hard.colors;
        THcolors.normalColor = new Color(hardColor.r, hardColor.g, hardColor.b, 0);

        var OEcolors = easy.colors;
        OEcolors.normalColor = new Color(easyColor.r, easyColor.g, easyColor.b, 1);

        var OHcolors = hard.colors;
        OHcolors.normalColor = new Color(hardColor.r, hardColor.g, hardColor.b, 1);

        if (StaticStore.difficulty.Equals("easy"))
        {
            hard.colors = THcolors;
            hardText.fontStyle = FontStyles.Normal;

            easy.colors = OEcolors;
            easyText.fontStyle = FontStyles.Bold;
        }
        if (StaticStore.difficulty.Equals("hard"))
        {
            hard.colors = OHcolors;
            hardText.fontStyle = FontStyles.Bold;

            easy.colors = TEcolors;
            easyText.fontStyle = FontStyles.Normal;
        }
    }

    public void ChangeDifficulty(string difficulty) {
        difficulty.ToLower();

        StaticStore.difficulty = difficulty;

        var TEcolors = easy.colors;
        TEcolors.normalColor = new Color(easyColor.r, easyColor.g, easyColor.b, 0);

        var THcolors = hard.colors;
        THcolors.normalColor = new Color(hardColor.r, hardColor.g, hardColor.b, 0);

        var OEcolors = easy.colors;
        OEcolors.normalColor = new Color(easyColor.r, easyColor.g, easyColor.b, 1);

        var OHcolors = hard.colors;
        OHcolors.normalColor = new Color(hardColor.r, hardColor.g, hardColor.b, 1);

        if (difficulty.Equals("easy")) 
        {
            hard.colors = THcolors;
            hardText.fontStyle = FontStyles.Normal;

            easy.colors = OEcolors;
            easyText.fontStyle = FontStyles.Bold;
        }
        if (difficulty.Equals("hard"))
        {
            hard.colors = OHcolors;
            hardText.fontStyle = FontStyles.Bold;

            easy.colors = TEcolors;
            easyText.fontStyle = FontStyles.Normal;
        }
    }
}
