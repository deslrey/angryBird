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
    /// ���pause��ť
    /// </summary>
    public void Pause()
    {
        //  1������pause����
        anim.SetBool("isPause", true);
        button.SetActive(false);

        if (GameManager._instance.birds.Count > 0)
        {
            //  û�зɳ�
            if (GameManager._instance.birds[0].isReleasev == false)
            {
                GameManager._instance.birds[0].canMove = false;
            }
        }
    }

    /// <summary>
    /// ����˼�����ť
    /// </summary>
    public void Resume()
    {
        //  1������resume����
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        if (GameManager._instance.birds.Count > 0)
        {
            //  û�зɳ�
            if (GameManager._instance.birds[0].isReleasev == false)
            {
                GameManager._instance.birds[0].canMove = true;
            }
        }
    }

    /// <summary>
    /// pause�������������
    /// </summary>
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// resume�������������
    /// </summary>
    public void ResumeAnimEnd()
    {
        button.SetActive(true);
    }
}
