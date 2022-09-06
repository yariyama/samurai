using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //���W
    private Vector3 _position;
    //�A�j���[�^�[
    private Animator _animator;

    //�X�e�[�^�X
    public int _st;
    //�^�C�}�[
    private float _timer;
    //�ړ��X�s�[�h
    public float _speed;
    //�ړ���x
    private float _vx;
    //�^�[�Q�b�gx1
    private float _target_x1;
    //�^�[�Q�b�gx2
    private float _target_x2;
    //�ړ�����
    private int _dire;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�_���[�W

    void Awake()
    {
        _position = transform.position;
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
        _target_x1 = _position.x - 3;
        _target_x2 = _position.x + 3;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _timer =0;
                _st = 2;
                _dire = 1;
                _vx = _speed;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            transform.Translate(_vx/50,0,0);
            _position = transform.position;
            if (_dire==1 && _position.x<=_target_x1)
            {
                _dire = 2;
                _vx = -_speed;
            }
            else if (_dire==2 && _position.x>=_target_x2)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void DameSet()
    {
        _st = 3;
        _animator.Play("BaseDame");
    }
}
