using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //FPS�L�����N�^�[�I�u�W�F�N�g
    private GameObject _FPS_Character;
    //FPS�L�����N�^�[�X�N���v�g
    private FPS_CharacterManager _FPS_CharacterManager;

    //�A�j���[�^�[
    private Animator _animator;
    //���W
    private Vector3 _position;

    //�X�e�[�^�X
    private int _st;
    //�ړ��X�s�[�h
    public float _speed;
    //�ړ���X
    private float _vx;
    //�ړ���Z
    private float _vz;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�V���b�g

    void Awake()
    {
        _FPS_Character = transform.Find("FPS_Character").gameObject;
        _FPS_CharacterManager = _FPS_Character.GetComponent<FPS_CharacterManager>();

        _animator = GetComponent<Animator>();
        _position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _vx = 0;
        _vz = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }
        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

        if (Input.GetKey("space"))
        {
            if (_st==1)
            {
                _st = 3;
                _animator.Play("Character_AimPose");
                _FPS_CharacterManager.ShootSet();
            }
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0||_vz!=0)
            {
                _st = 2;
                _animator.Play("Character_Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx==0 && _vz==0)
            {
                BaseSet();
            }
        }

        //�ړ�
        if (_vx != 0 || _vz != 0)
        {
            transform.Translate(_vx/50,0,_vz/50);
            _position = transform.position;
            _position.y = 0;
            transform.position = _position;
        }
    }

    public void BaseSet()
    {
        _st = 1;
        _animator.Play("Character_Idle");
    }
}
