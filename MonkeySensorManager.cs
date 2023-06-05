using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeySensorManager : MonoBehaviour
{
    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

    void Awake()
    {
        _Monkey = transform.parent.gameObject;
        _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Cloud" && _MonkeyManager._st==2)
        {
            _MonkeyManager.TurnSet();
        }
    }
}
