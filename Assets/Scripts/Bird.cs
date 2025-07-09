using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;



public class Bird : MonoBehaviour
{

    private bool isClick = false;
    public float maxDis = 3;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;

    protected TestMyTrail myTrail;

    [HideInInspector]
    public bool canMove = false;
    public float smooth = 3;

    public AudioClip select;
    public AudioClip fly;

    private bool isFly = false;
    public bool isReleasev = false;


    public Sprite hurt;
    protected SpriteRenderer render;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()//��갴��
    {
        if (canMove)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }
    }


    private void OnMouseUp() //���̧��
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            //���û������
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }

    }


    private void Update()
    {
        //  �ж��Ƿ�������UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (isClick)
        {//���һֱ���£�����λ�õĸ���
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);


            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            { //����λ���޶�
                Vector3 pos = (transform.position - rightPos.position).normalized;//��λ������
                pos *= maxDis;//��󳤶ȵ�����
                transform.position = pos + rightPos.position;

            }
            Line();
        }


        //�������
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y,
            Camera.main.transform.position.z), smooth * Time.deltaTime);


        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        isFly = true;
        isReleasev = true;
        AudioPlay(fly);
        myTrail.StartTrails();
        sp.enabled = false;
        Invoke("Next", 5);
    }

    /// <summary>
    /// ����
    /// </summary>
    void Line()
    {
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    /// <summary>
    /// ��һֻС��ķɳ�
    /// </summary>
    /// 
    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.ClearTrails();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }


    /// <summary>
    /// �ż�����
    /// </summary>
    public virtual void ShowSkill()
    {
        isFly = false;
    }

    public void Hurt()
    {

        render.sprite = hurt;
    }
}
