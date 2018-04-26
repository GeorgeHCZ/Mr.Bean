using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.5f);//0.5秒后销毁爆炸效果
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
