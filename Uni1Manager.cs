using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uni1Manager : MonoBehaviour
{
    //カウントゲームオブジェクト
    private GameObject _Count;

    //カウントテキスト
    private Text _count_text;

    //クリックカウント
    private int _click_c;

    void Awake()
    {
        _Count = GameObject.Find("Count");
        _count_text = _Count.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _click_c = 0;
    }

    void OnMouseDown()
    {
        ++_click_c;
        _count_text.text = _click_c.ToString("00");
    }
}
