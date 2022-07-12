using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;
    //スケール
    private Vector2 _scale;

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

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 2;
        _scale.x = -1.3f;
        _timer = 0;

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
                    _st = 2;
                    _timer = 0;
                    _animator.Play("Walk");
                }
            }
        }
        else if (_st==2)
        {
            if (_dire_x==1) {
                transform.Translate(_w_speed / 50, 0, 0);
            }
            else
            {
                transform.Translate(-_w_speed / 50, 0, 0);
            }
        }
        else if (_st == 3)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.1f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
                if (_dire_x == 1)
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
    }

    //振り向きセット
    public void TurnSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("Turn");
    }
}
