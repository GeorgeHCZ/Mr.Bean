using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2);//两秒后销毁炮弹
    }
    void OnExplode()
    {
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));//旋转角度 围绕Z轴进行随机角度旋转
        Instantiate(explosion, transform.position, randomRotation);//在制定位置已制定角度实例化炮弹的爆炸效果
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")//当炮弹击中敌人
        {
            col.gameObject.GetComponent<Enemy>().Hurt();
            OnExplode();
            Destroy(gameObject);
        }
        else if (col.gameObject.tag != "Player")
        {
            OnExplode();
            Destroy(gameObject);//销毁炮弹
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}