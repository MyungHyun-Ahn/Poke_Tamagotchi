using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;
using Assets.Scripts.Pokemon;
using Assets.Scripts.Tools;

public class PokeController : MonoBehaviour
{
    private GameObject      _imgObj;
    private Image           _pokeImg;
    private Image           _statusImg;
    private Button          _pokeInfoBtn;

    private Sprite[]        _sprites;
    private Sprite          _prevPokemonImg;
    private int             _pokeIndex = 1;
    private int             _dir = 0;
    private float           _time = 0;
    private float           _speed = 15.0f;
    private float           _moveTime = 0;
    private float           _spriteTime = 0;

    private PokeDataLoader  _dataLoader;
    private PokemonData     _pokemonData;

    // UI 영역
    private bool            _isInfoBtnClicked = false;

    private GameObject      _infoPrefab;
    private PokeInfoUI      _pokeInfoUI;
    private bool            _isInfoUIShow = false;
    private short           _statusFlag = (short)Status.NONE_FLAG;
    private float           _statusTime = 0;
    private float           _statusElapsedTime = 0; // 경과 시간
    private Sprite[]        _statusSprites;



    private enum Dir
    {
        MOVE_UP = 0,
        MOVE_DOWN = 1,
        MOVE_RIGHT = 2,
        MOVE_LEFT = 3,
        STOP = 4
    }

    private enum Status
    {
        NONE_FLAG = 0,
        READ_BOOK_FLAG = 1,
        SLEEP_FLAG = 2,
        HUNGRY_FLAG = 3,
        PATTING_FLAG = 4
    }

    // 1.0f, 0.0f : MOVE_RIGHT
    // -1.0f, 0.0f : MOVE_LEFT
    // 0.0f, 1.0f : MOVE_UP
    // 0.0f, -1.0f : MOVE_DOWN

