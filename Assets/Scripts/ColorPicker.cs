using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPicker : MonoBehaviour, IPointerDownHandler
{
    private ColorController colorController;
    [SerializeField]
    private Image colorImage;
    private ProjectController projectController;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            colorController.SetColor(colorImage.color, true);
            projectController.isPickerActive = false;
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            colorController.SetColor(colorImage.color, false);
            projectController.isPickerActive = false;
        }
    }
    private void Awake()
    {
        colorController = FindObjectOfType<ColorController>(); 
        colorImage = GetComponent<Image>();
        projectController = FindObjectOfType<ProjectController>();
    } 
    public void SetPickerColor(Color color)
    {
        colorImage.color = color;
    }
}