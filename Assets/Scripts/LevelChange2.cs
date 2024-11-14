using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelChange2 : MonoBehaviour
{
    public bool isNextScene = true;

    [SerializeField]
    public SceneInfo sceneinfo;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("LiamsScene");
        }
    }
}
