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

    //�t�F�C�X�I�u�W�F�N�g
    private GameObject[] _Face = new GameObject[2];

    void Awake()
    {
        _Score = transform.Find("Score").gameObject;
        _score_text = _Score.GetComponent<Text>();

        for (int i=0; i<2; i++)
        {
            _Face[i] = transform.Find("Face"+(i+1)).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _score_text.text = GameManager._score.ToString("0000");

        if (GameManager._zan_c<=1)
        {
            _Face[1].SetActive(false);
        }
        if (GameManager._zan_c == 0)
        {
            _Face[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
