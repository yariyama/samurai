using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-反応

    void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_st==1 && collision.gameObject.name=="Panda")
        {
            _st = 2;
            _PandaManager.FadeOut();
        }
    }

}
