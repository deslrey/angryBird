using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid : MonoBehaviour
{

    private bool isClick = false;
    public Transform rightPos;
    public float maxDis = 3f;
    private SpringJoint2D sp;
    private Rigidbody2D rg;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  鼠标一直按下，进行位置的跟随
        if (isClick)    
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);
            if (Vector3.Distance(transform.position,rightPos.position) > maxDis)    //  进行位置限定
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;  //  单位化向量
                pos *= maxDis;  //  最大长度的向量
                transform.position = pos + rightPos.position;
            }
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
    }
}
