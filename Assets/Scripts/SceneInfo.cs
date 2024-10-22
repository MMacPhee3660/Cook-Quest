using UnityEngine;

public class SceneInfo : ScriptableObject {
    [CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
    public bool isNextScene = true;
}

