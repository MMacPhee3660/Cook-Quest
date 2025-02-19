using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCPathing : MonoBehaviour
{
    private int gridHeight = 200;
    private int gridWidth = 200;
    private float cellHeight = 1f;
    private float cellWidth = 1f;

    private bool generatePath;
    private bool visualizeGrid;
    
    private bool pathGenerated;

    private Dictionary<Vector2, Cell> cells;

    private List<Vector2> cellsToSearch;
    private List<Vector2> searchedCells;
    private List<Vector2> finalPath;

    GameObject[] obstacles;
    List<Vector2> avoid = new List<Vector2>();
    List<Vector2> dests;
    Rigidbody rb;
    [SerializeField] GameObject target;
    Vector2 destination;
    Vector3 myPos;
    Vector3 targetPos;
    [SerializeField] float speed = 1f;
    int destIndex;
    Vector3 currentDest;
    bool destinationReached = false;
    
    void Start()
    {
        // Runs once at compilation
        // Makes a list of every object tagged as an obstacle and finds their hitbox
        // Adds every coordinate within the hitboxes to a list of coordinates to avoid for pathfinding
        // Also finds the target's position and makes the destination equal to it

        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            BoxCollider hitbox = obstacle.GetComponent<BoxCollider>();
            Vector2 center = new Vector2(obstacle.transform.position.x + hitbox.center.x, obstacle.transform.position.z + hitbox.center.z);
            int x = (int)(hitbox.size.x + 2);
            int y = (int)(hitbox.size.z + 2);

            for (int i = (int)(-x/2); i <= (int)(x/2); i++)
            {
                for (int j = (int)(-y/2); j <= (int)(y/2); j++)
                {
                    avoid.Add(new Vector2(i,j));
                    print( new Vector2(i,j));
                }
            }
        }
        GenerateGrid();
        rb = this.GetComponent<Rigidbody>();
        destination = new Vector2((int)(target.transform.position.x + 0.5f),(int)(target.transform.position.z + 0.5f));
        targetPos = target.transform.position;
    }
    void FixedUpdate()
    {
        // Runs every frame
        // Determines whether the current destination is different from the target position
        // i.e. did the target move?
        // If so, makes the destination equal to the target's position (rounded) and declares that a path has not been generated
        // Calls PathLoop method

        myPos = transform.position;

        if (Vector3.Distance(targetPos,target.transform.position) >= 0.5)
        {
            targetPos = target.transform.position;
            destination = new Vector2((int)(targetPos.x + 0.5f),(int)(targetPos.z + 0.5f));
            pathGenerated = false;
            destinationReached = false;
        }

        if (!destinationReached)
        {
            PathLoop(destination);
        }
        
    }

    private Vector2 RoundToVector2(Vector2 subject, Vector2 target, float step)
    {
        // Rounds a given vector in the direction of another vector
        // Meant to work with a variable step but only rounds to nearest int

        print("rounding");
        Vector2 direction = target - subject;
        if (direction.x != 0)
        {
            direction.x = direction.x / Mathf.Abs(direction.x);
        }
        if (direction.y != 0)
        {
            direction.y = direction.y / Mathf.Abs(direction.y);
        }

        direction *= step;
        Vector2 output = new Vector2((int)(subject.x + direction.x),(int)(subject.y+direction.y));
        return output;
        
    }
    public void PathLoop(Vector2 destination)
    {
        // If a path has not been generated:
        // Rounds the current position in the direction of the destination
        // Calls the pathfind method using the rounded position and the destination
        // If the pathfind method returns an actual path, moves towards the next destination in the final path
        // If within 1 of the point, advances to the next point in the path unless it was the destination, in which case it stops

        if (pathGenerated)
        {
            if (Vector3.Distance(currentDest, myPos) <= 0.1 && !destinationReached)
            {
                if(destIndex > 0)
                {
                    destIndex--;
                    currentDest = new Vector3(finalPath[destIndex].x, 0, finalPath[destIndex].y);
                    rb.velocity = (currentDest - myPos).normalized * speed;
                }
                else
                {
                    rb.velocity = new Vector3(0,0,0);
                    destinationReached = true;
                }
            }
        }
        else
        {
            Vector2 roundedPos = RoundToVector2(new Vector2(myPos.x,myPos.z),destination,cellHeight);
            if (roundedPos != destination)
            {
                FindPath(roundedPos, destination);
            }
            pathGenerated = true;
            if (finalPath.Count > 1)
            {
                destIndex = finalPath.Count-1;
                currentDest = new Vector3(finalPath[destIndex].x, 0, finalPath[destIndex].y);
                rb.velocity = (currentDest - myPos).normalized * speed;
            }
            else{
                destIndex = 0;
            }
        }
        print("pathing " + destIndex);
        
    }
    private void GenerateGrid()
    {
        // Nested for loop running through a grid with gridWidth and gridHeight
        // Creates a cell for each position in the grid
        // Marks the cell as an obstacle if it is in the list of obstacle positions

        cells = new Dictionary<Vector2, Cell>();

        for (int x = gridWidth / -2; x < gridWidth / 2; x += 1)
        {
            for (float y = gridHeight / -2; y < gridHeight / 2; y += 1)
            {
                Vector2 pos = new Vector2(x, y);
                cells.Add(pos, new Cell(pos));
            }
        }
        foreach (Vector2 wall in avoid)
        {
            cells[wall].isWall = true;
        }
        print("generating");
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        // A* pathfinding system creates a list of points to follow to reach a destination
        searchedCells = new List<Vector2>(); // new list that will contain searched cells
        cellsToSearch = new List<Vector2> {startPos}; // new list that will contain the cells that will need to be searched
        finalPath = new List<Vector2>(); // resets the final path list
        Cell startCell = cells[startPos]; // creates a cell that will be the starting position to path from
        startCell.gCost = 0; // g cost is distance from cell to current position
        startCell.hCost = GetDistance(startPos, endPos); // h cost is distance from cell to end
        startCell.fCost = GetDistance(startPos, endPos); // f cost is g cost plus h cost
        while (cellsToSearch.Count > 0) // while loop runs until the cells to search list is empty
        {
            print(cellsToSearch.Count);
            Vector2 cellToSearch = cellsToSearch[0]; // picks the first cell in the cellstosearch list to be compared to
            foreach (Vector2 pos in cellsToSearch) // goes through every position in the cellstosearch list
            {
                Cell c = cells[pos]; // finds the cell corresponding to the position
                if (c.fCost <= cells[cellToSearch].fCost && c.hCost == cells[cellToSearch].hCost)
                // if the fcost of the cell is less than or equal to the comparison cell and the hcost is equal to the comparison cell's hcost
                {
                    cellToSearch = pos; // makes the comparison cell equal to the current cell
                }
            }
            // that first bit found the best cell to move to in the cellstosearch list
            
            cellsToSearch.Remove(cellToSearch); // removes the searched cell from the cellstosearch list
            searchedCells.Add(cellToSearch); // adds the removed cell to the searched cells list

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
        print("finding path" + finalPath.Count);
    }

    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos)
    {
        // Used by the FindPath method to check the distance between adjacent cells

        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);

                if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) && (!cells[neighborPos].isWall))
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
        // Used by the SearchCellNeighbors method to identify the distance between two vector2s
        
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    private class Cell
    {
        // Class that stores the cost of a point and whether it is an obstacle

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