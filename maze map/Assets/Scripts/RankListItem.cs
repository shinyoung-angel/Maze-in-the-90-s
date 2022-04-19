using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankListItem : MonoBehaviour
{
    // ��� ������ ����
    [SerializeField]
    private TextMeshProUGUI rankText;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
    }


    public void SetUp(Dictionary<string, object> _info)// ��ŷ ���� �޾ƿ���
    {
        string strA = (string)_info["idx"];
        string strB = (string)_info["name"]; 
        string strC = (string)_info["time"];

        rankText.text = strA;
        nameText.text = strB;
        timeText.text = strC;
    }

    // Update is called once per frame
    void Update()
    {

    }
}