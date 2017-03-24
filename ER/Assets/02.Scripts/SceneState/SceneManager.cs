using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneManager : Singleton<SceneManager> {
    public bool AutoMode = false;

    GAME_MODE _CurGameMode = GAME_MODE.None;
    public static GAME_MODE GameMode
    {
        get { return SceneManager.Instance._CurGameMode; }
        set { SceneManager.Instance._CurGameMode = value; }
    }

    public void Init()
    {
        Application.targetFrameRate = GameDefine.GAME_FRAME;

    }
}
