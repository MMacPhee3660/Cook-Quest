using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{
    public string sceneName;
    public bool isNextScene = true;

    [SerializeField]
    public SceneInfo sceneinfo;
    void OnTriggerEnter(Collider other)
    {
            sceneinfo.isNextScene = isNextScene;
            SceneManager.LoadScene(sceneName);
    }
}
