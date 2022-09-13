using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //ハイスコアオブジェクト
    private GameObject _HiScore;

    //オーディオソース
    private AudioSource _audio;
    //BGM
    public AudioClip _bgm1;
    //ハイスコアテキスト
    private Text _hi_score_text;

    //ライフ
    public static float _life;
    //スコア
    public static int _score;
    //ハイスコア
    public static int _hi_score;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();

        _hi_score = PlayerPrefs.GetInt("HiScore", 0);
        _HiScore = GameObject.Find("HiScore");
        _hi_score_text = _HiScore.GetComponent<Text>();

        //PlayerPrefs.DeleteKey("HiScore");

        //PlayerPrefs.DeleteAll();
    }

    // Start is called before the first frame update
    void Start()
    {
        _life = 3;
        _score = 0;

        _audio.clip = _bgm1;
        _audio.Play();

        _hi_score_text.text = GameManager._hi_score.ToString("0000");
    }

    public void BGMStop()
    {
        _audio.Stop();
    }
}
