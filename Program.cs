using System;

class Program
{
    static void Main()
    {
        var rng = new Random();

        var character1 = new RpgCharacters.Character("Aragorn", new RpgCharacters.OrcRace(), new RpgCharacters.WarriorCategory());
        var character2 = new RpgCharacters.Character("Gandalf", new RpgCharacters.FairyRace(), new RpgCharacters.MageCategory());

        Console.WriteLine($"{character1.Name} the {character1.Race.Name} ({character1.Category.Name}) - HP: {character1.Hp}, XP: {character1.Xp}");
        Console.WriteLine($"{character2.Name} the {character2.Race.Name} ({character2.Category.Name}) - HP: {character2.Hp}, XP: {character2.Xp}");             
        Console.WriteLine();
        while (!character1.IsDead && !character2.IsDead)
        {
            double damageTo2 = character1.OnAttack(rng);
            character2.Hp = Math.Max(0, character2.Hp - (int)damageTo2);
            Console.WriteLine($"{character1.Name} deals {(int)damageTo2} damage to {character2.Name}. {character2.Name} HP: {character2.Hp}");

            if (character2.IsDead)
            {
                Console.WriteLine($"{character1.Name} wins the battle!");
                break;
            }

            double damageTo1 = character2.OnAttack(rng);
            character1.Hp = Math.Max(0, character1.Hp - (int)damageTo1);
            Console.WriteLine($"{character2.Name} deals {(int)damageTo1} damage to {character1.Name}. {character1.Name} HP: {character1.Hp}");

            if (character1.IsDead)
            {
                Console.WriteLine($"{character2.Name} wins the battle!");
                break;
            }
        }
    }
}