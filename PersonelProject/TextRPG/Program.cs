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
            public int maxHp;
            public int gold;

            public void PrintStatus()
            {
                Console.WriteLine($"Lv. {level.ToString("D2")}\n{name} ( {job} )\n공격력 : {atk}\n방어력 : {def}\n체 력 : {maxHp}\nGold : {gold} G\n\n0. 나가기");
            }
        }
        
        static void Main(string[] args)
        {
            int playerChoice = 0;
            Player player;
            player.level = 1;
            player.job = "전사";
            player.atk = 10;
            player.def = 5;
            player.maxHp = 100;
            player.gold = 1500;
            Console.WriteLine("당신의 이름은?");
            player.name = Console.ReadLine();

            StartScene(ref playerChoice, ref player);
        }

        public static void StartScene(ref int playerChoice, ref Player player)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다. \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n1. 상태보기\n2. 인벤토리\n\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            while (playerChoice == 0)
            {
                
                playerChoice = int.Parse(Console.ReadLine());
                if (playerChoice == 1)
                {
                    StatusScene(ref playerChoice, ref player);
                    break;
                }
                else if (playerChoice == 2)
                {
                    InventoryScene(ref playerChoice);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    playerChoice = 0;
                }
            }
        }

        public static void StatusScene(ref int playerChoice, ref Player player)
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
            StartScene(ref playerChoice, ref player);
        }

        public static void InventoryScene(ref int playerChoice)
        {
            Console.Clear();
            Console.WriteLine("inv 공사중");
        }
    }
}