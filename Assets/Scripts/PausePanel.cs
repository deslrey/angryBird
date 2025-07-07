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

    }

    public void Home()
    {

    }

    /// <summary>
    /// ���pause��ť
    /// </summary>
    public void Pause()
    {
        print("Pause");
        //  1������pause����
        anim.SetBool("isPause", true);
        button.SetActive(false);
    }

    /// <summary>
    /// ����˼�����ť
    /// </summary>
    public void Resume()
    {
        print("Resume");
        //  1������resume����
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
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
