using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCtr : MonoBehaviour
{
    
    public float MoveSpeed = 3.0f;
    public float RotateSpeed;
    
    Transform tf; //선언해주면 GetComponent 무조건 선언
    void Start()
    {
        tf = GetComponent<Transform>();
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
        Vector3 rot = Vector3.up*r;
        tf.Rotate(rot*RotateSpeed*Time.deltaTime);

        
    
    }
}
