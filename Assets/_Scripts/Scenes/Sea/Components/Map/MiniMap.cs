using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sea.Maps
{
    public class MiniMap
    {
        public MiniMap(WorldMap map)
        {
            Regions = new Card[map.Size * map.Size];
            int i = 0;

            for (int x = 0; x < map.Size; x++)
                for (int y = 0; y < map.Size; y++)
                    Regions[i++] = Card.CreateChild(nameof(Region) + x + ":" + y, Card.Canvas)
                        .SetImageSprite(Assets.White)
                        .SetImagePosition(new Vector2(1 - Cam.UIOrthoX + (x * .15f), 1 - Cam.UIOrthoY + (y * .15f)))
                        .SetImageSize(Vector2.one * .15f)
                        .SetImageColor(
                            map.GetSeaColorFromRegion(
                                map.Territories.TryGetValue(new Vector2Int(x, y), out R value)
                                    == true ? value : R.o))
                        ;
        }

        private Card _card;
        public Card Card => _card ??= new(nameof(MiniMap), null);

        public Card[] Regions;

        public void SelfDestruct()
        {
            Card.SelfDestruct();
            Resources.UnloadUnusedAssets();
        }

    }

    public static class MiniMapSystems
    {
        public static void BlinkMiniMap(this MiniMap mm, Vector2Int regionalCoord, int size)
        {
            for (int i = 0; i < mm.Regions.Length; i++)
            {
                mm.Regions[i].SetImageColor(new Color(
                    mm.Regions[i].Image.color.r,
                    mm.Regions[i].Image.color.g,
                    mm.Regions[i].Image.color.b,
                    .5f));
            }

            int index = regionalCoord.Vec2ToInt(size);
            mm.Regions[index].SetImageColor(new Color(
                mm.Regions[index].Image.color.r,
                mm.Regions[index].Image.color.g,
                mm.Regions[index].Image.color.b,
               .5f + (Mathf.Sin(Time.time * 4) * .5f)));
        }
    }
}