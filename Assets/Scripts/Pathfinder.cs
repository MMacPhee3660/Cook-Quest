using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Pathfinder : MonoBehaviour
{
    private int gridHeight = 201;
    private int gridWidth = 201;
    private float cellHeight = 1f;
    private float cellWidth = 1f;

    private bool generatePath;
    private bool visualizeGrid;
    
    private bool pathGenerated;

    private Dictionary<Vector2, Cell> cells;

    private List<Vector2> cellsToSearch;
    private List<Vector2> searchedCells;
    private List<Vector2> finalPath;
    
    /*
    private void Update()
    {
        if (generatePath && !pathGenerated)
        {
            GenerateGrid();
            RandomObstacles();
            FindPath(new Vector2(0,0), new Vector2(gridWidth-1,gridHeight-1));
            pathGenerated = true;
        }
        else if (!generatePath)
        {
            pathGenerated = false;
        }
    }
    */

    public Pathfinder()
    {
        generatePath = true;
        visualizeGrid = false;
    }

    public List<Vector2> Pathfind(Vector2 startPos, Vector2 endPos)
    {
        GenerateGrid();
        FindPath(startPos,endPos);
        return finalPath;
    }
    private void GenerateGrid()
    {
        cells = new Dictionary<Vector2, Cell>();

        for (float x = 0; x < gridWidth; x += cellWidth)
        {
            for (float y = 0; y < gridHeight; y += cellHeight)
            {
                Vector2 pos = new Vector2(x,y);
                cells.Add(pos, new Cell(pos));
            }
        }
    }
    private void RandomObstacles()
    {
        for (int i = 0; i < 40; i++)
        {
            Vector2 pos = new Vector2(Random.Range(0, gridWidth), Random.Range(0, gridHeight));
            cells[pos].isWall = true;
        }
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        searchedCells = new List<Vector2>();
        cellsToSearch = new List<Vector2> {startPos};
        finalPath = new List<Vector2>();
        Cell startCell = cells[startPos];
        startCell.gCost = 0;
        startCell.hCost = GetDistance(startPos, endPos);
        startCell.fCost = GetDistance(startPos, endPos);

        while (cellsToSearch.Count > 0)
        {
            Vector2 cellToSearch = cellsToSearch[0];
            foreach (Vector2 pos in cellsToSearch)
            {
                Cell c = cells[pos];
                if (c.fCost < cells[cellToSearch].fCost || c.fCost == cells[cellToSearch].fCost && c.hCost == cells[cellToSearch].hCost)
                {
                    cellToSearch = pos;
                }
            }
            
            cellsToSearch.Remove(cellToSearch);
            searchedCells.Add(cellToSearch);

            if (cellToSearch == endPos)
            {

                Cell pathCell = cells[endPos];

                while (pathCell.position != startPos)
                {
                    finalPath.Add(pathCell.position);
                    pathCell = cells[pathCell.connection];
                }

                finalPath.Add(startPos);
                return;
            }

            SearchCellNeighbors(cellToSearch,endPos);
        }
    }

    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos)
    {
        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);

                if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) && !cells[neighborPos].isWall)
                {
                    int GcostToNeighbor = cells[cellPos].gCost + GetDistance(cellPos, neighborPos);

                    if (GcostToNeighbor < cells[neighborPos].gCost)
                    {
                        Cell neighborNode = cells[neighborPos];

                        neighborNode.connection = cellPos;
                        neighborNode.gCost = GcostToNeighbor;
                        neighborNode.hCost = GetDistance(neighborPos, endPos);
                        neighborNode.fCost = neighborNode.gCost + neighborNode.hCost;

                        if (!cellsToSearch.Contains(neighborPos))
                        {
                            cellsToSearch.Add(neighborPos);
                        }
                    }
                }
            }
        }
    }

    private int GetDistance(Vector2 pos1, Vector2 pos2)
    {
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    private void OnDrawGizmos()
    {
        if(!visualizeGrid || cells == null)
        {
            return;
        }
        foreach (KeyValuePair<Vector2, Cell> kvp in cells)
        {
            if (!kvp.Value.isWall)
            {
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.black;
            }

            if (finalPath.Contains(kvp.Key))
            {
                Gizmos.color = Color.magenta;
            }

            //Gizmos.DrawCube(kvp.Key + (Vector2)transform.position, new Vector3(cellWidth,cellHeight));
        }
    }

    private class Cell
    {
        public Vector2 position;
        public int fCost = int.MaxValue;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Vector2 connection;
        public bool isWall;

        public Cell(Vector2 pos)
        {
            position = pos;
        }
    }
}