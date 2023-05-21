using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    
    private List<Vector2Int> downLeft = new List<Vector2Int>
    {
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, 1),
    };
    
    private List<Vector2Int> upLeft = new List<Vector2Int>
    {
        new Vector2Int(-1, 0),
        new Vector2Int(-1, -1),
        new Vector2Int(0, -1),
    };
    
    private List<Vector2Int> upRight = new List<Vector2Int>
    {
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
    };
    
    private List<Vector2Int> downRight = new List<Vector2Int>
    {
        new Vector2Int(1, 0),
        new Vector2Int(1, 1),
        new Vector2Int(0, 1)
    };

    public List<List<Vector2Int>> adjacentTiles;
    
    private void Start()
    {
        adjacentTiles = new List<List<Vector2Int>>
        {
            downLeft,
            upLeft,
            upRight,
            downRight
        };
        
        if (instance == null)
        {
            instance = this;
        }
    }

    public void MatchStart()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var tileButton = transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
    
    public void MatchEnd()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var tileButton = transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    
}
