using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    Transform tr;

    public GameObject Barrel;
    public GameObject Enemy;

    public float Spontime = 0.0f;
    
    
    void Start()
    {
        tr = GetComponent<Transform>();

        StartCoroutine(RDbarrel());
    }
    
    void Update() 
    {
        Spontime += Time.deltaTime;
        
        RDMonster();    
    }

    void RDMonster()
    {
        if(Spontime >= 5.0f)
        {
            Vector3 EnemyVec = new Vector3(Random.Range(-80,80),0,Random.Range(-80,80));
            Instantiate(Enemy,EnemyVec,Quaternion.identity);

            Spontime = 0.0f;
        }

        
    }    


    IEnumerator RDbarrel() 
    {
        for(int i = 1; i <= 30; i++)
        {
            Vector3 vec = new Vector3(Random.Range(-80,80),0,Random.Range(-80,80));

            Instantiate(Barrel,vec,Quaternion.identity);

            yield return null;
        }    
    }

    


}
