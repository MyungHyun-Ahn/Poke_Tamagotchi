using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChatManager : MonoBehaviour
{
    [SerializeField]
    List<string> _chatList = new List<string>();
    [SerializeField]
    Button _sendBtn;
    [SerializeField]
    Button _foldBtn;
    [SerializeField]
    TMP_Text _chatLog;
    [SerializeField]
    TMP_Text _chattingList =null; // Full Chat
    [SerializeField]
    TMP_InputField _input;
    string _chatters;
    ScrollRect _scrollRect = null;
    
    void Start()
    {
        _scrollRect = GameObject.FindObjectOfType<ScrollRect>();
        InitSendBtn();
        InitFoldBtn();

        // TODO : ���� connect ����
        _chatLog.text += "\n" + "���� ���� ��...";
        _chatLog.text += "\n" + "���� ���� ����";
    }

    public void SendBtnOnClick()
    {
        if (_input.text.Equals(""))
        {
            Debug.Log("Empty");
            return;
        }

        // TODO : ������ �޽��� ������

        string msg = string.Format("[{0}] : {1}", "Tester1", _input.text);
        RecvMsg(msg);
        _input.ActivateInputField();
        _input.text = "";
    }

    void Update()
    {
        if(_chattingList != null)
        {
            chatterUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !_input.isFocused)
            SendBtnOnClick();
    }

    void InitSendBtn()
    {
        _sendBtn.onClick.AddListener(() =>
        {
            SendBtnOnClick();
        });
    }

    void InitFoldBtn()
    {
        _foldBtn.onClick.AddListener(() =>
        {
            foldBtnOnClick();
        });
    }

    void foldBtnOnClick()
    {
        TMP_Text btnText = _foldBtn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        string text = btnText.text;

        if (text == "ä��â ����")
        {
            Debug.Log("������");
        }
        else
        {
            Debug.Log("��������");
        }
    }

    void chatterUpdate()
    {
        _chatters = "���� ����\n";

        // TODO : for each�� ���� �÷��̾� �߰� �۾�
        _chatters += "TestUser1\n";
        _chatters += "TestUser2\n";
        _chatters += "TestUser3\n";
        _chattingList.text = _chatters;
    }

    public void RecvMsg(string msg)
    {
        _chatLog.text += "\n" + msg;
        _scrollRect.verticalNormalizedPosition = 0.0f;
    }
}

