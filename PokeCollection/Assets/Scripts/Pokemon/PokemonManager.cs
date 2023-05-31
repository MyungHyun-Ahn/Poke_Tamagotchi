using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tools;
using Assets.Scripts.Player;
using Assets.Scripts.Pokemon;

public class PokemonManager : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    GameObject _pokemonPrefab;

    List<GameObject> _showPokemons;

    void Start()
    {
        // 서버에서 데이터 받아오기
        // Player의 보유 포켓몬 정보
        _player = new Player();
        _showPokemons = new List<GameObject>();

        // 미리 공간을 늘림
        // _showPokemons.Capacity = _player.getMaxPokemonCount();
        InstantiatePokemon();
    }

    void Update()
    {

    }

    private void InstantiatePokemon()
    {
        for(int index = 0; index < _player.getPokeList().Count(); index++)
        {
            _showPokemons.Add(Instantiate(_pokemonPrefab));
            PokemonData data = _player.getPokeData(index);
            int indexNum = data.getindexNum();
            _showPokemons[index].GetComponent<PokeController>().setPokemon(indexNum, data);
        }
    }
}
