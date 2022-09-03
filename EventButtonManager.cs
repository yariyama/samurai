using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventButtonManager : MonoBehaviour
{
    //�C�x���g�{�^���I�u�W�F�N�g
    private GameObject[] _EventButton = new GameObject[3];
    //�C�x���g�{�^���X�N���v�g
    private EventButtonManager[] _EventButtonManager = new EventButtonManager[3];
    //�}���I�u�W�F�N�g
    private GameObject _Man;
    //�}���X�N���v�g
    private ManManager _ManManager;
    //�f�����C��UI�I�u�W�F�N�g
    private GameObject _DemoMainUI;
    //�f�����C��UI�X�N���v�g
    private DemoMainUIManager _DemoMainUIManager;

    //�X�P�[��
    private Vector2 _scale;
    //���W
    private Vector2 _position;

    //�X�e�[�^�X
    public int _st;
    //�o�[�W����
    public int _ver;
    //�^�C�}�[
    private float _timer;
    //�Z�b�gx���W
    public float _set_x;
    //�Z�b�gy���W
    public float _set_y;


    //_st=1-��{�`
    //_st=2-�v�b�V��
    //_st=3-����
    //_st=4-�A�E�g�X���C�h
    //_st=5-�A�E�g
    //_st=6-�C���X���C�h

    void Awake()
    {
        _scale = transform.localScale;
        _position = transform.localPosition;

        _DemoMainUI = transform.parent.gameObject;
        _DemoMainUIManager = _DemoMainUI.GetComponent<DemoMainUIManager>();

        int i2 = 0;
        for (int i=1;i<=4;i++)
        {
            if (_ver!=i)
            {
                _EventButton[i2] = GameObject.Find("EventButton"+i);
                _EventButtonManager[i2] = _EventButton[i2].GetComponent<EventButtonManager>();
                ++i2;
            }
        }

        _Man = GameObject.Find("Man1");
        _ManManager = _Man.GetComponent<ManManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
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
                _scale.x = 1;
                _scale.y = 1;
                transform.localScale = _scale;
            }

            if (_ver==4)
            {
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                for (int i=0;i<3;i++)
                {
                    _EventButtonManager[i].OutSet();
                }
                _ManManager.OutSet(_ver);

                _DemoMainUIManager._mode_st = _ver;
            }
        }
        else if (_st==4)
        {
            _position.x -= 40;
            if (_position.x<=-700)
            {
                _st = 5;
            }
            transform.localPosition = _position;
        }
        else if (_st == 6)
        {
            _position.x += 40;
            if (_position.x >= _set_x)
            {
                _st = 1;
            }
            transform.localPosition = _position;
        }
    }

    public void ButtonPush()
    {
        if (_st==1 && _DemoMainUIManager._mode_st==0)
        {
            _st = 2;
            _timer = 0;
            _scale.x = 0.9f;
            _scale.y = 0.9f;
            transform.localScale = _scale;
        }
    }

    public void OutSet()
    {
        _st = 4;
    }

    public void InSet()
    {
        _st = 6;
    }
}
