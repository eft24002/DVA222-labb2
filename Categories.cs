using System;

namespace RpgCharacters
{
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

    public sealed class PaladinCategory : CategoryBase
    {
        public PaladinCategory() : base( "Paladin",  3.40)
        { }

        public override string GetAttackMessage(string n) => $" {n} smites with holy power!";
        public override string GetDefendMessage(string n) => $" {n} prays for divine protection!";

        public override double ComputeBaseAttack(IRace r) => 0.5 * r.Strength + 0.1 * r.Agility + 0.6 * r.Intelligence;
        
        public override double ComputeBaseDefense(IRace r) => 0.4 * r.Strength + 0.2 * r.Agility + 0.6 * r.Intelligence;        
    }
}
