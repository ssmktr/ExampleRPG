using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TinyJSON;

public class DataManager : Singleton<DataManager> {

    List<UnitInfo> ListUnitInfo = new List<UnitInfo>();

    public void AllLoadTable()
    {
        LoadUnitInfo();
    }

    // 유닛 정보 불러오기
    void LoadUnitInfo()
    {
        ListUnitInfo.Clear();
        TextAsset JsonData = Resources.Load("Tables/UnitInfo") as TextAsset;
        Variant jsonData = TinyJSON.JSON.Load(JsonData.text);
        TinyJSON.JSON.MakeInto(jsonData, out ListUnitInfo);
    }

    // 유닛정보 아이디로 가져오기
    public UnitInfo GetUnitInfo(int id)
    {
        return ListUnitInfo.Find(data => data.id == id);
    }
}

public class UnitInfo
{
    public int id = 0;
    public string modelname = "";
    public UnitType playertype = UnitType.Hero;
    public float collidersize = 0f;
    public float movespeed = 0;
    public float hp = 0f;
    public float mp = 0f;
    public int atk = 0;
    public int def = 0;
    public float cri = 0f;
    public float cridam = 0f;

    public void Set(UnitInfo data)
    {
        if (data == null)
            return;

        id = data.id;
        modelname = data.modelname;
        playertype = data.playertype;
        collidersize = data.collidersize;
        movespeed = data.movespeed;
        hp = data.hp;
        mp = data.mp;
        atk = data.atk;
        def = data.def;
        cri = data.cri;
        cri = data.cridam;
    }
}
