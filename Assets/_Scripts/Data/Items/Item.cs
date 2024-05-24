using System;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Data.Two
{
    public interface IItem
    {
        public string Name { get; }
        public string Description { get; }
        public int ID { get; }
    }

    public interface IData
    {
        public IItem[] Items { get; }
        public string GetDisplayLevel(IItem item);
        public string GetDescription(IItem item);
        public int GetLevel(IItem item);
        public void AdjustLevel(IItem item, int level);
        public void SetLevel(IItem item, int level);
        public bool InventoryIsFull(int space);
        public void Reset();
        public IPersistentData PersistentData { get; }
    }

    public interface IPersistentData
    {
        public IData TryLoadData();
        public void Save(IData data);
        public string FileName { get; }
    }

    [System.Serializable]
    public class SaveData : IPersistentData
    {
        public string FileName { get; }

        public SaveData(string name) { FileName = name; }

        public void Save(IData data)
        {
            FileStream fileStream = new(Application.persistentDataPath + FileName, FileMode.Create);
            new BinaryFormatter().Serialize(fileStream, data);
            fileStream.Close();
        }

        public IData TryLoadData()
        {
            IData tryLoadData = null;

            if (File.Exists(Application.persistentDataPath + FileName))
            {
                FileStream stream = new(Application.persistentDataPath + FileName, FileMode.Open)
                {
                    Position = 0
                };

                try
                {
                    tryLoadData = new BinaryFormatter().Deserialize(stream) as IData;
                    stream.Close();
                }

                catch
                {
                    stream.Close();
                    Debug.Log("A loading error has ocurred with " + FileName);
                }
            }

            return tryLoadData is not null ? tryLoadData : null;
        }
    }

    [System.Serializable]
    public class NotPersistentData : IPersistentData
    {
        public string FileName => "";
        public void Save(IData data) { }
        public IData TryLoadData() => null;
    }
}