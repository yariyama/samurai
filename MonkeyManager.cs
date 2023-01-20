using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animator;
    //�p�x
    private Vector3 _angle;

    //�X�e�[�^�X
    public int _st;
    //�ړ��X�s�[�h
    public float _w_speed;
    //����
    private int _dire;
    //�^�C�}�[
    private float _timer;
    //�����_���l
    private int _ran;

    //_st=1-��{�`
    //_st=2-�ړ�

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _angle = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer = 0;
        _animator.Play("Base");
        _angle.y = 270;
        transform.localEulerAngles = _angle;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _ran = Random.Range(0,5);
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
            transform.Translate(0,0,_w_speed/50);
        }
    }
}
