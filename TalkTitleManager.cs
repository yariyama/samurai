using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTitleManager : MonoBehaviour
{
    //�g�[�N�}�X�N�I�u�W�F�N�g
    private GameObject _TalkMask;
    //�g�[�N�}�X�N�X�N���v�g
    private TalkMaskManager _TalkMaskManager;


    private void Awake()
    {
        _TalkMask = transform.parent.gameObject;
        _TalkMaskManager = _TalkMask.GetComponent<TalkMaskManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPush()
    {
        if (_TalkMaskManager._st==2)
        {
            _TalkMaskManager.SlideSet(1);
        }
        else if (_TalkMaskManager._st == 1)
        {
            _TalkMaskManager.SlideSet(2);
        }
    }
}
