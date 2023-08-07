using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    //ステージクリアオブジェクト
    private GameObject _StageClear;
    //ステージクリアスクリプト
    private StageClearManager _StageClearManager;

    //ステータス
    public int _st;
    //タイマー
    private float _timer;
    //ステージ
    public string _stage;

    //_st=1-基本形
    //_st=2-クリア

    private void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();

        _StageClear = GameObject.Find("StageClear");
        _StageClearManager = _StageClear.GetComponent<StageClearManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer =0;
        _StageClear.SetActive(false);
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=3)
            {
                _timer = 0;
                SceneManager.LoadScene(_stage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Panda" && _st==1)
        {
            _st = 2;
            _PandaManager.ClearSet();
            _StageClear.SetActive(true);
            _StageClearManager.ActiveSet();
        }
    }
}
