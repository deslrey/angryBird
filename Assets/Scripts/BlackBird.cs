using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    public List<Pig> blocks = new List<Pig>();

    /// <summary>
    /// 进入触发区域
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
    /// 离开触发区
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        blocks.Remove(collision.gameObject.GetComponent<Pig>());
    }


    public override void ShowSkill()
    {
        base.ShowSkill();
        if (blocks != null && blocks.Count > 0)
        {

            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    protected override void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }

    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();
    }
}
