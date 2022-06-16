using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    //public GameObject SparkEffect;
    private void OnCollisionEnter(Collision other) 
    {
        /*if(other.collider.CompareTag("Bullet"))
        {
            
            ContactPoint cp = other.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);  //-를 해줘야 내 시야에서 보임 ContactPoint.normal (법선벡터)

            GameObject spark = Instantiate(SparkEffect, cp.point, rot); //cp.point (접점 위치)

            Destroy(spark, 0.5f);
            
            Destroy(other.gameObject);
            

        }*/
        Physics.IgnoreLayerCollision(11, 12,true);
        

    }

    
}
