using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Assets.Scripts.UI
{
    abstract class UI
    {
        public abstract void Init(GameObject gameObject);
        public abstract void showInit(GameObject gameObject);
        public abstract GameObject getPrefab();
        public abstract GameObject reset();

        // 내부에서만 
        protected abstract void btnInit();
    }

    class GameStartUI : UI
    {
        /* GetChild
         0. Image
         1. From Panel
            0. Main
            1. Game Start Button
        */

        private GameObject      _startPrefab;
        private GameObject      _showStart;
        private Button          _startBtn;
        private bool            _isClicked = false;

        public GameStartUI()
        {

        }

        public GameStartUI(GameObject startPrefab)
        {
            Init(startPrefab);
        }

        // 무조건 GameStartUI을 생성한 직후에는 Init을 호출
        public override void Init(GameObject startPrefab)
        {
            _startPrefab    = startPrefab;
        }

        public override void showInit(GameObject showStart)
        {
            _showStart = showStart;
            _startBtn = _showStart.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Button>();
            btnInit();
        }

        public override GameObject getPrefab()
        {
            return _startPrefab;
        }

        public override GameObject reset()
        {
            _isClicked = false;
            return _showStart;
        }

        public bool getIsClicked()
        {
            return _isClicked;
        }

        protected override void btnInit()
        {
            _startBtn.onClick.AddListener(() =>
            {
                _isClicked = true;
            });
        }

    }

    class LoginUI : UI
    {
        /* GetChild
             0. Image
             1. From Panel
                0. Main
                1. Id Text
                2. Id
                3. Password Text
                4. Password
                5. Login Button
                6. Register Button
       */

        private GameObject           _loginPrefab;
        private GameObject           _showLogin;
        private Button               _loginBtn;
        private Button               _registerBtn;
        private TMP_InputField       _idInput;
        private TMP_InputField       _passwordInput;
        private bool                 _lBtnIsClicked = false;
        private bool                 _rBtnIsClicked = false;

        public LoginUI()
        {

        }

        public LoginUI(GameObject loginPrefab)
        {
            Init(loginPrefab);
        }

        public override void Init(GameObject loginPrefab)
        {
            _loginPrefab        = loginPrefab;
        }

        public override void showInit(GameObject loginPrefab)
        {
            _showLogin          = loginPrefab;
            _loginBtn           = _showLogin.transform.transform.GetChild(1).GetChild(5).gameObject.GetComponent<Button>();
            _registerBtn        = _showLogin.transform.transform.GetChild(1).GetChild(6).gameObject.GetComponent<Button>();
            _idInput            = _showLogin.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_InputField>();
            _passwordInput      = _showLogin.transform.GetChild(1).GetChild(4).gameObject.GetComponent<TMP_InputField>();
            btnInit();
        }

        public override GameObject getPrefab()
        {
            return _loginPrefab;
        }

        public override GameObject reset()
        {
            _lBtnIsClicked = false;
            _rBtnIsClicked = false;
            return _showLogin;
        }

        public bool getLBtnIsClicked()
        {
            return _lBtnIsClicked;
        }

        public bool getRBtnIsClicked()
        {
            return _rBtnIsClicked;
        }

        public void setRBtnIsClicked(bool isClicked)
        {
            _rBtnIsClicked = isClicked;
        }

        protected override void btnInit()
        {
            _loginBtn.onClick.AddListener(() =>
            {
                _lBtnIsClicked = true;

                string id          = _idInput.text;
                string password    = _passwordInput.text;

                if (login(id, password))
                {
                    Debug.Log("Login Success");
                }
                else
                {
                    Debug.Log("Login Failed");
                }

                // 모든 과정 마치고 다시 false로
                _lBtnIsClicked = false;
            });

            _registerBtn.onClick.AddListener(() =>
            {
                _rBtnIsClicked = true;
            });
        }

        private bool login(string id, string password)
        {
            // 서버 통신하여 validation 체크 진행
            // 이후 if 문에서 패킷 비교하여 성공 실패 여부 리턴
            if (id == "test123" && password == "test123")
            {
                // TODO : Main Game Scene 이동
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class RegisterUI : UI
    {
        /* GetChild
             0. Image
             1. From Panel
                0. Main
                1. Id Text
                2. Id
                3. Password Text
                4. Password
                5. Password Check Text
                6. Password Check
                7. Nickname Text
                8. Nickname
                9. Cancel Button
                10. Register Button
        */

        private GameObject          _registerPrefab;
        private GameObject          _showRegister;
        private Button              _registerBtn;
        private Button              _cancelBtn;
        private TMP_InputField      _idInput;
        private TMP_InputField      _passwordInput;
        private TMP_InputField      _passwordCheckInput;
        private TMP_InputField      _nicknameInput;
        private bool                _rBtnIsClicked;
        private bool                _cBtnIsClicked;

        public RegisterUI()
        {

        }

        public RegisterUI(GameObject registerPrefab)
        {
            Init(registerPrefab);
        }

        public override void Init(GameObject registerPrefab)
        {
            _rBtnIsClicked          = false;
            _cBtnIsClicked          = false;
            _registerPrefab         = registerPrefab;
        }

        public override void showInit(GameObject showRegister)
        {
            _showRegister           = showRegister;
            _cancelBtn              = _showRegister.transform.GetChild(1).GetChild(9).gameObject.GetComponent<Button>();
            _registerBtn            = _showRegister.transform.GetChild(1).GetChild(10).gameObject.GetComponent<Button>();
            _idInput                = _showRegister.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_InputField>();
            _passwordInput          = _showRegister.transform.GetChild(1).GetChild(4).gameObject.GetComponent<TMP_InputField>();
            _passwordCheckInput     = _showRegister.transform.GetChild(1).GetChild(6).gameObject.GetComponent<TMP_InputField>();
            _nicknameInput          = _showRegister.transform.GetChild(1).GetChild(8).gameObject.GetComponent<TMP_InputField>();
            btnInit();
        }

        public override GameObject getPrefab()
        {
            return _registerPrefab;
        }

        public override GameObject reset()
        {
            _rBtnIsClicked = false;
            _cBtnIsClicked = false;
            return _showRegister;
        }

        protected override void btnInit()
        {
            _registerBtn.onClick.AddListener(() =>
            {
                _rBtnIsClicked = true;
                string id = _idInput.text;
                string password = _passwordInput.text;
                string passwordCheck = _passwordCheckInput.text;
                string nickname = _nicknameInput.text;
                if (Register(id, password, passwordCheck, nickname))
                {
                    // TODO : 성공하면 -> 로그인 창으로
                }
            });

            _cancelBtn.onClick.AddListener(() =>
            {
                _cBtnIsClicked = true;
            });
        }

        public bool getCBtnClicked()
        {
            return _cBtnIsClicked;
        }

        private bool Register(string id, string password, string passwordCheck, string nickname)
        {

            // TODO : 서버에서 데이터 비교하여 id, nickname 중복체크

            if (password != passwordCheck)
            {
                Debug.Log("비밀번호 확인 실패");
                return false;
            }

            Debug.Log("--- 가입 성공 ---" +
                "\n" +
                "ID : " + id +
                "\n" +
                "Password : " + password +
                "\n" +
                "Nickname :" + nickname);

            return true;
        }
    }
}
