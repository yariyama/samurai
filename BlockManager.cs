using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //�\��
    private MeshRenderer _renderer;
    //�}�e���A��
    private Material _material;
    //�e�N�X�`��
    public Texture _texture1;
    public Texture _texture2;
    public Texture _texture3;
    public Texture _texture4;

    //�X�e�[�^�X
    public int _st;
    //���C�t
    private int _life;

    //_st=1-��{�`

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _material = _renderer.material;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _life = 4;
        _material.mainTexture = _texture1;
    }

    //�_���[�W�Z�b�g
    public void DameSet()
    {
        --_life;
        if (_life==3)
        {
            _material.mainTexture = _texture2;
        }
        else if (_life == 2)
        {
            _material.mainTexture = _texture3;
        }
        else if (_life == 1)
        {
            _material.mainTexture = _texture4;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
