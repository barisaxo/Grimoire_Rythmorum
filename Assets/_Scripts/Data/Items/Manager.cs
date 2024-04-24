using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data.Two
{
    public class Manager
    {
        #region  INSTANCE
        private Manager() { }
        public static Manager Io => Instance.Io;
        private class Instance
        {
            static Instance() { }
            static Manager _io;
            internal static Manager Io => _io ??= new();
        }
        #endregion INSTANCE


        private WoodInventoryData _woodInventoryData;
        public WoodInventoryData WoodInventoryData => _woodInventoryData ??= new();

        private WoodStorageData _woodStorageData;
        public WoodStorageData WoodStorageData => _woodStorageData ??= WoodStorageData.GetData();

        private FishInventoryData _fishInventoryData;
        public FishInventoryData FishInventoryData => _fishInventoryData ??= new();

        private StarChartInventoryData _starChartInventoryData;
        public StarChartInventoryData StarChartInventoryData => _starChartInventoryData ??= new();


    }
}