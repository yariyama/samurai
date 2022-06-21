using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animator;

    //�X�e�[�^�X
    public int _st;
    //�ړ��X�s�[�h
    public float _w_speed;
    //x�ړ���
    private float _vx;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�U�����
    //_st=4-�W�����v�O
    //_st=5-�W�����v
    //_st=6-�W�����v��

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        if (Input.GetKey("right"))
        {
            _vx = _w_speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_w_speed;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }

        if (_vx!=0 && (_st==2||_st==4))
        {
            transform.Translate(_vx/50,0,0);
        }
    }
}
