using System;
using System.Collections.Generic;
using System.Linq;

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

            victoryM = victoryMessage?.Length >0 ? victoryMessage : new[] {$"{Name} celebrates!"};
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

// Task 1b 
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

    // Cateegory

    public interface ICategory
    {
        string Name { get; }
        double InitialXp{ get; }

        string GetAttackMessage(string characterName);
        string GetDefendMessage(string characterName);

        double ComputeBaseAttack(IRace race);
        double ComputeBaseDefense(IRace race);
    }

    public abstract class CategoryBase : ICategory
    {
        public string Name { get; }
        public double InitialXp { get; }

        protected CategoryBase(string name, double initialXp)
        {
            Name = name;
            InitialXp = initialXp;
        }

        public abstract string GetAttackMessage(string characterName);
        public abstract string GetDefendMessage(string characterName);
        public abstract double ComputeBaseAttack(IRace race);
        public abstract double ComputeBaseDefense(IRace race);
    }

    public sealed class WarriorCategory : CategoryBase
    {
        public WarriorCategory() : base( "Warrior",  3.70)
        { }

        public override string GetAttackMessage(string n) => $" {n} swings a mighty sword!";
        public override string GetDefendMessage(string n) => $" {n} blocks with a sturdy shield!";

        public override double ComputeBaseAttack(IRace r) => 0.6 * r.Strength + 0.3 * r.Agility + r.Intelligence;
        
        public override double ComputeBaseDefense(IRace r) => 0.3 * r.Strength + 0.3 * r.Agility + 0.2 * r.Intelligence;        
    }

    public sealed class MageCategory : CategoryBase
    {
        public MageCategory() : base( "Mage",  2.75)
        { }

        public override string GetAttackMessage(string n) => $" {n} casts a powerful spell!";
        public override string GetDefendMessage(string n) => $" {n} creates a magical barrier!";

        public override double ComputeBaseAttack(IRace r) => 0.2 * r.Strength + 0.2 * r.Agility + 1.0 * r.Intelligence;
        
        public override double ComputeBaseDefense(IRace r) => 0.1 * r.Strength + 0.4 * r.Agility + 0.8 * r.Intelligence;        
    }

    public sealed class ArcherCategory : CategoryBase
    {
        public ArcherCategory() : base( "Archer",  3.15)
        { }

        public override string GetAttackMessage(string n) => $" {n} shoots an arrow!";
        public override string GetDefendMessage(string n) => $" {n} dodges swiftly!";

        public override double ComputeBaseAttack(IRace r) => 0.3 * r.Strength + 0.7 * r.Agility + 0.2 * r.Intelligence;
        
        public override double ComputeBaseDefense(IRace r) => 0.2 * r.Strength + 0.7 * r.Agility + 0.4 * r.Intelligence;        
    }

    //Task 1b Ny charakter

    public sealed class PaladinCategory : CategoryBase
    {
        public PaladinCategory() : base( "Paladin",  3.40)
        { }

        public override string GetAttackMessage(string n) => $" {n} smites with holy power!";
        public override string GetDefendMessage(string n) => $" {n} prays for divine protection!";

        public override double ComputeBaseAttack(IRace r) => 0.5 * r.Strength + 0.1 * r.Agility + 0.6 * r.Intelligence;
        
        public override double ComputeBaseDefense(IRace r) => 0.4 * r.Strength + 0.2 * r.Agility + 0.6 * r.Intelligence;        
    }


    // Character (Race + Category)

    public sealed class Character
    {
        private double xp;
        private double hp;

        public string Name { get; }
        public IRace Race { get; }
        public ICategory Category { get; }

        public int MaxHp => Race.MaxHp;

        public int Hp
        {
            get => (int)hp;
            set
            {
                if (value < 0 || value > MaxHp)
                    throw new ArgumentOutOfRangeException(nameof(Hp), $"HP must be between 0 and {MaxHp}.");
                hp = value;
            }
        }

        public double Xp
        {
            get => xp;
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException(nameof(Xp), "XP must be between 0 and 10.");
                xp = value;
            }
        }

        public bool IsDead => Hp == 0;

        public Character(string name, IRace race, ICategory category)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name required.", nameof(name)) : name;
            Race = race ?? throw new ArgumentNullException(nameof(race));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Hp = race.MaxHp;
            Xp = category.InitialXp;
        }

        public double OnAttack(Random rng = null)
        {
            rng ??= Random.Shared;

            Console.WriteLine(Category.GetAttackMessage(Name));
            
            double baseAttack = Category.ComputeBaseAttack(Race);
            double factor = NextDouble(rng, 0, Xp);
            return baseAttack * factor;
        }

        public double OnDefend(Random rng = null)
        {
            rng ??= Random.Shared;

            Console.WriteLine(Category.GetDefendMessage(Name));
            
            double baseDefense = Category.ComputeBaseDefense(Race);
            double factor = NextDouble(rng, 0, Xp);
            return baseDefense * factor;
        }

        public string OnWin(Random rng = null)
        {
            rng ??= Random.Shared;
            return Race.GetVictoryMessage(rng);
        }

        public override string ToString()
        {
            return $"{Name} ({Race.Name} {Category.Name}) - HP {Hp}/{MaxHp}, XP {Xp:0.00}";
        }

        private static double NextDouble(Random rng, double minValue, double maxValue)
        {
            if (minValue < maxValue) throw new ArgumentOutOfRangeException($"Invalid range.");

            return minValue + rng.NextDouble() * (maxValue - minValue);
        }

    }   
}
