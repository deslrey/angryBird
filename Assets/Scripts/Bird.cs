using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid : MonoBehaviour
{
    private bool isClick = false;
    private bool isFly = false; // 飞出标记

    public float maxDis = 3f;

    [HideInInspector]
    public SpringJoint2D sp;

    private Rigidbody2D rg;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;

    private TestMyTrail myTrail;

    private bool canMove = true;

    public float smooth = 3;

    public AudioClip select;
    public AudioClip fly;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
    }

    void Start()
    {
        Line(); // 初始化线条
    }

    void Update()
    {
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }

            if (!isFly && sp.enabled)
            {
                Line();
            }
        }

        //  相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);

    }

    void FixedUpdate()
    {
        if (!isFly && sp.enabled)
        {
            Line();
        }
    }

    /// <summary>
    /// 鼠标按下
    /// </summary>
    private void OnMouseDown()
    {
        if (canMove)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }
    }

    /// <summary>
    /// 鼠标松开
    /// </summary>
    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            canMove = false;
        }
    }

    void Fly()
    {
        AudioPlay(fly);
        myTrail.StartTrails();
        sp.enabled = false;
        isFly = true;

        right.enabled = false;
        left.enabled = false;

        Invoke("Next", 5);
    }

    void Line()
    {
        if (isFly) return; // 飞出后不再划线

        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myTrail.ClearTrails();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
