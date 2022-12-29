using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animtor;

    //�ړ���x
    private float _vx;
    //�ړ���z
    private float _vz;
    //�X�s�[�h
    public float _speed;

    //�X�e�[�^�X
    private int _st;

    //_st=1-��{�`
    //_st=2-��]

    void Awake()
    {
        _animtor = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animtor.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(_vx / 50, 0, _vz / 50);
    }
}
