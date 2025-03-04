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
    GameObject testE;
    GameObject childE;
    private float distance;
    GameObject player;
    public float x;
    public float y;
    public float z;
    public GameObject spawnPoint;
    public PlayerStateManager state;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("Scene2");
        }
    }

    void Awake(){
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint");
    }
    
    void Start(){
        testE = E;
        childE = E;
        player = GameObject.Find("PlayerMove");
        state = player.GetComponent<PlayerStateManager>();
        E.SetActive(false);
        childE = Instantiate(testE, transform.position,Quaternion.identity);
        childE.transform.position += Vector3.up * 1f;
        childE.transform.position += Vector3.back * .5f;

    }
    void Update(){
         if(Input.GetKeyDown(KeyCode.E) && childE.activeSelf){
            Debug.Log("pressed");
            spawnPoint.transform.position = new Vector3 (x,y,z);
            Debug.Log("pos set");
            StartCoroutine(LoadScene(scene));
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
    public Animator transition;
    public float animTime = 1f;
    IEnumerator LoadScene(string levelName){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(animTime);
        SceneManager.LoadScene(levelName);
    }
}
