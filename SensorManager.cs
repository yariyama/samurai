using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if ((_ZombieManager._st==1 || _ZombieManager._st==2)&&other.gameObject.tag=="Player")
        {
            _ZombieManager.AttackSet();
        }
    }
}
