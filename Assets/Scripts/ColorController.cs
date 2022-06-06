using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorController : MonoBehaviour
{
    public Image foreGround;
    public Image backGround;

    Color foreGroundColor;
    Color backGroundColor;

    private void OnEnable()
    {
        foreGroundColor = Color.black;
        backGroundColor = Color.white;
        RefreshColorView();
    }

    public void SetColor(Color imageColor, bool isForeGround)
    {
        if(isForeGround)
        {
            foreGroundColor = imageColor;
        }
        else
        {
            backGroundColor = imageColor;
        }

        RefreshColorView();
    }
    public void RefreshColorView()
    {
        foreGround.color = foreGroundColor;
        backGround.color = backGroundColor;
    }

    public void SwapColors()
    {
        Color tempColor = foreGroundColor;

        foreGroundColor = backGroundColor;
        backGroundColor = tempColor;

        RefreshColorView();
    }
}
