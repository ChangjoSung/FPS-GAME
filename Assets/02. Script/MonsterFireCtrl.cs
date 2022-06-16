using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFireCtrl : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform EnemyFirePos;

   
    public void EnemyFire()
    {
        Instantiate(EnemyBullet, EnemyFirePos.position, EnemyFirePos.rotation);
    }

    
}
