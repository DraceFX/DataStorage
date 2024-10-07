using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadProgram
{
    private List<StringData> _gameObjectDataList;
    private string _savePath = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "programmData.json");

    public void SaveProgramsData(List<GameObject> gameObjects)
    {
        Debug.Log(_savePath);

        _gameObjectDataList = new List<StringData>();

        foreach (var item in gameObjects)
        {
            StringData data = item.GetComponent<DraggableItem>().Data;

            _gameObjectDataList.Add(data);
        }

        string json = JsonUtility.ToJson(new Serialization<StringData>(_gameObjectDataList), true);
        File.WriteAllText(_savePath, json);
    }

    public List<StringData> LoadProgramsData()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);

            Serialization<StringData> serialization = JsonUtility.FromJson<Serialization<StringData>>(json);
            return _gameObjectDataList = new List<StringData>(serialization.Items);
        }
        return null;
    }

    [System.Serializable]
    private class Serialization<T>
    {
        public List<T> Items;

        public Serialization(List<T> items)
        {
            this.Items = items;
        }
    }
}
