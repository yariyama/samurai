using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrawberryManager : MonoBehaviour
{
    //�R���C�_�[
    private BoxCollider _collider;
    //�A�j���[�^�[
    private Animator _animator;

    //�X�R�A�I�u�W�F�N�g
    private GameObject _Score;
    //�X�R�A�e�L�X�g
    private Text _score_text;

    //�X�e�[�^�X
    public int _st;

    //_st=1-��{�`
    //_st=2-�Q�b�g

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DelSet()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Panda" && _st==1)
        {
            _st = 2;
            _collider.enabled = false;
            _animator.Play("Get");
            GameManager._score += 100;
            _score_text.text = GameManager._score.ToString("0000");
        }
    }
}
