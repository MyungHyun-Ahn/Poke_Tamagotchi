using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using Assets.Scripts.Pokemon;


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

        private GameObject _startPrefab;
        private GameObject _showStart;
        private Button _startBtn;
        private bool _isClicked = false;

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
            _startPrefab = startPrefab;
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

        private GameObject _loginPrefab;
        private GameObject _showLogin;
        private Button _loginBtn;
        private Button _registerBtn;
        private TMP_InputField _idInput;
        private TMP_InputField _passwordInput;
        private bool _lBtnIsClicked = false;
        private bool _rBtnIsClicked = false;

        public LoginUI()
        {

        }

        public LoginUI(GameObject loginPrefab)
        {
            Init(loginPrefab);
        }

        public override void Init(GameObject loginPrefab)
        {
            _loginPrefab = loginPrefab;
        }

        public override void showInit(GameObject loginPrefab)
        {
            _showLogin = loginPrefab;
            _loginBtn = _showLogin.transform.transform.GetChild(1).GetChild(5).gameObject.GetComponent<Button>();
            _registerBtn = _showLogin.transform.transform.GetChild(1).GetChild(6).gameObject.GetComponent<Button>();
            _idInput = _showLogin.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_InputField>();
            _passwordInput = _showLogin.transform.GetChild(1).GetChild(4).gameObject.GetComponent<TMP_InputField>();
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

                string id = _idInput.text;
                string password = _passwordInput.text;

                if (login(id, password))
                {
                    Debug.Log("Login Success");
                    SceneManager.LoadScene("HomeScene");
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

        private GameObject _registerPrefab;
        private GameObject _showRegister;
        private Button _registerBtn;
        private Button _cancelBtn;
        private TMP_InputField _idInput;
        private TMP_InputField _passwordInput;
        private TMP_InputField _passwordCheckInput;
        private TMP_InputField _nicknameInput;
        private bool _rBtnIsClicked;
        private bool _cBtnIsClicked;

        public RegisterUI()
        {

        }

        public RegisterUI(GameObject registerPrefab)
        {
            Init(registerPrefab);
        }

        public override void Init(GameObject registerPrefab)
        {
            _rBtnIsClicked = false;
            _cBtnIsClicked = false;
            _registerPrefab = registerPrefab;
        }

        public override void showInit(GameObject showRegister)
        {
            _showRegister = showRegister;
            _cancelBtn = _showRegister.transform.GetChild(1).GetChild(9).gameObject.GetComponent<Button>();
            _registerBtn = _showRegister.transform.GetChild(1).GetChild(10).gameObject.GetComponent<Button>();
            _idInput = _showRegister.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_InputField>();
            _passwordInput = _showRegister.transform.GetChild(1).GetChild(4).gameObject.GetComponent<TMP_InputField>();
            _passwordCheckInput = _showRegister.transform.GetChild(1).GetChild(6).gameObject.GetComponent<TMP_InputField>();
            _nicknameInput = _showRegister.transform.GetChild(1).GetChild(8).gameObject.GetComponent<TMP_InputField>();
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

        public bool getRBtnClicked()
        {
            return _rBtnIsClicked;
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

    class ChatUI : UI
    {
        /* FoldChatUI와 FullChatUI의 공통적인 부분 구현 */

        protected GameObject _chatPrefab;
        protected GameObject _showChat;
        protected List<string> _chatList = new List<string>();
        protected Button _sendBtn;
        protected Button _foldBtn;
        protected TMP_Text _chatLog;
        protected TMP_InputField _input;
        protected ScrollRect _scrollRect = null;
        protected bool _modeSelector = false; // 버튼 클릭 여부


        protected ChatUI(GameObject chatPrefab)
        {
            Init(chatPrefab);
        }

        public override GameObject getPrefab()
        {
            return _chatPrefab;
        }

        public override void Init(GameObject chatPrefab)
        {
            _chatPrefab = chatPrefab;
        }

        public override GameObject reset()
        {
            _modeSelector = false;
            return _showChat;
        }

        public override void showInit(GameObject showChat)
        {
            _showChat = showChat;
            _sendBtn = _showChat.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Button>();
            _foldBtn = _showChat.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Button>();
            _chatLog = _showChat.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
            _input = _showChat.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TMP_InputField>();
            _scrollRect = GameObject.FindObjectOfType<ScrollRect>();

            btnInit();

            // 서버 접속 수행
            _chatLog.text += "\n" + "서버 접속 중...";
            _chatLog.text += "\n" + "서버 접속 성공";
        }

        protected override void btnInit()
        {
            _sendBtn.onClick.AddListener(() =>
            {
                SendBtnOnClick();
            });

            _foldBtn.onClick.AddListener(() =>
            {
                _modeSelector = true;
            });
        }

        public virtual void Update()
        {

        }


        protected void SendBtnOnClick()
        {
            if (_input.text.Equals(""))
            {
                Debug.Log("Empty");
                return;
            }

            // TODO : 서버에 메시지 보내기

            string msg = string.Format("[{0}] : {1}", "Tester1", _input.text);
            RecvMsg(msg);
            _input.ActivateInputField();
            _input.text = "";
        }

        public bool getMode()
        {
            return _modeSelector;
        }


        private void RecvMsg(string msg)
        {
            _chatLog.text += "\n" + msg;
            _scrollRect.verticalNormalizedPosition = -0.3f;
        }
    }


    class FullChatUI : ChatUI
    {
        private TMP_Text _chattingList = null;
        private string _chatters;

        public FullChatUI(GameObject fullChatPrefab) : base(fullChatPrefab)
        {
            
        }

        public override void showInit(GameObject showChat)
        {
            base.showInit(showChat);
            FullChatInit();
        }

        private void FullChatInit()
        {
            _chattingList = _showChat.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        }

        public override void Update()
        {

            if (_chattingList != null)
            {
                chatterUpdate();
            }
            if (Input.GetKeyDown(KeyCode.Return) && !_input.isFocused)
                SendBtnOnClick();

        }

        public void chatterUpdate()
        {
            _chatters = "접속 유저\n";

            // TODO : 서버에서 유저 정보들을 받아 for each문 돌며 플레이어 추가 작업

            _chatters += "TestUser1\n";
            _chatters += "TestUser2\n";
            _chatters += "TestUser3\n";
            _chattingList.text = _chatters;
        }
    }

    class FoldChatUI : ChatUI
    {
        public FoldChatUI(GameObject foldChatPrefab) : base(foldChatPrefab)
        {

        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !_input.isFocused)
                SendBtnOnClick();
        }
    }

    class PokeInfoUI : UI
    {
        private GameObject _infoPrefab;
        private GameObject _showInfo;

        private Image _mainPokemonImg;
        private Image _statusImg;
        private TMP_Text _pokemonName;
        private TMP_Text _pokemonStats;
        private Slider _expSlider;

        private Button _shutDownBtn;
        private Button _trainingBtn;
        private Button _readBookBtn;
        private Button _sleepBtn;
        private Button _feedFoodBtn;
        private Button _pattingBtn;

        private short _pokemonStatus = 0;
        private bool _isTrainingBtnClicked = false;
        private bool _isShutDownBtnClicked = false;
        private bool _isStatusBtnClicked = false;

        public PokeInfoUI(GameObject infoPrefab)
        {
            _infoPrefab = infoPrefab;
        }

        public override GameObject getPrefab()
        {
            return _infoPrefab;
        }

        public override void Init(GameObject infoPrefab)
        {
            _infoPrefab = infoPrefab;
        }

        public override GameObject reset()
        {
            ResetAllFlags();
            return _showInfo;
        }

        /*
         0. PokePanel
            0. ShutDownBtn
            1. InfoPanel
                0. PokeNameText
                1. PokeStatsText
                2. ImgBackground
                    0. PokeImg
                    1. InfoImg
                3. ExpSlider
         1. ButtonPanel
            0. TrainingBtn
            1. ReadBook
            2. Playing
            3. FeedFood
            4. Patting
         */

        public override void showInit(GameObject showInfo)
        {
            _showInfo = showInfo;

            // Info Panel
            _mainPokemonImg = _showInfo.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<Image>();
            _statusImg = _showInfo.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).gameObject.GetComponent<Image>();
            _pokemonName = _showInfo.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<TMP_Text>();
            _pokemonStats = _showInfo.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>();
            _expSlider = _showInfo.transform.GetChild(0).GetChild(1).GetChild(3).gameObject.GetComponent<Slider>();
            _shutDownBtn = _showInfo.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>();

            // Button Panel
            _trainingBtn = _showInfo.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Button>();
            _readBookBtn = _showInfo.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Button>();
            _sleepBtn = _showInfo.transform.GetChild(1).GetChild(2).gameObject.GetComponent<Button>();
            _feedFoodBtn = _showInfo.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Button>();
            _pattingBtn = _showInfo.transform.GetChild(1).GetChild(4).gameObject.GetComponent<Button>();

            btnInit();
        }

        protected override void btnInit()
        {
            _shutDownBtn.onClick.AddListener(() => {
                _isShutDownBtnClicked = true;
            });

            _trainingBtn.onClick.AddListener(() =>
            {
                _isTrainingBtnClicked = true;
            });

            _readBookBtn.onClick.AddListener(() =>
            {
                _pokemonStatus = 1;
                _isStatusBtnClicked = true;
            });

            _sleepBtn.onClick.AddListener(() =>
            {
                _pokemonStatus = 2;
                _isStatusBtnClicked = true;
            });

            _feedFoodBtn.onClick.AddListener(() =>
            {
                _pokemonStatus = 3;
                _isStatusBtnClicked = true;
            });

            _pattingBtn.onClick.AddListener(() =>
            {
                _pokemonStatus = 4;
                _isStatusBtnClicked = true;
            });
        }

        public void setInfoPanel(PokemonData pokemonData, Sprite mainImg, Sprite statusImg)
        {
            _pokemonName.text = pokemonData.getNameKor();
            int[] stats = pokemonData.getTotalStats();
            _pokemonStats.text = "체력 공격 방어 속도 특공\n" + stats[0].ToString("D4") + " " + stats[1].ToString("D4") + " " + stats[2].ToString("D4") + " " + stats[3].ToString("D4") + " " + stats[4].ToString("D4");
            _mainPokemonImg.sprite = mainImg;
            _statusImg.sprite = statusImg;
        }

        public void updateExpSlider(PokemonData pokemonData)
        {
            float exp = pokemonData.getEvolutionExp();
            if (exp <= 0)
            {
                _expSlider.transform.Find("Fill Area").gameObject.SetActive(false);
            }
            else
            {
                _expSlider.transform.Find("Fill Area").gameObject.SetActive(true);
                _expSlider.value = exp / 100.0f;
            }
        }

        public short getPokemonStatus()
        {
            return _pokemonStatus;
        }

        public bool getStatusBtnClicked()
        {
            return _isStatusBtnClicked;
        }

        public bool getTrainingBtnClicked()
        {
            return _isTrainingBtnClicked;
        }

        public bool getShutDownBtnClicked()
        {
            return _isShutDownBtnClicked;
        }

        public void ResetAllFlags()
        {
            _pokemonStatus = 0;
            _isStatusBtnClicked = false;
            _isShutDownBtnClicked = false;
            _isTrainingBtnClicked = false;
        }
    }
}
