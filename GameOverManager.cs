using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    //�X�P�[��
    private Vector2 _scale;

    //�n�C�X�R�A�I�u�W�F�N�g
    private GameObject _HiScore;
    //�n�C�X�R�A�e�L�X�g
    private Text _hi_score_text;

    //�X�e�[�^�X
    public int _st;

    //_st=1-��{�`
    //_st=2-�Z�b�g

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
