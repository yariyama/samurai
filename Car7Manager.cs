using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car7Manager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //BGMスクリプト
    private BGMManager _BGMManager;

    //座標
    private Vector2 _position;
    //表示
    private SpriteRenderer _renderer;
    //色
    private Color _color;
    //オーディオソース
    public AudioSource _audio;
    //効果音
    public AudioClip _se1;
    public AudioClip _se2;

    //ステータス
    private int _st;
    //スピード
    public float _speed;
    //カウント
    private int _count;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-消滅

    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _BGMManager = _GameMain.GetComponent<BGMManager>();

        _position = transform.position;
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
    }

    void FixedUpdate()
    {
        if (_st == 2)
        {
            //移動
            _position.x += _speed/50;
            transform.position = _position;
        }
        else if (_st == 3)
        {
            //消滅
            _color.a -= 0.05f;
            if (_count==0 && _color.a<=0.5)
            {
                _count = 1;
                //フェードアウト音
                _audio.clip = _se2;
                _audio.Play();
            }
            if (_color.a <= 0)
            {
                this.gameObject.SetActive(false);
            }
            _renderer.color = _color;
        }
    }

    void OnMouseDown()
    {
        _st = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _st = 3;

        //ヒット音
        _audio.clip = _se1;
        _audio.Play();

        _BGMManager._audio.Stop();
    }
}
