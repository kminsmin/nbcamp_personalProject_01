using System.Reflection.Emit;
using System.Collections.Generic;
using static TextRPG.Program;

namespace TextRPG
{
    internal class Program
    {
        public struct Player
        {
            public string name;
            public string job;
            public int level;
            public int atk;
            public int def;
            public int xAtk;
            public int xDef;
            public int maxHp;
            public int gold;
           
            public void PrintStatus()
            {
                Console.WriteLine($"Lv. {level.ToString("D2")}\n{name} ( {job} )\n공격력 : {atk} (+{xAtk})\n방어력 : {def} (+{xDef})\n체 력 : {maxHp}\nGold : {gold} G\n\n0. 나가기");
            }
        }

        public class IronArmor : Item
        {
            public IronArmor()
            {
                name = "무쇠 갑옷";
                addAtk = 0;
                addDef = 5;
                isOn = false;
                wasOn = false;
                description = "무쇠로 만들어져 튼튼한 갑옷입니다.";
            }
        }

        public class Item
        {
            public string name;
            public int addAtk;
            public int addDef;
            public string description;
            public bool isOn;
            public bool wasOn;
        }

        public class TestItem : Item
        {
            public TestItem()
            {
                name = "God Sword";
                addAtk = 99;
                addDef = 99;
                description = "창조자가 이 세계를 시험하기 위해 만든 검입니다.";
                isOn = false;
                wasOn = false;
            }
        }

        public class OldSword : Item
        {
            public OldSword()
            {
                name = "낡은 검";
                addAtk = 2;
                addDef = 0;
                isOn = false;
                wasOn = false;
                description = "쉽게 볼 수 있는 낡은 검입니다.";
            }
        }



        static void Main(string[] args)
        {
            int playerChoice = 0;
            List<Item> playerInv = new List<Item>(); 
            playerInv.Add(new OldSword());
            playerInv.Add(new IronArmor());
            playerInv.Add(new TestItem());
            Player player;
            player.level = 1;
            player.job = "전사";
            player.atk = 10;
            player.def = 5;
            player.xAtk = 0;
            player.xDef = 0;
            player.maxHp = 100;
            player.gold = 1500;
            Console.WriteLine("당신의 이름은?");
            player.name = Console.ReadLine();


            StartScene(ref playerChoice, ref player, ref playerInv);
        }

        public static void StartScene(ref int playerChoice, ref Player player, ref List<Item> playerInv)
        {
            LoadPlayerStat(ref player, ref playerInv);
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다. \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n1. 상태보기\n2. 인벤토리\n\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            while (playerChoice == 0)
            {
                
                playerChoice = int.Parse(Console.ReadLine());
                if (playerChoice == 1)
                {
                    StatusScene(ref playerChoice, ref player, ref playerInv);
                    break;
                }
                else if (playerChoice == 2)
                {
                    InventoryScene(ref playerChoice, ref player, ref playerInv);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    playerChoice = 0;
                }
            }
        }

        public static void StatusScene(ref int playerChoice, ref Player player, ref List<Item> playerInv)
        {
            Console.Clear();
            player.PrintStatus();
            while (true)
            {
                playerChoice = int.Parse(Console.ReadLine());
                if (playerChoice == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                }
            }
            StartScene(ref playerChoice, ref player, ref playerInv);
        }

        public static void InventoryScene(ref int playerChoice, ref Player player, ref List<Item> playerInv)
        {
            int i = 1;
    
            while (true)
            {
                Console.Clear();

                foreach (Item item in playerInv)
                {
                    Console.Write("- ");
                    PrintItemInfo(item);
                }

                Console.WriteLine("\n\n1. 장착 관리\n0.나가기\n\n원하시는 행동을 입력해주세요.");
                playerChoice = int.Parse(Console.ReadLine());
                if (playerChoice == 0)
                {
                    break;
                }
                else if (playerChoice == 1)
                {
                    Console.Clear();

                    while (true)
                    {
                        Console.Clear();
                        foreach (Item item in playerInv)
                        {
                            Console.Write("- {0} ", i);
                            PrintItemInfo(item);
                            i++;
                        }
                        i = 1;

                        Console.Write("\n\n\n0. 나가기\n\n장착 여부를 결정할 장비를 선택해주세요.");
                        int itemChoice = int.Parse(Console.ReadLine());
                        for (int j = 0; j < playerInv.Count; j++)
                        {
                            if (itemChoice == j + 1)
                            {
                                if (playerInv[j].isOn == false)
                                {
                                    playerInv[j].isOn = true;
                                }
                                else
                                {
                                    playerInv[j].isOn = false;
                                }
                            }
                        }
                        if (itemChoice == 0)
                        {
                            i = 1;                            
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                }
            }
            StartScene(ref playerChoice, ref player, ref playerInv);
        }

        public static void PrintItemInfo(Item item)
        {
            if (item.isOn)
            {
                if (item.addDef != 0 && item.addAtk == 0)
                    Console.WriteLine($"[E]{item.name} | 방어력 +{item.addDef} | {item.description}");
                else if (item.addDef == 0 && item.addAtk != 0)
                    Console.WriteLine($"[E]{item.name} | 공격력 +{item.addAtk} | {item.description}");
                else
                {
                    Console.WriteLine($"[E]{item.name} | 공격력 +{item.addAtk} 방어력 +{item.addDef} | {item.description}");
                }
            }
            else
            {
                if (item.addDef != 0 && item.addAtk == 0)
                    Console.WriteLine($"{item.name} | 방어력 +{item.addDef} | {item.description}");
                else if (item.addDef == 0 && item.addAtk != 0)
                    Console.WriteLine($"{item.name} | 공격력 +{item.addAtk} | {item.description}");
                else
                {
                    Console.WriteLine($"{item.name} | 공격력 +{item.addAtk} 방어력 +{item.addDef} | {item.description}");
                }
            }
        }

        public static void LoadPlayerStat(ref  Player player, ref List<Item> playerInv)
        {
            foreach (Item item in playerInv)
            {
                if (item.isOn && item.wasOn == false)
                {
                    player.atk += item.addAtk;
                    player.xAtk += item.addAtk;
                    player.def += item.addDef;
                    player.xDef += item.addDef;
                    item.wasOn = true;
                }
                else if (item.isOn && item.wasOn)
                {
                    continue;
                }
                else if (item.isOn ==false && item.wasOn)
                {
                    player.atk -= item.addAtk;
                    player.xAtk -= item.addAtk;
                    player.def -= item.addDef;
                    player.xDef -= item.addDef;
                    item.wasOn = false;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}