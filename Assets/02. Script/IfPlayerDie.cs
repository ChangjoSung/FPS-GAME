using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayerDie : MonoBehaviour
{
    public GameObject Ifplayer;
    public GameObject _Stages;
    
    void LateUpdate()
    {
        if(PlayCtr.nowHp <= 0)
        {
            _Stages.SetActive(false);
            Ifplayer.SetActive(true);
        }
    }
}
