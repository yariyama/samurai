using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    //ヒット状態
    private bool _hit_st;

    void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _hit_st = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda" && _hit_st && _PandaManager._st<=3)
        {
            _PandaManager.WinSet();
            _hit_st = false;
        }
    }
}
