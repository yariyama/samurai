using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;
    //スコアオブジェクト
    private GameObject _Score;

    //アニメーター
    private Animator _animator;
    //スコアテキスト
    private Text _score_text;
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;
    public AudioClip _se2;

    //ステータス
    private int _st;
    //タイプ
    public int _tp;

    //_st=1-基本形
    //_st=2-ゲット

    void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("base");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda" && _st==1)
        {
            if (_tp==2)
            {
                _PandaManager.SDameSet();
                _audio.clip = _se2;
            }
            else
            {
                _audio.clip = _se1;

                GameManager._score += 10;
                _score_text.text = GameManager._score.ToString("0000");
            }
            _st = 2;
            _animator.Play("get");
            _audio.Play();

            
        }
    }

    void DelSet()
    {
        Destroy(this.gameObject);
    }
}
