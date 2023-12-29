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
    //����
    private int _dire;
    //��]�X�s�[�h
    public float _a_speed;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�^�[��

    //_dire=1-�E
    //_dire=2-��

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
        _dire = 1;
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
        else if (_st==3)
        {
            if (_dire==1)
            {
                transform.Rotate(0,-_a_speed/50,0);
                _angle = transform.localEulerAngles;
                if (_angle.y<=90)
                {
                    _angle.y = 90;
                    transform.localEulerAngles = _angle;
                    _st = 1;
                    _dire = 2;
                    _animator.Play("Base");
                }
            }
            else if (_dire==2)
            {
                transform.Rotate(0, _a_speed / 50, 0);
                _angle = transform.localEulerAngles;
                if (_angle.y >= 270)
                {
                    _angle.y = 270;
                    transform.localEulerAngles = _angle;
                    _st = 1;
                    _dire = 1;
                    _animator.Play("Base");
                }
            }
        }
    }

    //�^�[���؂芷��
    public void TurnSet()
    {
        _st = 3;
    }
}
