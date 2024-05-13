using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class SkillData : IData
    {
        private Dictionary<Skill, int> _datum;
        private Dictionary<Skill, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<Skill, int> SetUpDatum()
        {
            Dictionary<Skill, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as Skill, 0);
            return datum;
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<SkillEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < items.Length; i++)
                        items[i] = SkillEnum.ToItem(enums[i]);

                    return items;
                }
            }
        }
        public float GetBonusRatio(IItem item) => 1 + (.01f * Datum[(Skill)item] * ((Skill)item).Per);

        public int GetSkillCost(IItem item)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            return ((Skill)item).Cost * (Datum[(Skill)item] + 1);
        }

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


        public int GetLevel(IItem item)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            return Datum[(Skill)item];
        }

        // public void DecreaseLevel(IItem item, int i)
        // {
        //     if (item is not Skill) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Skill)item] = Datum[(Skill)item] - i < 0 ? 0 : Datum[(Skill)item] - i;
        //     PersistentData.Save(this);
        // }
        // public void IncreaseLevel(IItem item, int i)
        // {
        //     if (item is not Skill) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Skill)item] += Datum[(Skill)item] + i > ((Skill)item).MaxLevel ? 0 : i;
        //     PersistentData.Save(this);
        // }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not Skill) throw new System.Exception(item.GetType().ToString());
            Datum[(Skill)item] =
                Datum[(Skill)item] + i > ((Skill)item).MaxLevel ? ((Skill)item).MaxLevel :
                Datum[(Skill)item] + i < 0 ? 0 :
                Datum[(Skill)item] + i;
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

        private SkillData() { }

        public static SkillData GetData()
        {
            SkillData data = new();
            if (data.PersistentData.TryLoadData() is not SkillData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Skills.Data");
    }

}