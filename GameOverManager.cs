using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private GameObject _ReplayButton;
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームマネージャー
    private GameManager _GameManager;
    //ハイスコアオブジェクト
    private GameObject _HiScore;

    private Vector2 _scale;
    //ハイスコアテキスト
    private Text _hi_score_text;

    private int _st;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        _scale = transform.localScale;

        _ReplayButton = GameObject.Find("ReplayButton");
        _ReplayButton.SetActive(false);

        _GameMain = GameObject.Find("GameMain");
        _GameManager = _GameMain.GetComponent<GameManager>();

        _HiScore = GameObject.Find("HiScore");
        _hi_score_text = _HiScore.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
                _ReplayButton.SetActive(true);
            }
            transform.localScale = _scale;
        }
    }

    //アクティブセット
    public void ActiveSet()
    {
        _st = 2;
        _scale.y = 0;
        transform.localScale = _scale;
        _GameManager.BGMStop();

        if (GameManager._score>GameManager._hi_score)
        {
            GameManager._hi_score = GameManager._score;
            _hi_score_text.text = GameManager._hi_score.ToString("0000");

            PlayerPrefs.SetInt("HiScore", GameManager._hi_score);
            PlayerPrefs.Save();
        }
    }
}
