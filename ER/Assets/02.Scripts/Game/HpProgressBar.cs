using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpProgressBar : MonoBehaviour {

    Transform _Parent = null;
    public UISlider _slider = null;

    public void Init(Transform parent, string name)
    {
        _Parent = parent;
        _slider = GetComponent<UISlider>();
        gameObject.name = name;
    }

    private void LateUpdate()
    {
        if (_Parent != null)
        {
            transform.localPosition = Camera.main.WorldToScreenPoint(_Parent.position) - new Vector3(640, 360, 0);
        }
    }
}
