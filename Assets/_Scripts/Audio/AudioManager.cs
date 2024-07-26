
using Data;

namespace Audio
{
    public class AudioManager
    {
        private BGMusic_AudioSystem _bgMusic;
        public BGMusic_AudioSystem BGMusic => _bgMusic ??= new(VolumeData);

        private BeatFishing_AudioSystem _beatFishing;
        public BeatFishing_AudioSystem BeatFishing => _beatFishing ??= new(VolumeData);

        private SoundFXAudioSystem _sfx;
        public SoundFXAudioSystem SFX => _sfx ??= new(VolumeData);

        private Batterie_AudioSystem _batterie;
        public Batterie_AudioSystem Batterie => _batterie ??= new(VolumeData);

        private AmbienceAudioSystem _ambience;
        public AmbienceAudioSystem Ambience => _ambience ??= new(VolumeData);

        private KeyboardAudioSystem _kba;
        public KeyboardAudioSystem KBAudio => _kba ??= new(VolumeData);

        private static VolumeData VolumeData => Manager.Io.Volume;

        private AudioParserB _audioParser = new();
        public AudioParserB AudioParser => _audioParser ??= new();


        //readonly VolumeData VolumeData;
        #region INSTANCE

        //public AudioManager(VolumeData vd) { VolumeData = vd; }

        public static AudioManager Io => Instance.Io;

        private static class Instance
        {
            private static AudioManager _io;
            internal static AudioManager Io => _io ??= new AudioManager();
        }

        #endregion INSTANCE
    }
}