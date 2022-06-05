using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public Color color = Color.yellow;

    private void OnDrawGizmos() 
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position,0.1f);    
    }
}
