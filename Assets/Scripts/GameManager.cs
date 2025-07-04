using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Brid> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 orignPos;   //  初始化的位置 


    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            orignPos = birds[0].transform.position;
        }
    }


    private void Start()
    {
        Initialized();
    }

    //  初始化小鸟
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0) //  第一只小鸟
            {
                birds[i].transform.position = orignPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    //  判定游戏逻辑
    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //  下一只飞吧
                Initialized();
            }
            else
            {
                //  输了
            }
        }
        else
        {
            //  赢了
        }
    }

}
