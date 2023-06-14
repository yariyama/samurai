using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animator;
    //�X�P�[��
    private Vector2 _scale;
    //�R���C�_�[
    private BoxCollider2D _collider;
    //���W�b�h�{�f�B
    private Rigidbody2D _rbody;
    //�����_���[
    private SpriteRenderer _renderer;
    //�J���[
    private Color _color;

    //�X�e�[�^�X
    public int _st;
    //�ړ��X�s�[�h
    public float _w_speed;
    //x����
    private int _dire_x;
    //�^�C�}�[
    private float _timer;
    //�����_���l
    private int _ran;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�U�����
    //_st=4-�_���[�W
    //_st=5-��

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
        _collider = GetComponent<BoxCollider2D>();
        _rbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
        _timer = 0;

        _scale.x = 1.3f;
        transform.localScale = _scale;

        _animator.Play("Base");
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
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
            if (_dire_x==1)
            {
                transform.Translate(_w_speed/50,0,0);
            }
            else
            {
                transform.Translate(-_w_speed / 50, 0, 0);
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");

                if (_dire_x==1)
                {
                    _dire_x = 2;
                    _scale.x = -1.3f;
                }
                else
                {
                    _dire_x = 1;
                    _scale.x = 1.3f;
                }
                transform.localScale = _scale;
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _timer =0;
                _st = 5;
                _collider.enabled = false;
                //_rbody.gravityScale = 0;
                _rbody.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
        else if (_st==5)
        {
            _color.a -= 0.1f;
            if (_color.a<=0)
            {
                Destroy(this.gameObject);
            }
            _renderer.color = _color;
        }
    }

    public void TurnSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("Turn");
    }

    public void DameSet()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("Dame");
    }
}
