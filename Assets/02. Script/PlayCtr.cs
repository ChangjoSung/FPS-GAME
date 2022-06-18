using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCtr : MonoBehaviour
{
    public GameObject[] bulletsUI;
    
    public AudioClip Reload;
    private new AudioSource audio;
    Transform tf; //선언해주면 GetComponent 무조건 선언
    Rigidbody rg;
    Animation anim;
    FireCtrl fireCtrl;

    //sprint 여부 
    public bool Issprint;
    bool sprintButton;
    bool FireButton;
    bool ReloadButton;
    bool IsBorder;
    
    public float MoveSpeed;
    public float RotateSpeed;
    float hAxis;
    float vAxis;
    float rAxis;

    
    int bulletindex = 29;

    Vector3 moveVec;
    Vector3 rotX;
    
    IEnumerator Start()
    {
        tf = GetComponent<Transform>();
        rg = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        fireCtrl = GetComponent<FireCtrl>();
        audio = GetComponent<AudioSource>();

        anim.Play("Idle");

        RotateSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        RotateSpeed = 30.0f;
    }

    
    void Update()
    {
        inputKey();
        
        Move();
        
        turn();
        
        //키보드 입력에 따른 애니메이션 효과
        AnimationClip(hAxis,vAxis);
        
        //스프린트 행동
        SprintAction();

        //달리면서 총쏘기 행동
        RunFireAction();
    }

    void inputKey()
    {
        vAxis = Input.GetAxis("Vertical"); //위 아래
        hAxis = Input.GetAxis("Horizontal"); //왼 오 
        rAxis = Input.GetAxis("Mouse X");
        sprintButton = Input.GetButton("sprint");
        FireButton = Input.GetButtonDown("Fire1");
        ReloadButton = Input.GetButtonDown("Reload");
    }
    
    void Move() 
    {
        moveVec = (Vector3.forward * vAxis) + (Vector3.right * hAxis);

        if(!IsBorder)
        {
            transform.Translate(moveVec.normalized * MoveSpeed * (sprintButton ? 1.5f : 1.0f) * Time.deltaTime);
        }
    }

    void turn()
    {
        rotX = Vector3.up* rAxis;
        tf.Rotate(rotX* RotateSpeed*Time.deltaTime);
    }
    
    void AnimationClip(float hAxis, float vAxis)
    {
        if(vAxis >= 0.1f)
        {
            anim.CrossFade((Issprint ? "SprintF" : "RunF"), 0.25f);
        }

        else if (vAxis <= -0.1f)
        {
            anim.CrossFade("RunB",0.25f);
        }

        else if (hAxis >= 0.1f)
        {
            anim.CrossFade("RunR",0.25f);
        }

        else if (hAxis <= -0.1f)
        {
            
            anim.CrossFade("RunL",0.25f);
        }

        else if(hAxis == 0.0f && vAxis == 0.0f)
        {
            StartCoroutine(IdleReloadAction());
        }

        else
        {
            anim.CrossFade("Idle",0.25f);
        }
    }

    //스프린트 행동
    void SprintAction()
    {
        if(sprintButton && !Issprint && fireCtrl.enabled)
        {
            Issprint = true;
            fireCtrl.enabled = false;

            Invoke("SprintOut", 0.3f);
        }
    }

    void SprintOut()
    {
        Issprint = false;
        fireCtrl.enabled = true;
    }

    //달리면서 총쏠때 총알 UI
    void RunFireAction()
    {
        if(FireButton && !Issprint && fireCtrl.enabled) 
        {
            if(bulletindex < 0)
            {
                fireCtrl.enabled = false;
                bulletindex = 0;
            }
            
            bulletsUI[bulletindex].SetActive(false);
            bulletindex--;
        }
    }
    
    void StopTowall()
    {
        Debug.DrawRay(tf.position, tf.forward*5, Color.green);
        IsBorder = Physics.Raycast(tf.position, tf.forward, 5, LayerMask.GetMask("WALL")); //Ray를 쏘아 닿는 오브젝트를 감지하는 함수

    }

    void freezeRotation()
    {
        rg.angularVelocity = Vector3.zero; //회전속도
    }

    void FixedUpdate() 
    {
        freezeRotation();
        StopTowall();    
    }
    
    IEnumerator IdleReloadAction()
    {    
        //멈춘 다음 재장전 행동
        if(ReloadButton)
        {
            StartCoroutine(ReLoadBulletUI());
            
            audio.PlayOneShot(Reload,1.0f);
            anim.CrossFade("IdleReloadSMG",0.25f);
            
            yield return new WaitForSeconds(0.5f);
            
            anim.CrossFade("Idle",0.25f);
        }
    }


    //총알 UI 재장전    
    IEnumerator ReLoadBulletUI()
    {
        for(bulletindex = 0; bulletindex < 30; bulletindex++)
        {
            bulletsUI[bulletindex].SetActive(true);
            
            yield return new WaitForSeconds(0.01f);
        }
        fireCtrl.enabled = true;
        bulletindex = 29;
    }

    
}
    
