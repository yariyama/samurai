using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;
    //スケール
    private Vector2 _scale;
    //コライダー
    private BoxCollider2D _collider;
    //リジッドボディ
    private Rigidbody2D _rbody;
    //レンダラー
    private SpriteRenderer _renderer;
    //カラー
    private Color _color;

    //ステータス
    public int _st;
    //移動スピード
    public float _w_speed;
    //x方向
    private int _dire_x;
    //タイマー
    private float _timer;
    //ランダム値
    private int _ran;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き
    //_st=4-ダメージ
    //_st=5-死

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
