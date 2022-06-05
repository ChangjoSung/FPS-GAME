using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour
{
    Transform CameraTr;
    public Transform Target;
    public float x;
    public float y;

    void Start()
    {
        CameraTr = GetComponent<Transform>();
        Target = GetComponent<Transform>();    
    }

    
    void LateUpdate()
    {
        CameraTr.position = Target.position + (-Vector3.forward*x)+(Vector3.up*y);

        CameraTr.LookAt(Target.position); 
    }
}
