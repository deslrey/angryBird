using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public int starsNum = 0;
    private bool isSelect = false;

    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;

    public Text starsText;

    public int startNum = 1;
    public int endNum = 8;

    private void Start()
    {
        //  清除已保存的数据
        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNum)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);
            //TODO:text显示

            int counts = 0;
            for (int i = starsNum; i <= endNum; i++)
            {
                counts += PlayerPrefs.GetInt("level" + i.ToString(), 0);
            }

            starsText.text = counts.ToString() + " / 24";
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
    public void panelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
