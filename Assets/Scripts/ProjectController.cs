using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ProjectController : MonoBehaviour
{
    private ProjectGrid currentGrid;
    public GridLayoutGroup gridLayoutGroup;
    public int objectsCount = 64;
    public int gridSize = 8;
    public GameObject pixelPrefab;
    private List<Pixel> pixelList = new List<Pixel>();
    public List<SpriteSheet> spriteSheetList;
    public static int currentGridToSave = 0;

    [HideInInspector]
    public bool isLeftInputPressed = false;
    [HideInInspector]
    public bool isRightInputPressed = false;
    [HideInInspector]
    public bool isPickerActive = false;

    [Header("PopUp")]
    public GameObject savePopUP;
    public InputField inputField;
    public GameObject emptyNameWarning;

    public GameObject draggingObject;
    [HideInInspector]
    public RawImage draggingObjectTexture;

    private void Start()
    {
        savePopUP.SetActive(false);
        draggingObject.SetActive(false);
        draggingObjectTexture = draggingObject.GetComponent<RawImage>();
    }

    public void CreateProjectWithGrid(ProjectGrid grid)
    {
        currentGrid = grid;

        switch (grid)
        {
            default:
            case ProjectGrid.Grid_8x8:
                gridLayoutGroup.cellSize = new Vector2(118f, 118f);
                objectsCount = 64;
                gridSize = 8;
                break;
            case ProjectGrid.Grid_16x16:
                gridLayoutGroup.cellSize = new Vector2(59f, 59f);
                objectsCount = 256;
                gridSize = 16;
                break;
            case ProjectGrid.Grid_32x32:
                gridLayoutGroup.cellSize = new Vector2(29.5f, 29.5f);
                objectsCount = 1024;
                gridSize = 32;
                break;
        }

        for (int i = 0; i < objectsCount; i++)
        {
            GameObject temp = Instantiate(pixelPrefab, gridLayoutGroup.transform);
            pixelList.Add(temp.GetComponent<Pixel>());
        }

        for (int i = 0; i < spriteSheetList.Count; i++)
        {
            for (int j = 0; j < objectsCount; j++)
            {
                GameObject temp = Instantiate(pixelPrefab, spriteSheetList[i].transform);
                spriteSheetList[i].pixels.Add(temp.GetComponent<Pixel>());
            }
        }

        LoadSpriteSheet(0);
    }
    public void ClearPixels()
    {
        foreach (Pixel pixel in pixelList)
        {
            pixel.SetPixelColor(Color.white);
        }
    }
    public void SetPickerStatus()
    {
        isPickerActive = !isPickerActive;
    }
    public void OpenSavePopUp()
    {
        savePopUP.SetActive(true);
    }
    public void CloseSavePopUp()
    {
        inputField.text = "";
        emptyNameWarning.SetActive(false);
        savePopUP.SetActive(false);
    }
    public Texture2D GenerateTextureToSave()
    {
        List<Pixel> tempList = pixelList;
        Texture2D texture = new Texture2D(gridSize, gridSize, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;
        int tempListIndex = 0;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                texture.SetPixel(j, i, tempList[tempListIndex].pixelColor);
                tempListIndex++;
            }
        }
        
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        
        return texture;
    }
    public void SaveTexture(string name, bool saveToPng, Texture2D texture)
    {
        byte[] bytes;
        string path;

        if(saveToPng)
        {
            bytes = texture.EncodeToPNG();
            path = $"{Application.dataPath}/Resources/{name}.png";
        }
        else
        {
            bytes = texture.EncodeToJPG();
            path = $"{Application.dataPath}/Resources/{name}.jpg";
        }
        System.IO.File.WriteAllBytes(path, bytes);
    }
    public void SaveTextureToPng()
    {
        if (GetFileNameStatus())
        {
            Texture2D tempTexture = GenerateTextureToSave();
            tempTexture.filterMode = FilterMode.Point;
            SaveTexture(inputField.text, true, tempTexture);
            emptyNameWarning.SetActive(false);
        }
        else
            emptyNameWarning.SetActive(true);
    }
    public void SaveTextureToJpg()
    {
        if (GetFileNameStatus())
        {
            Texture2D tempTexture = GenerateTextureToSave();
            tempTexture.filterMode = FilterMode.Point;
            SaveTexture(inputField.text, false, tempTexture);
            emptyNameWarning.SetActive(false);
        }
        else
            emptyNameWarning.SetActive(true);
    }
    public bool GetFileNameStatus()
    {
        return inputField.text != string.Empty;
    }
    public void SetGrid()
    {
        foreach (Pixel pixel in pixelList)
        {
            if(!pixel.gridSprite.activeInHierarchy)
                pixel.gridSprite.SetActive(true);
            else
                pixel.gridSprite.SetActive(false);
        }
    }
    public void LoadSpriteSheet(int id)
    {
        foreach (var spriteSheet in spriteSheetList)
        {
            spriteSheet.border.SetActive(spriteSheet.id == id);
        }

        List<Pixel> tempList = spriteSheetList[id].pixels;

        for (int i = 0; i < objectsCount; i++)
        {
            pixelList[i].SetPixelColor(tempList[i].pixelColor);
        }
    }
    public void SaveSpriteSheet()
    {
        List<Pixel> tempList = spriteSheetList[currentGridToSave].pixels;

        for (int i = 0; i < objectsCount; i++)
        {
            tempList[i].SetPixelColor(pixelList[i].pixelColor);
        }

        spriteSheetList[currentGridToSave].GenerateTextureByGrid();
    }
    public void SetCurrentGrid(int id)
    {
        currentGridToSave = id;

        for (int i = 0; i < spriteSheetList.Count; i++)
        {
            
        }
    }
    public void ClearSpriteSheets()
    {
        foreach (var spriteSheet in spriteSheetList)
        {
            spriteSheet.spriteSheetIcon.texture = null;
            
            for (int i = 0; i < spriteSheet.pixels.Count; i++)
            {
                Destroy(spriteSheet.pixels[i].gameObject);
            }
            spriteSheet.pixels.Clear();
        }
    }
    public void ClearPreviousGrid()
    {
        for (int i = 0; i < pixelList.Count; i++)
        {
            Destroy(pixelList[i].gameObject);
        }
        pixelList.Clear();
        currentGridToSave = 0;
    }
}

//utworzenie listy spriteSheetow V
//utworzenie pola typu int dla aktualnego ID spritesheetu
//spritesheet zawiera - liste pixeli, w momencie klikniecia save zapisujemy liste pixeli i ustawiamy obraz tekstury jako tlo przycisku spritesheetu
//metoda Load - w momencie klikniecia w przycisk obraz kliknietego spriteSheetu wyswietla sie na ekranie
