using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public float spawnTime = 5f;//重复调用的时间间隔
    public float spawnDelay = 3f;//初次调用的时间延迟
    public GameObject[] enemies;//创建敌人数组

	// Use this for initialization
	void Start () {
        //以spawnDelay延迟时间初次调用Spawn函数，之后以spawnTime做为时间间隔重复调用Spawn函数
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
	
	}

    void Spawn()
    {
        //获取随机值作为数组的下标索引
        int enemyIndex = Random.Range(0, enemies.Length);
        //实例化敌人1或敌人2
        Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
