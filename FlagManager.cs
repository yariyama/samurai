using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagManager : MonoBehaviour
{
    //�X�e�[�W�N���A�I�u�W�F�N�g
    private GameObject _StageClear;
    //�X�e�[�W�N���A�X�N���v�g
    private StageClearManager _StageClearManager;

    //�p���_�I�u�W�F�N�g
    private GameObject _Panda;
    //�p���_�X�N���v�g
    private PandaManager _PandaManager;

    //�X�e�[�^�X
    private int _st;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�N���A
    //_st=3-�V�[���`�����W

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
