using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGameReadyPanel : UIBasePanel {

    public GameObject BackBtn, StartSingleGameBtn, AddEnemyBtn, SubEnemyBtn, AddBossBtn, SubBossBtn;
    public UILabel TitleLbl, EnemyTitleLbl, EnemyCntLbl, BossTitleLbl, BossCntLbl;

    int EnemyCnt = 1;
    int BossCnt = 1;

    public override void Init()
    {
        base.Init();

        TitleLbl.text = "싱글게임 준비 패널";
        EnemyTitleLbl.text = "적 유닛 : ";
        BossTitleLbl.text = "보스 유닛 : ";

        UIEventListener.Get(BackBtn).onClick = (sender) =>
        {
            Hide();
            UIManager.Instance.Open(UIPANELTYPE.MAIN);
        };

        UIEventListener.Get(StartSingleGameBtn).onClick = OnClickStartSingleGame;
        UIEventListener.Get(AddEnemyBtn).onClick = OnClickAddEnemy;
        UIEventListener.Get(SubEnemyBtn).onClick = OnClickSubEnemy;
        UIEventListener.Get(AddBossBtn).onClick = OnClickAddBoss;
        UIEventListener.Get(SubBossBtn).onClick = OnClickSubBoss;
    }

    public override void LateInit()
    {
        base.LateInit();

        EnemyCnt = 1;
        EnemyCntLbl.text = EnemyCnt.ToString("N0");

        BossCnt = 1;
        BossCntLbl.text = BossCnt.ToString("N0");
    }

    void OnClickAddEnemy(GameObject sender)
    {
        EnemyCnt++;
        if (EnemyCnt > 10)
            EnemyCnt = 10;

        EnemyCntLbl.text = EnemyCnt.ToString("N0");
    }

    void OnClickSubEnemy(GameObject sender)
    {
        EnemyCnt--;
        if (EnemyCnt < 1)
            EnemyCnt = 1;

        EnemyCntLbl.text = EnemyCnt.ToString("N0");
    }

    void OnClickAddBoss(GameObject sender)
    {
        BossCnt++;
        if (BossCnt > 3)
            BossCnt = 3;

        BossCntLbl.text = BossCnt.ToString("N0");
    }

    void OnClickSubBoss(GameObject sender)
    {
        BossCnt--;
        if (BossCnt < 1)
            BossCnt = 1;

        BossCntLbl.text = BossCnt.ToString("N0");
    }

    void OnClickStartSingleGame(GameObject sender)
    {
        SceneManagerCustom.Instance.ActionEvent(_ACTION.GO_GAME);
    }

}
