using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class SkillStorageData : IData
    {
        private Dictionary<Skill, int> _datum;
        private Dictionary<Skill, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<Skill, int> SetUpDatum()
        {
            Dictionary<Skill, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as Skill, 0);
            return datum;
        }

        public static IItem[] Items => new Skill[] {
            new Apophenia(), new Preparation(), new CosmicConsciousness(), new LightTouch(),
            new CelestialNavigation(), new PulsePerception() };

        public string GetDescription(IItem item)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            return Datum[(Skill)item].ToString();
        }

        public void DecreaseLevel(IItem item)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            Datum[(Skill)item] = -1 < 0 ? 0 : Datum[(Skill)item] - 1;
            PersistentData.Save(this);
        }

        public int GetLevel(IItem item)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            return Datum[(Skill)item];
        }

        public void IncreaseLevel(IItem item)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            Datum[(Skill)item] += Datum[(Skill)item] + 1 > ((Skill)item).MaxLevel ? 0 : 1;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            Datum[(Skill)item] = level;
            PersistentData.Save(this);
        }

        private void LoadLevel(IItem item, int level)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            Datum[(Skill)item] = level;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private SkillStorageData() { }

        public static SkillStorageData GetData()
        {
            SkillStorageData data = new();
            if (data.PersistentData.TryLoadData() is not SkillStorageData loadData) return data;
            for (int i = 0; i < Items.Length; i++)
                try { data.LoadLevel(Items[i], loadData.GetLevel(Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Materials.Data");
    }

}