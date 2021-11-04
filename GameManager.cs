using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ブロックプレファブ
    private GameObject _Block_pf;
    //ブロックオブジェクト
    private GameObject _Block;
    //ブロックスクリプト
    private BlockManager _BlockManager;

    //オーディオソース★
    private AudioSource _audio;
    //BGM★
    public AudioClip _bgm;

    //ブロックカウント
    public static int _block_c=0;

    //ブロックコレクション
    public static List<bool> _block_st = new List<bool>();
    public static List<float> _block_px = new List<float>();
    public static List<float> _block_py = new List<float>();
    public static List<float> _block_pz = new List<float>();

    //★
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _block_c = PlayerPrefs.GetInt("BlockCount", 0);
        for (int i = 1; i <= _block_c; ++i)
        {
            _Block_pf = (GameObject)Resources.Load("Prefabs/Block");
            _Block = Instantiate(_Block_pf) as GameObject;
            _BlockManager = _Block.GetComponent<BlockManager>();
            _BlockManager._ver = i;
            _BlockManager._st = 1;
            _Block.transform.position = new Vector3(PlayerPrefs.GetFloat("BlockPx" + i, 0), PlayerPrefs.GetFloat("BlockPy" + i, 0), PlayerPrefs.GetFloat("BlockPz" + i, 0));

            _block_st.Add(true);
            _block_px.Add(PlayerPrefs.GetFloat("BlockPx" + i, 0));
            _block_py.Add(PlayerPrefs.GetFloat("BlockPy" + i, 0));
            _block_pz.Add(PlayerPrefs.GetFloat("BlockPz" + i, 0));
        }

        //★
        _audio.clip = _bgm;
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
