using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackManager : MonoBehaviour
{
    //�]���r�I�u�W�F�N�g
    private GameObject _Zombie;
    //�]���r�X�N���v�g
    private ZombieManager _ZombieManager;
    //�v���C���[�I�u�W�F�N�g
    private GameObject _Player;
    //�v���C���[�X�N���v�g
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
