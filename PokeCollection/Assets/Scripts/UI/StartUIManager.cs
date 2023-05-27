using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.UI;

public class StartUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inStartPrefab;

    [SerializeField]
    private GameObject _inLoginPrefab;

    [SerializeField]
    private GameObject _inRegisterPrefab;

    private GameStartUI _gameStartUI;
    private LoginUI _loginUI;
    private RegisterUI _registerUI;

    private bool _isStartShow = false;
    private bool _isLoginShow = false;
    private bool _isRegisterShow = false;


    void Start()
    {
        _gameStartUI = new GameStartUI(_inStartPrefab);
        _loginUI = new LoginUI(_inLoginPrefab);
        _registerUI = new RegisterUI(_inRegisterPrefab);

        _isStartShow = true;
        _gameStartUI.showInit(Instantiate(_inStartPrefab));
    }

    void Update()
    {
        if (_gameStartUI.getIsClicked() && !_isLoginShow)
        {
            show(_gameStartUI, _loginUI, out _isStartShow, out _isLoginShow);
        }


        if (_loginUI.getRBtnIsClicked() && !_isRegisterShow)
        {
            show(_loginUI, _registerUI, out _isLoginShow, out _isRegisterShow);

        }

        if (_registerUI.getCBtnClicked() && !_isLoginShow)
        {
            show(_registerUI, _loginUI, out _isRegisterShow, out _isLoginShow);
        }
    }

    private void show(UI nowView, UI nextView, out bool now, out bool next)
    {
        next = true;
        GameObject _show = Instantiate(nextView.getPrefab());
        nextView.showInit(_show);

        now = false;
        Reset(nowView);
    }

    private void Reset(UI ui)
    {
        Destroy(ui.reset());
    }
}
