using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMain : MonoBehaviour {
    UICamera _UICamera = null;

    void Start () {
        _UICamera = transform.FindChild("Camera").GetComponent<UICamera>();

        if (_UICamera != null)
        {
            UIManager.Instance.ListUIPanel.Clear();
            UIManager.Instance.ListUIPanel.Add(_UICamera.transform.FindChild("MainPanel").GetComponent<MainPanel>());
            UIManager.Instance.ListUIPanel.Add(_UICamera.transform.FindChild("SingleGameReadyPanel").GetComponent<SingleGameReadyPanel>());

            // 일단 모든 패널을 숨긴다
            UIManager.Instance.AllUIPanelHide();

            // 메인 패널을 켠다
            UIManager.Instance.Open(UIPANELTYPE.MAIN);
        }
    }
}
