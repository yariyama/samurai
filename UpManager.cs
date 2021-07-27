using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpManager : MonoBehaviour
{
    private GameObject _Cloud1;
    private Cloud1Manager _Cloud1Manager;

    public int _ver;

    void Awake()
    {
        _Cloud1 = GameObject.Find("Cloud1");
        _Cloud1Manager = _Cloud1.GetComponent<Cloud1Manager>();
    }

    void OnMouseDown()
    {
        if (_ver==1) {
            _Cloud1Manager.MoveSet(2);
        }
        else
        {
            _Cloud1Manager.MoveSet(1);
        }
    }
}
