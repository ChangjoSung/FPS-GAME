using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform FirePos;
    
    public AudioClip fireSound;
    private new AudioSource audio;
    private MeshRenderer flash;

    
    
    private void Start() {
        audio = GetComponent<AudioSource>();
        flash = FirePos.GetComponentInChildren<MeshRenderer>();
        
        flash.enabled = false;
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire() 
    {
        Instantiate(bullet, FirePos.position, FirePos.rotation);
        audio.PlayOneShot(fireSound,1.0f);
        StartCoroutine(showflash());
    }

    IEnumerator showflash()
    {
        Vector2 vec = new Vector2(Random.Range(0,2),Random.Range(0,2))*0.5f;
        flash.material.mainTextureOffset = vec;

        float angle = Random.Range(0,360);
        flash.transform.localRotation = Quaternion.Euler(0,0,angle);

        float scale = Random.Range(1.0f,2.0f);
        flash.transform.localScale = Vector3.one * scale;

        flash.enabled = true;

        yield return new WaitForSeconds(0.3f);

        flash.enabled = false;

    }
}
