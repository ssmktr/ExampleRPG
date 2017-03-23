using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _STATE
{
    START = 0,
    MAIN,
    GAME,
}

public enum _ACTION
{
    GO_MAIN,
    GO_GAME,
}

public partial class SceneManagerCustom : MonoBehaviour
{
    FSM.FSM<_ACTION, _STATE, SceneManagerCustom> FSM = null;

    void Init_FSM()
    {
        FSM = new FSM.FSM<_ACTION, _STATE, SceneManagerCustom>(this);

        InitFSM_ForClient();
    }

    void InitFSM_ForClient()
    {
        FSM.AddState(_STATE.START, gameObject.AddComponent<StartState>());
        FSM.AddState(_STATE.MAIN, gameObject.AddComponent<MainState>());

        FSM.RegistEvent(_STATE.START, _ACTION.GO_MAIN, _STATE.MAIN);

        FSM.Enable(_STATE.START);
    }

    public _STATE CurState()
    {
        return FSM._Current_State;
    }

    public SceneBaseState CurrentBaseState()
    {
        return (SceneBaseState)FSM.Current();
    }

    public T GetState<T>() where T : SceneBaseState
    {
        return gameObject.GetComponent<T>();
    }

    public void ActionEvent(_ACTION NewAction)
    {
        FSM.ChangeState(NewAction);
    }
}
