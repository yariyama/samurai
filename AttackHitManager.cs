using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitManager : MonoBehaviour
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
        _Zombie = transform.parent.gameObject;
        _ZombieManager = _Zombie.GetComponent<ZombieManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (_ZombieManager._st == 4 && other.gameObject.tag == "Player")
        {
            Debug.Log(100);   
            _Player = other.gameObject;
            _PlayerManager = _Player.GetComponent<PlayerManager>();
            if (_PlayerManager._st!=4)
            {
                _PlayerManager.DameSet();
            }
        
        }
    }
}
