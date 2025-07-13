using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 orignPos;   //  ��ʼ����λ�� 

    public GameObject lose;
    public GameObject win;

    public GameObject[] stars;

    private int starsNum = 0;

    private int totalNum = 12;
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

    /// <summary>
    /// ��ʼ��С��
    /// </summary>
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0) //  ��һֻС��
            {
                birds[i].transform.position = orignPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }

    /// <summary>
    /// �ж���Ϸ�߼�
    /// </summary>
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
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum >= stars.Length)
            {
                break;
                
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
    }

    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if (starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        //  �洢���е����Ǹ���
        int sum = 0;
        for (int i = 1; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);

        print(PlayerPrefs.GetInt("totalNum"));
    }

    public Transform levelParent; // ���������¹ؿ��ĸ�����

    public void LoadNextLevel()
    {
        // ��ȡ��ǰ�ؿ�������Ĭ�ϴ�1��ʼ��
        int currentLevel = PlayerPrefs.GetInt("nowLevelIndex", 1);

        // ���ٵ�ǰ�ؿ�
        GameObject oldLevel = GameObject.Find("CurrentLevel");
        if (oldLevel != null)
        {
            Destroy(oldLevel);
        }

        int maxLevel = 24;
        if (currentLevel < maxLevel)
        {
            int nextLevel = currentLevel + 1;
            string levelName = "level" + nextLevel;

            GameObject prefab = Resources.Load<GameObject>(levelName);
            if (prefab != null)
            {
                GameObject newLevel = Instantiate(prefab, levelParent);
                newLevel.name = "CurrentLevel";
                PlayerPrefs.SetInt("nowLevelIndex", nextLevel); // ��¼��ǰ�ؿ�
            }
            else
            {
                Debug.LogError("δ�ҵ��ؿ���" + levelName);
            }
        }
        else
        {
            Debug.Log("�Ѿ������һ����");
        }
    }
}
