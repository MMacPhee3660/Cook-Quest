using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 

public class LevelChange : MonoBehaviour
{
    public string sceneName;
   public bool isNextScene = true;

   [SerializeField]
   public SceneInfo sceneInfo;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            sceneInfo.isNextScene = isNextScene;
            SceneManager.LoadScene(sceneName);
        }
    }
}