    private Vector3 _up         = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 _down       = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 _right      = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 _left       = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 _stop       = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        Init();

    }

    void Update()
    {
        Move();
        StatusHandler();
        PokemonTraining();
        UIHandler();
        IsCanEvolution();
        StatusImageUpdater();
    }

    private void Init()
    {
        _dataLoader = new PokeDataLoader();
        _imgObj = transform.GetChild(0).gameObject;
        _pokeImg = _imgObj.GetComponent<Image>();
        _infoPrefab = Resources.Load("UI/PokeUI/PokemonCanvas") as GameObject;
        _pokeInfoUI = new PokeInfoUI(_infoPrefab);

        _sprites = _dataLoader.PokemonSpriteLoader(_pokeIndex);
        _statusSprites = _dataLoader.StatusSpriteLoader();
        _pokeImg.rectTransform.anchoredPosition = new Vector2(Random.Range(-300.0f, 300.0f), Random.Range(-300.0f, 300.0f));

        _pokeImg.sprite = _sprites[0];
        _pokeInfoBtn = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>();
        _statusImg = transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>();
        PokeInfoBtnInit();
    }

    private void PokeInfoBtnInit()
    {
        _pokeInfoBtn.onClick.AddListener(() =>
        {
            Debug.Log("클릭");
            _isInfoBtnClicked = true;
        });
    }

    public void setPokemon(int pokeIndex, PokemonData data)
    {
        _pokeIndex = pokeIndex;
        _pokemonData = data;
        Init();
        
    }

    private void Move()
    {
        if (_time == 0)
        {
            // 이동 방향과 이동 시간 결정
            _dir = Random.Range(0, 5);
            _time = Random.Range(1.0f, 5.0f);
        }
        else
        {
            _moveTime += Time.deltaTime;


            switch (_dir)
            {
                case (int)Dir.MOVE_UP:
                    _pokeImg.rectTransform.Translate(_speed * _up * Time.deltaTime);
                    PlaySprite(2, 3);
                    break;
                case (int)Dir.MOVE_DOWN:
                    _pokeImg.rectTransform.Translate(_speed * _down * Time.deltaTime);
                    PlaySprite(0, 1);
                    break;
                case (int)Dir.MOVE_RIGHT:
                    _pokeImg.rectTransform.Translate(_speed * _right * Time.deltaTime);
                    PlaySprite(6, 7);
                    break;
                case (int)Dir.MOVE_LEFT:
                    _pokeImg.rectTransform.Translate(_speed * _left * Time.deltaTime);
                    PlaySprite(4, 5);
                    break;
                case (int)Dir.STOP:
                    _pokeImg.rectTransform.Translate(_speed * _stop * Time.deltaTime);
                    PlaySprite(0, 0);
                    break;
            }

            if (_time < _moveTime)
            {
                _time = 0;
                _moveTime = 0;
            }
        }
    }

    private void UIHandler()
    {
        if (_isInfoBtnClicked && !_isInfoUIShow)
        {
            show(_pokeInfoUI, out _isInfoUIShow);
        }

        if (_isInfoUIShow)
        {
            _pokeInfoUI.setInfoPanel(_pokemonData, _sprites[0], _statusSprites[_statusFlag]);
            _pokeInfoUI.updateExpSlider(_pokemonData);

            if (_pokeInfoUI.getShutDownBtnClicked())
                Reset(_pokeInfoUI, out _isInfoUIShow);
        }
    }

    private void PokemonTraining()
    {
        if (_pokeInfoUI.getTrainingBtnClicked() && _isInfoUIShow)
        {
            int target = Random.Range(0, 5);
            int value = Random.Range(0, 10);

            Debug.Log(target);
            Debug.Log(value);

            _pokemonData.addTraingStats(target, value);
            _pokeInfoUI.ResetAllFlags();
        }
    }

    private void StatusImageUpdater()
    {
        _statusImg.sprite = _statusSprites[_statusFlag];
    }

    private void StatusHandler()
    {
        if (_statusTime == 0)
        {
            _statusFlag = (short)Random.Range(1, 5);
            _statusTime = Random.Range(30.0f, 60.0f);
        }
        else
        {
            _statusElapsedTime += Time.deltaTime;

            if (_statusFlag == _pokeInfoUI.getPokemonStatus() && _pokeInfoUI.getStatusBtnClicked() && _isInfoUIShow)
            {
                switch (_statusFlag)
                {
                    case (short)Status.NONE_FLAG:
                        _pokemonData.AddEvolutionExp(0);
                        break;
                    case (short)Status.READ_BOOK_FLAG:
                        _pokemonData.AddEvolutionExp(100);
                        break;
                    case (short)Status.SLEEP_FLAG:
                        _pokemonData.AddEvolutionExp(500);
                        break;
                    case (short)Status.HUNGRY_FLAG:
                        _pokemonData.AddEvolutionExp(600);
                        break;
                    case (short)Status.PATTING_FLAG:
                        _pokemonData.AddEvolutionExp(200);
                        break;
                }
                Debug.Log(_pokemonData.getEvolutionExp());
                // 미션 수행 후에는 NONE_FLAG로
                _statusFlag = (short)Status.NONE_FLAG;
                _pokeInfoUI.ResetAllFlags();
                _pokeInfoUI.updateExpSlider(_pokemonData);
            }
            // 버튼을 눌렀는데 flag가 같지 않은 상황
            else if (_pokeInfoUI.getStatusBtnClicked() && _isInfoUIShow)
            {
                if (_statusFlag != (short)Status.NONE_FLAG)
                {
                    _pokemonData.AddEvolutionExp(-20);
                    _pokeInfoUI.updateExpSlider(_pokemonData);
                    _pokeInfoUI.ResetAllFlags();
                }
            }
        }

        if (_statusTime < _statusElapsedTime)
        {
            _statusTime = 0;
            _statusElapsedTime = 0;
        }
    }

    private void IsCanEvolution()
    {
        if (_pokemonData.CanEvolution())
        {
            _prevPokemonImg = _sprites[0];

            PokemonData nextPokemonData = new PokemonData(_pokemonData.getEvolutionName(), _pokemonData.getTrainingStats());
            Reset(_pokeInfoUI, out _isInfoUIShow);
            setPokemon(nextPokemonData.getindexNum(), nextPokemonData);
        }
    }

    // 0, 1 : down
    // 2, 3 : up
    // 4, 5 : left
    // 6, 7 : right
    private void PlaySprite(int sp1, int sp2)
    {
        // 0.3초 마다 스프라이트 이미지 변경
        _spriteTime += Time.deltaTime;
        if (_spriteTime < 0.3f)
        {
            _pokeImg.sprite = _sprites[sp1];
        }
        else if (_spriteTime < 0.6f)
        {
            _pokeImg.sprite = _sprites[sp2];
        }
        else
        {
            _spriteTime = 0.0f;
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
        _isInfoBtnClicked = false;
        Destroy(ui.reset());
    }
}
