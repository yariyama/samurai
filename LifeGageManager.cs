using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGageManager : MonoBehaviour
{
    //�܂�I�u�W�F�N�g
    private GameObject _Maru;

    //�X�e�[�^�X
    private int _st;
    //�o�[�W����
    public int _ver;

    //_st=1-��{�`
    //_st=2-�܂�

    void Awake()
    {
        _Maru = transform.Find("Maru").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_ver!=1)
        {
            _st = 1;
            _Maru.SetActive(false);
        }
        else
        {
            _st = 2;
        }
    }

    public void MaruSet()
    {
        _st = 2;
        _Maru.SetActive(true);
    }
}
