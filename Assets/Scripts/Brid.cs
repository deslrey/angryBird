using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid : MonoBehaviour
{

    private bool isClick = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  ���һֱ���£�����λ�õĸ���
        if (isClick)    
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);
        }
    }

    //  ��갴��
    private void OnMouseDown()
    {
        isClick = true;
    }

    //  ���̧��
    private void OnMouseUp()
    {
        isClick = false;
    }
}
