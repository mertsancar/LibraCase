using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    private int x;
    private int y;
    public bool IsSelected;

    public GameObject selectedText;

    public void Prepare(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


    public void ClickTile()
    {
        selectedText.SetActive(true);
        IsSelected = true;
        TryMatch(new List<Tile>{this});
    }

    private void TryMatch(List<Tile> matchList)
    {
        var currentTile = matchList.Last();

        foreach (var t in TileManager.instance.adjacentTiles)
        {
            foreach (var offset in t)
            {
                if (matchList.Count >= 3)
                    break;

                var x = currentTile.x + offset.x;
                var y = currentTile.y + offset.y;

                if (TryAddMatchList(matchList, x, y))
                {
                    if (matchList.Count >= 3)
                        break;
                }
            }

            if (matchList.Count < 3)
            {
                matchList.Clear();
                matchList.Add(currentTile);
            }
        }

        if (matchList.Count >= 3)
        {
            StartCoroutine(Match(matchList));
        }

    }
    
    private bool TryAddMatchList(List<Tile> matchList, int x, int y)
    {
        var grid = GameController.instance.grid;
        if (IsValidCell(x, y) && grid[y, x].IsSelected)
        {
            matchList.Add(grid[y, x]);
            return true;
        }
        return false;
    }

    private bool IsValidCell(int x, int y)
    {
        var gridSize = GameController.instance.gridSize;
        return x >= 0 && x < gridSize && y >= 0 && y < gridSize;
    }
    
    private IEnumerator Match(List<Tile> matchList)
    {
        TileManager.instance.MatchStart();
        foreach (var tile in matchList)
        {
            tile.IsSelected = false;
            yield return new WaitForSeconds(0.1f);
            tile.selectedText.SetActive(false);
        }
        TileManager.instance.MatchEnd();
    }

}