using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCtr : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateSpeed = 30.0f;

    
    int bullet = 29;
    public GameObject[] bulletsUI;
    
    public AudioClip Reload;
    private new AudioSource audio;


    Transform tf; //선언해주면 GetComponent 무조건 선언
    Animation anim;
    FireCtrl fireCtrl;

    //sprint 여부 
    bool Issprint;
    int i = 1; // 홀수 = 스프린트 , 짝수 = 뛰어가기

    IEnumerator Start()
    {
        tf = GetComponent<Transform>();
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
        //-------------MOVE
        float h = Input.GetAxis("Horizontal"); //왼 오 
        float v = Input.GetAxis("Vertical"); //위 아래
        
        
        
        //방향*속도*프레임 
        Vector3 MoveDir = (Vector3.forward*v) + (Vector3.right*h);
        tf.Translate(MoveDir.normalized * MoveSpeed * Time.deltaTime);
        
        //-------------ROTATE
        float r = Input.GetAxis("Mouse X");
        
        
        //축*회전속도*프레임
        Vector3 rotX = Vector3.up*r;
        tf.Rotate(rotX*RotateSpeed*Time.deltaTime);
        
    
        //스프린트 행동
        StartCoroutine(SprintAction());

        //달리면서 총쏘기 행동
        StartCoroutine(RunFireAction());

        

        //키보드 입력에 따른 애니메이션 효과
        AnimationClip(h,v);
    }

    void AnimationClip(float h, float v)
    {
        if(v >= 0.1f)
        {
            anim.CrossFade((Issprint == true) ? "SprintF" : "RunF", 0.25f);
        }

        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB",0.25f);
        }

        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR",0.25f);
        }

        else if (h <= -0.1f)
        {
            
            anim.CrossFade("RunL",0.25f);
        }

        else if(h == 0.0f && v==0.0f)
        {
            // sprint 후 멈추면 세팅 초기화
            Issprint = false;
            MoveSpeed = 8.0f;
            fireCtrl.enabled = true;
            i = 1;
            
            StartCoroutine(IdleReloadAction());
        }

        else
        {
            anim.CrossFade("Idle",0.25f);
        }
    }

    //스프린트 행동
    IEnumerator SprintAction()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(i%2 == 1) //스프린트
            {
                Issprint = true;
                MoveSpeed = 13.0f;
                fireCtrl.enabled = false;
            }
            
            else if(i%2 == 0) // 뛰기
            {
                Issprint = false;
                MoveSpeed = 8.0f;
                fireCtrl.enabled = true;
            }
                
            i++;  //짝수 <-> 홀수 

            yield return null; 
        }
    }

    //달리면서 총쏠때 총알 UI
    IEnumerator RunFireAction()
    {
        if(Input.GetMouseButtonDown(0) && Issprint == false) 
        {
            if(bullet < 0)
            {
                fireCtrl.enabled = false;
                bullet = 0;
                yield return null;
            }
            
            bulletsUI[bullet].SetActive(false);
            bullet--;

            yield return null;
        }
    }
    
    
    IEnumerator IdleReloadAction()
    {    
        //멈춘 다음 재장전 행동
        if(Input.GetKeyDown(KeyCode.R))
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
        for(bullet = 0; bullet < 30; bullet++)
        {
            bulletsUI[bullet].SetActive(true);
            
            yield return new WaitForSeconds(0.01f);
        }

        bullet = 29;
    }

    
}
    
