using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid : MonoBehaviour
{

    private bool isClick = false;
    public float maxDis = 3f;
    [HideInInspector]
    public SpringJoint2D sp;
    private Rigidbody2D rg;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;


    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Line(); // ��ʼ������

    }

    // Update is called once per frame
    void Update()
    {
        //  ���һֱ���£�����λ�õĸ���
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)    //  ����λ���޶�
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;  //  ��λ������
                pos *= maxDis;  //  ��󳤶ȵ�����
                transform.position = pos + rightPos.position;
            }
            // ������ɻ����ڣ��ͳ�����������
            if (sp.enabled)
            {
                //Line();
            }
        }
    }

    void FixedUpdate()
    {
        if (sp.enabled)
        {
            Line();
        }
    }

    //  ��갴��
    private void OnMouseDown()
    {
        isClick = true;
        rg.isKinematic = true;

    }

    //  ���̧��
    private void OnMouseUp()
    {
        isClick = false;
        rg.isKinematic = false;
        Invoke("Fly", 0.1f);
    }

    void Fly()
    {
        sp.enabled = false;
        Invoke("Next", 5);
    }

    //  ���߲���
    void Line()
    {
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    //  ��һֻС�񷢷ɳ�
    void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }
}