using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeAreaManager : MonoBehaviour
{
    //音符オブジェクト
    private GameObject _Note;
    //音符スクリプト
    private NoteManager _NoteManager;
    //セーフエリアスコアオブジェクト
    private GameObject _SafeAreaScore;
    //スコアオブジェクト
    private GameObject _Score;

    //表示
    private SpriteRenderer _renderer;
    //色
    private Color _color;
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;
    //スコアテキスト
    private Text _score_text;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;
    //プッシュステータス
    private bool _push_st;
    //ヒットステータス
    private bool _hit_st;

    //_st=1-基本形
    //_st=2-アクション
    //_st=3-失敗

    void Awake()
    {
        _SafeAreaScore = transform.Find("SafeAreaScore").gameObject;
        _Score = GameObject.Find("Score");

        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;
        _audio = GetComponent<AudioSource>();
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
        _push_st = false;
        _color.a = 0.4f;
        _renderer.color = _color;

        _SafeAreaScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if (_st==1 && !_push_st)
            {
                _push_st = true;
                _st = 2;
                _timer = 0;
                _hit_st = false;
                _color.a = 0.7f;
                _renderer.color = _color;
            }
        }
        else
        {
            _push_st = false;
        }
    }

    void FixedUpdate()
    {
        //アクション
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _timer = 0;
                if (_hit_st)
                {
                    _st = 1;
                    _color.a = 0.4f;
                }
                else
                {
                    _st = 3;
                    _color.r = 0.1f;
                    _color.g = 0.1f;
                    _color.b = 0.1f;

                    _audio.clip = _se1;
                    _audio.Play();

                    _SafeAreaScore.SetActive(true);

                    GameManager._score -= 20;
                    _score_text.text = GameManager._score.ToString();
                }
                _renderer.color = _color;
            }
        }
        //失敗
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _st = 1;
                _timer = 0;
                _color.r = 1;
                _color.g = 1;
                _color.b = 1;
                _color.a = 0.4f;
                _renderer.color = _color;

                _SafeAreaScore.SetActive(false);
            }
        }
    }

    //音符と当たった場合
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Note" && _st==2)
        {
            _Note = collision.gameObject;
            _NoteManager = _Note.GetComponent<NoteManager>();
            Debug.Log(100);
            if (_NoteManager._st==1)
            {
                
                _hit_st = true;
                _NoteManager.GoodSet();

                GameManager._score += 10;
                _score_text.text = GameManager._score.ToString();
            }
        }
    }
}
