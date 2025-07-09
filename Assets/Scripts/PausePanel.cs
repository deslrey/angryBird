using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;

    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 点击pause按钮
    /// </summary>
    public void Pause()
    {
        //  1、播放pause动画
        anim.SetBool("isPause", true);
        button.SetActive(false);

        if (GameManager._instance.birds.Count > 0)
        {
            //  没有飞出
            if (GameManager._instance.birds[0].isReleasev == false)
            {
                GameManager._instance.birds[0].canMove = false;
            }
        }
    }

    /// <summary>
    /// 点击了继续按钮
    /// </summary>
    public void Resume()
    {
        //  1、播放resume动画
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        if (GameManager._instance.birds.Count > 0)
        {
            //  没有飞出
            if (GameManager._instance.birds[0].isReleasev == false)
            {
                GameManager._instance.birds[0].canMove = true;
            }
        }
    }

    /// <summary>
    /// pause动画播放完调用
    /// </summary>
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// resume动画播放完调用
    /// </summary>
    public void ResumeAnimEnd()
    {
        button.SetActive(true);
    }
}
