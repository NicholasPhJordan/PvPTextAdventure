using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    //Create a turn based PvP game. It should have a battle loop where both players
    //must fight until one is dead. The game should allow players to upgrade their stats
    //using items. Both players and items should be defined as structs. 


    struct Item
    {
        public int statBoost;
        public string name;
    }


    class Game
    {
        private bool _gameOver = false;
        private Player _player1;
        private Player _player2;
        private Item _longSword;
        private Item _dagger;
        private Item _bow;
        private Item _crossbow;
        private Item _cherrybomb;
        private Item _mace;

        //Run the game
        public void Run()
        {
            Start();

            while (_gameOver == false)
            {
                Update();
            }

            End();

        }

        public void InitializeItems()
        {
            _longSword.statBoost = 15;
            _longSword.name = "Long Sword";
            _dagger.statBoost = 10;
            _dagger.name = "Dagger";
            _bow.statBoost = 12;
            _bow.name = "Bow";
            _crossbow.statBoost = 34;
            _crossbow.name = "Cross Bow";
            _cherrybomb.statBoost = 24;
            _cherrybomb.name = "Cherry Bomb";
            _mace.statBoost = 25;
            _mace.name = "Mace";
        }

        //Displays two options to the player. Outputs the choice of the two options
        public void GetInput(out char input, string option1, string option2, string query)
        {
            //Print description to console
            Console.WriteLine(query);
            //print options to console
            Console.WriteLine("1." + option1);
            Console.WriteLine("2." + option2);
            Console.Write("> ");

            input = ' ';
            //loop until valid input is received
            while (input != '1' && input != '2')
            {
                input = Console.ReadKey().KeyChar;
                if (input != '1' && input != '2')
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }

        public void GetInput(out char input, string option1, string option2, string option3, string query)
        {
            //Print description to console
            Console.WriteLine(query);
            //print options to console
            Console.WriteLine("1." + option1);
            Console.WriteLine("2." + option2);
            Console.WriteLine("3." + option3);
            Console.Write("> ");

            input = ' ';
            //loop until valid input is received
            while (input != '1' && input != '2' && input !='3')
            {
                input = Console.ReadKey().KeyChar;
                if (input != '1' && input != '2' && input != '3')
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }

        //Equip items to both players in the beginning of the game
        public void SelectLoadout(Player player)
        {
            Console.WriteLine("Loadout 1: ");
            Console.WriteLine(_longSword.name);
            Console.WriteLine(_dagger.name);
            Console.WriteLine(_bow.name);

            Console.WriteLine("\nLoadout 2: ");
            Console.WriteLine(_crossbow.name);
            Console.WriteLine(_cherrybomb.name);
            Console.WriteLine(_mace.name);
            //Get input for player one
            char input;
            GetInput(out input, "Loadout 1", "Loadout 2", "Welcome! Please choose a loadout.");
            //Equip item based on input value
            if (input == '1')
            {
                player.AddItemToInventory(_longSword, 0);
                player.AddItemToInventory(_dagger, 1);
                player.AddItemToInventory(_bow, 2);
            }
            else if (input == '2')
            {
                player.AddItemToInventory(_crossbow, 0);
                player.AddItemToInventory(_cherrybomb, 1);
                player.AddItemToInventory(_mace, 2);
            }
        }

        public Player CreateCharacter()
        {
            Console.WriteLine("What is your name?");
            Console.Write("> ");
            string name = Console.ReadLine();
            Player player = new Player(name, 100, 10, 3);
            SelectLoadout(player);
            ClearScreen();
            return player;
        }

        public void ClearScreen()
        {
            Console.WriteLine("\nPress any key to continue");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }

        public void SwitchWeapon(Player player)
        {
            Item[] inventory = player.GetInventory();

            char input;
            //print out all of the options
            for(int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine("\n" + (i+1) + ". " + inventory[i].name + "\n Damage: " + inventory[i].statBoost);
            }
            input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '1':
                    player.EquipItem(0);
                    Console.WriteLine("You equiped " + inventory[0].name);
                    Console.WriteLine("Base damage increased by " + inventory[0].statBoost);
                    break;
                case '2':
                    player.EquipItem(1);
                    Console.WriteLine("You equiped " + inventory[1].name);
                    Console.WriteLine("Base damage increased by " + inventory[1].statBoost);
                    break;
                case '3':
                    player.EquipItem(2);
                    Console.WriteLine("You equiped " + inventory[2].name);
                    Console.WriteLine("Base damage increased by " + inventory[2].statBoost);
                    break;
                default:
                    player.UnequipItem();
                    Console.WriteLine("You accidently dropped your weapon! \nUnfortunate :(");
                    break;
            }
        }

        public void StartBattle()
        {
            ClearScreen();
            Console.WriteLine("Now GO!");

            while (_player1.GetIsAlive() && _player2.GetIsAlive())
            {
                //print player stats to console
                Console.WriteLine("Player1");
                _player1.PrintStats();
                Console.WriteLine();
                Console.WriteLine("Player2");
                _player2.PrintStats();
                //Player 1 turn start
                //Get player input
                char input;
                GetInput(out input, "Attack", "Change Weapon", "Your turn Player 1");

                if (input == '1')
                {
                    _player1.Attack(_player2);
                }
                else
                {
                    SwitchWeapon(_player1);
                }

                GetInput(out input, "Attack", "Change Weapon", "Your turn Player 2");

                if (input == '1')
                {
                    _player2.Attack(_player1);
                }
                else
                {
                    SwitchWeapon(_player2);
                }
                Console.Clear();
            }
            if (_player1.GetIsAlive())
            {
                Console.WriteLine("Player 1 wins!?");
            }
            else
            {
                Console.WriteLine("Player 2 wins?");
            }
            ClearScreen();
            _gameOver = true;
        }


        //Performed once when the game begins
        public void Start()
        {
            InitializeItems();
        }

        //Repeated until the game ends
        public void Update()
        {
            _player1 = CreateCharacter();
            _player2 = CreateCharacter();
            StartBattle();
        }

        //Performed once when the game ends
        public void End()
        {

        }
    }
}
