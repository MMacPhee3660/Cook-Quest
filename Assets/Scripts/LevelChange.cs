using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 

public class LevelChange : MonoBehaviour
{
   public bool isNextScene = true;

   [SerializeField]
   public SceneInfo sceneInfo;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("Scene2");
            DontDestroyOnLoad(gameObject);
        }
    }
}
