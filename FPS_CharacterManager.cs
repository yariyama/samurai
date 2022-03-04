using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_CharacterManager : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    private GameObject _Player;
    //�v���C���[�X�N���v�g
    private PlayerManager _PlayerManager;

    //���W
    private Vector3 _position;

    //�X�e�[�^�X
    private int _st;
    //�J�E���g
    private int _count;


    //_st=1-��{�`
    //_st=2-�V���b�g

    void Awake()
    {
        _Player = transform.parent.gameObject;
        _PlayerManager = _Player.GetComponent<PlayerManager>();

        _position = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            if (_count==0)
            {
                _position.z -= 0.02f;
                if (_position.z<=-0.06f)
                {
                    _position.z = -0.06f;
                    _count = 1;
                }
            }
            else
            {
                _position.z += 0.02f;
                if (_position.z >= 0)
                {
                    _position.z = 0;
                    _st = 1;
                    _count = 0;

                    _PlayerManager.BaseSet();
                }
            }

            transform.localPosition = _position;
        }
    }

    public void ShootSet()
    {
        _st = 2;
    }
}
