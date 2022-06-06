using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Text redValue;
    public Text greenValue;
    public Text blueValue;

    private float r;
    private float g;
    private float b;

    public ColorPicker customColorPicker;

    void OnEnable()
    {
        redSlider.onValueChanged.AddListener(ChangeRedValue);
        greenSlider.onValueChanged.AddListener(ChangeGreenValue);
        blueSlider.onValueChanged.AddListener(ChangeBlueValue);
        RefreshColorSelector();
    }
    public void ChangeRedValue(float value)
    {
        r = value;
        redValue.text = r.ToString();
        SetPreviewColor();
    }
    public void ChangeGreenValue(float value)
    {
        g = value;
        greenValue.text = g.ToString();
        SetPreviewColor();
    }
    public void ChangeBlueValue(float value)
    {
        b = value;
        blueValue.text = b.ToString();
        SetPreviewColor();
    }
    public void SetPreviewColor()
    {
        Color tempColor = new Color(r / 255, g / 255, b / 255);
        customColorPicker.SetPickerColor(tempColor);
    }
    public void RefreshColorSelector()
    {
        ChangeRedValue(255);
        ChangeGreenValue(255);
        ChangeBlueValue(255);

        redSlider.value = redSlider.maxValue;
        greenSlider.value = greenSlider.maxValue;
        blueSlider.value = blueSlider.maxValue;

        redValue.text = "255";
        greenValue.text = "255";
        blueValue.text = "255";
    }
}
