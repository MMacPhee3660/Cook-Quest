using UnityEngine;
using System.Collections;

public class DontKillMeScript : MonoBehaviour {
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}