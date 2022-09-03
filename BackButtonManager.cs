using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonManager : MonoBehaviour
{
    //�C�x���g�{�^���I�u�W�F�N�g
    private GameObject[] _EventButton = new GameObject[4];
    //�C�x���g�{�^���X�N���v�g
    private EventButtonManager[] _EventButtonManager = new EventButtonManager[4];
    //�f�����C��UI�I�u�W�F�N�g
    private GameObject _DemoMainUI;
    //�f�����C��UI�X�N���v�g
    private DemoMainUIManager _DemoMainUIManager;
    //�}���I�u�W�F�N�g
    private GameObject _Man;
    //�}���X�N���v�g
    private ManManager _ManManager;

    //�X�P�[��
    private Vector2 _scale;

    //�X�e�[�^�X
    public int _st;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�v�b�V��


    void Awake()
    {
        _scale = transform.localScale;

        for (int i=0;i<4;i++)
        {
            _EventButton[i] = GameObject.Find("EventButton"+(i+1));
            _EventButtonManager[i] = _EventButton[i].GetComponent<EventButtonManager>();
        }
        _DemoMainUI = transform.parent.gameObject;
        _DemoMainUIManager = _DemoMainUI.GetComponent<DemoMainUIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _st = 0;
                _timer = 0;
                _scale.x = 1;
                _scale.y = 1;
                transform.localScale = _scale;

                for (int i=0; i<4;i++)
                {
                    if (_EventButtonManager[i]._st==5)
                    {
                        _EventButtonManager[i].InSet();
                    }
                }

                _Man = GameObject.Find("Man"+(_DemoMainUIManager._mode_st+1));
                _ManManager = _Man.GetComponent<ManManager>();
                _ManManager.OutSet(0);
                _DemoMainUIManager._mode_st = 0;

                this.gameObject.SetActive(false);
            }
        }
    }

    public void ButtonPush()
    {
        if (_st==1)
        {
            _st = 2;
            _timer = 0;
            _scale.x = 0.9f;
            _scale.y = 0.9f;
            transform.localScale = _scale;
        }
    }

    public void ActiveSet()
    {
        _st = 1;
    }
}
