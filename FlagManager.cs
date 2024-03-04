using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagManager : MonoBehaviour
{
    //ステージクリアオブジェクト
    private GameObject _StageClear;
    //ステージクリアスクリプト
    private StageClearManager _StageClearManager;

    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-クリア
    //_st=3-シーンチャンジ

    void Awake()
    {
        _StageClear = GameObject.Find("StageClear");
        _StageClearManager = _StageClear.GetComponent<StageClearManager>();

        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
        _StageClear.SetActive(false);
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=2)
            {
                _timer =0;
                _st = 3;
                ++GameManager._stage;
                SceneManager.LoadScene(GameManager._stage+"Stage");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player" && _st==1)
        {
            _st = 2;
            _StageClear.SetActive(true);
            _StageClearManager.ActiveSet();
            _PandaManager.ClearSet();
        }
    }
}
