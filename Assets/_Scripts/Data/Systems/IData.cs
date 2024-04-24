

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data
{
    public interface IData
    {
        public DataEnum[] DataItems { get; }
        /// <summary>
        /// Give this to the menu objects text to display the current volume level.
        /// </summary>
        public string GetDisplayLevel(DataEnum item);
        // public string GetDescription(DataEnum item);
        public int GetLevel(DataEnum item);
        public void IncreaseLevel(DataEnum item);
        public void DecreaseLevel(DataEnum item);
        public void SetLevel(DataEnum item, int level);
        public IPersistentData PersistentData { get; }
        public bool InventoryIsFull(int i);
        public void Reset();
        // public string FileName { get; }
        // public IData TryLoadData(IData data);
        // public void Save();
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
                    Debug.Log("A loading error has ocurred!");
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