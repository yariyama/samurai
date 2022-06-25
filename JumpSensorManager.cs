using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSensorManager : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    private GameObject _Player;
    //�v���C���[�X�N���v�g
    private PlayerManager _PlayerManager;

    //�X�e�[�^�X
    public int _st;

    void Awake()
    {
        _Player = transform.parent.gameObject;
        _PlayerManager = _Player.GetComponent<PlayerManager>();
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
        if (_st==1)
        {
            _PlayerManager.UpSet();
            _st = 0;
        }
    }
}
