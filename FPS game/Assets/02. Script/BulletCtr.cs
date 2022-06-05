using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtr : MonoBehaviour
{
    public float bulletSpeed;

    public float Damage;
    
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
        rigid.AddForce(bulletSpeed * transform.forward);
    }

    

}
