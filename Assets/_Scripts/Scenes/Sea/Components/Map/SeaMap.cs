using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;

namespace Sea.Maps
{
    public interface IMap
    {
        /// <summary>
        /// The x&y grid length of regions.
        /// </summary>
        public int RegionResolution { get; }
        /// <summary>
        /// The subdivision x&y length of cells in a region.
        /// </summary>
        public int RegionSize { get; }
        /// <summary>
        /// Size * RegionSize = the x&y length regions on the map.
        /// </summary>
        public int GlobalSize { get; }
        public Region[] Regions { get; }
        public Dictionary<Vector2Int, Feature[]> Features { get; }
        public Dictionary<Vector2Int, R> Territories { get; }
        public Dictionary<Vector2Int, R> LightHouses { get; }
        public Vector2Int[] ShippingLanes { get; }
        public Vector2Int[] Docks { get; }
        public Vector2Int[] Gunneries { get; }
        public Vector2Int[] Islands { get; }
        public Vector2Int[] Coves { get; }
        //Lane, LightHouse, Cove, Dock, ShipYard, Gunnery, Island
    }

    public class IosMap : IMap
    {
        public int RegionResolution { get; private set; } = 3;
        public int RegionSize { get; private set; } = 30;
        public int GlobalSize { get; private set; }
        public Region[] Regions { get; private set; }

        public Dictionary<Vector2Int, Feature[]> Features => throw new System.NotImplementedException();

        public Dictionary<Vector2Int, R> Territories => throw new System.NotImplementedException();

        public Dictionary<Vector2Int, R> LightHouses => throw new System.NotImplementedException();

        public Vector2Int[] ShippingLanes => throw new System.NotImplementedException();

        public Vector2Int[] Docks => throw new System.NotImplementedException();

        public Vector2Int[] Gunneries => throw new System.NotImplementedException();

        public Vector2Int[] Islands => throw new System.NotImplementedException();

        public Vector2Int[] Coves => throw new System.NotImplementedException();

        public IosMap(Data.Two.Manager data)
        {
            GlobalSize = RegionResolution * RegionSize;
            Regions = new Region[RegionResolution * RegionResolution];

            int i = 0;
            for (int x = 0; x < RegionResolution; x++)
                for (int y = 0; y < RegionResolution; y++)
                    Regions[i++] = new(new Vector2Int(x, y), RegionSize, data, Territories.GetValueOrDefault(new Vector2Int(x, y)));
        }


    }



    public class WorldMap : IMap
    {
        public int RegionResolution { get; private set; } = 12;
        public int RegionSize { get; private set; } = 30;
        public int GlobalSize { get; private set; }
        public Region[] Regions { get; private set; }

        public WorldMap(Data.Two.Manager data)
        {
            GlobalSize = RegionResolution * RegionSize;
            Regions = new Region[RegionResolution * RegionResolution];

            int i = 0;
            for (int x = 0; x < RegionResolution; x++)
                for (int y = 0; y < RegionResolution; y++)
                    Regions[i++] = new(new Vector2Int(x, y), RegionSize, data, Territories.GetValueOrDefault(new Vector2Int(x, y)));
        }

