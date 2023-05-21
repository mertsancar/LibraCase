using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public Tile[,] grid;
    public int gridSize;
    public Transform gridLayout;

    public GameObject tilePrefab;

    public TMP_InputField gridSizeInput;

    public CanvasScaler scaler;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Init();
    }
    

    void Init()
    {
        gridSize = PlayerPrefs.GetInt("gridSize", 5);
        
        SetGridSizeInput(gridSize);
        
        grid = new Tile[gridSize, gridSize];

        var layout = gridLayout.GetComponent<GridLayoutGroup>();
        layout.constraintCount = gridSize;

        var offset = 5f;
        var scrrenSize = Screen.width > Screen.height ? Screen.height : Screen.width; 
        var cellSize = ((scrrenSize - (layout.spacing.x * (gridSize-1))) / gridSize) - offset;
        
        layout.cellSize = new Vector2(cellSize, cellSize);
        
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                var tile = Instantiate(tilePrefab, gridLayout).GetComponent<Tile>();
                tile.Prepare(x, y);
                grid[y, x] = tile;
            }
        }
    }

    private int GetGridSizeInput()
    {
        return gridSizeInput.text == "" ? PlayerPrefs.GetInt("gridSize", 5) : int.Parse(gridSizeInput.text);
    }
    
    private void SetGridSizeInput(int currentSize)
    {
        gridSizeInput.text = currentSize.ToString();
    }
    
    public void OnClickChangeGridSize()
    {
        var newGridSize = GetGridSizeInput();
        PlayerPrefs.SetInt("gridSize", newGridSize);
        SceneManager.LoadScene("SampleScene");
    }
    
    public void OnClickSetDefaultGridSize()
    {
        PlayerPrefs.SetInt("gridSize",5);
        SceneManager.LoadScene("SampleScene");
    }
}
