using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStart : MonoBehaviour {

	void Start () {
        GameManager.Instance.IsGameStart = true;
        SceneManagerCustom.Instance.Init_FSM();
	}
	
}