        public readonly Dictionary<Vector2Int, R> Territories = new()
        {
            //locria
            {new (2, 8), R.s}, {new (1, 8), R.s}, {new (1, 9), R.s}, {new (2, 9), R.s}, {new (3, 9), R.s},
            {new (4, 9), R.s}, {new (5,9), R.s}, {new (4, 10), R.s}, {new (5, 10), R.s},
            
            //Doria
            {new (9, 6), R.d}, {new (10, 6), R.d}, {new (8, 6), R.d}, {new (8, 7), R.d}, {new (9, 7), R.d},
            {new (10, 7), R.d}, {new (8,8), R.d}, {new (10, 8), R.d}, {new (11, 8), R.d},
            
            //Ios
            {new (4, 5), R.i}, {new (5, 5), R.i}, {new (6, 5), R.i}, {new (4, 6), R.i}, {new (5, 6), R.i},
            {new (6, 6), R.i}, {new (4,7), R.i}, {new (5, 7), R.i}, {new (6, 7), R.i},
            
            //Phrygia
            {new (2, 3), R.p}, {new (3, 3), R.p}, {new (1, 4), R.p}, {new (2, 4), R.p}, {new (1, 5), R.p},
            {new (2, 5), R.p}, {new (0, 6), R.p}, {new (1, 6), R.p}, {new (2, 6), R.p},
            
            //Aeolia
            {new (1, 0), R.a}, {new (2, 0), R.a}, {new (3, 0), R.a}, {new (4, 0), R.a}, {new (5, 0), R.a},
            {new (1, 1), R.a}, {new (2, 1), R.a}, {new (3, 1), R.a}, {new (4, 1), R.a},

            //Lydia
            {new (7, 0), R.l}, {new (8, 0), R.l}, {new (7, 1), R.l}, {new (8, 1), R.l}, {new (7, 2), R.l},
            {new (8, 2), R.l}, {new (6, 2), R.l}, {new (6, 3), R.l}, {new (7, 3), R.l},
            
            //MixoLydia
            {new (10, 1), R.m}, {new (11, 1), R.m}, {new (10, 2), R.m}, {new (11, 2), R.m}, {new (10, 3), R.m},
            {new (11, 3), R.m}, {new (9, 4), R.m}, {new (10, 4), R.m}, {new (11, 4), R.m},

            //pirate
            {new (9, 10), R.t}
        };

        public Dictionary<Vector2Int, R> LightHouses { get; private set; } = new(){
            { new (1,0), R.a },
            { new (8,0), R.l },
            { new (11,4), R.m },
            { new (0,6), R.p },
            { new (5,6), R.i },
            { new (11,8), R.d },
            { new (5,10), R.s },
            { new (9,10), R.t },
        };

        public Dictionary<Vector2Int, Feature[]> Features { get; private set; } = new(){
            { new (1,0), new[]{Feature.LightHouse} },
            { new (8,0),  new[]{Feature.LightHouse}},
            { new (11,4), new[]{Feature.LightHouse }},
            { new (0,6),  new[]{Feature.LightHouse}},
            { new (5,6),  new[]{Feature.LightHouse, Feature.Cove}},
            { new (11,8), new[]{Feature.LightHouse }},
            { new (5,10), new[]{Feature.LightHouse }},
            { new (9,10), new[]{Feature.LightHouse }},
        };

        public Vector2Int[] ShippingLanes { get; private set; } = new Vector2Int[]{
            new(6,0), new(3,2), new(9,2), new(9,5), new(6,4), new(1,7), new(6,0)
        };

        Dictionary<Vector2Int, R> IMap.Territories => throw new System.NotImplementedException();

        public Vector2Int[] Docks => throw new System.NotImplementedException();

        public Vector2Int[] Gunneries => throw new System.NotImplementedException();

        public Vector2Int[] Islands => throw new System.NotImplementedException();

        public Vector2Int[] Coves => throw new System.NotImplementedException();
    }


    public enum Feature { Lane, LightHouse, Cove, Dock, ShipYard, Gunnery, Island }


    /// <summary>Regional Occupiers </summary>
    public enum R
    {
        /// <summary> Uncontrolled </summary>
        o,

        /// <summary> Pirate berth </summary>
        t,

        /// <summary> Shipping Lanes </summary>
        c,

        /// <summary> Aeolian </summary>
        a,

        /// <summary> Dorian </summary>
        d,

        /// <summary> Phrygian </summary>
        p,

        /// <summary> Ionian </summary>
        i,

        /// <summary> Lydian </summary>
        l,

        /// <summary> Mixolydian </summary>
        m,

        /// <summary> Locrian </summary>
        s,

        /// <summary>Null Cove </summary>
        n,
    }

