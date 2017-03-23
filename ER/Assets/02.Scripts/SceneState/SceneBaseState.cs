﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBaseState : FSM.BaseState<SceneManagerCustom> {

    public string SceneName = "";
    public override void OnEnter(System.Action callback = null)
    {
        base.OnEnter(callback);
    }

    public override void OnExit(System.Action callback = null)
    {
        base.OnExit(callback);
    }

    public void LoadLevelAsync(string _sceneName, System.Action callback = null)
    {
        StartCoroutine(_LoadLevelAsync(_sceneName, callback));
    }

    IEnumerator _LoadLevelAsync(string _sceneName, System.Action callback = null)
    {
        yield return new WaitForSeconds(0.5f);

        // 같은 씬인지 체크
        if (SceneManager.GetActiveScene().name == _sceneName)
            GameHelper.DevDebugLog("같은 씬 입니다", LOGSTATE.WARRING);
        else
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            AsyncOperation async = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);

            while (!async.isDone)
            {
                GameHelper.DevDebugLog(async.progress);
                yield return null;
            }

            Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
            if (callback != null)
                callback();
        }
    }
}
