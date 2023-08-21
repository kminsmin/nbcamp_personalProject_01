using System.Reflection.Emit;
using System.Collections.Generic;
using static TextRPG.Program;
using System.Text;

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
                if (xAtk == 0 && xDef == 0)
                {
                    Console.WriteLine($"Lv. {level.ToString("D2")}\n{name} ( {job} )\n공격력 : {atk} \n방어력 : {def}\n체 력 : {maxHp}\nGold : {gold} G\n\n0. 나가기");
                }
                else if(xAtk != 0 && xDef == 0)
                {
                    Console.WriteLine($"Lv. {level.ToString("D2")}\n{name} ( {job} )\n공격력 : {atk} (+{xAtk})\n방어력 : {def}\n체 력 : {maxHp}\nGold : {gold} G\n\n0. 나가기");
                }
                else if(xDef != 0 && xAtk == 0)
                {
                    Console.WriteLine($"Lv. {level.ToString("D2")}\n{name} ( {job} )\n공격력 : {atk} \n방어력 : {def} (+{xDef})\n체 력 : {maxHp}\nGold : {gold} G\n\n0. 나가기");
                }
                else
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
                name = "전설의 검";
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다. \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n1. 상태보기\n2. 인벤토리\n\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.White;
            while (playerChoice == 0)
            {
                if (int.TryParse(Console.ReadLine(),out playerChoice))
                {
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
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    playerChoice = 0;
                }
            }
        }

        public static void StatusScene(ref int playerChoice, ref Player player, ref List<Item> playerInv) //플레이어의 현재 스탯을 보여줍니다.
        {
            Console.Clear();
            player.PrintStatus();
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out playerChoice))
                {
                    if (playerChoice == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다!");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                }
            }
            StartScene(ref playerChoice, ref player, ref playerInv);
        }

        /* 인벤토리에 있는 장비들을 보여줍니다.
         * 장비 관리 칸에서는 장비의 인덱스 번호를 입력하면 착용 여부를 변경할 수 있으며, 0을 입력하면
         * 인벤토리 기본 화면을 다시 출력합니다. 이 상태에서 다시 0을 입력하면 시작화면으로 돌아갑니다.
         */
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
                if (int.TryParse(Console.ReadLine(), out playerChoice))
                {
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
                            if (int.TryParse(Console.ReadLine(), out int itemChoice))
                            {
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
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다!");
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다!");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    Thread.Sleep(1000);
                }

            }
            StartScene(ref playerChoice, ref player, ref playerInv);
        }

        public static void PrintItemInfo(Item item) // 장비의 착용여부, 증가 스탯, 상세 설명 등을 출력합니다.
        {
            string itemName;
            int nameLength;
            byte[] data; 
            int blank;
            if (item.isOn)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                itemName = " " + item.name;
                data = Encoding.Unicode.GetBytes(itemName);
                nameLength = data.Length;
                blank = (30 - nameLength) / 2;

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < blank; j++)
                    {
                        Console.Write(" ");
                    }
                    if (i == 1)
                        break;
                    Console.Write($"[E]{item.name}");
                }
                if (item.addDef != 0 && item.addAtk == 0)
                    Console.WriteLine($"|            방어력 +{item.addDef}           | {item.description}");
                else if (item.addDef == 0 && item.addAtk != 0)
                    Console.WriteLine($"|            공격력 +{item.addAtk}           | {item.description}");

                else
                {
                    Console.WriteLine($"|      공격력 +{item.addAtk} 방어력 +{item.addDef}     | {item.description}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                itemName = item.name;
                data = Encoding.Unicode.GetBytes(itemName);
                nameLength = data.Length;
                blank = (30 - nameLength) / 2;

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < blank; j++)
                    {
                        Console.Write(" ");
                    }
                    if (i == 1)
                        break;
                    Console.Write($"{item.name}");
                }
                    
                if (item.addDef != 0 && item.addAtk == 0)
                    Console.WriteLine($"|            방어력 +{item.addDef}           | {item.description}");               
                else if (item.addDef == 0 && item.addAtk != 0)
                    Console.WriteLine($"|            공격력 +{item.addAtk}           | {item.description}");
                else
                {
                    Console.WriteLine($"|      공격력 +{item.addAtk} 방어력 +{item.addDef}     | {item.description}");
                }
            }
        }

        public static void LoadPlayerStat(ref Player player, ref List<Item> playerInv) //시작화면으로 돌아갈 때마다 호출되어, 장비 변경 등으로 플레이어의 스탯에 변화가 있다면 이를 플레이어 구조체에 반영합니다.
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
                else if (item.isOn == false && item.wasOn)
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