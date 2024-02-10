
public class CharacterData
{
    public int MaxHealth = 50;

    private int _currentHealth = 50;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = (value > MaxHealth) ? MaxHealth : value;
        }
    }

    public int
        Coins = 150, Materials = 20, Rations = 25, Gramos = 2, Maps = 2;

    public bool Map, Sextant;

    public Sea.Region[] ActivatedLighthouses = new Sea.Region[] { };
    // public NumberOfCannons NumberOfCannons = NumberOfCannons.Four;

}
// public enum NumberOfCannons { Four, Eight, Sixteen, ThirtyTwo, SixtyFour }

/*
TODO: Capsa: A scroll container
TODO: Codex: A book to hold blueprints
TODO:  Schemas (or schemata) are units of understanding that can be hierarchically 
todo      categorized as well as webbed into complex relationships with one another.
*/