    public class SmallMap : IMap
    {
        public int RegionResolution { get; private set; } = 1;
        public int RegionSize { get; private set; } = 30;
        public int GlobalSize { get; private set; }
        public Region[] Regions { get; private set; }

        public SmallMap(Data.Two.Manager data)
        {
            GlobalSize = RegionResolution * RegionSize;
            Regions = new Region[RegionResolution * RegionResolution];

            int i = 0;
            for (int x = 0; x < RegionResolution; x++)
                for (int y = 0; y < RegionResolution; y++)
                    Regions[i++] = new(new Vector2Int(x, y), RegionSize, data, Territories.GetValueOrDefault(new Vector2Int(x, y)));
        }

        public readonly Dictionary<Vector2Int, R> Territories = new()
        {
            //Ios
            {new (0, 0), R.i},
        };

        public Dictionary<Vector2Int, R> LightHouses { get; private set; } = new(){
            { new (0,0), R.i },
        };

        public Dictionary<Vector2Int, Feature[]> Features { get; private set; } = new(){
            { new (0,0),  new[]{Feature.LightHouse, Feature.Cove}},
        };

        public Vector2Int[] ShippingLanes { get; private set; } = new Vector2Int[]{
        };

        Dictionary<Vector2Int, R> IMap.Territories => throw new System.NotImplementedException();

        public Vector2Int[] Docks => throw new System.NotImplementedException();

        public Vector2Int[] Gunneries => throw new System.NotImplementedException();

        public Vector2Int[] Islands => throw new System.NotImplementedException();

        public Vector2Int[] Coves => throw new System.NotImplementedException();
    }
}

// public readonly R[] RegionMap = new R[]{
//       //    0     1     2     3     4     5     6     7     8     9     10    11     
//     /*0*/  R.o,  R.a,  R.a,  R.a,  R.a,  R.a,  R.c,  R.l,  R.l,  R.o,  R.o,  R.o,   

//     /*1*/  R.o,  R.a,  R.a,  R.a,  R.a,  R.o,  R.o,  R.l,  R.l,  R.o,  R.m,  R.m,   

//     /*2*/  R.o,  R.o,  R.o,  R.c,  R.o,  R.o,  R.l,  R.l,  R.l,  R.c,  R.m,  R.m,   

//     /*3*/  R.o,  R.o,  R.p,  R.p,  R.o,  R.o,  R.l,  R.l,  R.o,  R.o,  R.m,  R.m,   

//     /*4*/  R.o,  R.p,  R.p,  R.o,  R.o,  R.o,  R.c,  R.o,  R.o,  R.m,  R.m,  R.m,   

//     /*5*/  R.o,  R.p,  R.p,  R.o,  R.i,  R.i,  R.i,  R.o,  R.o,  R.c,  R.o,  R.o,   

//     /*6*/  R.p,  R.p,  R.p,  R.o,  R.i,  R.i,  R.i,  R.o,  R.d,  R.d,  R.d,  R.o,   

//     /*7*/  R.o,  R.c,  R.o,  R.o,  R.i,  R.i,  R.i,  R.o,  R.d,  R.d,  R.d,  R.o,   

//     /*8*/  R.o,  R.s,  R.s,  R.o,  R.o,  R.o,  R.o,  R.o,  R.d,  R.o,  R.d,  R.d,   

//     /*9*/  R.o,  R.s,  R.s,  R.s,  R.s,  R.s,  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,   

//     /*a*/  R.o,  R.o,  R.o,  R.o,  R.s,  R.s,  R.o,  R.o,  R.o,  R.t,  R.o,  R.o,   

//     /*b*/  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,  R.o,
//       //    0     1     2     3     4     5     6     7     8     9     10    11  
// };

// public static class RToRegionHelper{
//     public static MusicTheory.RegionalMode RToRegion(this Sea.Maps.R r) => r switch{
//         Sea.Maps.R.a =
//     }
// }