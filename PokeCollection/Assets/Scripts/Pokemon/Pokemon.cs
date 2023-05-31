using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Scripts.Tools;

namespace Assets.Scripts.Pokemon
{
    public class PokemonData
    {
        // 포켓몬 정보 필드
        private int _indexNum;
        private string _name_eng;
        private string _name_kor;

        // 포켓몬 스텟 필드 : 처음에 생성자에서 불러오고 절대 변하지 X
        // 0 : hp, 1 : attack, 2 : _defense, 3 : _speed, 4: _sAttack
        private int[] _stats = new int[5] { 0, 0, 0, 0, 0 };

        // 포켓몬 훈련치 필드
        // 0 : hp, 1 : attack, 2 : _defense, 3 : _speed, 4: _sAttack
        private int[] _trainingStats = new int[5] { 0, 0, 0, 0, 0 };

        // 포켓몬 총합 스텟 필드
        private int[] _totalStats = new int[5] { 0, 0, 0, 0, 0 };

        // 진화 관련 - _evolutionValue를 쌓아 _evolution 값보다 높으면 진화
        private float _evolution = 100.0f;
        private float _evolutionExp = 0.0f;

        // 다음 진화의 이름
        private string _nextEvolutionName;

        public PokemonData(string name_eng)
        {
            int[] trainingStats = { 0, 0, 0, 0, 0 };
            Init(name_eng, trainingStats);
        }

        public PokemonData(string name_eng, int[] trainingStats)
        {
            Init(name_eng, trainingStats);
        }

        public PokemonData(int indexNum)
        {
            int[] trainingStats = { 0, 0, 0, 0, 0 };
            Init(indexNum, trainingStats);
        }

        public PokemonData(int indexNum, int[] trainingStats)
        {
            Init(indexNum, trainingStats);
        }

        private void Init(int indexNum, int[] trainingStats)
        {
            _indexNum = indexNum;
            PokeDataLoader pokeDataLoader = new PokeDataLoader();
            pokeDataLoader.getPokemonInfo(_indexNum, out _name_eng, out _name_kor, out _stats, out _nextEvolutionName);
            _trainingStats = trainingStats;
            calculTotalStats();
        }

        public void Init(string name_eng, int[] trainingStats)
        {
            _name_eng = name_eng;
            PokeDataLoader pokeDataLoader = new PokeDataLoader();
            pokeDataLoader.getPokemonInfo(_name_eng, out _indexNum, out _name_kor, out _stats, out _nextEvolutionName);
            _trainingStats = trainingStats;
            calculTotalStats();
        }

        // 포켓몬의 모든 정보 불러오기
        public void getInfo(out int index, out string name_eng, out string name_kor, out int[] stats, out int[] trainingStats, out int[] totalStats)
        {
            index = _indexNum;
            name_eng = _name_eng;
            name_kor = _name_kor;
            stats = _stats;
            trainingStats = _trainingStats;
            totalStats = _totalStats;
        }

        public int getindexNum()
        {
            return _indexNum;
        }

        public int[] getStats()
        {
            return _stats;
        }

        public int[] getTrainingStats()
        {
            return _trainingStats;
        }

        public void setTrainingStats(int[] trainingStats)
        {
            _trainingStats = trainingStats;
        }

        public int[] getTotalStats()
        {
            return _totalStats;
        }

        public string getNameKor()
        {
            return _name_kor;
        }

        public string getNextName()
        {
            return _nextEvolutionName;
        }

        public string getEvolutionName()
        {
            return _nextEvolutionName;
        }

        public void calculTotalStats()
        {
            for (int i = 0; i < 5; i++)
            {
                _totalStats[i] = _stats[i] + _trainingStats[i];
            }
        }

        public void addTraingStats(int target, int plusStat)
        {
            _trainingStats[target] += plusStat;
            calculTotalStats();
        }

        public bool CanEvolution()
        {
            if (_evolutionExp > _evolution && _nextEvolutionName != "null")
                return true;
            else
                return false;
        }

        public float getEvolutionExp()
        {
            return _evolutionExp;
        }

        public void AddEvolutionExp(int exp)
        {
            _evolutionExp += exp;
            if (_evolutionExp < 0)
                _evolutionExp = 0;
        }
    }

    class PokemonList
    {
        private List<PokemonData> _pokeList;

        public PokemonList()
        {
            _pokeList = new List<PokemonData>();
        }

        public void Push(PokemonData pokemon)
        {
            _pokeList.Add(pokemon);
        }

        public PokemonData GetData(int index)
        {
            PokemonData pokemon;
            if (_pokeList[index] != null)
                pokemon = _pokeList[index];
            else
                pokemon = null;
            return pokemon;
        }

        public int Count()
        {
            return _pokeList.Count();
        }
    }
}
