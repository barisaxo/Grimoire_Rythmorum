using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramosData
{
    private (DataItem gramoItem, int count)[] _gramoLevels;
    private (DataItem gramoItem, int count)[] GramoLevels => _gramoLevels ??= SetUpGramoLevels();
    private (DataItem gramoItem, int count)[] SetUpGramoLevels()
    {
        var items = Enumeration.All<DataItem>();
        var gramoLevels = new (DataItem gramoItem, int count)[items.Length];

        for (int i = 0; i < items.Length; i++) gramoLevels[i] = (items[i], 0);

        return gramoLevels;
    }

    /// <summary>
    /// Give this to the menu objects text to display the current gramo count.
    /// </summary>
    /// <returns>an int 0 to 100</returns>
    public string GetItemCount(DataItem item) => GramoLevels[item].count.ToString();

    public void IncreaseCount(DataItem item) =>
        GramoLevels[item].count = GramoLevels[item].count + 5 > 100 ? 0 : GramoLevels[item].count + 5;

    public void DecreaseCount(DataItem item) =>
        GramoLevels[item].count = GramoLevels[item].count - 5 < 0 ? 100 : GramoLevels[item].count - 5;

    public void SetCount(DataItem item, int newGramoLevel) => GramoLevels[item].count = newGramoLevel;

    public class DataItem : DataEnum
    {
        public DataItem() : base(0, "") { }
        public DataItem(int id, string name) : base(id, name) { }
        public DataItem(int id, string name, string description) : base(id, name) => Description = description;
        public static DataItem Gramo = new(0, "Gramophone", "The Old Bards used Musica to store great treasures in these." +
            "If you don't answer with the correct combination, the Gramophone locks shut and it's contents are lost forever.");
    }
}
