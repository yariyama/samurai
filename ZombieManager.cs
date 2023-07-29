using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animator;
    //�i�r���b�V���G�[�W�F���g
    private NavMeshAgent _agent;

    //�^�[�Q�b�g�I�u�W�F�N�g
    private GameObject _Target;

    //�X�e�[�^�X
    public int _st;
    //�^�C�}�[
    private float _timer;
    //�����_���l
    private int _ran;

    //_st=1-��{�`
    //_st=2-�ړ�

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        _Target = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
        _animator.Play("Idle");
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=3)
            {
                _ran = Random.Range(0,10);
                if (_ran==1)
                {
                    _timer =0;
                    _st = 2;
                    _animator.Play("Walking");
                }
            }
        }
        else if (_st==2)
        {
            _agent.destination = _Target.transform.position;

            _timer += Time.deltaTime;
            if (_timer>=10)
            {
                _ran = Random.Range(0,10);
                if (_ran==1)
                {
                    _timer = 0;
                    _st = 1;
                    _animator.Play("Idle");
                    _agent.destination = transform.position;
                }
            }
        }
    }
}
