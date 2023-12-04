using MusicTheory;
using MusicTheory.Rhythms;

public class GameplayData
{
    private BatterieDifficulty _batterie_Difficulty;

    private CadenceDifficulty _cadence_Difficulty;

    private KeyOf _currentKey = KeyOf.C;

    private int _latency = 5;

    public bool EasyModeWon = false;

    public bool ExplainBatterie = true;

    public GameDifficulty GameDifficulty = 0;
    public bool MediumModeWon = false;

    public CellShape RecentCell;


    public BatterieDifficulty Batterie_Difficulty
    {
        get => _batterie_Difficulty;
        set => _batterie_Difficulty = _batterie_Difficulty + (int)value < 0 || (int)(_batterie_Difficulty + (int)value) > 5
            ? _batterie_Difficulty : value;
    }

    public CadenceDifficulty Cadence_Difficulty
    {
        get => _cadence_Difficulty;
        set => _cadence_Difficulty = _cadence_Difficulty + (int)value < 0 || (int)(_cadence_Difficulty + (int)value) > 3
            ? _cadence_Difficulty : value;
    }

    public int Latency
    {
        get => _latency;
        set => _latency = value > 25 ? 0 : value;
    }

    public KeyOf CurrentKey
    {
        get => _currentKey;
        set => _currentKey = value > KeyOf.B ? KeyOf.C : value < KeyOf.C ? KeyOf.B : value;
    }

    public string GetData(DataItem item)
    {
        return item switch
        {
            _ when item == DataItem.Transpose => CurrentKey.ToString(),
            _ when item == DataItem.Latency => Latency.ToString(),
            _ => ""
        };
    }

    public void IncreaseItem(DataItem item)
    {
        if (item == DataItem.Transpose) CurrentKey++;
        else if (item == DataItem.Latency) Latency++;
    }
    public void DecreaseItem(DataItem item)
    {
        if (item == DataItem.Transpose) CurrentKey--;
        else if (item == DataItem.Latency) Latency--;
    }

    public class DataItem : DataEnum
    {
        public static readonly DataItem Latency = new(0, "LATENCY",
            "Lag offset for rhythm input. The margin for an accurate hit is +- 15." +
            "\nIf you are missing beats try adjusting this latency. Default setting is 5");

        public static readonly DataItem Transpose = new(1, "KEY TRANSPOSITION",
            "C: Concert pitch: flute, piano, guitar, violin, etc..." +
            "\nEb: Alto & baritone saxophone" +
            "\nF: French horn" +
            "\nBb: Clarinet, trumpet, soprano & tenor saxophone" +
            "\nB: Guitar in Eb standard tuning");

        public static DataItem Tuning = new(2, "TUNING NOTE A 440",
            "If your 'A' note doesn't match this \nyou might be out of tune, or in the wrong key");


        public DataItem() : base(0, "")
        {
        }

        public DataItem(int id, string name) : base(id, name)
        {
        }

        private DataItem(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }
    }

    //public CellShape RecentCell = 0;


    private RegionalMode _currentLevel = RegionalMode.Dorian;
    public RegionalMode CurrentLevel
    {
        get => _currentLevel;
        set { _currentLevel = value; }
    }

    // private static List<KeyOf> _availableKeys;
    // public static List<KeyOf> AvailableKeys => _availableKeys ??= new List<KeyOf>
    // {
    //     KeyOf.C,
    //     KeyOf.G,
    //     KeyOf.D,
    //     KeyOf.A,
    //     KeyOf.E,
    //     KeyOf.B,
    //     KeyOf.Gb,
    //     KeyOf.Db,
    //     KeyOf.Ab,
    //     KeyOf.Eb,
    //     KeyOf.Bb,
    //     KeyOf.F
    // };

    // public static KeyOf RandomAvailableKey => AvailableKeys[Random.Range(0, AvailableKeys.Count)];
    // public void AddToAvailableKeys(KeyOf key)
    // {
    //     if (!AvailableKeys.Contains(key)) AvailableKeys.Add(key);
    // }
    // public void RemoveFromAvailableKeys(KeyOf key)
    // {
    //     while (AvailableKeys.Contains(key)) AvailableKeys.Remove(key);
    // }

    // private static List<MusicalScale> _availableScales;
    // public static List<MusicalScale> AvailableScales => _availableScales ??= new List<MusicalScale>()
    // {
    //     MusicalScale.Major
    // };
    // public static MusicalScale RandomAvailableScale => AvailableScales[Random.Range(0, AvailableScales.Count)];
    // public void AddToAvailableScales(MusicalScale scale)
    // {
    //     if (!AvailableScales.Contains(scale)) AvailableScales.Add(scale);
    // }
    // public void RemoveFromAvailableScales(MusicalScale scale)
    // {
    //     while (AvailableScales.Contains(scale)) AvailableScales.Remove(scale);
    // }

    // private static List<Extension> _availableExtensions;
    // public static List<Extension> AvailableExtensions => _availableExtensions ??= new List<Extension>()
    // {
    //     Extension.Triad,
    //     Extension.Seventh
    // };
    // public static Extension RandomAvailableExtension => AvailableExtensions[Random.Range(0, AvailableExtensions.Count)];
    // public void AddToAvailableExtensions(Extension extension)
    // {
    //     if (!AvailableExtensions.Contains(extension)) { AvailableExtensions.Add(extension); }
    // }
    // public void RemoveFromAvailableExtensions(Extension extension)
    // {
    //     while (AvailableExtensions.Contains(extension)) { AvailableExtensions.Remove(extension); }
    // }

    // private static List<Genre> _availableGenres;
    // public static List<Genre> AvailableGenres => _availableGenres ??= new List<Genre>()
    // {
    //     0
    // };
    // public static Genre RandomAvailableGenre => AvailableGenres[Random.Range(0, AvailableGenres.Count)];
    // public void AddToAvailableExtensions(Genre genre)
    // {
    //     if (!AvailableGenres.Contains(genre)) { AvailableGenres.Add(genre); }
    // }
    // public void RemoveFromAvailableGenres(Genre genre)
    // {
    //     while (AvailableGenres.Contains(genre)) { AvailableGenres.Remove(genre); }
    // }

    // public TestPhase TestPhase = TestPhase.None;
}

// public enum TestPhase { None, Battery }
public enum BatterieDifficulty
{
    LVL1,
    LVL2,
    LVL3,
    LVL4,
    LVL5
}

public enum CadenceDifficulty
{
    I_II_V,
    I_IV_V_VI,
    ALL /* LVL4, LVL5, LVL6 */
}

public enum GameDifficulty
{
    Easy,
    Medium,
    Hard
}