using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTitleManager : MonoBehaviour
{
    //トークマスクオブジェクト
    private GameObject _TalkMask;
    //トークマスクスクリプト
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
