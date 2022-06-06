using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pixel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public Color pixelColor = Color.white;
    private Image pixelImage;
    private ColorController colorController;
    private ProjectController projectController;
    public GameObject gridSprite;

    private void Start()
    {
        pixelImage = GetComponent<Image>();
        colorController = FindObjectOfType<ColorController>();
        projectController = FindObjectOfType<ProjectController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            projectController.isLeftInputPressed = true;

            if(projectController.isPickerActive)
            {
                colorController.SetColor(pixelColor, true);
                projectController.SetPickerStatus();
                return;
            }
            SetPixelColor(true);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            projectController.isRightInputPressed = true;

            if (projectController.isPickerActive)
            {
                colorController.SetColor(pixelColor, false);
                projectController.SetPickerStatus();
                return;
            }
            SetPixelColor(false);
        }
    }
    private void SetPixelColor(bool isLeftButton)
    {
        if(isLeftButton)
        {
            pixelColor = colorController.foreGround.color;
            RefreshPixelColor();
        }
        else
        {
            pixelColor = colorController.backGround.color;
            RefreshPixelColor();
        }
    }
    public void SetPixelColor(Color color)
    {
        pixelColor = color;
        RefreshPixelColor();
    }
    private void RefreshPixelColor()
    {
        if(pixelImage == null )
        {
            pixelImage = GetComponent<Image>();
        }

        pixelImage.color = pixelColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            projectController.isLeftInputPressed = false;
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            projectController.isRightInputPressed = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (projectController.isLeftInputPressed)
        {
            SetPixelColor(true);
        }
        if (projectController.isRightInputPressed)
        {
            SetPixelColor(false);
        }
    }
}
