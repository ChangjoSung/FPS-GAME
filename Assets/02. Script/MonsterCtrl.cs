using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public GameObject IdieGun;
    public GameObject GunHoldGun;
    public GameObject EnemyBullet;
    
    
    public Transform EnemyFirePos;
    Transform PlayerTr;

    public AudioClip fireSound;
    
    
    new AudioSource audio;
    MeshRenderer Eflash;
    Animator anim;
    NavMeshAgent nav;
    
  

    float distance;  
    public float AnimTime = 0.0f;
    public float fireTime = 0.0f;


    public bool IsDie = false;
    
    void Awake() 
    {
        stats = Stats.IDLE;
     
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        Eflash = EnemyFirePos.GetComponentInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        PlayerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        Eflash.enabled = false;
        

        StartCoroutine(MonsterCheck());
        
        StartCoroutine(MonsterAction());
    }

    void Update() 
    {
        if(AnimTime <= 5.0f)
        {
            AnimTime += Time.deltaTime;
        }
        
        else if(AnimTime >= 5.0f)
        {
            fireTime += Time.deltaTime;
        }
    }

    void Destination()
    {
        distance = Vector3.Distance(PlayerTr.position, this.transform.position); 
    }

    void EnemyFire()
    {
        GameObject bulletPrefab = Instantiate(EnemyBullet, EnemyFirePos.position, EnemyFirePos.rotation);
        Rigidbody rigid = bulletPrefab.GetComponent<Rigidbody>();
        rigid.velocity = EnemyFirePos.forward * 50; 

        audio.PlayOneShot(fireSound,0.2f);
        StartCoroutine(showflash());
    }


    IEnumerator MonsterCheck()
    {
        while(!IsDie)
        {
            yield return new WaitForSeconds(0.3f);
            
            if(AnimTime < 3.0f)
            {
                stats = Stats.IDLE;
            }
            
            else if(AnimTime >= 3.0f && AnimTime < 5.0f)
            {
                stats = Stats.GUNHOLDIDLE;
            }
            
            if(fireTime < 1.3f && AnimTime >= 5.0f)
            {
                stats = Stats.TRACE;
            }
            
            else if (fireTime >= 1.3f)
            {
                stats = Stats.FIRE;
            }
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

                    nav.isStopped = true;
                    
                    anim.SetTrigger("HoldGun");
                    break;
                }

                case Stats.GUNHOLDIDLE:
                {
                    IdieGun.SetActive(false);
                    GunHoldGun.SetActive(true);

                    nav.isStopped = true;

                    anim.SetTrigger("HoldGun");
                    
                    break;
                }

                case Stats.TRACE:
                {   
                    nav.isStopped = false;
                    nav.destination = PlayerTr.position;
                    nav.speed = 4.0f;

                    anim.SetBool("IsTrace",true);
                    anim.SetBool("IsFire",false);
                    
                    break;
                }
                case Stats.FIRE:
                {
                    nav.speed = 2.0f;
                    
                    anim.SetBool("IsFire",true);
                    yield return new WaitForSeconds(0.3f);
                    EnemyFire();
                    
                    
                    fireTime = 0.0f;
                    break;
                } 

            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator showflash()
    {
        Vector2 vec = new Vector2(Random.Range(0,2),Random.Range(0,2))*0.5f;
        Eflash.material.mainTextureOffset = vec;

        float angle = Random.Range(0,360);
        Eflash.transform.localRotation = Quaternion.Euler(0,0,angle);

        float scale = Random.Range(1.0f,2.0f);
        Eflash.transform.localScale = Vector3.one * scale;

        Eflash.enabled = true;

        yield return new WaitForSeconds(0.3f);

        Eflash.enabled = false;

    }
    
}
