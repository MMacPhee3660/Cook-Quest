using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHistory : MonoBehaviour
{
    private List<string> sceneHistory = new List<string>();
    private Scene previous;
    public GameObject player;
    public GameObject genstorespawn;
    private Vector3 mover;
    public LevelLoader Loader;
    //Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update(){
        Loader.LoadNext();
        sceneHistory.Add(SceneManager.GetActiveScene().name);

        if((sceneHistory.Count - 1) + "" == "MaywensScene"){
            player.transform.position = Vector3.MoveTowards(player.transform.position, genstorespawn.transform.position, 100f);
        }
    }
    // public void LoadScene(string newScene)
    // {
    //     sceneHistory.Add(newScene);
    //     SceneManager.LoadScene(newScene);
    // }
    // public bool PreviousScene()
    // {
    //     bool returnValue = false;
    //     if (sceneHistory.Count >= 2)  //Checking that we have actually switched scenes enough to go back to a previous scene
    //     {
    //         returnValue = true;
    //         sceneHistory.RemoveAt(sceneHistory.Count -1);
    //         SceneManager.LoadScene(sceneHistory[sceneHistory.Count -1]);
    //     }

    //     return returnValue;
    // }

}