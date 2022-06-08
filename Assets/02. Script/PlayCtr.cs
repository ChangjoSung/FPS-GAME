using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCtr : MonoBehaviour
{
    public float MoveSpeed = 8.0f;
    public float RotateSpeed = 30.0f;


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
        }
        
        
        AnimationClip(h,v);
    }

    
    void AnimationClip(float h, float v)
    {
        //키보드 입력에 따른 애니메이션 효과
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
            StartCoroutine(IdleAction());
        }

        else
        {
            anim.CrossFade("Idle",0.25f);
        }
    }

    IEnumerator IdleAction()
    {
        // sprint 후 멈추면 세팅 초기화
        Issprint = false;
        MoveSpeed = 8.0f;
        fireCtrl.enabled = true;
        i = 1;
        
        
        if(Input.GetMouseButtonDown(0))
        {
            anim.CrossFade("IdleFireSMG",0.25f);
            yield return new WaitForSeconds(0.2f);
            anim.CrossFade("Idle",0.25f);
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reload");
            
            anim.CrossFade("IdleReloadSMG",0.25f);
            yield return new WaitForSeconds(1.2f);
            anim.CrossFade("Idle",0.25f);
        }
    }
           


    
}
