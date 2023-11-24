
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
        Coins = 150, Materials = 20, Rations = 25;

    public bool Map, Sextant;

    // public NumberOfCannons NumberOfCannons = NumberOfCannons.Four;
}
// public enum NumberOfCannons { Four, Eight, Sixteen, ThirtyTwo, SixtyFour }