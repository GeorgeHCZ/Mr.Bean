using UnityEngine;
using System.Collections;

public class SwanSpawner : MonoBehaviour {
    public Rigidbody2D backgroundProp;//例体
    public float leftSpawnPosX;//左边界
    public float rightSpawnPosX;//右边界
    public float minSpawnPosY;//最低高度
    public float maxSpawnPosY;//最大高度
    public float minTimeBetweenSpawns;//最短时间间隔
    public float maxTimeBetweenSpawns;//最大时间间隔
    public float minSpeed;//最小速度
    public float maxSpeed;//最大速度

    // Use this for initialization
    void Start()
    {
        //设置随机数种子，使用时间的毫秒数做随机数种子
        Random.seed = System.DateTime.Today.Millisecond;
        //启用协程函数
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        //等待waitTime延迟
        yield return new WaitForSeconds(waitTime);
        //天鹅朝左的状态参数
        bool facingLeft = Random.Range(0, 2) == 0;
        //确定实例化的X位置，如果facingLeft为真，从右边界实例化，否则左
        float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;
        //确定实例化的高度，在最小与最大高度之间
        float posY = Random.Range(minSpawnPosY, maxSpawnPosY);
        //设置实例化的位置
        Vector3 spawnPos = new Vector3(posX, posY, transform.position.z);
        //在指定位置实例化
        Rigidbody2D propInstance = Instantiate(backgroundProp, spawnPos, Quaternion.identity) as Rigidbody2D;
        if (!facingLeft)//如果向右飞行，控制天鹅的反向(镜像处理)
        {
            Vector3 scale = propInstance.transform.localScale;
            scale.x *= -1;
            propInstance.transform.localScale = scale;
        }
        float speed = Random.Range(minSpeed, maxSpeed);
        //使用facingLeft值的真假确定speed的符号
        speed *= facingLeft ? -1f : 1f;
        //设置刚体速度
        propInstance.velocity = new Vector2(speed, 0);
        //启用协程函数 递归调用
        StartCoroutine(Spawn());
        //以下内容控制天鹅超出边界销毁
        while (propInstance != null)
        {
            if (facingLeft)//朝左飞，若x小于左边界-0.5则销毁
            {
                if (propInstance.transform.position.x < leftSpawnPosX - 0.5f)
                    Destroy(propInstance.gameObject);
            }
            else//朝右飞，若x大于右边界0.5则销毁
            {
                if (propInstance.transform.position.x > rightSpawnPosX + 0.5f)
                    Destroy(propInstance.gameObject);
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
