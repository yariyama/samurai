using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;
    //サルオブジェクト
    private GameObject _Monkey;
    //サルスクリプト
    private MonkeyManager _MonkeyManager;

    void Awake()
    {
        _Monkey = transform.parent.gameObject;
        _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda")
        {
            Debug.Log(100);
            _Panda = collision.gameObject;
            _PandaManager = _Panda.GetComponent<PandaManager>();
            if (!_PandaManager._jump_down && _PandaManager._st!=9 && _PandaManager._st != 10 && _MonkeyManager._st!=4 && _MonkeyManager._st != 5)
            {
                _PandaManager.DameSet();
            }
        }
    }
}
