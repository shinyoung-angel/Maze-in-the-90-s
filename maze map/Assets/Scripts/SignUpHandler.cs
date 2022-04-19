using UnityEngine;
using UnityEngine.UI;
using FirebaseWebGL.Scripts.FirebaseBridge;
using TMPro;

namespace FirebaseWebGL.Examples.Auth
{
    public class SignUpHandler : MonoBehaviour
    {
        public static SignUpHandler instance;

        [Header("Register References")]
        [SerializeField]
        private TMP_InputField registerUsername;
        [SerializeField]
        private TMP_InputField registerEmail;
        [SerializeField]
        private TMP_InputField registerPassword;
        [SerializeField]
        private TMP_InputField registerConfirmPassword;
        [SerializeField]
        private TMP_Text registerNameErrorText;
        [SerializeField]
        private TMP_Text registerEmailErrorText;
        [SerializeField]
        private TMP_Text registerPasswordErrorText;

        public TMP_Text statusText;

        private void Start()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                DisplayError("Webgl �÷����� �ƴϸ� Javascript ����� �νĵ��� �ʽ��ϴ�.");
                return;
            }
        }

        private void DisplayError(string errortext)
        {
            statusText.text = errortext;
        }

        private void DisPlayInfo(string Infotext)
        {
            statusText.text = Infotext;
        }


        private void CheckedName(int result)
        {

            if (result == 0)
            {
                registerNameErrorText.text = "����� �� ���� �г����Դϴ�";
            }
            else if (result == 1)
            {
                registerNameErrorText.text = "��� ������ �г����Դϴ�";
            }
            else if (result == 3)
            {
                registerNameErrorText.text = "�г����� �Է����ּ���";
            }
        }


       
        public void CheckNickname() =>
           FirebaseDatabase.CheckNickname(registerUsername.text);

        public void CreateUserWithEmailAndPassword() =>
           //Firebase Authentication & Realtime Database�� ���� ���
           FirebaseAuth.CreateUserWithEmailAndPassword(registerUsername.text, registerEmail.text, registerPassword.text, gameObject.name, "DisPlayInfo");


        public void LoginScreen()
        {
            GameManager.instance.ChangeScene("Login");
        }

    }

}