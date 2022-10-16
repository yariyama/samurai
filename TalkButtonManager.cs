using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButtonManager : MonoBehaviour
{
    //�g�[�N�}�X�N�I�u�W�F�N�g
    private GameObject _TalkMask;
    //�g�[�N���C���X�N���v�g
    private TalkMainManager _TalkMainManager;

    //�X�P�[��
    private Vector2 _scale;

    //�X�e�[�^�X
    private int _st;
    //�o�[�W����
    public int _ver;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�v�b�V��


    void Awake()
    {
        _scale = transform.localScale;

        _TalkMask = transform.parent.gameObject;
        _TalkMainManager = _TalkMask.GetComponent<TalkMainManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _st = 1;
                _timer = 0;
                _scale.x = 1f;
                _scale.y = 1f;
                transform.localScale = _scale;

                _TalkMainManager.FukidashiSet(1,_ver);
            }
        }
    }

    public void ButtonPush()
    {
        if (_TalkMainManager._st==1) {
            _st = 2;
            _timer = 0;
            _scale.x = 0.9f;
            _scale.y = 0.9f;
            transform.localScale = _scale;
        }
    }
}
