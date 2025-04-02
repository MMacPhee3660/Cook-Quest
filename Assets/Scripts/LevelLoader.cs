using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour{


    public GameObject player;
    public int X;
    public int Y;
    public int Z;
    public GameObject spawnPoint;
    public void Start(){
        
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint");
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint.transform.position = new Vector3 (X,Y,Z);
    }
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
