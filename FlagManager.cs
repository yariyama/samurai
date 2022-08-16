using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    private void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda" && _PandaManager._st>=1 && _PandaManager._st <= 8)
        {
            _PandaManager.WinSet();
        }
    }
}
