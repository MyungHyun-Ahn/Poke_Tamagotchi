using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Pokemon;

namespace Assets.Scripts.Player
{
    class Player
    {
        enum MAX_POKEMON_COUNT
        {
            LEVEL_1 = 4,
            LEVEL_5 = 12,
            LEVEL_10 = 16,
            LEVEL_20 = 20,
            LEVEL_30 = 25,
            LEVEL_50 = 30
        }

        enum PLAYER_DATA
        {
            MAX_MONEY = 99999999,
            MAX_LEVEL = 50
        }

        private string _playerName;

        // private int _money;
        private int _playerLevel;
        private int _requireExp;
        private int _playerExp;

        private int _maxPokemonCount;
        private PokemonList _pokeList;

        public Player()
        {
            // 서버에서 데이터 받아오기
            // 경험치, 돈, 포켓몬 정보
            // _money = 99999999;
            _playerLevel = 1;
            setRequireExp();

            // 포켓몬 정보 가져와서 리스트에 삽입
            _pokeList = new PokemonList();
            _pokeList.Push(new PokemonData(1));
            _pokeList.Push(new PokemonData(4));
            _pokeList.Push(new PokemonData(7));

        }

        public int getRequireExp()
        {
            return _requireExp;
        }

        private void setRequireExp()
        {
            if (_playerLevel == 1)
                _requireExp = 100;
            else
                _requireExp = 100 + (_playerLevel - 1) * 100;
        }

        public void addExp(int exp)
        {
            _playerExp += exp;
        }

        public int getExp()
        {
            return _playerExp;
        }

        public void setMaxPokemonCount()
        {
            if (_playerLevel >= 1 && _playerLevel < 5)
                _maxPokemonCount = (int)MAX_POKEMON_COUNT.LEVEL_1;
            else if (_playerLevel >= 5 && _playerLevel < 10)
                _maxPokemonCount = (int)MAX_POKEMON_COUNT.LEVEL_5;
            else if (_playerLevel >= 10 && _playerLevel < 20)
                _maxPokemonCount = (int)MAX_POKEMON_COUNT.LEVEL_10;
            else if (_playerLevel >= 20 && _playerLevel < 30)
                _maxPokemonCount = (int)MAX_POKEMON_COUNT.LEVEL_20;
            else if (_playerLevel >= 30 && _playerLevel < 50)
                _maxPokemonCount = (int)MAX_POKEMON_COUNT.LEVEL_30;
            else if (_playerLevel >= 50)
                _maxPokemonCount = (int)MAX_POKEMON_COUNT.LEVEL_50;
            else
                Debug.Log("Invalid Level");

        }

        public int getMaxPokemonCount()
        {
            return _maxPokemonCount;
        }

        public int getPokemonCount()
        {
            return _pokeList.Count();
        }

        public PokemonData getPokeData(int index)
        {
            return _pokeList.GetData(index);
        }

        public PokemonList getPokeList()
        {
            return _pokeList;
        }
    }
}
