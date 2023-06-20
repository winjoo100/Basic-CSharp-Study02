using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace rpg_Map
{
    internal class Program
    {
        const int MAP_SIZEY = 10;
        const int MAP_SIZEX = 30;
        const int shopStart_PosY = 3;
        const int shopEnd_PosY = 5;
        const int playgroundStart_PosY = 3;
        const int playgroundEnd_PosY = 5;

        static void Main(string[] args)
        {
            // 처음 플레이어 위치
            int player_PosY = MAP_SIZEY - 3;
            int player_PosX = MAP_SIZEX / 2;

            // 플레이어 정보
            string playerType = "초보자";
            int playerHp = 100;
            int playerMaxHp = 100;
            int playerDef = 5;
            int playerDamage = 20;
            int playerMoney = 1000;

            // 내 가방 만들기
            List<string> myInventory = new List<string>();
            myInventory.Add("텅텅빈 통장");

            // 시작 맵 세팅
            string[,] set_Map = new string[MAP_SIZEY, MAP_SIZEX];
            Start_Map(ref set_Map, player_PosY, player_PosX);

            // 게임 시작
            while (true)
            {
                Print_Map(set_Map, ref playerType, ref playerMaxHp, ref playerHp, ref playerDef, ref playerDamage, ref playerMoney, ref myInventory);
                Console.WriteLine("플레이어 Y 좌표 {0:D2}, X 좌표 {1:D2}", player_PosY, player_PosX);

                Player_Key(ref set_Map, ref player_PosY, ref player_PosX, ref playerMaxHp, ref playerHp, ref playerDef, ref playerDamage, ref playerMoney);
            }

        }

        // 플레이어 키 입력
        static void Player_Key(ref string[,] set_Map, ref int player_PosY, ref int player_PosX, ref int playerMaxHp, ref int playerHp, ref int playerDef, ref int playerDamage, ref int playerMoney)
        {
            // 키 입력 받기
            ConsoleKey move_Direction = Console.ReadKey(true).Key;

            // A를 눌렀을 때 and 바닥이 었을 때
            if (move_Direction == ConsoleKey.A)
            {
                if (set_Map[player_PosY, player_PosX - 1] == "　 ")
                {
                    string temp = set_Map[player_PosY, player_PosX];
                    set_Map[player_PosY, player_PosX] = set_Map[player_PosY, player_PosX - 1];
                    set_Map[player_PosY, player_PosX - 1] = temp;
                    player_PosX--;
                }
                else if (set_Map[player_PosY, player_PosX - 1] == "▣")
                {
                    // 상점 포탈이었을 경우
                    if ((player_PosY == 6) && (player_PosX == 9))
                    {
                        // 상점 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("상점에 진입 중 입니다...");
                        }
                        Console.Clear();

                        // 상점 로직 시작
                        Shop_Info(ref playerMoney);
                    }
                    // 필드 (전투) 포탈 이었을 경우
                    if ((player_PosY == 1) && (player_PosX == 18))
                    {
                        // 필드 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("필드에 진입합니다...");
                        }
                        Console.Clear();

                        // 필드 로직 시작
                        Battle(ref playerMaxHp, ref playerHp, ref playerDef, ref playerDamage, ref playerMoney);
                    }

                    // 게임장 포탈 이었을 경우
                    if ((player_PosY == 6) && (player_PosX == 25))
                    {
                        // 게임장 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("게임장에 진입합니다...");
                        }
                        Console.Clear();

                        // 게임장 로직 시작
                        Gamble(ref playerMoney);
                    }
                }
            }       // A를 눌렀을 때 end

            // D를 눌렀을 때
            else if (move_Direction == ConsoleKey.D)
            {
                // 바닥이 있을 때
                if (set_Map[player_PosY, player_PosX + 1] == "　 ")
                {
                    string temp = set_Map[player_PosY, player_PosX];
                    set_Map[player_PosY, player_PosX] = set_Map[player_PosY, player_PosX + 1];
                    set_Map[player_PosY, player_PosX + 1] = temp;
                    player_PosX++;
                }
                // 포탈을 만났을 때
                else if (set_Map[player_PosY, player_PosX + 1] == "▣")
                {
                    // 상점 포탈이었을 경우
                    if ((player_PosY == 6) && (player_PosX == 4))
                    {
                        // 상점 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("상점에 진입 중 입니다...");
                        }
                        Console.Clear();

                        // 상점 로직 시작
                        Shop_Info(ref playerMoney);
                    }

                    // 필드 (전투) 포탈 이었을 경우
                    if ((player_PosY == 1) && (player_PosX == 11))
                    {
                        // 필드 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("필드에 진입합니다...");
                        }
                        Console.Clear();

                        // 필드 로직 시작
                        Battle(ref playerMaxHp, ref playerHp, ref playerDef, ref playerDamage, ref playerMoney);
                    }

                    // 게임장 포탈 이었을 경우
                    if ((player_PosY == 6) && (player_PosX == 20))
                    {
                        // 게임장 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("게임장에 진입합니다...");
                        }
                        Console.Clear();

                        // 게임장 로직 시작
                        Gamble(ref playerMoney);
                    }
                }
            }

            // W를 눌렀을 때 and 바닥이 었을 때
            else if (move_Direction == ConsoleKey.W)
            {
                // 바닥이 있을 때 이동
                if (set_Map[player_PosY - 1, player_PosX] == "　 ")
                {
                    string temp = set_Map[player_PosY, player_PosX];
                    set_Map[player_PosY, player_PosX] = set_Map[player_PosY - 1, player_PosX];
                    set_Map[player_PosY - 1, player_PosX] = temp;
                    player_PosY--;
                }
                // 포탈을 만났을 때
                else if (set_Map[player_PosY - 1, player_PosX] == "▣")
                {
                    // 상점 포탈이었을 경우
                    if ((player_PosY == 7) && (5 <= player_PosX) && (player_PosX <= 8))
                    {
                        // 상점 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("상점에 진입 중 입니다...");
                        }
                        Console.Clear();

                        // 상점 로직 시작
                        Shop_Info(ref playerMoney);
                    }

                    // 필드 (전투) 포탈 이었을 경우
                    if ((player_PosY == 2) && (12 <= player_PosX) && (player_PosX <= 17))
                    {
                        // 필드 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("필드에 진입합니다...");
                        }
                        Console.Clear();

                        // 필드 로직 시작
                        Battle(ref playerMaxHp, ref playerHp, ref playerDef, ref playerDamage, ref playerMoney);
                    }

                    // 게임장 포탈 이었을 경우
                    if ((player_PosY == 7) && (21 <= player_PosX) && (player_PosX <= 24))
                    {
                        // 게임장 진입 연출
                        Console.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(10, (5 + i) * 2);
                            Thread.Sleep(500);
                            Console.Write("게임장에 진입합니다...");
                        }
                        Console.Clear();

                        // 게임장 로직 시작
                        Gamble(ref playerMoney);
                    }
                }
            }

            // S를 눌렀을 때
            else if (move_Direction == ConsoleKey.S)
            {
                // 바닥이 있을 때 이동
                if (set_Map[player_PosY + 1, player_PosX] == "　 ")
                {
                    string temp = set_Map[player_PosY, player_PosX];
                    set_Map[player_PosY, player_PosX] = set_Map[player_PosY + 1, player_PosX];
                    set_Map[player_PosY + 1, player_PosX] = temp;
                    player_PosY++;
                }
            }

        }   // 플레이어 키 입력

        // 맵 생성
        static void Start_Map(ref string[,] set_Map, int player_PosY, int player_PosX)
        {

            for (int y = 0; y < MAP_SIZEY; y++)
            {

                for (int x = 0; x < MAP_SIZEX; x++)
                {
                    // 벽 생성 (왼쪽, 오른쪽, 위쪽, 아래쪽)
                    if (((0 <= x) && (x <= 2)) || ((MAP_SIZEX - 3 <= x) && (x <= MAP_SIZEX)) || ((y == 0)) || ((y == MAP_SIZEY - 1)))
                    {
                        set_Map[y, x] = "::";
                        continue;
                    }

                    // 플레이어 생성
                    if ((y == player_PosY) && (x == player_PosX))
                    {
                        set_Map[y, x] = "& ";
                        continue;
                    }

                    // 상점 생성
                    if ((shopStart_PosY <= y) && (y <= shopEnd_PosY) && (4 <= x) && (x <= 9))
                    {
                        // 상점 간판
                        {
                            if ((y == shopStart_PosY) && (5 == x))
                            {
                                set_Map[y, x] = "S ";
                                continue;
                            }
                            if ((y == shopStart_PosY) && (6 == x))
                            {
                                set_Map[y, x] = "H ";
                                continue;
                            }
                            if ((y == shopStart_PosY) && (7 == x))
                            {
                                set_Map[y, x] = "O ";
                                continue;
                            }
                            if ((y == shopStart_PosY) && (8 == x))
                            {
                                set_Map[y, x] = "P ";
                                continue;
                            }
                        }       // 상점 간판
                        set_Map[y, x] = "X ";
                        continue;
                    }       // 상점 생성

                    // 게임장 생성
                    if ((playgroundStart_PosY <= y) && (y <= playgroundEnd_PosY) && (20 <= x) && (x <= 25))
                    {
                        // 게임장 간판
                        {
                            if ((y == playgroundStart_PosY) && (21 == x))
                            {
                                set_Map[y, x] = "G ";
                                continue;
                            }
                            if ((y == playgroundStart_PosY) && (22 == x))
                            {
                                set_Map[y, x] = "A ";
                                continue;
                            }
                            if ((y == playgroundStart_PosY) && (23 == x))
                            {
                                set_Map[y, x] = "M ";
                                continue;
                            }
                            if ((y == playgroundStart_PosY) && (24 == x))
                            {
                                set_Map[y, x] = "E ";
                                continue;
                            }
                        }
                        set_Map[y, x] = "X ";
                        continue;
                    }


                    // 포탈 생성
                    // 상점 포탈
                    if ((y == shopEnd_PosY + 1) && ((5 <= x) && (x <= 8)))
                    {
                        set_Map[y, x] = "▣";
                        continue;
                    }

                    // 게임장 포탈
                    if ((y == playgroundEnd_PosY + 1) && ((21 <= x) && (x <= 24)))
                    {
                        set_Map[y, x] = "▣";
                        continue;
                    }

                    // 필드 포탈
                    if ((y == 1) && ((12 <= x) && (x <= 17)))
                    {
                        set_Map[y, x] = "▣";
                        continue;
                    }

                    // 바닥 생성
                    set_Map[y, x] = "　 ";
                }
            }
        }

        // 맵 출력
        static void Print_Map(string[,] set_Map, ref string playerType, ref int playerMaxHp, ref int playerHp, ref int playerDef, ref int playerDamage, ref int playerMoney, ref List<string> myInventory)
        {
            Console.SetCursorPosition(0, 1);
            Print_Forest();

            for (int y = 0; y < MAP_SIZEY; y++)
            {
                for (int x = 0; x < MAP_SIZEX; x++)
                {
                    Console.SetCursorPosition(26 + (x * 2), 7 + (y));

                    // 플레이어 색상 변경
                    if (set_Map[y, x] == "& ")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0} ", set_Map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 상점 색상 변경
                    if (set_Map[y, x] == "X ")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("{0} ", set_Map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 벽 색상 변경
                    if (set_Map[y, x] == "::")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("{0} ", set_Map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 바닥 색상 변경
                    if (set_Map[y, x] == "　 ")
                    {
                        Console.Write("{0} ", set_Map[y, x]);
                        continue;
                    }

                    // 포탈 색상 변경
                    if (set_Map[y, x] == "▣")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("{0} ", set_Map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 나머지 간판 색상 출력
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} ", set_Map[y, x]);
                    Console.ResetColor();

                }
                Console.WriteLine();
            }
            PlayerInfo(playerType, ref playerHp, ref playerDef, ref playerDamage, ref playerMoney, ref myInventory);
        }

        // 플레이어 정보창
        static void PlayerInfo(string playerType, ref int playerHp, ref int playerDef, ref int playerDamage, ref int playerMoney, ref List<string> myInventory)
        {
            // 플레이어 : 직업, 체력, 방어력, 공격력, 소지금
            Console.WriteLine("\t\t\t\t---------------- 플레이어 정보 -----------------");
            Console.WriteLine("\t\t\t\t   직업 : {0} \t\t현재 체력 : {1}", playerType, playerHp);
            Console.WriteLine("\t\t\t\t   방어력 : {0} \t\t        공격력 : {1}", playerDef, playerDamage);
            Console.WriteLine("\t\t\t\t   현재 소지금 : {0}", playerMoney);
            Console.WriteLine("\t\t\t\t------------------------------------------------");
            Console.WriteLine("\t\t\t\t---------------- 인  벤  토  리 ----------------");
            for (int i = 0; i < myInventory.Count; i++)
            {
                Console.WriteLine("\t\t\t\t[{0}]", myInventory[i]);
            }

        }

        // 게임장 로직
        static void Gamble(ref int playerMoney)
        {
            // 게임 시작
            bool goGame = false;
            while (goGame == false)
            {
                // { 변수 선언부
                int[] cardPack = new int[52];
                int[] cardPackShape = new int[4];
                string[] shapeName = new string[] { "♠", "◈", "♥", "♣" };
                string[] shapeNumber = new string[] { "A", "J", "Q", "K" };


                // 모양을 위한 변수
                string firstshape = "";
                string secondshape = "";
                string playerShape = "";

                // 카드 숫자를 위한 변수
                int computerFirstCardIdx = 0;
                int computerSecondCardIdx = 0;
                int computerFirstCard = 0;
                int computerSecondCard = 0;
                int playerCardIdx = 0;
                int playerCard = 0;

                int ptrComFirstCard = 0;
                int ptrComSecondCard = 0;
                int ptrPlayerCard = 0;

                // 카드 숫자를 A, J, Q, K 로 출력하기 위한 변수
                string strComputerFirstCard = "";
                string strComputerSecondCard = "";
                string strPlayerCard = "";

                // 52의 숫자 중 카드 숫자가 같은지 여부
                bool sameCardCheck = true;

                // 게임 참여금액
                int joinGameMoney = 500;

                // } 변수 선언부

                // 카드 배열에 1 ~ 52의 카드 넣기
                for (int i = 0; i < cardPack.Length; i++)
                {
                    cardPack[i] = i + 1;
                }

                // 카드 모양 배열에 1 ~ 4 문양 넣기 (출력용)
                // 1 = 스페이드, 2 = 다이아, 3 = 하트, 4 = 클로버
                for (int i = 0; i < cardPackShape.Length; i++)
                {
                    cardPackShape[i] = i + 1;
                }

                // 1 ~ 13 = 스페이드 / 14 ~ 26 = 다이아 / 27 ~ 39 = 하트 /40 ~ 52 = 클로버

                // 랜덤한 난수를 불러와서 해당 인덱스 번호의 카드의 숫자를 대입
                Random random = new Random();
                while (sameCardCheck)
                {
                    computerFirstCardIdx = random.Next(0, 52);
                    computerSecondCardIdx = random.Next(0, 52);
                    playerCardIdx = random.Next(0, 52);

                    // 랜덤한 3개의 값이 같지 않다면 대입
                    if ((computerFirstCardIdx != computerSecondCardIdx) && (computerFirstCardIdx != playerCardIdx) && (computerSecondCardIdx != playerCardIdx))
                    {
                        computerFirstCard = cardPack[computerFirstCardIdx];
                        computerSecondCard = cardPack[computerSecondCardIdx];
                        playerCard = cardPack[playerCardIdx];

                        // 출력용 카드 수를 할당
                        ptrComFirstCard = (computerFirstCard % 13) + 1;
                        ptrComSecondCard = (computerSecondCard % 13) + 1;
                        ptrPlayerCard = (playerCard % 13) + 1;

                        // 출력용 카드 중에 더 작은 수가 앞에 오도록 스왑 (승리 여부 판단용)
                        if (ptrComFirstCard > ptrComSecondCard)
                        {
                            int temp = ptrComFirstCard;
                            ptrComFirstCard = ptrComSecondCard;
                            ptrComSecondCard = temp;
                        }
                        sameCardCheck = false;
                    }
                }

                // - 디버그 모드 -
                // 52숫자중에 몇번째 인지 숫자 였는지
                //Console.WriteLine("{0} {1} {2}", computerFirstCard, computerSecondCard, playerCard);

                // 숫자 조작
                //ptrComFirstCard = 1;
                //ptrComSecondCard = 2;
                //ptrPlayerCard = 3;

                // { 카드 번호가 1이면 A, 11이면 J, 12이면 Q, 13면 K
                // 첫번째 카드
                if (ptrComFirstCard == 1)
                {
                    strComputerFirstCard = shapeNumber[0];
                }
                else if (ptrComFirstCard == 11)
                {
                    strComputerFirstCard = shapeNumber[1];
                }
                else if (ptrComFirstCard == 12)
                {
                    strComputerFirstCard = shapeNumber[2];
                }
                else if (ptrComFirstCard == 13)
                {
                    strComputerFirstCard = shapeNumber[3];
                }
                // 두번째카드
                if (ptrComSecondCard == 1)
                {
                    strComputerSecondCard = shapeNumber[0];
                }
                else if (ptrComSecondCard == 11)
                {
                    strComputerSecondCard = shapeNumber[1];
                }
                else if (ptrComSecondCard == 12)
                {
                    strComputerSecondCard = shapeNumber[2];
                }
                else if (ptrComSecondCard == 13)
                {
                    strComputerSecondCard = shapeNumber[3];
                }
                // 플레이어 카드
                if (ptrPlayerCard == 1)
                {
                    strPlayerCard = shapeNumber[0];
                }
                else if (ptrPlayerCard == 11)
                {
                    strPlayerCard = shapeNumber[1];
                }
                else if (ptrPlayerCard == 12)
                {
                    strPlayerCard = shapeNumber[2];
                }
                else if (ptrPlayerCard == 13)
                {
                    strPlayerCard = shapeNumber[3];
                }
                // }인덱스 번호가 0이면 A, 10이면 J, 11이면 Q, 12면 K


                // { 카드의 문양 정하기
                // 1 ~ 13 = 스페이드 / 14 ~ 26 = 다이아 / 27 ~ 39 = 하트 /40 ~ 52 = 클로버
                // 컴퓨터의 첫번째 카드 문양
                if (0 <= computerFirstCard && computerFirstCard < 13)
                {
                    firstshape = shapeName[0];
                }
                else if (13 <= computerFirstCard && computerFirstCard < 26)
                {
                    firstshape = shapeName[1];
                }
                else if (26 <= computerFirstCard && computerFirstCard < 39)
                {
                    firstshape = shapeName[2];
                }
                else
                {
                    firstshape = shapeName[3];
                }
                // 컴퓨터의 두번재 카드 문양
                if (0 <= computerSecondCard && computerSecondCard < 13)
                {
                    secondshape = shapeName[0];
                }
                else if (13 <= computerSecondCard && computerSecondCard < 26)
                {
                    secondshape = shapeName[1];
                }
                else if (26 <= computerSecondCard && computerSecondCard < 39)
                {
                    secondshape = shapeName[2];
                }
                else
                {
                    secondshape = shapeName[3];
                }

                // 플레이어의 카드 문양
                if (0 <= playerCard && playerCard < 13)
                {
                    playerShape = shapeName[0];
                }
                else if (13 <= playerCard && playerCard < 26)
                {
                    playerShape = shapeName[1];
                }
                else if (26 <= playerCard && playerCard < 39)
                {
                    playerShape = shapeName[2];
                }
                else
                {
                    playerShape = shapeName[3];
                }
                // } 카드 문양 정하기

                // { 출력하기!
                bool gameEnd = false;
                bool yesno = true;
                while (gameEnd == false)
                {
                    // 플레이어의 소지금 출력
                    Console.WriteLine("\t플레이어의 현재 소지금 : {0}\n", playerMoney);


                    // 카드 넘버의 인덱스가 0, 10, 11, 12이 포함되어 있을 경우 문자열 카드 넘버를 출력하고,
                    // 카드 넘버의 인덱스가 그 외라면 그대로 정수형 카드 넘버를 출력합니다.
                    // 첫번째 컴퓨터 카드 출력
                    Console.WriteLine("\t컴퓨터의 2개의 카드");
                    Console.WriteLine("\t  -----     -----");

                    if (ptrComFirstCard == 1 || (11 <= ptrComFirstCard && ptrComFirstCard <= 13))  // 문자열 출력
                    {
                        Console.Write("\t | {0}{1} |  ", firstshape, strComputerFirstCard);
                    }
                    else
                    {
                        Console.Write("\t | {0}{1} |  ", firstshape, ptrComFirstCard);
                    }

                    // 두번째 컴퓨터 카드 출력
                    if (ptrComSecondCard == 1 || (11 <= ptrComSecondCard && ptrComSecondCard <= 13))
                    {
                        Console.Write(" | {0}{1} | \n", secondshape, strComputerSecondCard);
                    }
                    else
                    {
                        Console.Write(" | {0}{1} | \n", secondshape, ptrComSecondCard);
                    }
                    Console.WriteLine("\t  -----     -----");

                    // 베팅 여부 물어보기
                    Console.WriteLine("\t베팅 하시겠습니까? (Y = 네 / N = 아니요)");
                    Console.WriteLine("\t(Q를 눌러 마을로 돌아갈 수 있습니다.)");
                    string yesOrNo = Console.ReadLine();

                    // { 베팅을 수락했을 경우
                    if (yesOrNo == "y" || yesOrNo == "Y")
                    {
                        while (yesno)
                        {

                            // 베팅 금액 여부
                            Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                            Console.WriteLine("\t얼마 베팅 하시겠습니까? 승리시 베팅금액의 2배 획득\n");
                            Console.WriteLine("\t[베팅 최소금액 100원] / [베팅 최대금액 1000원]\n");
                            Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");

                            string palyerBetMoney = Console.ReadLine();
                            int betValue = 0;
                            int.TryParse(palyerBetMoney, out betValue);
                            bool niceBet = false;
                            while (niceBet == false)
                            {
                                // 베팅을 1000원 이상 & 현재 소지금 보다 적거나 같게한 올바른 경우
                                if ((1000 <= betValue) && (betValue <= playerMoney))
                                {
                                    Console.WriteLine("\t{0}원 베팅하셨습니다.", betValue);
                                    Console.WriteLine("\t그럼 이제... 플레이어의 카드를 뽑습니다.\n", betValue);
                                    Thread.Sleep(500);

                                    // { 플레이어의 카드 출력
                                    Console.WriteLine("\t플레이어의 카드");
                                    Console.WriteLine("\t  -----");
                                    if (ptrPlayerCard == 1 || (11 <= ptrPlayerCard && ptrPlayerCard <= 13))
                                    {
                                        Console.WriteLine("\t | {0}{1} |", playerShape, strPlayerCard);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\t | {0}{1} |", playerShape, ptrPlayerCard);
                                    }
                                    Console.WriteLine("\t  -----");
                                    // } 플레이어의 카드 출력

                                    // { 게임 승리 여부 판단
                                    // 승리
                                    if ((ptrComFirstCard < ptrPlayerCard) && (ptrPlayerCard < ptrComSecondCard))
                                    {
                                        Console.WriteLine("\t☆승리하셨습니다! 베팅금액 {0}의 2배를 획득하셨습니다.☆", betValue);
                                        Console.WriteLine("\t아무키나 눌러 게임을 다시 시작하세요.");
                                        playerMoney = playerMoney + (betValue * 2);
                                        Console.ReadKey();
                                        Console.Clear();
                                        gameEnd = true;
                                        yesno = false;
                                        break;
                                    }
                                    // 패배
                                    else
                                    {
                                        Console.WriteLine("\t＠패배하셨습니다. 베팅금액 {0}을 잃었습니다.＠", betValue);
                                        playerMoney = playerMoney - betValue;
                                        if (playerMoney <= 0)
                                        {
                                            Console.WriteLine("\t소지금을 모두 잃었습니다. 게임종료\n\n");
                                            gameEnd = true;
                                            yesno = false;
                                            goGame = true;
                                            break;
                                        }
                                        Console.WriteLine("\t아무키나 눌러 게임을 다시 시작하세요.");
                                        Console.ReadKey();
                                        Console.Clear();
                                        gameEnd = true;
                                        yesno = false;
                                        break;
                                    }
                                    // } 게임 승리 여부 판단
                                }

                                // { 베팅 금액을 잘못 입력했을 경우
                                else if (betValue < 100)
                                {
                                    Console.WriteLine("\t낙장불입! 100원 이상을 베팅해야합니다. 다시 입력하세요");
                                    niceBet = true;
                                }

                                else if (betValue > 1000)
                                {
                                    Console.WriteLine("\t베팅금액이 너무 많습니다. 다시 입력하세요");
                                    niceBet = true;
                                }

                                else if (betValue > playerMoney)
                                {
                                    Console.WriteLine("\t현재 소지금보다 많습니다. 다시 입력하세요");
                                    Console.WriteLine("\t현재 소지금 : {0}원", playerMoney);
                                    niceBet = true;
                                }

                                else
                                {
                                    Console.WriteLine("\t금액을 잘못입력하셨습니다. 숫자만 입력하세요.");
                                    Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                                    niceBet = true;
                                }
                                // } 베팅 금액을 잘못 입력했을 경우
                            }
                        }
                    } // } 베팅을 수락했을 경우

                    // 베팅에 참여하지 않을 경우
                    else if (yesOrNo == "n" || yesOrNo == "N")
                    {
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        //Console.WriteLine("\t게임 참여비 {0}원 지불하셨습니다.\n", joinGameMoney);
                        //playerMoney = playerMoney - joinGameMoney;
                        Console.WriteLine("\t컴퓨터가 다시 카드를 뽑습니다.");
                        Console.WriteLine("\t...아무키나 눌러 게임을 다시 시작하세요.\n");
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        gameEnd = true;
                        Console.ReadKey();
                        Console.Clear();
                    }

                    // 게임장을 나갈 때
                    else if (yesOrNo == "Q" || yesOrNo == "q")
                    {
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        Console.WriteLine("\t게임장을 나갑니다.");
                        Console.WriteLine("\t아무키나 눌러 게임장을 나가세요...\n");
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        gameEnd = true;
                        yesno = false;
                        goGame = true;
                        Console.ReadKey();
                        Console.Clear();
                    }

                    // 그 외 다른 입력을 했을 경우
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        Console.WriteLine("\t잘못입력하셨습니다. Y 또는 N 를 입력하세요.\n");
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                    }
                }
            }
        }



        // 상점 로직
        static void Shop_Info(ref int playerMoney)
        {
            int itemPool = 3;                            // 상점에 출력될 아이템 갯수
            string[] market = new string[3];             // 상점
            string[] marketTemp = new string[3];         // 상점 출력용
            int[] sameNumcheck = new int[itemPool];      // 상점 아이템 갯수
            string[] removeCheck = new string[itemPool]; // 구매한 아이템은 창고에서 삭제
            int storageRandomItem;                       // 창고에서 무작위로 아이템을 선정할 변수
            int Print_PosY = 5;                          // 출력을 위한 커서 위치
            bool quitMarket_ = false;                    // 재 입력값을 위한 조건
            bool shopingEnd_ = default;                  // 상점을 나가기 위한 조건

            // 내 가방 만들기
            List<string> myInventory = new List<string>();
            myInventory.Add("텅텅빈 통장");

            // 아이템 목록 도감
            Dictionary<string, int> itemDictionary = new Dictionary<string, int>();
            itemDictionary.Add("나무검", 500);       // 데미지 5
            itemDictionary.Add("철 검", 1000);        // 데미지 10
            itemDictionary.Add("다이아검", 2000);     // 데미지 20
            itemDictionary.Add("나무방패", 500);        // 방어력 5
            itemDictionary.Add("철방패", 1000);           // 방어력 10
            itemDictionary.Add("다이아방패", 2000);     // 방어력 20
            itemDictionary.Add("빨강포션", 1000);          // 체력회복 20
            itemDictionary.Add("하양포션", 2500);     // 체력회복 50

            // 창고 만들기
            List<string> storage = new List<string>();
            storage.Add("나무검");
            storage.Add("철 검");
            storage.Add("다이아검");
            storage.Add("나무방패");
            storage.Add("철방패");
            storage.Add("다이아방패");
            storage.Add("빨강포션");
            storage.Add("하양포션");


            // 게임 시작
            while (shopingEnd_ == false)
            {
                // 상점 만들기
                // 상점에 아이템 진열하기
                Random random = new Random();
                for (int i = 0; i < itemPool; i++)
                {
                    storageRandomItem = random.Next(0, storage.Count);
                    sameNumcheck[i] = storageRandomItem;   // 중복된 아이템 반복출력 X

                    // 중복 체크
                    bool isDuplicate = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (sameNumcheck[j] == storageRandomItem)
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    // 중복된 값이면 다시 랜덤한 값을 생성
                    if (isDuplicate)
                    {
                        i--;    // i값을 감소시켜 현재 위치를 다시 검사하도록 함
                        continue;
                    }

                    sameNumcheck[i] = storageRandomItem;
                }
                // int[] sameNumCheck (넘버)을 이용해서 (창고아이템 인덱스를 체크한 후) market[] 에 아이템 놓기
                for (int i = 0; i < itemPool; i++)
                {
                    market[i] = storage[sameNumcheck[i]];
                    marketTemp[i] = market[i];
                }


                // 상점 출력하기 
                while (quitMarket_ == false)
                {
                    Console.SetCursorPosition(0, Print_PosY);
                    Print_PosY++;
                    Console.WriteLine("\t\t플레이어의 현재 소지금 : [ {0} ]", playerMoney);

                    Console.SetCursorPosition(0, Print_PosY);
                    Print_PosY++;
                    Console.WriteLine("\t\t■■ 상  점 ■■■■■■■■■■■■■■■■■■■");
                    int[] itemPrice = new int[itemPool];

                    // 상점 아이템이름, 가격 출력하기 (딕셔너리에서 출력)
                    for (int i = 0; i < itemPool; i++)
                    {
                        // 아이템딕셔너리의 키값을 새로운 키값으로 돌려보면서 market[i]와 키값이 같다면 출력합니다.
                        foreach (string key in itemDictionary.Keys)
                        {
                            if (key == market[i])
                            {
                                Console.SetCursorPosition(0, Print_PosY);
                                Print_PosY++;
                                int price = itemDictionary[key];  // 해당 아이템의 가격을 가져옴
                                itemPrice[i] = price;             // 해당 아이템의 가격을 저장해서 아이템을 구매할 때 비교할 때 사용
                                Console.WriteLine("\t\t■   {0} : {1}           \t가격: {2}\t■", i + 1, key, price);
                                break;
                            }
                        }
                    }
                    Console.WriteLine("\t\t■■■■■■■■■■■■■■■■■■■■■■■■■");
                    Console.WriteLine();

                    // 인벤토리 만들기
                    Console.WriteLine("\t\t■■ 인  벤  토  리 ■■■■■■■■■■■■■■■");

                    int countInven_ = 0;
                    foreach (string Inventroy in myInventory)
                    {
                        Console.WriteLine("\t\t■   ${0}   \t\t\t\t", Inventroy);
                        countInven_++;
                        if (countInven_ % 7 == 0)    // 가방 아이템의 갯수가 7이상이면 한칸 띄우기
                        {
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine("\t\t■■■■■■■■■■■■■■■■■■■■■■■■■");
                    Console.WriteLine();
                    Console.WriteLine();

                    // { 아이템 구매 로직  
                    int[] sameBuy = new int[4];     // 중복된 숫자를 또 누를 경우를 방지
                    int buyCount = 0;               // 구매횟수를 확인


                    // 창고의 개수가 3개 미만일 경우 게임 종료
                    if (storage.Count < 3)
                    {
                        Console.WriteLine("창고에 남은 물건이 {0}개 여서 상점주인이 퇴근했습니다.", storage.Count);
                        Console.WriteLine("아무키나 눌러 게임을 종료하세요");
                        Console.ReadKey();
                        break;
                    }

                // 플레이어가 입력을 해서 아이템을 구매하기
                reSelect:
                    Console.WriteLine("상점을 나가시려면 숫자 [0]을 입력하세요... ");
                    Console.Write("아이템을 구매하시려면 숫자 [1, 2, 3] 중에 하나를 눌러주세요 : ");
                    string itemBuy_ = Console.ReadLine();
                    Console.WriteLine();

                    // 1번째 아이템 구매
                    if (itemBuy_ == "1")
                    {
                        // 중복 체크
                        for (int i = 0; i < 4; i++)
                        {
                            if (sameBuy[i] == 1)
                            {
                                Console.WriteLine("{0} 아이템은 이미 구매했습니다", marketTemp[0]);
                                Console.WriteLine();
                                goto reSelect;
                            }

                        }

                        // 소지금 체크
                        // 1번째 아이템 구매 완료
                        if (playerMoney >= itemPrice[0])
                        {
                            Console.WriteLine("{0}을 구매했습니다.", marketTemp[0]);
                            Console.WriteLine();
                            playerMoney -= itemPrice[0]; // 소지금에서 아이템의 가격만큼 뺍니다.
                            removeCheck[0] = marketTemp[0]; // 해당 아이템을 창고에서 지우기 위해 저장
                            myInventory.Add(marketTemp[0]); // 구매한 아이템을 인벤토리에 추가합니다.
                            buyCount++;
                            sameBuy[buyCount] = 1;
                        }

                        // 아이템의 가격이 플레이어 소지금보다 비싸다면...
                        else
                        {
                            Console.WriteLine("{0}의 가격이 현재 소지금보다 비쌉니다.", marketTemp[0]);
                            Console.WriteLine();
                            goto reSelect;
                        }

                        // 창고에서 구매한 아이템 제거
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < storage.Count; j++)
                            {
                                if (removeCheck[0] == storage[j])
                                {
                                    storage.Remove(removeCheck[0]);
                                }
                            }
                        }
                    }       // 1번째 아이템 구매

                    // 2번째 아이템 구매
                    else if (itemBuy_ == "2")
                    {
                        // 중복 체크
                        for (int i = 0; i < 4; i++)
                        {
                            if (sameBuy[i] == 2)
                            {
                                Console.WriteLine("{0} 아이템은 이미 구매했습니다", marketTemp[1]);
                                Console.WriteLine();
                                goto reSelect;
                            }

                        }

                        // 소지금 체크
                        // 2번째 아이템 구매 완료
                        if (playerMoney >= itemPrice[1])
                        {
                            Console.WriteLine("{0}을 구매했습니다.", marketTemp[1]);
                            Console.WriteLine();
                            playerMoney -= itemPrice[1]; // 소지금에서 아이템의 가격만큼 뺍니다.
                            removeCheck[1] = marketTemp[1]; // 해당 아이템을 창고에서 지우기 위해 저장
                            myInventory.Add(marketTemp[1]); // 구매한 아이템을 인벤토리에 추가합니다.
                            buyCount++;
                            sameBuy[buyCount] = 2;
                        }

                        // 아이템의 가격이 플레이어 소지금보다 비싸다면...
                        else
                        {
                            Console.WriteLine("{0}의 가격이 현재 소지금보다 비쌉니다.", marketTemp[1]);
                            Console.WriteLine();
                            goto reSelect;
                        }

                        // 창고에서 구매한 아이템 제거
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < storage.Count; j++)
                            {
                                if (removeCheck[1] == storage[j])
                                {
                                    storage.Remove(removeCheck[1]);
                                }
                            }
                        }
                    }       // 2번째 아이템 구매

                    // 3번째 아이템 구매
                    else if (itemBuy_ == "3")
                    {
                        // 중복 체크
                        for (int i = 0; i < 4; i++)
                        {
                            if (sameBuy[i] == 3)
                            {
                                Console.WriteLine("{0} 아이템은 이미 구매했습니다", marketTemp[2]);
                                Console.WriteLine();
                                goto reSelect;
                            }
                        }

                        // 소지금 체크
                        // 3번째 아이템 구매 완료
                        if (playerMoney >= itemPrice[2])
                        {
                            Console.WriteLine("{0}을 구매했습니다.", marketTemp[2]);
                            Console.WriteLine();
                            playerMoney -= itemPrice[2]; // 소지금에서 아이템의 가격만큼 뺍니다.
                            removeCheck[2] = marketTemp[2]; // 해당 아이템을 창고에서 지우기 위해 저장
                            myInventory.Add(marketTemp[2]); // 구매한 아이템을 인벤토리에 추가합니다.
                            buyCount++;
                            sameBuy[buyCount] = 3;
                        }

                        // 아이템의 가격이 플레이어 소지금보다 비싸다면...
                        else
                        {
                            Console.WriteLine("{0}의 가격이 현재 소지금보다 비쌉니다.", marketTemp[2]);
                            Console.WriteLine();
                            goto reSelect;
                        }

                        // 창고에서 구매한 아이템 제거
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < storage.Count; j++)
                            {
                                if (removeCheck[2] == storage[j])
                                {
                                    storage.Remove(removeCheck[2]);
                                }
                            }
                        }
                    }       // 3번째 아이템 구매

                    // 0을 눌러서 상점 나가기
                    else if (itemBuy_ == "0")
                    {
                        Console.WriteLine("상점에서 나갔습니다");
                        Console.WriteLine();
                        quitMarket_ = true;
                        shopingEnd_ = true;
                    }

                    // buyCount가 3이상이면 상점 나가기
                    if (buyCount >= 3)
                    {
                        Console.WriteLine("모든 아이템을 구매했습니다.");
                        Console.WriteLine("아무키나 눌러 상점을 나갑니다....");
                        Console.WriteLine();
                        Console.ReadKey();
                        quitMarket_ = true;
                        shopingEnd_ = true;
                    }       // } 아이템 구매 로직

                    // 창고의 개수가 3개 미만일 경우 게임 종료
                    if (storage.Count < 3)
                    {
                        break;
                    }

                    Console.Clear();
                }
            }       // 게임 전체 while()
        }       // 상점 로직

        // 필드 로직
        static void Battle(ref int playerMaxHp, ref int playerHp, ref int playerDef, ref int playerDamage, ref int playerMoney)
        {
            Console.Clear();
            // 변수 선언부
            // 몬스터의 종류는 박쥐, 곰, 슬라임
            int monsterBatHp = 30;
            int monsterBatDamage = 8;

            int monsterSlimeHp = 50;
            int monsterSlimeDamage = 5;

            int monsterBearHp = 70;
            int monsterBearDamage = 10;

            int randomDice = 0;
            bool monsterAlive = false;

            // 변수 선언 부 end 

            // 주사위 굴려 배틀 or 체력회복 하기
            Random randomDice_ = new Random();
            Console.SetCursorPosition(10, 5);
            Console.WriteLine("랜덤 주사위 값이 1 ~ 5일 경우 몬스터와 전투합니다.");
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("랜덤 주사위 값이 6일 경우 체력을 회복합니다.");
            Console.SetCursorPosition(10, 9);
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.SetCursorPosition(10, 11);
            Console.WriteLine("아무키를 눌러 주사위를 굴려보세요.");
            Console.ReadKey();
            randomDice = randomDice_.Next(1, 7);

            // 디버그 모드
            // randomDice = 6;
            // 전투
            if ((1 <= randomDice) && (randomDice <= 5))
            {
                // 박쥐와 전투
                if (randomDice <= 2)
                {
                    monsterAlive = true;
                    Console.SetCursorPosition(30, 14);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("      ▨▥ 주사위의 값 : {0}\n\n", randomDice);
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(30, 16);
                    Console.WriteLine("   !!  박쥐가 나타났습니다  !!\n\n");
                    Console.SetCursorPosition(30, 18);
                    Console.WriteLine("   [체력 : {0}]   [공격력 : {1}] \n\n", monsterBatHp, monsterBatDamage);
                    Console.ResetColor();

                    Bat_Art();
                    Console.SetCursorPosition(50, 27);
                    Console.WriteLine("▶ 아무키나 눌러 공격하세요!!\n\n");
                    Console.ReadKey();

                    Console.WriteLine();
                    Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
                    while (monsterAlive)
                    {
                        Console.WriteLine("당신은 박쥐에게 {0}의 데미지를 입혔습니다.\n", playerDamage);
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        monsterBatHp = monsterBatHp - playerDamage;
                        Thread.Sleep(500);

                        if (monsterBatHp <= 0)
                        {
                            Console.WriteLine("박쥐의 남은 체력 : 0\n\n");
                            monsterAlive = false;
                            Thread.Sleep(500);
                            Console.WriteLine("박쥐를 처치했습니다!\n");
                            Console.WriteLine("박쥐와의 전투에 승리해 100원을 얻었습니다.\n");
                            playerMoney = playerMoney + 100;
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n\n");
                            Console.WriteLine("▶ 아무키를 눌러 전투를 종료하세요...\n\n");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        else
                        {
                            Console.WriteLine("박쥐의 남은 체력 : {0}\n\n", monsterBatHp);
                            Thread.Sleep(500);
                            Console.WriteLine("박쥐가 당신을 공격했습니다!!\n");
                            Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                            Thread.Sleep(500);
                            Console.WriteLine("★★★ -{0} ★★★\n", monsterBatDamage);
                            playerHp = playerHp - monsterBatDamage;
                            Thread.Sleep(500);
                            Console.WriteLine("플레이어 현재 체력 : {0}\n\n", playerHp);
                            Thread.Sleep(500);

                            // 전투를 진행하다 플레이어의 체력이 0이하가 되면 게임오버 함수를 불러오고 게임을 종료합니다.
                            if (playerHp <= 0)
                            {
                                GameOver(playerHp);
                                break;
                            }
                        }
                    }
                }       // 박쥐 전투 end

                // 슬라임과 전투
                if ((2 < randomDice) && (randomDice <= 4))
                {
                    monsterAlive = true;
                    Console.SetCursorPosition(30, 14);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("      ▨▥ 주사위의 값 : {0}\n\n", randomDice);
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(30, 16);
                    Console.WriteLine("   !!  슬라임이 나타났습니다  !!\n\n");
                    Console.SetCursorPosition(30, 18);
                    Console.WriteLine("   [체력 : {0}]   [공격력 : {1}] \n\n", monsterSlimeHp, monsterSlimeDamage);
                    Console.ResetColor();

                    Slime_Art();
                    Console.SetCursorPosition(50, 27);
                    Console.WriteLine("▶ 아무키나 눌러 공격하세요!!\n\n");
                    Console.ReadKey();

                    Console.WriteLine();
                    Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
                    while (monsterAlive)
                    {
                        Console.WriteLine("당신은 슬라임에게 {0}의 데미지를 입혔습니다.\n", playerDamage);
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        monsterSlimeHp = monsterSlimeHp - playerDamage;
                        Thread.Sleep(500);

                        if (monsterSlimeHp <= 0)
                        {
                            Console.WriteLine("슬라임의 남은 체력 : 0\n\n");
                            monsterAlive = false;
                            Thread.Sleep(500);
                            Console.WriteLine("슬라임을 처치했습니다!\n");
                            Console.WriteLine("슬라임과의 전투에 승리해 150원을 얻었습니다.\n");
                            playerMoney = playerMoney + 150;
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n\n");
                            Console.WriteLine("▶ 아무키를 눌러 전투를 종료하세요...\n\n");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        else
                        {
                            Console.WriteLine("슬라임의 남은 체력 : {0}\n\n", monsterSlimeHp);
                            Thread.Sleep(500);
                            Console.WriteLine("슬라임이 당신을 공격했습니다!!\n");
                            Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                            Thread.Sleep(500);
                            Console.WriteLine("★★★ -{0} ★★★\n", monsterSlimeDamage);
                            playerHp = playerHp - monsterSlimeDamage;
                            Thread.Sleep(500);
                            Console.WriteLine("플레이어 현재 체력 : {0}\n\n", playerHp);
                            Thread.Sleep(500);

                            // 전투를 진행하다 플레이어의 체력이 0이하가 되면 게임오버 함수를 불러오고 게임을 종료합니다.
                            if (playerHp <= 0)
                            {
                                GameOver(playerHp);
                                break;
                            }
                        }
                    }
                }       // 슬라임 전투 end

                // 곰과 전투
                if (randomDice == 5)
                {
                    monsterAlive = true;
                    Console.SetCursorPosition(30, 14);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("      ▨▥ 주사위의 값 : {0}\n\n", randomDice);
                    Console.ResetColor();

                    Console.SetCursorPosition(30, 16);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("   !!  곰이 나타났습니다  !!\n\n");
                    Console.SetCursorPosition(30, 18);
                    Console.WriteLine("   [체력 : {0}]   [공격력 : {1}] \n\n", monsterBearHp, monsterBearDamage);
                    Console.ResetColor();

                    Bear_Art();
                    Console.SetCursorPosition(50, 30);
                    Console.WriteLine("▶ 아무키나 눌러 공격하세요!!\n\n");
                    Console.ReadKey();

                    Console.WriteLine();
                    Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
                    while (monsterAlive)
                    {
                        Console.WriteLine("당신은 곰에게 {0}의 데미지를 입혔습니다.\n", playerDamage);
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        monsterBearHp = monsterBearHp - playerDamage;
                        Thread.Sleep(500);

                        if (monsterBearHp <= 0)
                        {
                            Console.WriteLine("곰의 남은 체력 : 0\n\n");
                            monsterAlive = false;
                            Thread.Sleep(500);
                            Console.WriteLine("곰을 처치했습니다!\n");
                            Console.WriteLine("곰과의 전투에 승리해 200원을 얻었습니다.\n");
                            playerMoney = playerMoney + 200;
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n\n");
                            Console.WriteLine("▶ 아무키를 눌러 전투를 종료하세요...\n\n");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        else
                        {
                            Console.WriteLine("곰의 남은 체력 : {0}\n\n", monsterBearHp);
                            Thread.Sleep(500);
                            Console.WriteLine("곰이 당신을 공격했습니다!!\n");
                            Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                            Thread.Sleep(500);
                            Console.WriteLine("★★★ -{0} ★★★\n", monsterBearDamage);
                            playerHp = playerHp - monsterBearDamage;
                            Thread.Sleep(500);
                            Console.WriteLine("플레이어 현재 체력 : {0}\n\n", playerHp);
                            Thread.Sleep(500);

                            // 전투를 진행하다 플레이어의 체력이 0이하가 되면 게임오버 함수를 불러오고 게임을 종료합니다.
                            if (playerHp <= 0)
                            {
                                GameOver(playerHp);
                                break;
                            }
                        }
                    }       // 곰 전투 end
                }
            }
            // 체력회복
            else
            {
                Console.Clear();
                MountainHeal_Art();

                Console.SetCursorPosition(15, 22);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\t\t\t      ▨▥ 주사위의 값 : {0}\n\n", randomDice);
                Console.ResetColor();

                Console.SetCursorPosition(15, 24);
                Console.WriteLine("\t\t\t    플레어어는 잠시 쉬면서 경치를 바라봅니다.");
                Console.SetCursorPosition(15, 26);
                Console.WriteLine("\t\t\t    ▲ 플레이어의 체력이 [20] 회복되었습니다.");
                playerHp = playerHp + 20;

                if (playerHp > playerMaxHp)
                {
                    playerHp = playerMaxHp;
                }
                Console.SetCursorPosition(15, 28);
                Console.WriteLine("\t\t\t    현재 플레이어의 체력 : [{0}] 입니다.", playerHp);
                Console.SetCursorPosition(50, 30);
                Console.WriteLine("\t\t▶ 아무키를 눌러 마을로 돌아가세요...");
                Console.ReadKey();
                Console.Clear();
            }
        }       // 배틀 필드 로직

        // 박쥐 이미지
        static void Bat_Art()
        {
            Console.SetCursorPosition(31, 20);
            Console.WriteLine("    =/\\                 /\\=			");
            Console.SetCursorPosition(31, 21);
            Console.WriteLine("    / \\\'._   (\\_/)   _.\'/ \\			");
            Console.SetCursorPosition(31, 22);
            Console.WriteLine("   / .\'\'._\'--(o.o)--\'_.\'\'. \\		");
            Console.SetCursorPosition(31, 23);
            Console.WriteLine("  /.\' _/ |`\'=/ \" \\ = `|\\_ `.\\		");
            Console.SetCursorPosition(31, 24);
            Console.WriteLine(" /` .\' `\\;-,\'\\___/\',-;/` \'. \'\\		");
            Console.SetCursorPosition(31, 25);
            Console.WriteLine("/.-\'       `\\(-V-)/`       `-.\\				");
            Console.SetCursorPosition(31, 26);
            Console.WriteLine("`            \"   \"            `				");
        }

        // 슬라임 이미지
        static void Slime_Art()
        {
            Console.SetCursorPosition(42, 20);
            Console.WriteLine(" ╭────╮ \n");
            Console.SetCursorPosition(42, 21);
            Console.WriteLine(" │\\_/│ \n");
            Console.SetCursorPosition(42, 22);
            Console.WriteLine(" │    │ \n");
            Console.SetCursorPosition(42, 23);
            Console.WriteLine("─┴────┴─\n");
        }

        // 곰 이미지
        static void Bear_Art()
        {
            Console.SetCursorPosition(39, 20);
            Console.WriteLine("    ╭─────╮    \n");
            Console.SetCursorPosition(39, 21);
            Console.WriteLine(" (O)│ ‾o‾ │(O) \n");
            Console.SetCursorPosition(39, 22);
            Console.WriteLine("╭─╨─╯╔═══╗╰─╨─╮\n");
            Console.SetCursorPosition(39, 23);
            Console.WriteLine("│ ╭╮╔╝   ╚╗╭╮ │\n");
            Console.SetCursorPosition(39, 24);
            Console.WriteLine("╰─╯╔╝     ╚╗╰─╯\n");
            Console.SetCursorPosition(39, 25);
            Console.WriteLine("   ╚╗     ╔╝   \n");
            Console.SetCursorPosition(39, 26);
            Console.WriteLine("  ╭╯╚╗   ╔╝╰╮  \n");
            Console.SetCursorPosition(39, 27);
            Console.WriteLine("  │ ╭╚═══╝╮ │  \n");
            Console.SetCursorPosition(39, 28);
            Console.WriteLine("  ╰─╯     ╰─╯   \n");
        }

        // 게임 오버
        static void GameOver(int playerHp)
        {
            if (playerHp <= 0)
            {
                Console.Clear();
                Console.SetCursorPosition(15, 10);
                Console.WriteLine("＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠\n");
                Console.SetCursorPosition(15, 12);
                Console.WriteLine("＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠\n\n");
                Console.SetCursorPosition(15, 14);
                Console.WriteLine("＠＠＠＠＠＠＠           당신의 체력이 0이 되었습니다.      ＠＠＠＠＠＠＠\n");
                Console.SetCursorPosition(15, 16);
                Console.WriteLine("＠＠＠＠＠＠＠                   GAME OVER                  ＠＠＠＠＠＠＠\n");
                Console.SetCursorPosition(15, 18);
                Console.WriteLine("＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠\n");
                Console.SetCursorPosition(15, 10);
                Console.WriteLine("＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠＠\n");
                Console.SetCursorPosition(15, 12);
                Console.ReadKey();
            }
        }

        // 체력 회복 이미지
        static void MountainHeal_Art()
        {
            Console.WriteLine("\n\n");
            Console.SetCursorPosition(15, 3);
            Console.WriteLine("\t\t                            .-\"\"\"\"-                                   ");
            Console.SetCursorPosition(15, 4);
            Console.WriteLine("\t\t                           F   .-'                                    ");
            Console.SetCursorPosition(15, 5);
            Console.WriteLine("\t\t                          F   J                                       ");
            Console.SetCursorPosition(15, 6);
            Console.WriteLine("\t\t                         I    I                                       ");
            Console.SetCursorPosition(15, 7);
            Console.WriteLine("\t\t                          L   `.                                      ");
            Console.SetCursorPosition(15, 8);
            Console.WriteLine("\t\t                           L    `-._,                                 ");
            Console.SetCursorPosition(15, 9);
            Console.WriteLine("\t\t                            `-.__.-'            ##                    ");
            Console.SetCursorPosition(15, 10);
            Console.WriteLine("\t\t                                               ###                    ");
            Console.SetCursorPosition(15, 11);
            Console.WriteLine("\t\t                        #                      ####                   ");
            Console.SetCursorPosition(15, 12);
            Console.WriteLine("\t\t              _____   ##                 .---#####-...__              ");
            Console.SetCursorPosition(15, 13);
            Console.WriteLine("\t\t          .--'     `-###          .--..-'    ######     \"\"`---....    ");
            Console.SetCursorPosition(15, 14);
            Console.WriteLine("\t\t _____.----.        ###`.._____ .'          #######                   ");
            Console.SetCursorPosition(15, 15);
            Console.WriteLine("\t\t a:f                ###       /       -.    ####### _.---             ");
            Console.SetCursorPosition(15, 16);
            Console.WriteLine("\t\t                    ###     .(              #######                   ");
            Console.SetCursorPosition(15, 17);
            Console.WriteLine("\t\t                     #      : `--...        ######                    ");
            Console.SetCursorPosition(15, 18);
            Console.WriteLine("\t\t                     #       `.     ``.     ######                    ");
            Console.SetCursorPosition(15, 19);
            Console.WriteLine("\t\t                               :       :.    #####                    ");

            Console.WriteLine("\n\n");
        }

        // 맵 숲 출력
        static void Print_Forest()
        {
            //Console.WriteLine("\t\t\t    .                  .-.    .  _   *     _   .");
            //Console.WriteLine("\t\t\t           *          /   \\     ((       _/ \\       *    .");
            //Console.WriteLine("\t\t\t         _    .   .--'\\/\\_ \\     `      /    \\  *    ___");
            //Console.WriteLine("\t\t\t     *  / \\_    _/ ^      \\/\\'__        /\\/\\  /\\  __/   \\ *");
            Console.WriteLine("\t\t\t       /    \\  /    .'   _/  /  \\  *' /    \\/  \\/ .`'\\_/\\   .");
            Console.WriteLine("\t\t\t  .   /\\/\\  /\\/ :' __  ^/  ^/    `--./.'  ^  `-.\\ _    _:\\ _");
            Console.WriteLine("\t\t\t     /    \\/  \\  _/  \\-' __/.' ^ _   \\_   .'\\   _/ \\ .  __/ \\");
            Console.WriteLine("\t\t\t   /\\  .-   `. \\/     \\ / -.   _/ \\ -. `_/   \\ /    `._/  ^  \\");
            Console.WriteLine("\t\t\t  /  `-.__ ^   / .-'.--'    . /    `--./ .-'  `-.  `-. `.  -  `.");
            Console.WriteLine("\t\t\t@/        `.  / /      `-.   /  .-'   / .   .'   \\    \\  \\  .-  \\%");
        }
    }
}

