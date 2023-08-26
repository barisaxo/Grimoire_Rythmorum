namespace Audio
{
    public class AudioManager
    {
        private BGMusic_AudioSystem _bgMusic;
        private SoundFXAudioSystem _sfx;
        public SoundFXAudioSystem SFX => _sfx ??= new SoundFXAudioSystem(VolumeData);
        public BGMusic_AudioSystem BGMusic => _bgMusic ??= new BGMusic_AudioSystem(VolumeData);
        private Batterie_AudioSystem _batterie;
        public Batterie_AudioSystem Batterie => _batterie ??= new(VolumeData);

        readonly VolumeData VolumeData;
        #region INSTANCE

        public AudioManager(VolumeData vd) { VolumeData = vd; }

        //public static AudioManager Io => Instance.Io;

        //private static class Instance
        //{
        //    private static AudioManager _io;
        //    internal static AudioManager Io => _io ??= new AudioManager();
        //}

        #endregion INSTANCE
    }
}