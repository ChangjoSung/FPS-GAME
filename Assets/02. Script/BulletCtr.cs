using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletCtr : MonoBehaviour
{
    
    Rigidbody rigid;
    Transform tr;
    
    MonsterCtrl monsterCtrl;
    BarrelCtrl BC;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        BC = GetComponent<BarrelCtrl>();
        monsterCtrl = GetComponent<MonsterCtrl>();
        
        
        Destroy(this.gameObject,10.0f);
    }


    void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "ENEMY":
            {
                monsterCtrl.MonsterHp -= PlayCtr.PlayerAttack;
                
                Destroy(this.gameObject);
                break;
            }

            case "Player":
            {
                PlayCtr.nowHp -= MonsterCtrl.Attack; 
                Destroy(this.gameObject);
                break;
            }

            

            default :
                break;

        } 
    }
    

}
