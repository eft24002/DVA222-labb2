using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using RpgCharacters;

class Program

{    static void Main()
    {
        var rng = new Random();
        List<RpgCharacters.Character> characters = new List<RpgCharacters.Character>();

        Console.WriteLine("\n---Welcome to the RPG Battle Simulator!---\nChoose your characters and let the battle begin!");
        Console.WriteLine("How many characters do you want to create? (Maximum 10)");
        int numCharacters = Convert.ToInt32(Console.ReadLine());
        if(numCharacters > 10 || numCharacters < 1) throw new Exception("Too many characters or too few characters");
    
        for (int i = 0; i < numCharacters; i++)
        {
            Console.WriteLine($"Choose races för Character {i + 1}:\n1. Fairy\n2. Orc\n3. Elf\n4. Human");
            int characterRaceChoice = Convert.ToInt32(Console.ReadLine());    
            Console.WriteLine($"Choose category for Character {i + 1}:\n1. Warrior\n2. Mage\n3. Archer\n4. Paladin");
            int characterCategoryChoice = Convert.ToInt32(Console.ReadLine());

            
            characters.Add(new RpgCharacters.Character(
                name: $"Character{i + 1}",
                race: characterRaceChoice switch
                {
                    1 => new RpgCharacters.FairyRace(),
                    2 => new RpgCharacters.OrcRace(),
                    3 => new RpgCharacters.ElfRace(),
                    4 => new RpgCharacters.HumanRace(),
                    _ => throw new ArgumentException("Invalid race selection")
                },
                category: characterCategoryChoice switch 
                {
                    1 => new RpgCharacters.WarriorCategory(),
                    2 => new RpgCharacters.MageCategory(),
                    3 => new RpgCharacters.ArcherCategory(),
                    4 => new RpgCharacters.PaladinCategory(),
                    _ => throw new ArgumentException("Invalid category selection")
                }
            ));
        }
                        
        int fighter1index, fighter2index;
        int countRound, countBattle = 1, maxRounds = 5;
        int fighterStarts; 
        RpgCharacters.Character attacker, defender;

        while(characters.Count > 1)
        {

            Console.WriteLine($"\n\n---> Fighters in the arena: <---");
            for(int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{characters[i].Name} - the {characters[i].Race.Name} ({characters[i].Category.Name}) - HP: {characters[i].Hp}, XP: {characters[i].Xp}");
            }  

            fighter1index = rng.Next(0, characters.Count);
            do{
                fighter2index = rng.Next(0, characters.Count);
            }while(fighter1index == fighter2index);

            Character fighter1 = characters[fighter1index];
            Character fighter2 = characters[fighter2index];

            Console.WriteLine($"\n---> Battle {countBattle} between {fighter1.Name} the {fighter2.Name} <---");
            Console.WriteLine($"{fighter1.Name} the {fighter1.Race.Name} ({fighter1.Category.Name}) - HP: {fighter1.Hp}, XP: {fighter1.Xp}");
            Console.WriteLine($"{fighter2.Name} the {fighter2.Race.Name} ({fighter2.Category.Name}) - HP: {fighter2.Hp}, XP: {fighter2.Xp}");             
            Console.WriteLine("--------------------------------------");
            
            countBattle++;
            countRound=0;
            
            fighterStarts = rng.Next(1, 3); 

            while (!fighter1.IsDead && !fighter2.IsDead && countRound < maxRounds)
            {
                countRound++;
                if (fighterStarts == 1)
                {
                    attacker = fighter1;
                    defender = fighter2;
                }
                else
                {
                    attacker = fighter2;
                    defender = fighter1;
                }   

                Attack(attacker, defender, rng);
                if (defender.IsDead) 
                {
                    victory(attacker, defender, characters, rng); 
                    break;
                }

                fighterStarts = 3 - fighterStarts;
            }
            if(countRound == maxRounds)
            {
                Console.WriteLine("\nThe battle ended in a draw after reaching the maximum number of rounds. Both fighters gain 0.25 XP.");
                fighter1.Xp = Math.Min(fighter1.Xp + 0.25, 10);
                fighter2.Xp = Math.Min(fighter2.Xp + 0.25, 10);
            }
        }

        Console.WriteLine($"\n---> The ultimate champion is {characters[0].Name} the {characters[0].Race.Name} ({characters[0].Category.Name}) with {characters[0].Hp} HP and {characters[0].Xp} XP! <---\n"+ characters[0].OnWin(rng));
        return;
    }  
    static void victory(RpgCharacters.Character winner, RpgCharacters.Character defeated, List<RpgCharacters.Character> characters, Random rng)
    {
        
        Console.WriteLine($"\n{winner.Name} wins the battle and gain 0.5 XP!");
        Console.WriteLine(winner.OnWin(rng));
        characters.Remove(defeated);
        winner.Xp = Math.Min(winner.Xp + 0.5, 10);
        winner.Hp = (int)Math.Round(winner.Hp + (winner.MaxHp - winner.Hp) * 0.8);
    }
    static void Attack(RpgCharacters.Character attacker, RpgCharacters.Character defender, Random rng)
    {
        double damageTo2 = attacker.OnAttack(rng);
        double defense2 = defender.OnDefend(rng);

        defender.Hp -= (int)Math.Max(damageTo2 - defense2, 0);
        Console.WriteLine($"{attacker.Name} deals {(int)damageTo2} damage to {defender.Name}. {defender.Name} HP: {defender.Hp}");
    }  
}


               