using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour{


    
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

    public Animator transition;
        public float animTime = 1f;
        public String scene;
    IEnumerator LoadScene(string levelName){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(animTime);
        SceneManager.LoadScene(levelName);
    }
}
