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
        _Score = GameObject.Find("Score");

        _animator = GetComponent<Animator>();
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda" && _st==1)
        {
            if (_tp==2)
            {
                _PandaManager.SDameSet();
            }
            else
            {
                GameManager._score += 10;
                _score_text.text = GameManager._score.ToString("0000");
            }

            _st = 2;
            _animator.Play("Get");
        }
    }
}
