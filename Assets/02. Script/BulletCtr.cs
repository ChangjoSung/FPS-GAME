using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtr : MonoBehaviour
{
    public float bulletSpeed;

    public float Damage;
    
    Rigidbody rigid;
    Transform tr;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        
        rigid.AddForce(bulletSpeed * tr.forward);
    }

    

}
