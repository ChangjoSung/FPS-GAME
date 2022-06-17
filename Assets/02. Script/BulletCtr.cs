using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtr : MonoBehaviour
{
    Rigidbody rigid;
    Transform tr;

    BarrelCtrl BC;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        BC = GetComponent<BarrelCtrl>();
        
        Destroy(this.gameObject,10.0f);
    }


    void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "ENEMY":
            {
                Debug.Log("적 피가 줄어듦");
                Destroy(this.gameObject);
                break;
            }

            case "Player":
            {
                Debug.Log("내 피가 줄어듦");
                Destroy(this.gameObject);
                break;
            }

            

            default :
                break;

        } 
    }
    

}
