using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEmitter : MonoBehaviour
{

    [field: SerializeField] public GameObject ObjectPrefab;

    private ParticleSystem ps;
    private List<ParticleSystem.Particle> exitParticles = new();

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger(){
        Debug.Log("detected");
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitParticles);

        foreach(ParticleSystem.Particle p in exitParticles){
            GameObject spawnedObject = Instantiate(ObjectPrefab, p.position, Quaternion.Euler(new Vector3(0,0,0)));
        }
    }

}
