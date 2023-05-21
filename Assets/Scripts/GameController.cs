using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    [NonSerialized]  public Tile[,] grid;
    [SerializeField] public int gridSize;
    [SerializeField] private Transform gridLayout;

    [SerializeField] private GameObject tilePrefab;

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

    public void OnClickChangeGridSize()
    {
        PlayerPrefs.SetInt("gridSize", Random.Range(3, 20));
        SceneManager.LoadScene("SampleScene");
    }
    
    public void OnClickSetDefaultGridSize()
    {
        PlayerPrefs.SetInt("gridSize",5);
        SceneManager.LoadScene("SampleScene");
    }
}
