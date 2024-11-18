using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterLevel : MonoBehaviour
{
    public GameObject Entrance;
    public GameObject Exit;

    [SerializeField]
    public SceneInfo sceneInfo;
    public Vector3 offset = new Vector3(1, 0.5f, 0);
    private Rigidbody body;
    private Vector3 offsetEntrance;
    private Vector3 offsetExit;

    void Awake()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        GameObject target = sceneInfo.isNextScene ? Entrance : Exit;
        Vector3 offset = sceneInfo.isNextScene ? offsetEntrance : offsetExit;

        body.position = target.transform.position + offset;
    }
}
