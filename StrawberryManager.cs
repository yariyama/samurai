using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrawberryManager : MonoBehaviour
{
    //�R���C�_�[
    private BoxCollider _collider;
    //�����_���[
    private Renderer _renderer;
    //�}�e���A��
    private Material _material;
    //�J���[
    private Color _color;

    //�w�^�I�u�W�F�N�g
    private GameObject _Heta;
    //�w�^�����_���[
    private Renderer _heta_renderer;
    //�w�^�}�e���A��
    private Material _heta_material;
    //�w�^�J���[
    private Color _heta_color;

    //�G�t�F�N�g�I�u�W�F�N�g
    private GameObject _Effect;
    //�G�t�F�N�g�A�j���[�^�[
    private Animator _effect_animator;

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
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _color = _material.color;

        _Heta = transform.Find("Heta").gameObject;
        _heta_renderer = _Heta.GetComponent<Renderer>();
        _heta_material = _heta_renderer.material;
        _heta_color = _heta_material.color;

        _Effect = transform.Find("Effect").gameObject;
        _effect_animator = _Effect.GetComponent<Animator>();
        _Effect.SetActive(false);

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _color.a -= 0.05f;
            _heta_color.a -= 0.05f;
            if (_color.a<=0)
            {
                _color.a = 0;
                _heta_color.a = 0;
                Destroy(this.gameObject);
            }
            _material.color = _color;
            _heta_material.color = _heta_color;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Panda" && _st==1)
        {
            _st = 2;
            _collider.enabled = false;

            _Effect.SetActive(true);
            _effect_animator.Play("Get");

            GameManager._score += 100;
            _score_text.text = GameManager._score.ToString("0000");
        }
    }
}
