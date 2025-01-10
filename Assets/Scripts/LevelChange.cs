using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class LevelChange : MonoBehaviour
{
    public string scene;
    [SerializeField] GameObject E;
    public GameObject testE;
    public GameObject childE;
    private float distance;
    GameObject player;
    public Vector3 mover = new Vector3(0,0,0);
    public Vector3 returnPoint;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("Scene2");
        }
    }
    
    void Start(){
        player = GameObject.Find("PlayerMove");
        E.SetActive(false);
        childE = Instantiate(testE, transform.position,Quaternion.identity);
        childE.transform.position += Vector3.up * 1f;
        childE.transform.position += Vector3.back * .5f;

    }
    void Update(){
         if(Input.GetKeyDown(KeyCode.E) && childE.activeSelf){
            SceneManager.LoadScene(scene);
            Vector3.MoveTowards(mover, returnPoint, 100f);
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= 1.5f){
            E.transform.position = transform.position;
            childE.SetActive(true);
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else{
            childE.SetActive(false);
        }
    }
}
