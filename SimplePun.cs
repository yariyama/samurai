using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class SimplePun : MonoBehaviourPunCallbacks
{

    // Use this for initialization
    void Start()
    {
        //���o�[�W�����ł͈����K�{�ł������APUN2�ł͕s�v�ł��B
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnGUI()
    {
        //���O�C���̏�Ԃ���ʏ�ɏo��
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //���[���ɓ�����ɌĂяo�����
    public override void OnJoinedRoom()
    {
        //�L�����N�^�[�𐶐�
        GameObject monster = PhotonNetwork.Instantiate("monster", Vector3.zero, Quaternion.identity, 0);
        //��������������ł���悤�ɃX�N���v�g��L���ɂ���
        MonsterScript monsterScript = monster.GetComponent<MonsterScript>();
        monsterScript.enabled = true;
    }
}

