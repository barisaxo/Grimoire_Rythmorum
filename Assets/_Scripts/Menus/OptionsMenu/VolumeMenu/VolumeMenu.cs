using UnityEngine;
using System.Collections.Generic;

namespace Menus.OptionsMenu
{
    public class VolumeMenu : Menu<VolumeData.DataItem, VolumeMenu>
    {
        public VolumeMenu() : base(nameof(VolumeMenu)) { }

        public Menu<VolumeData.DataItem, VolumeMenu> Initialize(VolumeData data)
        {
            this.UpdateAllItems(data);
            return base.Initialize();
        }

        public override MenuLayoutStyle Style => MenuLayoutStyle.TwoColumns;
    }

}
