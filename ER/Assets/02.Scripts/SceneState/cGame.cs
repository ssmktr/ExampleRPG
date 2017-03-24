using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGame : MonoBehaviour {

	void Start () {
		
	}

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "MAIN"))
            SceneManagerCustom.Instance.ActionEvent(_ACTION.GO_MAIN);
    }
}
