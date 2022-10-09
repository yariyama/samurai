using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMainManager : MonoBehaviour
{
    //吹き出しオブジェクト
    private GameObject[] _Fukidashi = new GameObject[3];
    //吹き出しスクリプト
    private FukidashiManager[] _FukidashiManager = new FukidashiManager[3];

    //トークNO
    public int _talk_no;


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
