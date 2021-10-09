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
    //ラムダム値
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
        _dire_x = 1;
        _scale.x = 1.3f;
        transform.localScale = _scale;
        _timer = 0;
        _animator.Play("base");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
