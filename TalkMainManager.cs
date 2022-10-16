using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMainManager : MonoBehaviour
{
    //�����o���I�u�W�F�N�g
    private GameObject[] _Fukidashi = new GameObject[3];
    //�����o���X�N���v�g
    private FukidashiManager[] _FukidashiManager = new FukidashiManager[3];

    //�X�e�[�^�X
    public int _st;
    //�g�[�NNO
    public int _talk_no;

    //_st=1-��{�`
    //_st=2-�b����


    private void Awake()
    {
        for (int i=0;i<3;i++)
        {
            _Fukidashi[i] = GameObject.Find("Fukidashi"+(i+1));
            _FukidashiManager[i] = _Fukidashi[i].GetComponent<FukidashiManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 0;
        _talk_no = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FukidashiSet(int _no1,int _no2)
    {
        ++_talk_no;
        if (_talk_no==4)
        {
            _talk_no = 1;
        }
        _FukidashiManager[_talk_no - 1].ActiveSet(_no1,_no2);
    }
}
