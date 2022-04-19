using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;//�ؽ�Ʈ �޽� ���� ��� ���
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text populationText;
    [SerializeField] TMP_Text modeText;

    // Start is called before the first frame update
    public RoomInfo info;
    public void SetUp(RoomInfo _info)// ������ �޾ƿ���
    {
        info = _info;
        nameText.text = _info.Name;
        populationText.text = _info.PlayerCount.ToString() + "/" + _info.MaxPlayers.ToString();
        if (_info.CustomProperties["Mode"].ToString() == "0")
        {
            modeText.text = "Maze";
        }
        else if (_info.CustomProperties["Mode"].ToString() == "1")
        {
            modeText.text = "Hide and Seek";
        }
        else
        {
            modeText.text = "Mode3";
        }
    }

    // Update is called once per frame
    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);//��ó��ũ��Ʈ �޼���� JoinRoom ����
    }
}
