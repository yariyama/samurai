using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private GameObject _ReplayButton;
    //�Q�[�����C���I�u�W�F�N�g
    private GameObject _GameMain;
    //�Q�[���}�l�[�W���[
    private GameManager _GameManager;
    //�n�C�X�R�A�I�u�W�F�N�g
    private GameObject _HiScore;

    private Vector2 _scale;
    //�n�C�X�R�A�e�L�X�g
    private Text _hi_score_text;

    private int _st;

    //_st=1-��{�`
    //_st=2-�Z�b�g

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

    //�A�N�e�B�u�Z�b�g
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
