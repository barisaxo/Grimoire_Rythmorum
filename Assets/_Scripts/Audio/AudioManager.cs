namespace Audio
{
    public class AudioManager
    {
        private BGMusic_AudioSystem _bgMusic;
        private SoundFXAudioSystem _sfx;
        public SoundFXAudioSystem SFX => _sfx ??= new SoundFXAudioSystem(VolumeData);
        public BGMusic_AudioSystem BGMusic => _bgMusic ??= new BGMusic_AudioSystem(VolumeData);

        private static VolumeData VolumeData => DataManager.Io.Volume;

        #region INSTANCE

        private AudioManager()
        {
        }

        public static AudioManager Io => Instance.Io;

        private static class Instance
        {
            private static AudioManager _io;
            internal static AudioManager Io => _io ??= new AudioManager();
        }

        #endregion INSTANCE
    }
}


//private SoundFX_AudioSystem _soundFX;
//public SoundFX_AudioSystem SoundFX => _soundFX ??= new(VolumeData);

//private MuscopaAudioManager _cadencePuzzle_AudioManager;
//public MuscopaAudioManager CadencePuzzle => _cadencePuzzle_AudioManager ??= new(VolumeData);
//public void StopCadence() { CadencePuzzle.StopTheCadence(); _cadencePuzzle_AudioManager = null; }

//private Battery_AudioSystem _batteryCannon;
//public Battery_AudioSystem BatteryCannon => _batteryCannon ??= new(VolumeData);
//public void StopBattery() { BatteryCannon.Stop(); _batteryCannon = null; }

//private BGMusic_AudioSystem _bgMusic;
//public BGMusic_AudioSystem BGMusic => _bgMusic ??= new(VolumeData);
// public void StopBGMusic() { BGMusic.Stop(); _bgMusic = null; }

// private GramoAudioManager _gramophone_AudioManager;
// public GramoAudioManager Gramophone => _gramophone_AudioManager ??= new(VolumeData);
// public void StopGramo() { Gramophone.StopTheGramo(); _gramophone_AudioManager = null; }