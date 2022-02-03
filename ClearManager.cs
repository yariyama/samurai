using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    //�N���A�X�R�A�I�u�W�F�N�g
    private GameObject _ClearScore;
    //�N���A�{�[�i�X�I�u�W�F�N�g
    private GameObject _ClearBonus;
    //�X�R�A�I�u�W�F�N�g
    private GameObject _Score;
    //�Q�[�����C���I�u�W�F�N�g
    private GameObject _GameMain;
    //�Q�[�����C���X�N���v�g
    private GameMainManager _GameMainManager;

    //���W
    private Vector2 _position;
    //�X�P�[��
    private Vector2 _scale;
    //�e�L�X�g
    private Text _text;
    //�N���A�X�R�A�e�L�X�g
    private Text _clear_score_text;
    //�N���A�{�[�i�X�e�L�X�g
    private Text _clear_bonus_text;
    //�X�R�A�e�L�X�g
    private Text _score_text;

    //�X�e�[�^�X
    private int _st;
    //�X�s�[�h
    public float _speed;
    //�^�C�}�[
    private float _timer;


    //_st=1-��{�`
    //_st=2-�X���C�h
    //_st=3-�g��

    void Awake()
    {
        _ClearScore = GameObject.Find("ClearScore");
        _ClearBonus = GameObject.Find("ClearBonus");
        _ClearScore.SetActive(false);
        _ClearBonus.SetActive(false);
        _Score = GameObject.Find("Score");
        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();

        _position = transform.localPosition;
        _scale = transform.localScale;
        _text = GetComponent<Text>();

        _clear_score_text = _ClearScore.GetComponent<Text>();
        _clear_bonus_text = _ClearBonus.GetComponent<Text>();
        _score_text = _Score.GetComponent<Text>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            if (_text.text == "CLEAR" && GameManager._stage < _GameMainManager._t_stage)
            {
                _timer += Time.deltaTime;
                if (_timer >= 3)
                {
                    ++GameManager._stage;
                    _GameMainManager.ActiveSet(GameManager._stage);
                    _ClearBonus.SetActive(false);
                    _ClearScore.SetActive(false);
                    this.gameObject.SetActive(false);


                }
            }
        }
        else if (_st == 2)
        {
            _position.x += _speed;
            if (_position.x >= 0)
            {
                _position.x = 0;
                _st = 1;
                _timer = 0;

                _ClearScore.SetActive(true);
                _clear_score_text.text = "SUCCESS " + (int)GameManager._success_per + "%";

                _ClearBonus.SetActive(true);
                _clear_bonus_text.text = "BONUS 10�~" + (int)GameManager._success_per;

                GameManager._score += GameManager._score + 10 * (int)GameManager._success_per;
                _score_text.text = GameManager._score.ToString();
            }
            transform.localPosition = _position;
        }
        else if (_st == 3)
        {
            _scale.y += 0.1f;
            if (_scale.y >= 1)
            {
                _scale.y = 1;
                _st = 1;
                _timer = 0;

                _ClearScore.SetActive(true);
                _clear_score_text.text = "SUCCESS " + (int)GameManager._success_per + "%";
            }
            transform.localScale = _scale;
        }
    }

    public void ActiveSet(int _no)
    {

        if (_no == 1)
        {
            _text.text = "CLEAR";
            _st = 2;
            _position.x = -640;
        }
        else
        {
            _text.text = "FAILED";
            _st = 3;
            _position.x = 0;
            _scale.y = 0;
            transform.localScale = _scale;
        }
        transform.localPosition = _position;
    }
}
