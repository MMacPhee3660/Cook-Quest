using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTransition : MonoBehaviour
{
    private bool squareChosen = false;
    private bool circleChosen = false;
    private bool triangleChosen = false;

    public GameObject squareChar;
    public GameObject circleChar;
    public GameObject triangleChar;

    public static CharacterController Instance;

    public void SquareSpawner()
    {
        squareChosen = true;
    }
    public void CirclerSpawner()
    {
        circleChosen = true;
    }
    public void TriangleSpawner()
    {
        triangleChosen = true;
    }

    public void StartGame()
    {
        if(squareChosen == true)
        {
            SpawnPlayer(squareChar);
        }
        else if(circleChosen == true)
        {
            SpawnPlayer(circleChar);
        }
        else if(triangleChosen == true)
        {
            SpawnPlayer(triangleChar);
        }
    }

    private void SpawnPlayer(GameObject playerPrefab)
    {
        GameObject playerInstance = Instantiate(playerPrefab);
        DontDestroyOnLoad(playerInstance);
        SceneManager.LoadScene("MainScene");
        playerInstance.transform.position = Vector2.zero;
    }
}
