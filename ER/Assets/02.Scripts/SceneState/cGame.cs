using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGame : MonoBehaviour {

    private void Awake()
    {
        if (!GameManager.Instance.IsGameStart)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            return;
        }
    }

    void Start () {
        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Hero))
            Debug.Log(GameManager.Instance.AllUnitDic[UnitType.Hero].Count);
        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Enemy))
            Debug.Log(GameManager.Instance.AllUnitDic[UnitType.Enemy].Count);
        if (GameManager.Instance.AllUnitDic.ContainsKey(UnitType.Boss))
            Debug.Log(GameManager.Instance.AllUnitDic[UnitType.Boss].Count);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "MAIN"))
            SceneManagerCustom.Instance.ActionEvent(_ACTION.GO_MAIN);
    }
}
