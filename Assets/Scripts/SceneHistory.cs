using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHistory : MonoBehaviour
{
    // private Scene previous;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void SceneStoring()
    {
        // previous = SceneManager.GetActiveScene;
    }
}
