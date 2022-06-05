using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform targetTf;
    public Transform CamTf;
    
    [Range(0.0f,20.0f)]
    public float distance = 3.0f;
    
    [Range(0.0f,20.0f)]
    public float height = 2.0f;

    Vector3 veclocity = Vector3.zero;

    public float time = 10.0f; 
    void Start()
    {
         
        CamTf = GetComponent<Transform>();
        
    }

    
    void LateUpdate() //카메라는 LateUpdate
    {
        //카메라 위치 Vector 저장
        CamTf.position = targetTf.position + (-targetTf.forward*distance)+(height*Vector3.up);

        //보간함수 (부드럽게)
        //CamTf.position = Vector3.SmoothDamp(CamTf.position,pos,ref veclocity,time);

        //LookAt
        CamTf.LookAt(targetTf.position + (Vector3.up * 2.0f));
    }
}
