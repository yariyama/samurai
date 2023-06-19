using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAttackHitManager : MonoBehaviour
{
    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    void Awake()
    {
        _Monkey = transform.parent.gameObject;
        _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda")
        {
            _Panda = collision.gameObject;
            _PandaManager = _Panda.GetComponent<PandaManager>();

            if (_MonkeyManager._st<=3 && _PandaManager._st!=5 && _PandaManager._st!=9)
            {
                _PandaManager.DameSet();
            }
        }
    }
}
