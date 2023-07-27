using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    //スケール
    private Vector2 _scale;

    //ハイスコアオブジェクト
    private GameObject _HiScore;
    //ハイスコアテキスト
    private Text _hi_score_text;

    //ステータス
    public int _st;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        _scale = transform.localScale;

        _HiScore = GameObject.Find("HiScore");
        _hi_score_text = _HiScore.GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _scale.y += 0.1f;
            if (_scale.y>=1)
            {
                _scale.y = 1;
                _st = 1;
            }

            transform.localScale = _scale;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _scale.y = 0;
        transform.localScale = _scale;

        if (GameManager._score>GameManager._hi_score)
        {
            GameManager._hi_score = GameManager._score;
            PlayerPrefs.SetInt("HiScore", GameManager._hi_score);
            PlayerPrefs.Save();
            _hi_score_text.text = GameManager._hi_score.ToString("0000");
        }
    }
}
