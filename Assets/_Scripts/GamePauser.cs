using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour {

    private bool paused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.P))
        {
            paused = !paused;
        }
        if (paused)
            Time.timeScale = 0;
        /*Time.timeScale为0时，场景中所有与物理系统相关的操作失效*/
        else
            Time.timeScale = 1;
        /*Time.timeScale为1时，场景中所有与物理系统相关的操作恢复*/

    }
}
