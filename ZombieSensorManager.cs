using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSensorManager : MonoBehaviour
{
    //ゾンビオブジェクト
    private GameObject _Zombie;
    //ゾンビスクリプト
    private ZombieManager _ZombieManager;

    void Awake()
    {
        _Zombie = transform.parent.gameObject;
        _ZombieManager = _Zombie.GetComponent<ZombieManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_ZombieManager._st==1|| _ZombieManager._st == 2) && other.gameObject.name=="Player")
        {
            _ZombieManager.AttackSet();
        }
    }
}
