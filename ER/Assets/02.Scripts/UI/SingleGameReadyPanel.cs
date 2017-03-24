using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGameReadyPanel : UIBasePanel {

    public GameObject BackBtn, StartSingleGameBtn;
    public UILabel TitleLbl;

    public override void Init()
    {
        _MyPanelType = UIPANELTYPE.SINGLEGAMEREADY;
        base.Init();

        TitleLbl.text = "싱글게임 준비 패널";

        UIEventListener.Get(BackBtn).onClick = (sender) =>
        {
            Hide();
            UIManager.Instance.Open(UIPANELTYPE.MAIN);
        };

        UIEventListener.Get(StartSingleGameBtn).onClick = OnClickStartSingleGame;
    }

    public override void LateInit()
    {
        base.LateInit();
    }

    void OnClickStartSingleGame(GameObject sender)
    {
        SceneManagerCustom.Instance.ActionEvent(_ACTION.GO_GAME);
    }

}
