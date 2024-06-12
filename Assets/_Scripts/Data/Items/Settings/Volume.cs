using System;
namespace Data
{
    public interface IVolume : IItem
    {
        VolumeEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct BGMusic : IVolume { public readonly VolumeEnum Enum => VolumeEnum.BGMusic; }
    [Serializable] public struct SoundFX : IVolume { public readonly VolumeEnum Enum => VolumeEnum.SoundFX; }
    [Serializable] public struct Click : IVolume { public readonly VolumeEnum Enum => VolumeEnum.Click; }
    [Serializable] public struct BatterieVolume : IVolume { public readonly VolumeEnum Enum => VolumeEnum.Batterie; }
    [Serializable] public struct Chords : IVolume { public readonly VolumeEnum Enum => VolumeEnum.Chords; }
    [Serializable] public struct Bass : IVolume { public readonly VolumeEnum Enum => VolumeEnum.Bass; }
    [Serializable] public struct Drums : IVolume { public readonly VolumeEnum Enum => VolumeEnum.Drums; }

    [Serializable]
    public class VolumeEnum : Enumeration
    {
        public VolumeEnum() : base(0, "") { }
        public VolumeEnum(int id, string name) : base(id, name) { }
        public VolumeEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public static VolumeEnum BGMusic = new(0, "BG MUSIC", "Background music volume level");
        public static VolumeEnum SoundFX = new(1, "SOUND FX", "Sound effects volume level");
        public static VolumeEnum Click = new(2, "CLICK", "Metronome click volume level");
        public static VolumeEnum Batterie = new(3, "BATTERIE", "Batterie snare drum volume level");
        public static VolumeEnum Chords = new(4, "CHORDS", "Puzzle chords volume level");
        public static VolumeEnum Bass = new(5, "BASS", "Puzzle Bass volume level");
        public static VolumeEnum Drums = new(6, "DRUMS", "Puzzle drums volume level");

        public static IItem ToItem(VolumeEnum @enum) => @enum switch
        {
            _ when @enum == BGMusic => new BGMusic(),
            _ when @enum == SoundFX => new SoundFX(),
            _ when @enum == Click => new Click(),
            _ when @enum == Batterie => new BatterieVolume(),
            _ when @enum == Chords => new Chords(),
            _ when @enum == Bass => new Bass(),
            _ when @enum == Drums => new Drums(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }
}