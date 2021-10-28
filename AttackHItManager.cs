using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHItManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda")
        {
            _Panda = collision.gameObject;
            _PandaManager = _Panda.GetComponent<PandaManager>();
            if (_PandaManager._st!=9 && _PandaManager._st != 10 && !_PandaManager._jump_down)
            {
                _PandaManager.DameSet();
            }
        }
    }
}
