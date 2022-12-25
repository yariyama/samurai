using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrawberryManager : MonoBehaviour
{
    //コライダー
    private BoxCollider _collider;
    //レンダラー
    private Renderer _renderer;
    //マテリアル
    private Material _material;
    //カラー
    private Color _color;

    //ヘタオブジェクト
    private GameObject _Heta;
    //ヘタレンダラー
    private Renderer _heta_renderer;
    //ヘタマテリアル
    private Material _heta_material;
    //ヘタカラー
    private Color _heta_color;

    //エフェクトオブジェクト
    private GameObject _Effect;
    //エフェクトアニメーター
    private Animator _effect_animator;

    //スコアオブジェクト
    private GameObject _Score;
    //スコアテキスト
    private Text _score_text;

    //ステータス
    public int _st;

    //_st=1-基本形
    //_st=2-ゲット

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _color = _material.color;

        _Heta = transform.Find("Heta").gameObject;
        _heta_renderer = _Heta.GetComponent<Renderer>();
        _heta_material = _heta_renderer.material;
        _heta_color = _heta_material.color;

        _Effect = transform.Find("Effect").gameObject;
        _effect_animator = _Effect.GetComponent<Animator>();
        _Effect.SetActive(false);

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _color.a -= 0.05f;
            _heta_color.a -= 0.05f;
            if (_color.a<=0)
            {
                _color.a = 0;
                _heta_color.a = 0;
                Destroy(this.gameObject);
            }
            _material.color = _color;
            _heta_material.color = _heta_color;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Panda" && _st==1)
        {
            _st = 2;
            _collider.enabled = false;

            _Effect.SetActive(true);
            _effect_animator.Play("Get");

            GameManager._score += 100;
            _score_text.text = GameManager._score.ToString("0000");
        }
    }
}
