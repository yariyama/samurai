using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSetManager : MonoBehaviour
{
    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形

    void FixedUpdate()
    {
        if (_st == 1)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ActiveSet()
    {
        _st = 1;
        _timer = 0;
    }
}
