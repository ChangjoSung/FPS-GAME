using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject SparkEffect;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.CompareTag("Bullet"))
        {
            Instantiate(SparkEffect, other.transform.position, Quaternion.identity);
            
            Destroy(other.gameObject);
        }    
    }
}
