using UnityEngine;
using System;
using OLD;

namespace OldMenus.OptionsMenu
{
    // public class VolumeMenu : Menu<VolumeData.DataItem, VolumeMenu>
    // {
    //     public VolumeMenu() : base(nameof(VolumeMenu), new TwoColumns<VolumeData.DataItem>())
    //     {
    //         West = new ButtonInput(IncreaseItem);
    //     }

    //     public Menu<VolumeData.DataItem, VolumeMenu> Initialize(VolumeData data)
    //     {
    //         this.UpdateAllItems(data);
    //         return base.Initialize();
    //     }

    //     private void IncreaseItem()
    //     {
    //         DataManager.Io.Volume.IncreaseLevel(Selection.Item);
    //         Selection.Card.SetTextString(Selection.Item.DisplayData(DataManager.Io.Volume));
    //         Audio.AudioManager.Io.BGMusic.VolumeLevelSetting = DataManager.Io.Volume.GetScaledLevel(VolumeData.DataItem.BGMusic);
    //         Audio.AudioManager.Io.SFX.VolumeLevelSetting = DataManager.Io.Volume.GetScaledLevel(VolumeData.DataItem.SoundFX);
    //     }

    // }

}
