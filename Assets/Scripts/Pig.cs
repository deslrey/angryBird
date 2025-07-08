using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{

    public float maxSpeed = 8;
    public float minSpeed = 4;
    public SpriteRenderer render;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;

    public bool isPig = false;

    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        print("标签为 ======》 " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollision);
            Brid bird = collision.transform.GetComponent<Brid>();
            if (bird != null)
            {
                bird.Hurt();
                print("调用小鸟受伤成功");
            }
            else
            {
                print("找不到 Brid 组件！");
            }

        }

        if (collision.relativeVelocity.magnitude > maxSpeed)    //  直接死亡
        {
            Dead();
        }else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)  //  受伤
        {
            render.sprite = hurt;
            //  受伤音乐
            AudioPlay(hurtClip);
        }
    }

   public void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject go = Instantiate(score, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(go, 1.5f);

        //  播放死亡音乐
        AudioPlay(dead);
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
