using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBasePanel : MonoBehaviour {

    protected object[] parameters;
    protected UIPANELTYPE _MyPanelType = UIPANELTYPE.NONE;
    public UIPANELTYPE GetMyPanelType { get { return _MyPanelType; } }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        LateInit();
    }

    // 파라미터 세팅
    public void SetParameters(params object[] _params)
    {
        parameters.Initialize();
        parameters = _params;
    }

    public virtual void Init()
    {

    }
    public virtual void LateInit()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        Destroy(gameObject);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    public virtual void Prev()
    {

    }
}
