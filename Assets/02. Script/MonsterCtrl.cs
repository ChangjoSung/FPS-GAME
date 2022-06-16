using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    public enum Stats 
    {
        IDLE,
        GUNHOLDIDLE,
        TRACE,
        FIRE,
        DIE
    }

    public Stats stats = Stats.IDLE;

    public bool IsDie = false;
    
    public GameObject IdieGun;
    public GameObject GunHoldGun;

    public AudioClip fireSound;
    private new AudioSource audio;
    
    MonsterFireCtrl MC;
    
  
    
    float time = 0.0f;
    public float fireTime = 0.0f;


    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        MC = GetComponent<MonsterFireCtrl>();
        audio = GetComponent<AudioSource>();

        MC.enabled = false;

        StartCoroutine(MonsterCheck());
        StartCoroutine(MonsterAction());
    }

    void Update() 
    {
        time += Time.deltaTime;
        fireTime += Time.deltaTime;
    }

    IEnumerator MonsterCheck()
    {
        while(!IsDie)
        {
            if(time >= 3.0f && time < 5.0f)
            {
                stats = Stats.GUNHOLDIDLE;
            }

            else if (time >=5.0f)
            {
                stats = Stats.FIRE;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator MonsterAction()
    {
        while(!IsDie)
        {
            switch (stats)
            {
                case Stats.IDLE:
                {
                    IdieGun.SetActive(true);
                    GunHoldGun.SetActive(false);

                    break;
                }

                case Stats.GUNHOLDIDLE:
                {
                    IdieGun.SetActive(false);
                    GunHoldGun.SetActive(true);

                    anim.SetBool("IsHoldGun",true);
                    break;
                }

                case Stats.FIRE:
                {
                    if(fireTime >= 2.0f)
                    {
                        MC.EnemyFire();
                        audio.PlayOneShot(fireSound,0.5f);

                        fireTime = 0.0f;
                    }
                    
                    break;
                } 

            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    
    
}
