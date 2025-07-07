using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<Brid> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 orignPos;   //  ��ʼ����λ�� 

    public GameObject lose;
    public GameObject win;

    public GameObject[] stars;

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

    //  ��ʼ��С��
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0) //  ��һֻС��
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

    //  �ж���Ϸ�߼�
    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //  ��һֻ�ɰ�
                Initialized();
            }
            else
            {
                //  ����
                lose.SetActive(true);
            }
        }
        else
        {
            //  Ӯ��
            win.SetActive(true); 
        }
    }

    public void ShowStars()
    {
        StartCoroutine(Show());
    }


    IEnumerator Show()
    {
        for (int i = 0; i < birds.Count + 1; i++)
        {
            yield return new WaitForSeconds(0.2f);
            stars[i].SetActive(true);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }
}
