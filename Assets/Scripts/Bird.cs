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
        Line(); // 初始化线条

    }

    // Update is called once per frame
    void Update()
    {
        //  鼠标一直按下，进行位置的跟随
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)    //  进行位置限定
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;  //  单位化向量
                pos *= maxDis;  //  最大长度的向量
                transform.position = pos + rightPos.position;
            }
            // 如果弹簧还存在，就持续绘制线条
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

    //  鼠标按下
    private void OnMouseDown()
    {
        isClick = true;
        rg.isKinematic = true;

    }

    //  鼠标抬起
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

    //  划线操作
    void Line()
    {
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    //  下一只小鸟发飞出
    void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }
}