using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Stage01 : SceneBaseState
{
    public override void OnEnter(Action callback = null)
    {
        base.OnEnter(callback);

        Debug.Log("Enter Game");
    }

    //private void OnLevelWasLoaded(int level)
    //{

    //}

    public override void OnExit(Action callback = null)
    {
        base.OnExit(callback);
    }
}
