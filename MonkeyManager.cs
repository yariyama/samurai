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
    private BoxCollider2D _collder;
    //リジッドボディ
    private Rigidbody2D _rbody;
    //表示
    private SpriteRenderer _render;
    //色
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
        _collder = GetComponent<BoxCollider2D>();
        _rbody = GetComponent<Rigidbody2D>();
        _render = GetComponent<SpriteRenderer>();
        _color = _render.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
        _scale.x = 1.3f;
        transform.localScale = _scale;
        _timer = 0;
        _animator.Play("base");
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
                    _animator.Play("walk");
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
            if (_timer>=0.1f)
            {
                _st = 1;
                _timer = 0;
                _animator.Play("base");
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
                _st = 5;
                _timer = 0;
                _collder.enabled = false;
                _rbody.gravityScale = 0;
            }
        }
        else if (_st==5)
        {
            _color.a -= 0.1f;
            if (_color.a<=0)
            {
                Destroy(this.gameObject);
            }
            _render.color = _color;
        }
    }

    public void TurnSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("turn");
    }

    public void DameSet()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("dame");
    }
}
