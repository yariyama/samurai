using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    //ブロックオブジェクト
    private GameObject _Block;
    //ブロックスクリプト
    private BlockManager _BlockManager;
    //チップセットオブジェクト
    private GameObject _ChipSet;
    //チップセットスクリプト
    private ChipSetManager _ChipSetManager;

    //表示
    private MeshRenderer _renderer;
    //オーディオソース★
    private AudioSource _audio;
    //効果音★
    public AudioClip _se1;

    //ステータス
    private int _st;
    //ヒットステータス
    private bool _hit_st;

    //_st=1-基本形

    void Awake()
    {
        _ChipSet = GameObject.Find("ChipSet");
        _ChipSetManager = _ChipSet.GetComponent<ChipSetManager>();
        _ChipSet.SetActive(false);

        _renderer = GetComponent<MeshRenderer>();
        //★
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer.enabled = false;
        _st = 0;
        _hit_st = false;
    }

    //ブロックとの当たり判定
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Block" && _hit_st)
        {
            _hit_st = false;
            _Block = other.gameObject;
            _BlockManager = _Block.GetComponent<BlockManager>();
            _BlockManager.DameSet();

            _ChipSet.SetActive(true);
            _ChipSetManager.ActiveSet();

            Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            _ChipSet.transform.position = hitPos;

            //★
            _audio.clip = _se1;
            _audio.Play();
        }
    }

    //ポールのセット
    public void ActiveSet(int _no1, int _no2)
    {
        if (_no1 == 1)
        {
            _renderer.enabled = true;
            _st = 1;

            if (_no2 == 1)
            {
                transform.localPosition = new Vector3(-0.7f, 2.4f, 0f);
                transform.localEulerAngles = new Vector3(90, 0, 0);
            }
            else if (_no2 == 2)
            {
                transform.localPosition = new Vector3(-0.2f, 2.4f, 0.1f);
                transform.localEulerAngles = new Vector3(90, 0, 0);
            }
            else if (_no2 == 3)
            {
                transform.localPosition = new Vector3(0.2f, 2.4f, -0.1f);
                transform.localEulerAngles = new Vector3(90, 0, 0);
            }
            else if (_no2 == 4)
            {
                transform.localPosition = new Vector3(0.6f, 2.4f, 0f);
                transform.localEulerAngles = new Vector3(90, 0, 0);
            }
        }
        else
        {
            _hit_st = true;
            if (_no2 == 1)
            {
                transform.localPosition = new Vector3(-0.4f, 0.9f, -0.5f);
                transform.localEulerAngles = new Vector3(-15, -10, 0);
            }
            else if (_no2 == 2)
            {
                transform.localPosition = new Vector3(-1.1f, 0.9f, 0.1f);
                transform.localEulerAngles = new Vector3(-15, 80, 0);
            }
            else if (_no2 == 3)
            {
                transform.localPosition = new Vector3(1.1f, 1f, -0.1f);
                transform.localEulerAngles = new Vector3(25, 80, 0);
            }
            else if (_no2 == 4)
            {
                transform.localPosition = new Vector3(0.4f, 1.1f, 0.5f);
                transform.localEulerAngles = new Vector3(20, -10, 0);
            }
        }
    }

    //隠しセット
    public void DelSet()
    {
        _hit_st = false;
        _renderer.enabled = false;
        _st = 0;
    }
}
