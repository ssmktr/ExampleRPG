using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGame : MonoBehaviour {
    UICamera _UICamera = null;
    InGameHUDPanel hudPanel = null;

    public UISlider HpProgressBar;

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

            hudPanel = _UICamera.transform.FindChild("InGameHUDPanel").GetComponent<InGameHUDPanel>();
            UIManager.Instance.AddUIPanel(hudPanel, UIPANELTYPE.INGAMEHUD);

            GameInfo.GetComponent<GameInfoBase>().Init(this);

            // 일단 모든 패널을 숨긴다
            UIManager.Instance.AllUIPanelHide();

            // 메인 패널을 켠다
            UIManager.Instance.Open(UIPANELTYPE.INGAMEHUD);
        }
    }

    // 체력 게이지 생성
    public void CreateHpSlider(Unit unit, Transform parent, string name)
    {
        if (hudPanel != null)
        {
            GameObject hpBar = (GameObject)Instantiate(HpProgressBar.gameObject);
            hpBar.transform.parent = hudPanel.transform;
            hpBar.transform.localScale = Vector3.one;
            hpBar.GetComponent<HpProgressBar>().Init(parent, name);
            unit._HpProgressBar = hpBar.GetComponent<HpProgressBar>();
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "MAIN"))
            SceneManagerCustom.Instance.ActionEvent(_ACTION.GO_MAIN);

    }
}
