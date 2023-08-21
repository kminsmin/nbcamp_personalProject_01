namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartScene();
        }

        public static void StartScene()
        {
            int playerChoice = 0;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다. \n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n1. 상태보기\n2. 인벤토리\n\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            while (true)
            {
                
                playerChoice = int.Parse(Console.ReadLine());
                if (playerChoice == 1)
                {
                    StatusScene();
                }
                else if (playerChoice == 2)
                {
                    InventoryScene();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                }
            }
        }

        public static void StatusScene()
        {
            Console.Clear();
            Console.WriteLine("stat 공사중");
        }

        public static void InventoryScene()
        {
            Console.Clear();
            Console.WriteLine("inv 공사중");
        }
    }
}