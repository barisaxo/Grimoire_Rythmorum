using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class SkillData : IData
    {
        private Dictionary<ISkill, int> _datum;
        private Dictionary<ISkill, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<ISkill, int> SetUpDatum()
        {
            Dictionary<ISkill, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as ISkill, 0);
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
        public float GetBonusRatio(IItem item) => 1 + (.01f * Datum[(ISkill)item] * ((ISkill)item).Per);

        public int GetSkillCost(IItem item)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            return ((ISkill)item).Cost * (Datum[(ISkill)item] + 1);
        }

        public string GetDescription(IItem item)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            return Datum[(ISkill)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            return Datum[(ISkill)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            Datum[(ISkill)item] =
                Datum[(ISkill)item] + i > ((ISkill)item).MaxLevel ? ((ISkill)item).MaxLevel :
                Datum[(ISkill)item] + i < 0 ? 0 :
                Datum[(ISkill)item] + i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            Datum[(ISkill)item] = level;
            PersistentData.Save(this);
        }

        private void LoadLevel(IItem item, int level)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            Datum[(ISkill)item] = level;
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