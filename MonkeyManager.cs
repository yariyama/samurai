using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //�p�x
    private Vector3 _angle;
    //�A�j���[�^�[
    private Animator _animator;

    //�X�s�[�h
    public float _speed;
    //�X�e�[�^�X
    private int _st;
    //�^�C�}�[
    private float _timer;
    //�����_���l
    private int _ran;

    //_st=1-��{�`
    //_st=2-�ړ�

    void Awake()
    {
        _angle = this.transform.localEulerAngles;
        _animator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _angle.y = 270;
        transform.localEulerAngles = _angle;
        _animator.Play("Base");
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _ran = Random.Range(0,10);
                if (_ran==1)
                {
                    _timer = 0;
                    _st = 2;
                    _animator.Play("Walk");
                }
            }
        }
        else if (_st==2)
        {
            transform.Translate(0,0,_speed/50);
        }
    }
}
