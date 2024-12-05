using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    [SerializeField] float attackRange = 0.75f;
    public LayerMask enemyLayers;
    public LayerMask resourceLayers;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Attack();
        }


    }
    private void Attack(){
        Collider[] resourcesHit = Physics.OverlapSphere(attackPoint.position,attackRange, resourceLayers); 
        foreach(Collider resource in resourcesHit){
            Debug.Log("We hit " + resource.name);
        }

    }

    void OnDrawGizmosSelected(){

        if(attackPoint == null)
        return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
