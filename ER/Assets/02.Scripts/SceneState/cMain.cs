using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMain : MonoBehaviour {
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
            UIManager.Instance.ListUIPanel.Clear();
            UIManager.Instance.AddUIPanel(_UICamera.transform.FindChild("MainPanel").GetComponent<MainPanel>(), UIPANELTYPE.MAIN);
            UIManager.Instance.AddUIPanel(_UICamera.transform.FindChild("SingleGameReadyPanel").GetComponent<SingleGameReadyPanel>(), UIPANELTYPE.SINGLEGAMEREADY);

            // 일단 모든 패널을 숨긴다
            UIManager.Instance.AllUIPanelHide();

            // 메인 패널을 켠다
            UIManager.Instance.Open(UIPANELTYPE.MAIN);
        }
    }
}
