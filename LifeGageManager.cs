using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGageManager : MonoBehaviour
{
    //まるオブジェクト
    private GameObject _Maru;

    //ステータス
    private int _st;
    //バージョン
    public int _ver;

    //_st=1-基本形
    //_st=2-まる

    void Awake()
    {
        _Maru = transform.Find("Maru").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_ver!=1)
        {
            _st = 1;
            _Maru.SetActive(false);
        }
        else
        {
            _st = 2;
        }
    }

    public void MaruSet()
    {
        _st = 2;
        _Maru.SetActive(true);
    }
}
