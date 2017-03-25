using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : UIBasePanel {

    public GameObject SingleGameReadyBtn;
    public UILabel TitleLbl;

    public override void Init()
    {
        base.Init();

        TitleLbl.text = "메인 패널";

        UIEventListener.Get(SingleGameReadyBtn).onClick = (sender) =>
        {
            Hide();
            UIManager.Instance.Open(UIPANELTYPE.SINGLEGAMEREADY);
        };
    }

    public override void LateInit()
    {
        base.LateInit();
    }
}
