using System.Collections.Generic;
using System;

namespace Data
{
    [Serializable]
    public class InventoryData : IData
    {
        private Dictionary<ICurrency, int> _datum;
        private Dictionary<ICurrency, int> Datum => _datum ??= SetUpDatum();
        private Dictionary<ICurrency, int> SetUpDatum()
        {
            Dictionary<ICurrency, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
            {
                datum.TryAdd((ICurrency)Items[i], Items[i] switch
                {
                    Gold => (int)(1000 * Manager.Io.Skill.GetBonusRatio(new Preparation()) * (Manager.Io.ActiveShip.ShipStats.HullStats.Hull.ID + 1)),
                    Material => (int)(Manager.Io.ActiveShip.GetLevel(new MaterialStorage()) * .5f * Manager.Io.Skill.GetBonusRatio(new Preparation()) * (Manager.Io.ActiveShip.ShipStats.HullStats.Hull.ID + 1)),
                    Ration => (int)(Manager.Io.ActiveShip.GetLevel(new RationStorage()) * .15f * Manager.Io.Skill.GetBonusRatio(new Preparation()) * (Manager.Io.ActiveShip.ShipStats.HullStats.Hull.ID + 1)),
                    StarChart => (int)(1.5f * Manager.Io.Skill.GetBonusRatio(new Preparation())) - 1 < 0 ? 0 : (int)(1f * Manager.Io.Skill.GetBonusRatio(new Preparation())) - 1,
                    Gramophone => 0,
                    _ => throw new System.ArgumentException(Items[i].Name)
                });
            }
            return datum;
        }

        public void Reset() => _datum = SetUpDatum();
        public string GetDisplayLevel(IItem item) => Datum[(ICurrency)item].ToString();
        public int GetLevel(IItem item) => Datum[(ICurrency)item];

        public void SetLevel(IItem item, int newInventoryLevel)
        {
            throw new System.NotImplementedException("You probably shouldn't be using this");
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not ICurrency) throw new ArgumentException(item.Name);
            switch ((ICurrency)item)
            {
                case Gold:
                    Datum[(ICurrency)item] = Datum[(ICurrency)item] + i < 0 ? 0 :
                    Datum[(ICurrency)item] + i;
                    break;

                case Material:
                    Datum[(ICurrency)item] = Datum[(ICurrency)item] + i < 0 ? 0 :
                    Datum[(ICurrency)item] + i > Data.Manager.Io.ActiveShip.GetLevel(new Data.MaterialStorage()) ?
                    Data.Manager.Io.ActiveShip.GetLevel(new Data.MaterialStorage()) :
                    Datum[(ICurrency)item] + i;
                    break;

                case Ration:
                    Datum[(ICurrency)item] = Datum[(ICurrency)item] + i < 0 ? 0 :
                    Datum[(ICurrency)item] + i > Data.Manager.Io.ActiveShip.GetLevel(new Data.RationStorage()) ?
                    Data.Manager.Io.ActiveShip.GetLevel(new Data.RationStorage()) :
                    Datum[(ICurrency)item] + i;
                    break;

                case StarChart:
                    Datum[(ICurrency)item] = Datum[(ICurrency)item] + i < 0 ? 0 :
                    Datum[(ICurrency)item] + i > Data.Manager.Io.ActiveShip.GetLevel(new Data.StarChartStorage()) ?
                    Data.Manager.Io.ActiveShip.GetLevel(new Data.StarChartStorage()) :
                    Datum[(ICurrency)item] + i;
                    break;

                case Gramophone:
                    Datum[(ICurrency)item] = Datum[(ICurrency)item] + i < 0 ? 0 :
                    Datum[(ICurrency)item] + i > Data.Manager.Io.ActiveShip.GetLevel(new Data.GramophoneStorage()) ?
                    Data.Manager.Io.ActiveShip.GetLevel(new Data.GramophoneStorage()) :
                    Datum[(ICurrency)item] + i;
                    break;
            }
        }

        private IItem[] _items;
        public IItem[] Items => _items ??= new IItem[]{
            new Gold(),
            new Material(),
            new Ration(),
            new StarChart(),
            new Gramophone()
        };

        public bool InventoryIsFull(int Space) => false;

        string IData.GetDescription(IItem item)
        {
            throw new NotImplementedException();
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();

    }

}
