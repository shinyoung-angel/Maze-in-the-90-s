using UnityEngine;
using UnityEngine.UI;
using FirebaseWebGL.Scripts.FirebaseBridge;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Photon.Pun;

namespace FirebaseWebGL.Examples.Auth
{
    public class LobbyHandler : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        //1. ����������
        private GameObject profileUI;
        [SerializeField]
        //2. �г��� ����������
        private GameObject changeNicknameUI;
        [SerializeField]
        //3. ��й�ȣ ����
        private GameObject changePasswordUI;
        [SerializeField]
        //4. ȸ�� Ż��
        private GameObject deleteUserConfirmUI;
        [SerializeField]
        //5. Ȯ��������
        private GameObject actionSuccessPanelUI;
        [Space(5f)]

        [Header("Basic Info References")]
        [SerializeField]
        private TMP_Text lobbyUsernameText;
        [SerializeField]
        private Text myPageUsernameText;
        [Space(5f)]

        [Header("Change Nickname References")]
        [SerializeField]
        private Image lobbyProfilePicture;
        [SerializeField]
        private Image myPageProfilePicture;
        [SerializeField]
        private TMP_Text currNickname;
        [SerializeField]
        private TMP_InputField newNickname;
        [SerializeField]
        private TMP_Text outputText;

        [Header("Change Password References")]
        [SerializeField]
        private TMP_InputField changePasswordInputField;
        [SerializeField]
        private TMP_InputField changePasswordConfirmInputField;
        [SerializeField]
        private TMP_Text pwErrorText;
        [Space(5f)]

        [Header("Action Success Panel References")]
        [SerializeField]
        private TMP_Text actionSuccessText;

        //�г���, ����
        public static LobbyHandler instance;
        public static string userName = null;
        

        public void Start()
        {
            //���� ���� �� ������ �ε�
            CheckAuthState();
            Debug.Log("lobby scene");
            Debug.Log(FirebaseWebGL.Examples.Auth.LoginHandler.UserUid);
        }

        public void GetUsername(string _username)
        {
            userName = _username;

            //�� �� ���̸� �ε������� ����
            if (userName != null)
            {
                LoadUsername();
            }
        }

        

        private void LoadUsername()
        {
            lobbyUsernameText.text = userName.ToString();
            myPageUsernameText.text = userName.ToString();
        }



        public void ClearUI()
        {
            profileUI.SetActive(false);
            changeNicknameUI.SetActive(false);
            changePasswordUI.SetActive(false);
            actionSuccessPanelUI.SetActive(false);
            deleteUserConfirmUI.SetActive(false);
            actionSuccessText.text = "";
            currNickname.text = "";
            newNickname.text = "";
            outputText.text = "";
            changePasswordInputField.text = "";
            changePasswordConfirmInputField.text = "";
            pwErrorText.text = "";
        }

        //1. ����������
        public void ProfileUI()
        {
            ClearUI();
            profileUI.SetActive(true);
            //������ �ٲ��� �� �ٽ� ȣ��
            CheckAuthState();
        }

        //2. �г� ����
        public void ChangeNickUI()
        {
            ClearUI();
            changeNicknameUI.SetActive(true);
            currNickname.text = userName;
        }

        public void CheckNicknameForChange() =>
           FirebaseDatabase.CheckNicknameForChange(newNickname.text);

        private void CheckedNameForChange(int result)
        {

            if (result == 0)
            {
                outputText.text = "����� �� ���� �г����Դϴ�";
            }
            else if (result == 1)
            {
                outputText.text = "��� ������ �г����Դϴ�";
            }
            else if (result == 2)
            {
                outputText.text = "���� �г��Ӱ� �����ϴ�";
            }
            else if (result == 3)
            {
                outputText.text = "������ �г����� �Է����ּ���";
            }
        }


        public void ChangeNicknameSuccess()
        {
            if (outputText.text == "��� ������ �г����Դϴ�")
            {
                changeNicknameUI.SetActive(false);
                actionSuccessPanelUI.SetActive(true);
                actionSuccessText.text = "�г����� ���������� ����Ǿ����ϴ�";
                FirebaseAuth.UpdateNickname(newNickname.text);
                Debug.Log("newnickname@@");
                Debug.Log(newNickname.text);
            }
        }

        //2. ��� ����
        public void ChangePwUI()
        {
            ClearUI();
            changePasswordUI.SetActive(true);
        }

        public void ChangePwSuccess()
        {
            ClearUI();
            actionSuccessPanelUI.SetActive(true);
            actionSuccessText.text = "��й�ȣ�� ���������� ����Ǿ����ϴ�";
        }

        public void SubmitNewPwButton()
        {
            if ((changePasswordInputField.text == changePasswordConfirmInputField.text) == true && changePasswordConfirmInputField.text.Length >= 6)
            {
                UpdatePw(changePasswordInputField.text);
                ChangePwSuccess();
            }
            else if ((changePasswordInputField.text == changePasswordConfirmInputField.text) == true && changePasswordConfirmInputField.text.Length < 6)
            {
                pwErrorText.text = "��й�ȣ�� �ּ� 6�ڸ� �̻����� ������ּ���";
            }
            else if ((changePasswordInputField.text == changePasswordConfirmInputField.text) == false)
            {
                pwErrorText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�";
            }
        }

        //4. ȸ��Ż��
        public void DeleteUserConfirmUI()
        {
            ClearUI();
            deleteUserConfirmUI.SetActive(true);
        }

        public void DeleteUserSuccess()
        {
            ClearUI();
            actionSuccessPanelUI.SetActive(true);
            actionSuccessText.text = "ȸ��Ż�� �Ϸ�Ǿ����ϴ�";
        }

        public void DeleteUserButton()
        {
            PhotonNetwork.Disconnect();
            DeleteUser();
        }


        //�α׾ƿ�
        public void SignOutButton()
        {
            PhotonNetwork.Disconnect();
            SignOut();
        }




        public void CheckAuthState() =>
           FirebaseAuth.CheckAuthState();

        public void UpdateProfilePicture(string newProfile) =>
           FirebaseAuth.UpdateProfilePicture(newProfile);

        public void UpdatePw(string newPw) =>
           FirebaseAuth.UpdatePw(newPw);

        public void DeleteUser() =>
           FirebaseAuth.DeleteUser();

        public void SignOut() =>
           FirebaseAuth.SignOut();


        public void LoginScreen()
        {
            GameManager.instance.ChangeScene("Login");
        }

    }

}