using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGame : MonoBehaviour {
    UICamera _UICamera = null;

    private void Awake()
    {
        if (!GameManager.Instance.IsGameStart)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            return;
        }
    }

    void Start () {
        _UICamera = transform.FindChild("Camera").GetComponent<UICamera>();

        if (_UICamera != null)
        {
            GameObject GameInfo = (GameObject)Instantiate(Resources.Load("GameInfo/SingleGameInfo"));
            GameInfo.name = "GameInfo/SingleGameInfo";
            GameInfo.transform.localPosition = Vector3.zero;
            GameInfo.transform.localScale = Vector3.one;
            GameInfo.GetComponent<GameInfoBase>().Init();

            UIManager.Instance.AddUIPanel(_UICamera.transform.FindChild("InGameHUDPanel").GetComponent<InGameHUDPanel>(), UIPANELTYPE.INGAMEHUD);

            // 일단 모든 패널을 숨긴다
            UIManager.Instance.AllUIPanelHide();

            // 메인 패널을 켠다
            UIManager.Instance.Open(UIPANELTYPE.INGAMEHUD);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "MAIN"))
            SceneManagerCustom.Instance.ActionEvent(_ACTION.GO_MAIN);
    }
}
