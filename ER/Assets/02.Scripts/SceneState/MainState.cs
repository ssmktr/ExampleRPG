using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : SceneBaseState {

    public override void OnEnter(Action callback = null)
    {
        SceneName = "MainScene";
        base.OnEnter(callback);

        LoadLevelAsync(SceneName);
    }

    public override void OnExit(Action callback = null)
    {
        base.OnExit(callback);

        CameraSetting();

        EnterMain();
    }

    //void OnLevelWasLoaded(int level)
    //{
    //    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == SceneName)
    //    {
    //        CameraSetting();

    //        Debug.Log("Enter Main");
    //        EnterMain();
    //    }
    //}

    void EnterMain()
    {
    }

    void CameraSetting()
    {
        Screen.SetResolution(1280, 720, true);
    }
}

