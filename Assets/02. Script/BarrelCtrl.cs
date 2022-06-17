using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarrelCtrl : MonoBehaviour
{
    public GameObject ExpEffect;
    public Texture[] textures;
    public AudioClip BarrelaudioClip;
    private new AudioSource audio;

    new MeshRenderer renderer;
    Rigidbody rb;
    Transform tr;

    float radius = 10.0f;


    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        renderer = GetComponentInChildren<MeshRenderer>();
        audio = GetComponent<AudioSource>();

        int index = Random.Range(0, textures.Length);
        renderer.material.mainTexture = textures[index];
    
    }

    void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "Bullet" :
            {
                Contact(other);
                Debug.Log("bang");
                
                break;
            }

            case "EBullet":
            {
                Contact(other);
                
                break;
            }

        }
        
          
    }
    
    public void Contact(Collider other)
    {
        GameObject exp = Instantiate(ExpEffect, tr.position , tr.rotation);
        Destroy(exp,2.0f);

        BarrelsExp();

        playerBarrelDamaged();
        
        Destroy(other.gameObject);
    }

    void BarrelsExp()
    {
        Collider[] cols = Physics.OverlapSphere(tr.position , radius, 1<<3);

        foreach(var col in cols) 
        {
            rb = col.GetComponent<Rigidbody>();

            rb.mass = 1.0f;

            rb.constraints = RigidbodyConstraints.None;

            rb.AddExplosionForce(1500.0f,tr.position,radius,1200.0f);

            audio.PlayOneShot(BarrelaudioClip,1.0f);

            Destroy(col.gameObject,3.0f);
        }
    }

    void playerBarrelDamaged()
    {
        Collider[] colls = Physics.OverlapSphere(tr.position,radius, 1<<6);

        foreach(var coll in colls) 
        {
            rb = coll.GetComponent<Rigidbody>();
            
            rb.AddExplosionForce(1500.0f,tr.position,radius,1200.0f);
        }
    }
    
    
}
