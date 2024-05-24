using System;
namespace Data.Two
{
    public interface IQuest : IItem
    {
        QuestEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public readonly struct Navigation : IQuest { public readonly QuestEnum Enum => QuestEnum.Navigation; }
    [Serializable] public readonly struct Bounty : IQuest { public readonly QuestEnum Enum => QuestEnum.Bounty; }

    [Serializable]
    public class QuestEnum : Enumeration
    {
        public QuestEnum() : base(0, null) { }
        public QuestEnum(int id, string name) : base(id, name) { }
        public QuestEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public readonly static QuestEnum Navigation = new(0, "Navigation", "Sail to the location.");
        public readonly static QuestEnum Bounty = new(1, "Bounty", "Hunt down the pirate ship.");

        internal static IItem ToItem(QuestEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Navigation => new Navigation(),
                _ when @enum == Bounty => new Bounty(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}