using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainUIManager : MonoBehaviour
{
    //�X�R�A�I�u�W�F�N�g
    private GameObject _Score;
    //�X�R�A�e�L�X�g
    private Text _score_text;

    //���C�t�Q�[�W�I�u�W�F�N�g
    private GameObject[] _LifeGage = new GameObject[3];
    //���C�t�Q�[�W�X�N���v�g
    private LifeGageManager[] _LifeGageManager = new LifeGageManager[3];

    //�n�C�X�R�A�I�u�W�F�N�g
    private GameObject _HiScore;
    //�n�C�X�R�A�e�L�X�g
    private Text _hi_score_text;

    void Awake()
    {
        _Score = transform.Find("Score").gameObject;
        _score_text = _Score.GetComponent<Text>();

        for (int i = 0; i < 3; i++)
        {
            _LifeGage[i] = transform.Find("LifeGage" + (i + 1)).gameObject;
            _LifeGageManager[i] = _LifeGage[i].GetComponent<LifeGageManager>();
        }

        _HiScore = transform.Find("HiScore").gameObject;
        _hi_score_text = _HiScore.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _score_text.text = GameManager._score.ToString("0000");

        if (GameManager._zan_c == 2)
        {
            _LifeGage[0].SetActive(false);
            _LifeGageManager[1].MaruSet();
        }
        else if (GameManager._zan_c == 1)
        {
            _LifeGage[1].SetActive(false);
            _LifeGageManager[2].MaruSet();
        }
        else if (GameManager._zan_c == 0)
        {
            _LifeGage[2].SetActive(false);
        }

        _hi_score_text.text = GameManager._hi_score.ToString("0000");
    }
}
