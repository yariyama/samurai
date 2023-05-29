using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animator;
    //�X�P�[��
    private Vector2 _scale;

    //�X�e�[�^�X
    private int _st;
    //�ړ��X�s�[�h
    public float _w_speed;
    //x����
    private int _dire_x;
    //�^�C�}�[
    private float _timer;
    //�����_���l
    private int _ran;

    //_st=1-��{�`
    //_st=2-�ړ�

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
        _timer = 0;

        _scale.x = 1.3f;
        transform.localScale = _scale;

        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
