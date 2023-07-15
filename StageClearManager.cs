using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    //���W
    private Vector2 _position;
    //�I�[�f�B�I�\�[�X
    private AudioSource _audio;
    //�X���C�h��
    public AudioClip _slide_se;

    //�X�e�[�^�X
    private int _st;

    //_st=1-��{�`
    //_st=2-�Z�b�g

    void Awake()
    {
        _position = transform.localPosition;
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _position.x -= 20;
            if (_position.x<=0)
            {
                _position.x = 0;
                _st = 1;
            }
            transform.localPosition = _position;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _position.x = 800;
        transform.localPosition = _position;

        _audio.clip = _slide_se;
        _audio.Play();
    }
}
