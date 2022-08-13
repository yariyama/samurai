using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPointManager : MonoBehaviour
{
    //テキスト
    private Text _text;

    void Awake()
    {
        _text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _text.text = GameManager._get_point.ToString("0000");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
