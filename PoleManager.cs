using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    //�u���b�N�I�u�W�F�N�g
    private GameObject _Block;
    //�u���b�N�X�N���v�g
    private BlockManager _BlockManager;

    //�X�e�[�^�X
    private int _st;
    //�q�b�g�̉�
    private bool _hit_st;

    //_st=1-��{�`

    void OnTriggerStay(Collider other)
    {
        if (_hit_st)
        {
            if (other.gameObject.tag=="Block")
            {
                _hit_st = false;
                _Block = other.gameObject;
                _BlockManager = _Block.GetComponent<BlockManager>();
                _BlockManager.DameSet();
            }
        }
    }

    public void ActiveSet()
    {
        _st = 1;
        _hit_st = true;
    }
}
