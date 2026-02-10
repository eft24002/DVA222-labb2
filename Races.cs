using System;

namespace RpgCharacters
{
    // Race
    public interface IRace
    {
        string Name { get; }
        int Strength { get; }
        int Agility { get; }
        int Intelligence { get; }
        int MaxHp { get; }
        string GetVictoryMessage(Random rng);
    }

    public abstract class RaceBase : IRace
    {
        private readonly string[] victoryM;

        public string Name {get; }
        public int Strength { get; }
        public int Agility { get; }
        public int Intelligence { get; }
        public int MaxHp { get; }

        protected RaceBase(string name, int strength, int agility, int intelligence, int maxHp, params string[] victoryMessage)
        {
            Name = name;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            MaxHp = maxHp;

            victoryM = victoryMessage?.Length > 0 ? victoryMessage : new[] {$"{Name} celebrates!"};
        }

        public string GetVictoryMessage(Random rng)
        {
            return victoryM[rng.Next(victoryM.Length)];
        }
    }

    public sealed class FairyRace : RaceBase
    {
        public FairyRace() : base(
            name: "Fairy",
            strength: 2,
            agility: 5,
            intelligence: 9,
            maxHp: 20,
            victoryMessage: " Fairy laughs triumphantly!")
        { }
    }

    public sealed class OrcRace : RaceBase
    {
        public OrcRace() : base(
            name: "Orc",
            strength: 10,
            agility: 3,
            intelligence: 2,
            maxHp: 40,
            victoryMessage: " Orc roars victoriously!")
        { }
    }

    public sealed class ElfRace : RaceBase
    {
        public ElfRace() : base(
            name: "Elf",
            strength: 4,
            agility: 7,
            intelligence: 6,
            maxHp: 30,
            victoryMessage: " Elf bows gracefully in victory!")
        { }
    }

    public sealed class HumanRace : RaceBase
    {
        public HumanRace() : base(
            name: "Human",
            strength: 6,
            agility: 6,
            intelligence: 6,
            maxHp: 35,
            victoryMessage: " Human raises a fist in triumph!")
        { }
    }
}
