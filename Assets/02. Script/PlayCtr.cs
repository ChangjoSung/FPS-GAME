using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCtr : MonoBehaviour
{
    

    public float MoveSpeed;
    public float RotateSpeed;


    
    Transform tf; //선언해주면 GetComponent 무조건 선언
    Animation anim;

    void Start()
    {
        tf = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");
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
    
        AnimationClip(h,v);
    }

    void AnimationClip(float h, float v)
    {
        //키보드 입력에 따른 애니메이션 효과
        if(v >= 0.1f)
        {
            anim.CrossFade("RunF",0.25f);
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

        else
        {
            anim.CrossFade("Idle",0.25f);
        }
    }
}
