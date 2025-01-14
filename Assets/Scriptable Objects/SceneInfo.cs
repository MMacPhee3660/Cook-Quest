using UnityEngine;
[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistance")]
public class SceneInfo : ScriptableObject {
    public bool isNextScene = true;

    void OnEnable()
    {
        isNextScene = true;
    }
}


