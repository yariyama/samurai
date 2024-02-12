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

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Cube"|| other.gameObject.tag == "Cloud")
        {
            _MonkeyManager.TurnSet();
        }
    }
}
