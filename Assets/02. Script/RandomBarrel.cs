using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBarrel : MonoBehaviour
{
    Transform tr;

    public GameObject Barrel;
    void Start()
    {
        tr = GetComponent<Transform>();

        StartCoroutine(letgo());
    }

    IEnumerator letgo() 
    {
        for(int i = 1; i <= 30; i++)
        {
            Vector3 vec = new Vector3(Random.Range(-240,240),0,Random.Range(-240,240));

            Instantiate(Barrel,vec,Quaternion.identity);

            yield return null;
        }    
    }
}
