using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeSensorManager : MonoBehaviour
{
    private GameObject _Monkey;
    private MonkeyManager _MonkeyManager;

    void Awake()
    {
        _Monkey = transform.parent.gameObject;
        _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _MonkeyManager.TurnSet();
    }
}
