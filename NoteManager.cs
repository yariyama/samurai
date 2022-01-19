using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //アニメーション
    private Animator _animator;

    //バージョン
    public int _ver;
    //ステータス
    public int _st;
    //ラインNO
    private int _line_no;
    //色タイプ
    private int _color_tp;

    //ノーマルスピード
    public float _n_speed=5;

    //_st=1-基本移動

    void Awake()
    {
        _position = transform.position;
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //基本移動
        if (_st==1)
        {
            transform.Translate(-_n_speed/50,0,0);
        }
    }

    //アクティブ化
    public void ActiveSet(int _no1)
    {
        _line_no = _no1;
        _st = 1;
        _color_tp = Random.Range(1,4);
        if (_color_tp==1) {
            _animator.Play("red_base");
        }
        else if (_color_tp == 2)
        {
            _animator.Play("blue_base");
        }
        else if (_color_tp == 3)
        {
            _animator.Play("yellow_base");
        }

        _position.x = 7.6f;
        _position.y = 3.5f-2*(_line_no-1);
        transform.position = _position;
    }
}
