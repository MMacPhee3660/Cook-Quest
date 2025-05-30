using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
  [Header("Spawn Settings")]
  public GameObject[] resourcePrefabs; 
  public GameObject[] decorativeObjectsPrefab;
  public EnemySpawnData[] enemySpawnData;
  public float spawnChance;

  [Header("Raycast Settings")]
  public float distanceBetweenChecks;
  public float heightOfCheck = 10f, rangOfCheck = 30f;
  public LayerMask layerMask;
  public Vector2 positivePosition, negativePosition;

  private void Start(){
    resourcePrefabs = GameObject.FindGameObjectsWithTag("Resource");
    if (resourcePrefabs.Length > 0)
    {
      SpawnResources();
      SpawnCosResources();
      SpawnEnemies();
    }
    
  }


    void SpawnResources(){

         for( float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks){
             for( float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks){

              RaycastHit hit;
              if(Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangOfCheck, layerMask)){
                if(spawnChance > Random.Range(0,101)){
                Vector3 spawnPosition = hit.point;

            // Add a small random vertical offset for more natural placement
            spawnPosition.y = -1;

                // Optionally, add a small random horizontal offset
                spawnPosition.x += Random.Range(-0.3f, 0.3f);
                spawnPosition.z += Random.Range(-0.3f, 0.3f);

                Instantiate(
                  resourcePrefabs[Random.Range(0, resourcePrefabs.Length)],
                  spawnPosition,
                  Quaternion.Euler(0, Random.Range(0, 360), 0), // random Y rotation for variety
                  transform
                );
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

    void SpawnEnemies()
    {
      foreach (EnemySpawnData enemy in enemySpawnData)
      {
        bool spawned = false;
        while (!spawned)
        {
          for (int n = 0; n < enemy.numToSpawn; n++)
          {
            Vector2 pos = new Vector2(Random.Range(negativePosition.x, positivePosition.x), Random.Range(negativePosition.y, positivePosition.y));
            RaycastHit hit;
            if(Physics.Raycast(new Vector3(pos.x, heightOfCheck, pos.y), Vector3.down, out hit, rangOfCheck, layerMask))
            {
              Instantiate(enemy.enemyPrefab, hit.point, Quaternion.Euler(new Vector3(0,0,0)), transform);
              spawned = true;
            }
          }
        }
        
      }
    }

    [System.Serializable]
    public class EnemySpawnData
    {
      public GameObject enemyPrefab;
      public int numToSpawn = 1;
    }
}
