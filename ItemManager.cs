using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //�p���_�I�u�W�F�N�g
    private GameObject _Panda;
    //�p���_�X�N���v�g
    private PandaManager _PandaManager;
    //�X�R�A�I�u�W�F�N�g
    private GameObject _Score;

    //�A�j���[�^�[
    private Animator _animator;
    //�X�R�A�e�L�X�g
    private Text _score_text;

    //�X�e�[�^�X
    private int _st;
    //�^�C�v
    public int _tp;

    //_st=1-��{�`
    //_st=2-�Q�b�g

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
