using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimationField : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public RawImage field;
    private ProjectController projectController;

    public void OnDrop(PointerEventData eventData)
    {
        field.texture = projectController.draggingObjectTexture.texture;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            field.texture = null;
    }

    private void Start()
    {
        projectController = FindObjectOfType<ProjectController>();
    }
}
