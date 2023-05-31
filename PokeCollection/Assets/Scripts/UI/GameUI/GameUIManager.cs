using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _inFullChatPrefab;

    [SerializeField]
    GameObject _inFoldChatPrefab;

    [SerializeField]
    GameObject _inGameContentsPrefab;

    [SerializeField]
    GameObject _inInfoPrefab;

    [SerializeField]
    GameObject _inQuestPrefab;

    [SerializeField]
    GameObject _inSystemPrefab;

    private FoldChatUI _foldChatUI;
    private FullChatUI _fullChatUI;

    private bool _isFullChatShow = false;
    private bool _isFoldChatShow = false;
    // private bool _isGameContentShow = false;
    // private bool _isInfoShow = false;
    // private bool _isQuestShow = false;
    // private bool _isSystemShow = false;


    void Start()
    {
        _fullChatUI = new FullChatUI(_inFullChatPrefab);
        _foldChatUI = new FoldChatUI(_inFoldChatPrefab);

        // �⺻������ FullChatUI�� ����
        show(_fullChatUI, out _isFullChatShow);
    }

    void Update()
    {
        if (_foldChatUI.getMode() && !_isFullChatShow)
        {
            show(_fullChatUI, out _isFullChatShow);

            if (_isFoldChatShow)
            {
                Reset(_foldChatUI, out _isFoldChatShow);
            }
        }

        // FullChatUI�� �����ִ� ��Ȳ
        if (_isFullChatShow)
        {
            _fullChatUI.Update();
        }

        if (_fullChatUI.getMode() && !_isFoldChatShow)
        {
            show(_foldChatUI, out _isFoldChatShow);

            if (_isFullChatShow)
            {
                Reset(_fullChatUI, out _isFullChatShow);
            }
        }

        if (_isFoldChatShow)
        {
            _foldChatUI.Update();
        }
    }

    private void show(UI nextView, out bool isShow)
    {
        isShow = true;
        GameObject _show = Instantiate(nextView.getPrefab());
        nextView.showInit(_show);
    }

    private void Reset(UI ui, out bool isShow)
    {
        isShow = false;
        Destroy(ui.reset());
    }
}
