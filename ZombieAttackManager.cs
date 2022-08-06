using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackManager : MonoBehaviour
{
    //ゾンビオブジェクト
    private GameObject _Zombie;
    //ゾンビスクリプト
    private ZombieManager _ZombieManager;
    //プレイヤーオブジェクト
    private GameObject _Player;
    //プレイヤースクリプト
    private PlayerManager _PlayerManager;

    void Awake()
    {
        _Zombie = transform.root.gameObject;
        _ZombieManager = _Zombie.GetComponent<ZombieManager>();
        _Player = GameObject.Find("Player");
        _PlayerManager = _Player.GetComponent<PlayerManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_ZombieManager._st==3 && other.gameObject.name=="Player" && _PlayerManager._st!=6)
        {
            _PlayerManager.DeathSet();
        }
    }
}
