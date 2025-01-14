using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour{


    public Animator transition;
    public float animTime = 1f;
    public int scene;
    GameObject spawnpoint;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            LoadNext();
        }
    }

    public void LoadNext(){
        StartCoroutine(LoadScene(scene));
    }


    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(animTime);
        SceneManager.LoadScene(levelIndex);
    }
}
