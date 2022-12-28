using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeySensorManager : MonoBehaviour
{
    //�����L�[�I�u�W�F�N�g
    private GameObject _Monkey;
    //�����L�[�X�N���v�g
    private MonkeyManager _MonkeyManager;


    void Awake()
    {
        _Monkey = transform.parent.gameObject;
        //_Monkey = GameObject.Find("Monkey");
        _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Cloud" && _MonkeyManager._st==2)
        {
            _MonkeyManager.TurnSet();
        }
    }
}
