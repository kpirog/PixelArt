using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpriteSheet : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int id;
    [HideInInspector]
    public List<Pixel> pixels;
    ProjectController projectController;
    [HideInInspector]
    public RawImage spriteSheetIcon;
    public GameObject border;
    void Start()
    {
        projectController = FindObjectOfType<ProjectController>();
        spriteSheetIcon = GetComponent<RawImage>();
    }

    void Update()
    {
        
    }
    public void Load()
    {
        //zaladowanie konkretnego arkusza z poziomu ProjectControllera
        projectController.SetCurrentGrid(id);
        projectController.LoadSpriteSheet(id);
    }
    public void GenerateTextureByGrid()
    {
        Texture2D texture = new Texture2D(projectController.gridSize, projectController.gridSize);
        int tempListIndex = 0;

        for (int i = 0; i < projectController.gridSize; i++)
        {
            for (int j = 0; j < projectController.gridSize; j++)
            {
                texture.SetPixel(j, i, pixels[tempListIndex].pixelColor);
                tempListIndex++;
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();

        spriteSheetIcon.texture = texture;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(spriteSheetIcon.texture == null)
            GenerateTextureByGrid();

        projectController.draggingObjectTexture.texture = spriteSheetIcon.texture;
        projectController.draggingObject.SetActive(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        projectController.draggingObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        projectController.draggingObject.transform.position = eventData.position;
    }
}
