using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
  [Header("Spawn Settings")]
  public GameObject[] resourcePrefabs; 
  public GameObject[] decorativeObjectsPrefab;
  public float spawnChance;

  [Header("Raycast Settings")]
  public float distanceBetweenChecks;
  public float heightOfCheck = 10f, rangOfCheck = 30f;
  public LayerMask layerMask;
  public Vector2 positivePosition, negativePosition;

  private void Start(){
    Debug.Log(spawnChance);
    resourcePrefabs = GameObject.FindGameObjectsWithTag("Resource");
    if (resourcePrefabs.Length > 0)
    {
      SpawnResources();
      SpawnCosResources();
    }
    
  }


    void SpawnResources(){

         for( float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks){
             for( float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks){

              RaycastHit hit;
              if(Physics.Raycast(new Vector3(x,heightOfCheck,z), Vector3.down, out hit, rangOfCheck, layerMask)){
                  if(spawnChance > Random.Range(0,101)){
                    Instantiate(resourcePrefabs[Random.Range(0,resourcePrefabs.Length)], hit.point, Quaternion.Euler(new Vector3(0,0,0)),transform);
                  }
              }
            }

         }
    }

    void SpawnCosResources(){

         for( float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks){
             for( float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks){

              RaycastHit hit;
              if(Physics.Raycast(new Vector3(x,heightOfCheck,z), Vector3.down, out hit, rangOfCheck, layerMask)){
                  if(spawnChance > Random.Range(0,26)){
                    Instantiate(decorativeObjectsPrefab[Random.Range(0,3)], hit.point, Quaternion.Euler(new Vector3(0,0,0)),transform);
                  }
              }
            }

         }

    }
}
