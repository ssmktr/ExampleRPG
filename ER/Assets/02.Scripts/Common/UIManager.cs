using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    public List<UIBasePanel> ListUIPanel = new List<UIBasePanel>();
    UIPANELTYPE _CurUIPanelType = UIPANELTYPE.NONE;

    public void AddUIPanel(UIBasePanel panel, UIPANELTYPE type)
    {
        if (panel == null)
            return;

        for (int i = 0; i < ListUIPanel.Count; ++i)
        {
            if (ListUIPanel[i].MyPanelType == panel.MyPanelType)
                return;
        }

        panel.MyPanelType = type;
        ListUIPanel.Add(panel);
    }

    public void Open(UIPANELTYPE type = UIPANELTYPE.NONE)
    {
        if (type == UIPANELTYPE.NONE)
            return;

        // 같은 타입의 패널 켠다
        UIBasePanel panel = null;
        for (int i = 0; i < ListUIPanel.Count; ++i)
        {
            if (ListUIPanel[i].MyPanelType == type)
            {
                _CurUIPanelType = type;
                ListUIPanel[i].LateInit();

                panel = ListUIPanel[i];
                break;
            }
        }

        // 선택한 패널 리스트의 맨앞으로 옮긴다
        if (panel != null)
        {
            ListUIPanel.Remove(panel);
            ListUIPanel.Insert(0, panel);
        }
    }

    // 메인 패너을 제외한 모든 패널을 삭제
    public void AllUIPanelDelete()
    {
        ListUIPanel.Clear();
    }

    // 모든 패널을 숨김
    public void AllUIPanelHide(bool firstexcept = true)
    {
        for (int i = (firstexcept ? 1 : 0); i < ListUIPanel.Count; ++i)
            ListUIPanel[i].Hide();
    }
}
