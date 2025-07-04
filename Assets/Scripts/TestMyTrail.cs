using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMyTrail : MonoBehaviour
{
    public WeaponTrail myTrail;

    private float t = 0.033f;
    private float tempT = 0;
    private float animationIncrement = 0.003f;

     void LateUpdate()
    {
        t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);
        if (t > 0)
        {
            while(tempT < t)
            {
                tempT += animationIncrement;
                if (myTrail.time > 0)
                {
                    myTrail.Itterate(Time.time - t + tempT);
                }
                else
                {
                    myTrail.ClearTrail();
                }
            }

            tempT -= t;
            if (myTrail.time > 0)
            {
                myTrail.UpdateTrail(Time.time, t);
            }
        }
    }

    private void Start()
    {
        //  Ĭ��û����βЧ��
        myTrail.SetTime(0, 0, 1);
    }

    public void StartTrails()
    {
        //  ������βʱ��
        myTrail.SetTime(2, 0, 1);
        //  ��ʼ������β
        myTrail.StartTrail(0.5f, 0.4f);
    }

    public void ClearTrails()
    {
        //  �����β
        myTrail.ClearTrail();
    }
}
