using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace FirebaseWebGL.Scripts.FirebaseBridge
{
    public static class FirebaseDatabase
    {
        //�г��� �ߺ��˻�
        [DllImport("__Internal")]
        public static extern void CheckNickname(string name);

        //�г��� �ߺ��˻� �Ҽ� ȸ������
        [DllImport("__Internal")]
        public static extern void CheckNicknameForSocial(string name);

        //�г��� ���� �ߺ��˻�
        [DllImport("__Internal")]
        public static extern void CheckNicknameForChange(string name);

        //��ŷ������ �� ������������ ��� �ø���
        [DllImport("__Internal")]
        public static extern void PostGameRecord(string json);

        //��ŷ������ �� ������������ ��� �ø���
        [DllImport("__Internal")]
        public static extern void PostMyRecord(string json);

        //��ŷ�������� ��� �޾ƿ���
        [DllImport("__Internal")]
        public static extern void SetGameRecord();

        //��ŷ������ �� Ŭ��(���, �ʺ� ������)
        [DllImport("__Internal")]
        public static extern void SetByInfo(string mode, string map);

        //������������ ��� �޾ƿ���(����)
        [DllImport("__Internal")]
        public static extern void GetRecords(string username);

        //������������ ��� �޾ƿ���(��ŷ)
        [DllImport("__Internal")]
        public static extern void GetRanks(string username);

        //ĳ���� ����
        [DllImport("__Internal")]
        public static extern void UpdateCharacter(int charIdx);

    }
}