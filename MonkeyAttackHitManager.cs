using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAttackHitManager : MonoBehaviour
{
    //�����L�[�I�u�W�F�N�g
    private GameObject _Monkey;
    //�����L�[�X�N���v�g
    private MonkeyManager _MonkeyManager;

    //�p���_�I�u�W�F�N�g
    private GameObject _Panda;
    //�p���_�X�N���v�g
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
