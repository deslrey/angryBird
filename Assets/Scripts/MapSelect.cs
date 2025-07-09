using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    public int starsNum = 0;
    private bool isSelect = false;

    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;

    private void Start()
    {
        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNum)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);
            //TODO:text显示
        }
    }

    /// <summary>
    /// 鼠标选择
    /// </summary>
    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }

    /// <summary>
    /// 返回关卡选择
    /// </summary>
    public void Retuen()
    {
        SceneManager.LoadScene(1);
    }
}
