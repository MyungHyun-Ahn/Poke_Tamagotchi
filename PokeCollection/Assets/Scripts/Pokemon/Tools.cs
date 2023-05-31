using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts.Tools
{
    class JsonDataLoader
    {

        // Default 경로 Assets 부터 ex) /Resources/Data/Stats.json
        private string _filePath;
        private byte[] _bfileData;
        private string _sfileData;

        public JsonDataLoader(string filePath)
        {
            Init(filePath);
        }

        public string getFilePath()
        {
            return _filePath;
        }

        public string getStringData()
        {
            return _sfileData;
        }

        private void Init(string filePath)
        {
            _filePath = Application.dataPath + filePath;
            FileStream fileStream = new FileStream(_filePath, FileMode.Open);
            _bfileData = new byte[fileStream.Length];
            fileStream.Read(_bfileData, 0, _bfileData.Length);
            fileStream.Close();
            _sfileData = ByteToString(_bfileData);
        }

        private string ByteToString(byte[] data)
        {
            string str = Encoding.Default.GetString(data);
            return str;
        }
        
        public JToken Parse(string objectName)
        {
            JObject jObject = JObject.Parse(_sfileData);
            JToken jToken = jObject[objectName];

            return jToken;
        }
    }
    class PokeDataLoader
    {
        private JsonDataLoader _jsonDataLoader;
        private JToken _dataArray;
        static string _jsonDataPath = "/Resources/Data/Stats.json";
        static string _pokemonPngPath = Application.dataPath + "/Resources/Pokemon/Generation1";
        private DirectoryInfo[] _directoryList;
        public PokeDataLoader()
        {
            Init();
        }

        private void Init()
        {
            _jsonDataLoader = new JsonDataLoader(_jsonDataPath);
            _dataArray = _jsonDataLoader.Parse("Pokemon");
        }

        public Sprite[] PokemonSpriteLoader(int pokeIndex)
        {
            pokeIndex -= 1;
            if (System.IO.Directory.Exists(_pokemonPngPath))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(_pokemonPngPath);

                // 파일 이름을 저장
                _directoryList = di.GetDirectories();
            }
            Sprite[] sprites = Resources.LoadAll<Sprite>("Pokemon/Generation1/" + _directoryList[pokeIndex].Name);
            return sprites;
        }

        public Sprite[] StatusSpriteLoader()
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("StatusIcon");
            return sprites;
        }


        // 포켓몬의 도감 번호를 받아 추출
        public void getPokemonInfo(int indexNum, out string name_eng, out string name_kor, out int[] stats, out string nextName)
        {
            stats = new int[5] { 0, 0, 0, 0, 0 };
            name_eng = "name_eng";
            name_kor = "name_kor";
            nextName = "null";
            foreach (JToken data in _dataArray)
            {
                if (indexNum == int.Parse(data["num"].ToString()))
                {
                    name_eng = data["name_eng"].ToString();
                    name_kor = data["name_kor"].ToString();
                    stats[0] = int.Parse(data["H"].ToString());
                    stats[1] = int.Parse(data["A"].ToString());
                    stats[2] = int.Parse(data["B"].ToString());
                    stats[3] = int.Parse(data["S"].ToString());
                    stats[4] = int.Parse(data["C"].ToString());
                    nextName = data["next"].ToString();
                    break;
                }
            }
        }

        // 포켓몬의 이름을 받는 버전
        public void getPokemonInfo(string name_eng, out int indexNum, out string name_kor, out int[] stats, out string nextName)
        {
            indexNum = 0;
            stats = new int[5] { 0, 0, 0, 0, 0 };
            name_kor = "name_kor";
            nextName = "null";
            foreach (JToken data in _dataArray)
            {
                if (name_eng == data["name_eng"].ToString())
                {
                    indexNum = int.Parse(data["num"].ToString());
                    name_kor = data["name_kor"].ToString();
                    stats[0] = int.Parse(data["H"].ToString());
                    stats[1] = int.Parse(data["A"].ToString());
                    stats[2] = int.Parse(data["B"].ToString());
                    stats[3] = int.Parse(data["S"].ToString());
                    stats[4] = int.Parse(data["C"].ToString());
                    nextName = data["next"].ToString();
                    break;
                }
            }
        }
    }


}
