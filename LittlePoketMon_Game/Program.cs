using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LittlePoketMon_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 플레이어 스탯
            int player_Hp = 500;
            int player_Def = 0;
            int player_Damage = 50;

            // 맵 크기
            int mapSize_Y = 20;
            int mapSize_X = 40;
            string[,] map = new string[mapSize_Y, mapSize_X];

            // 처음 플레이어 좌표
            int playerPos_Y = mapSize_Y - 5;
            int playerPos_X = 5;

            // 풀숲안에 있는지 체크해주는 변수 ( 2 이상일 경우 풀 숲 )
            int inGrassCheckCount;

            // 전투 승리 횟수
            int victory_Battle = 0;
            int quest_Battle = 0;

            // 퀘스트 진행 확인을 위한 값
            bool isQuest_ing = false;

            // 초기 맵 세팅
            Set_Map(ref map, mapSize_Y, mapSize_X);

            // 게임 시작
            while(true)
            {
                // 첫 세팅
                inGrassCheckCount = 0;
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);
                
                // 맵 출력
                Print_Map(ref map, mapSize_Y, mapSize_X);
                Console.SetCursorPosition(0, 21);
                Console.WriteLine("플레이어 정보  :  [  체　력  :  {0}  ]   [  방어력  :  {1}  ]    [  공격력  :  {2}  ]", player_Hp, player_Def, player_Damage);
                Console.Write("\t\t  [  전투  승리 : {0}  ]", victory_Battle);
                
                // 퀘스트 중인가?
                if(isQuest_ing == true)
                {
                    Quest_Start(quest_Battle);
                }
                
                // 플레이어 행동
                Action_Player(ref map, mapSize_Y, mapSize_X, ref playerPos_Y, ref playerPos_X, ref quest_Battle, ref isQuest_ing, ref player_Hp);

                // 풀 숲인지 체크
                InGrassCheck(map, playerPos_Y, playerPos_X, ref inGrassCheckCount);

                // 플레이어가 움직인 후 풀 숲이면 10% 확률로 전투합니다.
                if (inGrassCheckCount >= 2)
                {
                    Random random = new Random();
                    int checkDice_ = random.Next(1, 11); // 1 ~ 11 까지 중 1 나오면 전투 시작
                    if (checkDice_ == 1)
                    {
                        Battle_Start(ref player_Hp, ref player_Def, ref player_Damage, ref victory_Battle, ref quest_Battle);
                        Console.Clear();
                    }
                    
                }
                else { /*pass*/ }
            }      
        }
        // 맵 세팅
        static void Set_Map(ref string[,] map, int mapSize_Y, int mapSize_X)
        {
            for (int y = 0; y < mapSize_Y; y++)
            {
                for (int x = 0; x < mapSize_X; x++)
                {
                    // 벽 생성
                    if (((y == 0) || (y == mapSize_Y - 1)) || ((x == 0) || (x == mapSize_X - 1)))
                    {
                        map[y, x] = "■";
                        continue;
                    }

                    // 플레이어 생성
                    if ((y == mapSize_Y - 5) && (x == 5))
                    {
                        map[y, x] = "◎";
                        continue;
                    }

                    // 풀 숲 생성
                    if ((6 <= y ) && (y <= 12) && (15 <= x) && (x <= 35))
                    {
                        map[y, x] = "# ";
                        continue;
                    }

                    // 상점 생성
                    if (((2 <= y) && (y <= 5)) && ((2 <= x) && (x <= 7)))
                    {
                        // 상점 간판
                        {
                            if ((y == 2) && (3 == x))
                            {
                                map[y, x] = "S";
                                continue;
                            }
                            if ((y == 2) && (4 == x))
                            {
                                map[y, x] = "H";
                                continue;
                            }
                            if ((y == 2) && (5 == x))
                            {
                                map[y, x] = "O";
                                continue;
                            }
                            if ((y == 2) && (6 == x))
                            {
                                map[y, x] = "P";
                                continue;
                            }
                        }       // 상점 간판
                        map[y, x] = "X";
                        continue;
                    }

                    // NPC 생성
                    if((y == 7) && (x == 6))
                    {
                        map[y, x] = "&";
                        continue;
                    }

                    // 바닥 생성
                    map[y, x] = "□";
                }
            }
        }

        // 맵 출력
        static void Print_Map(ref string[,] map, int mapSize_Y, int mapSize_X)
        {
            for (int y = 0; y < mapSize_Y; y++)
            {
                for (int x = 0; x < mapSize_X; x++)
                {
                    // 벽 색상 변경
                    if (map[y, x] == "■")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 플레이어 색상 변경
                    if (map[y, x] == "◎")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 풀 숲 색상 변경
                    if (map[y, x] == "# ")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 상점 색상 변경
                    if (map[y, x] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("{0} ", map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    if ((map[y, x] == "S") || (map[y, x] == "H") || (map[y, x] == "O") || (map[y, x] == "P"))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("{0} ", map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // NPC 색상 변경
                    if (map[y, x] == "&")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{0} ", map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 바닥 색상 변경
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(map[y, x]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        // 플레이어 이동
        static void Action_Player(ref string[,] map, int mapSize_Y, int mapSize_X, ref int playerPos_Y, ref int playerPos_X, ref int quest_Battle, ref bool isQuest_ing, ref int player_Hp)
        {
            ConsoleKey MoveKey = Console.ReadKey(true).Key;

            // A 입력 받았을 때,
            if (MoveKey == ConsoleKey.A)
            {
                // 플레이어 좌표 (y, x - 1) 이
                // 바닥이나 풀숲 일 경우 스왑
                if ((map[playerPos_Y, playerPos_X - 1] == "□") || (map[playerPos_Y, playerPos_X - 1] == "# "))
                {
                    Swap_A(ref map, ref playerPos_Y, ref playerPos_X);
                }

                else { /*pass*/ }

            }
            // D 입력 받았을 때,
            if (MoveKey == ConsoleKey.D)
            {
                // 플레이어 좌표 (y, x - 1) 이
                // 바닥이나 풀숲 일 경우 스왑
                if ((map[playerPos_Y, playerPos_X + 1] == "□") || (map[playerPos_Y, playerPos_X + 1] == "# "))
                {
                    Swap_D(ref map, ref playerPos_Y, ref playerPos_X);
                }

                else { /*pass*/ }

            }
            // W 입력 받았을 때,
            if (MoveKey == ConsoleKey.W)
            {
                // 플레이어 좌표 (y, x - 1) 이
                // 바닥이나 풀숲 일 경우 스왑
                if ((map[playerPos_Y - 1, playerPos_X] == "□") || (map[playerPos_Y - 1, playerPos_X] == "# "))
                {
                    Swap_W(ref map, ref playerPos_Y, ref playerPos_X);
                }

                else { /*pass*/ }
            }
            // S 입력 받았을 때,
            if (MoveKey == ConsoleKey.S)
            {
                // 플레이어 좌표 (y, x - 1) 이
                // 바닥이나 풀숲 일 경우 스왑
                if ((map[playerPos_Y + 1, playerPos_X] == "□") || (map[playerPos_Y + 1, playerPos_X] == "# "))
                {
                    Swap_S(ref map, ref playerPos_Y, ref playerPos_X);
                }

                else { /*pass*/ }
            }

            // NPC 근처에서 X를 입력받았을 때,
            if (MoveKey == ConsoleKey.X)
            {
                if((map[playerPos_Y, playerPos_X - 1] == "&") || (map[playerPos_Y, playerPos_X + 1] == "&") ||
                        (map[playerPos_Y - 1, playerPos_X] == "&") || (map[playerPos_Y + 1, playerPos_X] == "&"))
                {
                    Quest(ref quest_Battle, ref isQuest_ing, ref player_Hp);
                    Console.ReadKey();
                    Console.Clear();
                }
                else { /*pass*/ }
            }
        }

        // 스왑을 위한 함수
        #region
        static void Swap_A(ref string[,] map, ref int playerPos_Y, ref int playerPos_X)
        {
            string temp_;

            // 처음 풀 숲에 진입했을 경우
            if ((map[playerPos_Y, playerPos_X - 1] == "# ") && (map[playerPos_Y, playerPos_X + 1] == "□") && (map[playerPos_Y + 1, playerPos_X] != "# ") && (map[playerPos_Y - 1, playerPos_X] != "# "))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y, playerPos_X - 1];
                map[playerPos_Y, playerPos_X - 1] = temp_;
                playerPos_X--;

                // 뒤에 있는 풀을 제거합니다.
                map[playerPos_Y, playerPos_X + 1] = "□";

            }

            // 처음 풀 숲에서 나갔을 경우
            // A 쪽은 바닥이고, 뒤에는 풀 숲이고, 위아래 둘중 하나에 풀이 있을 경우
            else if ((map[playerPos_Y, playerPos_X - 1] == "□") && (map[playerPos_Y, playerPos_X + 1] == "# ") && ((map[playerPos_Y + 1, playerPos_X] == "# ") || (map[playerPos_Y - 1, playerPos_X] == "# ")))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y, playerPos_X - 1];
                map[playerPos_Y, playerPos_X - 1] = temp_;
                playerPos_X--;

                // 뒤에 바닥대신 풀을 생성합니다.
                map[playerPos_Y, playerPos_X + 1] = "# ";

            }

            // 풀숲에서 이동했을 경우와 그외 이동 했을 경우는 
            // 그냥 스왑하면 됩니다.
            else
            {
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y, playerPos_X - 1];
                map[playerPos_Y, playerPos_X - 1] = temp_;

                playerPos_X--;
            }
        }

        static void Swap_D(ref string[,] map, ref int playerPos_Y, ref int playerPos_X)
        {
            string temp_;

            // 처음 풀 숲에 진입했을 경우
            if ((map[playerPos_Y, playerPos_X + 1] == "# ") && (map[playerPos_Y, playerPos_X - 1] == "□") && (map[playerPos_Y + 1, playerPos_X] != "# ") && (map[playerPos_Y - 1, playerPos_X] != "# "))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y, playerPos_X + 1];
                map[playerPos_Y, playerPos_X + 1] = temp_;
                playerPos_X++;

                // 뒤에 있는 풀을 제거합니다.
                map[playerPos_Y, playerPos_X - 1] = "□";
                
            }

            // 처음 풀 숲에서 나갔을 경우
            // D 쪽은 바닥이고, 뒤에는 풀 숲이고, 위아래 둘중 하나에 풀이 있을 경우
            else if ((map[playerPos_Y, playerPos_X + 1] == "□") && (map[playerPos_Y, playerPos_X - 1] == "# ") && ((map[playerPos_Y + 1, playerPos_X] == "# ") || (map[playerPos_Y - 1, playerPos_X] == "# ")))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y, playerPos_X + 1];
                map[playerPos_Y, playerPos_X + 1] = temp_;
                playerPos_X++;

                // 뒤에 바닥대신 풀을 생성합니다.
                map[playerPos_Y, playerPos_X - 1] = "# ";

            }

            // 풀숲에서 이동했을 경우와 그외 이동 했을 경우는 
            // 그냥 스왑하면 됩니다.
            else
            {
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y, playerPos_X + 1];
                map[playerPos_Y, playerPos_X + 1] = temp_;

                playerPos_X++;
            }
        }

        static void Swap_W(ref string[,] map, ref int playerPos_Y, ref int playerPos_X)
        {
            string temp_;

            // 처음 풀 숲에 진입했을 경우
            if ((map[playerPos_Y - 1, playerPos_X] == "# ") && (map[playerPos_Y + 1, playerPos_X] == "□") && ((map[playerPos_Y, playerPos_X - 1] != "# ") && (map[playerPos_Y, playerPos_X + 1] != "# ")))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y - 1, playerPos_X];
                map[playerPos_Y - 1, playerPos_X] = temp_;
                playerPos_Y--;

                // 뒤에 있는 풀을 제거합니다.
                map[playerPos_Y + 1, playerPos_X] = "□";

            }

            // 처음 풀 숲에서 나갔을 경우
            // A 쪽은 바닥이고, 뒤에는 풀 숲이고, 위아래 둘중 하나에 풀이 있을 경우
            else if ((map[playerPos_Y - 1, playerPos_X] == "□") && (map[playerPos_Y + 1, playerPos_X] == "# ") && ((map[playerPos_Y, playerPos_X - 1] == "# ") || (map[playerPos_Y, playerPos_X + 1] == "# ")))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y - 1, playerPos_X];
                map[playerPos_Y - 1, playerPos_X] = temp_;
                playerPos_Y--;

                // 뒤에 바닥대신 풀을 생성합니다.
                map[playerPos_Y + 1, playerPos_X] = "# ";

            }

            // 풀숲에서 이동했을 경우와 그외 이동 했을 경우는 
            // 그냥 스왑하면 됩니다.
            else
            {
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y - 1, playerPos_X];
                map[playerPos_Y - 1, playerPos_X] = temp_;
                playerPos_Y--;
            }
        }

        static void Swap_S(ref string[,] map, ref int playerPos_Y, ref int playerPos_X)
        {
            string temp_;

            // 처음 풀 숲에 진입했을 경우
            if ((map[playerPos_Y + 1, playerPos_X] == "# ") && (map[playerPos_Y - 1, playerPos_X] == "□") && ((map[playerPos_Y, playerPos_X + 1] != "# ") && (map[playerPos_Y, playerPos_X - 1] != "# ")))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y + 1, playerPos_X];
                map[playerPos_Y + 1, playerPos_X] = temp_;
                playerPos_Y++;

                // 뒤에 있는 풀을 제거합니다.
                map[playerPos_Y - 1, playerPos_X] = "□";

            }

            // 처음 풀 숲에서 나갔을 경우
            // A 쪽은 바닥이고, 뒤에는 풀 숲이고, 위아래 둘중 하나에 풀이 있을 경우
            else if ((map[playerPos_Y + 1, playerPos_X] == "□") && (map[playerPos_Y - 1, playerPos_X] == "# ") && ((map[playerPos_Y, playerPos_X + 1] == "# ") || (map[playerPos_Y, playerPos_X - 1] == "# ")))
            {
                // 스왑 한 뒤
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y + 1, playerPos_X];
                map[playerPos_Y + 1, playerPos_X] = temp_;
                playerPos_Y++;

                // 뒤에 바닥대신 풀을 생성합니다.
                map[playerPos_Y - 1, playerPos_X] = "# ";

            }

            // 풀숲에서 이동했을 경우와 그외 이동 했을 경우는 
            // 그냥 스왑하면 됩니다.
            else
            {
                temp_ = map[playerPos_Y, playerPos_X];
                map[playerPos_Y, playerPos_X] = map[playerPos_Y + 1, playerPos_X];
                map[playerPos_Y + 1, playerPos_X] = temp_;
                playerPos_Y++;
            }
        }
        
        #endregion

        // 현재 위치가 풀숲인지 확인하는 함수
        static void InGrassCheck(string[,] map, int playerPos_Y, int playerPos_X, ref int inGrassCheckCount)
        {
            // 풀이 앞뒤좌우 중 2개 이상일 경우 풀 숲에 있다고 판단할 것임.
            // A 방향 확인
            if (map[playerPos_Y, playerPos_X - 1] == "# ")
            {
                inGrassCheckCount++;
            }
            // D 방향 확인
            if (map[playerPos_Y, playerPos_X + 1] == "# ")
            {
                inGrassCheckCount++;
            }
            // W 방향 확인
            if (map[playerPos_Y - 1, playerPos_X] == "# ")
            {
                inGrassCheckCount++;
            }
            // S 방향 확인
            if (map[playerPos_Y + 1, playerPos_X] == "# ")
            {
                inGrassCheckCount++;
            }
        }


        // 몬스터 생성
        static void Creative_Monster()
        {
            Monster coco = new Monster("코코", 100, 5, 10);
            Monster rira = new Monster("리라", 150, 10, 5);
            Monster muuo = new Monster("무우", 300, 20, 3);
        }

        // 전투 시작
        static void Battle_Start(ref int player_Hp, ref int player_Def, ref int player_Damage, ref int victory_Battle, ref int quest_Battle)
        {
            Console.Clear();
            int playerTurn_Count = 1;
            int enemyTurn_Count = 1;

            // 몬스터 목록
            Monster coco = new Monster("코코", 100, 5, 10);
            Monster rira = new Monster("리라", 150, 10, 5);
            Monster muuo = new Monster("무우", 300, 20, 3);

            // 랜덤 주사위를 굴려서 누구와 전투할지 선택
            Random random = new Random();
            int monster_Choice = random.Next(1, 4); // 1 ~ 3

            //디버그 모드
            //monster_Choice = 1;

            // 1일 경우
            if (monster_Choice == 1)
            {
                bool isBattle_ = true;
                Console.WriteLine("야생의 [{0}]가 나타났다!", coco.monster_Name);
                while (isBattle_)
                {
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", coco.monster_Name, coco.monster_Hp);

                    // 직접 공격하기
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                    Console.SetCursorPosition(33, 7);
                    Console.WriteLine("X를 눌러 공격하세요!!");
                    ConsoleKey attack_ = ConsoleKey.A;

                    while (attack_ != ConsoleKey.X)
                    {
                        attack_ = Console.ReadKey(true).Key;
                    }

                    // 플레이어 턴
                    Console.WriteLine();
                    Console.SetCursorPosition(0, 9 + playerTurn_Count);
                    Thread.Sleep(250);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("   당신이 [{0}]에게 _{1}_의 데미지를 입혔습니다. ▶ ", coco.monster_Name, player_Damage);
                    Console.ResetColor();
                    coco.Monster_DecreaseHP(player_Damage);
                    Console.WriteLine();
                    playerTurn_Count = playerTurn_Count + 4;

                    // 플레이어 턴 종료 후
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", coco.monster_Name, coco.monster_Hp);

                    // 몬스터의 체력이 0이 되었을 때, 전투 승리
                    if (coco.monster_Hp <= 0)
                    {
                        Console.SetCursorPosition(28, 13 + enemyTurn_Count);
                        Console.WriteLine("[{0}]와의 전투에서 승리했습니다.", coco.monster_Name);

                        ConsoleKey quit_ = ConsoleKey.A;

                        while (quit_ != ConsoleKey.Q)
                        {
                            Console.SetCursorPosition(33, 15 + enemyTurn_Count);
                            Console.WriteLine("Q를 눌러 돌아가세요.");
                            quit_ = Console.ReadKey(true).Key;
                        }
                        break;
                    }

                    // 몬스터 턴
                    Console.SetCursorPosition(0, 11 + enemyTurn_Count);
                    Thread.Sleep(250);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t ◀ [{0}]이/가 당신에게 _{1}_의 데미지를 입혔습니다.", coco.monster_Name, coco.monster_Damage);
                    Console.ResetColor();
                    player_Hp = player_Hp - coco.monster_Damage;
                    enemyTurn_Count = enemyTurn_Count + 4;

                    // 몬스터 턴 종료 후
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", coco.monster_Name, coco.monster_Hp);


                    // 플레이어의 체력이 0이 되었을 때, 전투 패배
                    if (player_Hp <= 0)
                    {
                        Console.WriteLine("[{0}]와의 전투에서 패배했습니다.", coco.monster_Name);
                        Console.WriteLine("당신의 남은 체력이 0이기 때문에 게임 종료됩니다.");
                        break;
                    }
                }

            }

            // 2일 경우
            if (monster_Choice == 2)
            {
                bool isBattle_ = true;
                Console.WriteLine("야생의 [{0}]가 나타났다!", rira.monster_Name);
                while (isBattle_)
                {
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", rira.monster_Name, rira.monster_Hp);

                    // 직접 공격하기
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                    Console.SetCursorPosition(33, 7);
                    Console.WriteLine("X를 눌러 공격하세요!!");
                    ConsoleKey attack_ = ConsoleKey.A;

                    while (attack_ != ConsoleKey.X)
                    {
                        attack_ = Console.ReadKey(true).Key;
                    }

                    // 플레이어 턴
                    Console.WriteLine();
                    Console.SetCursorPosition(0, 9 + playerTurn_Count);
                    Thread.Sleep(250);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("   당신이 [{0}]에게 _{1}_의 데미지를 입혔습니다. ▶ ", rira.monster_Name, player_Damage);
                    Console.ResetColor();
                    rira.Monster_DecreaseHP(player_Damage);
                    Console.WriteLine();
                    playerTurn_Count = playerTurn_Count + 4;

                    // 플레이어 턴 종료 후
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", rira.monster_Name, rira.monster_Hp);

                    // 몬스터의 체력이 0이 되었을 때, 전투 승리
                    if (rira.monster_Hp <= 0)
                    {
                        Console.SetCursorPosition(28, 13 + enemyTurn_Count);
                        Console.WriteLine("[{0}]와의 전투에서 승리했습니다.", rira.monster_Name);

                        ConsoleKey quit_ = ConsoleKey.A;

                        while (quit_ != ConsoleKey.Q)
                        {
                            Console.SetCursorPosition(33, 15 + enemyTurn_Count);
                            Console.WriteLine("Q를 눌러 돌아가세요.");
                            quit_ = Console.ReadKey(true).Key;
                        }
                        break;
                    }

                    // 몬스터 턴
                    Console.SetCursorPosition(0, 11 + enemyTurn_Count);
                    Thread.Sleep(250);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t ◀ [{0}]이/가 당신에게 _{1}_의 데미지를 입혔습니다.", rira.monster_Name, rira.monster_Damage);
                    Console.ResetColor();
                    player_Hp = player_Hp - rira.monster_Damage;
                    enemyTurn_Count = enemyTurn_Count + 4;

                    // 몬스터 턴 종료 후
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", rira.monster_Name, rira.monster_Hp);


                    // 플레이어의 체력이 0이 되었을 때, 전투 패배
                    if (player_Hp <= 0)
                    {
                        Console.WriteLine("[{0}]와의 전투에서 패배했습니다.", rira.monster_Name);
                        Console.WriteLine("당신의 남은 체력이 0이기 때문에 게임 종료됩니다.");
                        break;
                    }
                }

            }

            // 3일 경우
            if (monster_Choice == 3)
            {
                bool isBattle_ = true;
                Console.WriteLine("야생의 [{0}]가 나타났다!", muuo.monster_Name);
                while (isBattle_)
                {
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", muuo.monster_Name, muuo.monster_Hp);

                    // 직접 공격하기
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                    Console.SetCursorPosition(33, 7);
                    Console.WriteLine("X를 눌러 공격하세요!!");
                    ConsoleKey attack_ = ConsoleKey.A;

                    while (attack_ != ConsoleKey.X)
                    {
                        attack_ = Console.ReadKey(true).Key;
                    }

                    // 플레이어 턴
                    Console.WriteLine();
                    Console.SetCursorPosition(0, 9 + playerTurn_Count);
                    Thread.Sleep(250);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("   당신이 [{0}]에게 _{1}_의 데미지를 입혔습니다. ▶ ", muuo.monster_Name, player_Damage);
                    Console.ResetColor();
                    muuo.Monster_DecreaseHP(player_Damage);
                    Console.WriteLine();
                    playerTurn_Count = playerTurn_Count + 4;

                    // 플레이어 턴 종료 후
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", muuo.monster_Name, muuo.monster_Hp);

                    // 몬스터의 체력이 0이 되었을 때, 전투 승리
                    if (muuo.monster_Hp <= 0)
                    {
                        Console.SetCursorPosition(28, 13 + enemyTurn_Count);
                        Console.WriteLine("[{0}]와의 전투에서 승리했습니다.", muuo.monster_Name);

                        ConsoleKey quit_ = ConsoleKey.A;

                        while (quit_ != ConsoleKey.Q)
                        {
                            Console.SetCursorPosition(33, 15 + enemyTurn_Count);
                            Console.WriteLine("Q를 눌러 돌아가세요.");
                            quit_ = Console.ReadKey(true).Key;
                        }
                        break;
                    }

                    // 몬스터 턴
                    Console.SetCursorPosition(0, 11 + enemyTurn_Count);
                    Thread.Sleep(250);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t ◀ [{0}]이/가 당신에게 _{1}_의 데미지를 입혔습니다.", muuo.monster_Name, muuo.monster_Damage);
                    Console.ResetColor();
                    player_Hp = player_Hp - muuo.monster_Damage;
                    enemyTurn_Count = enemyTurn_Count + 4;

                    // 몬스터 턴 종료 후
                    // 체력 표시
                    Console.SetCursorPosition(10, 5);
                    Console.Write("플레이어 현재 체력 : {0}", player_Hp);
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("[{0}]의 현재 체력 : {1:D3}", muuo.monster_Name, muuo.monster_Hp);


                    // 플레이어의 체력이 0이 되었을 때, 전투 패배
                    if (player_Hp <= 0)
                    {
                        Console.WriteLine("[{0}]와의 전투에서 패배했습니다.", muuo.monster_Name);
                        Console.WriteLine("당신의 남은 체력이 0이기 때문에 게임 종료됩니다.");
                        break;
                    }
                }
            }

            victory_Battle++;
            quest_Battle++;
        }

        // 퀘스트 내용
        static void Quest(ref int quest_Battle, ref bool isQuest_ing, ref int player_Hp)
        {
            // 퀘스트 중이라면 대사가 바뀜.
            if (isQuest_ing == true)
            {
                Console.SetCursorPosition(80, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" NPC 낑낑");
                Console.ResetColor();
                Console.SetCursorPosition(80, 2);
                Console.WriteLine(" 안녕하세요! 몬스터 처치는 잘 되가시나요!!");

                if (quest_Battle >= 5)
                {
                    Console.SetCursorPosition(80, 4);
                    Console.WriteLine(" 와! 감사해요. 당분간 몬스터 걱정은 안해도 되겠어요.");
                    Console.SetCursorPosition(80, 6);
                    Console.WriteLine(" [System Message]");
                    Console.SetCursorPosition(80, 7);
                    Console.WriteLine(" [플레이어의 체력이 50 회복 되었습니다.]");
                    player_Hp = player_Hp + 50;

                    // 최대 체력이 500 이상이면
                    if (player_Hp > 500)
                    {
                        player_Hp = 500;
                    }

                    isQuest_ing = false;
                }
            }

            // 퀘스트를 아직 안받았을 때,
            else if (isQuest_ing == false)
            {
                // 퀘스트 중이 아니라면 퀘스트 배틀이 초기화됨.
                quest_Battle = 0;

                Console.SetCursorPosition(80, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" NPC 낑낑");
                Console.ResetColor();
                Console.SetCursorPosition(80, 2);
                Console.WriteLine(" 안녕하세요! 저를 도와 풀숲의 몬스터를 처치해주세요.");
                Console.SetCursorPosition(80, 4);
                Console.WriteLine(" ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(80, 5);
                Console.WriteLine("\t\t  § 퀘스트 내용 §");
                Console.SetCursorPosition(80, 7);
                Console.WriteLine(" §낑낑을 도와 풀숲에 출현하는 몬스터 5마리 처치하기§ ");
                Console.ResetColor();
                Console.SetCursorPosition(80, 9);
                Console.WriteLine(" ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                Console.SetCursorPosition(80, 11);
                Console.WriteLine("\t\tY = 수락 / N = 거절");

                ConsoleKey yesNo = Console.ReadKey(true).Key;

                if (yesNo == ConsoleKey.Y)
                {
                    Console.SetCursorPosition(80, 13);
                    Console.WriteLine(" 저의 부탁을 들어주셔서 감사해요!");
                    isQuest_ing = true;
                }

                else if (yesNo == ConsoleKey.N)
                {
                    Console.SetCursorPosition(80, 13);
                    Console.WriteLine(" 아쉽지만 다음에 꼭 도와주세요!!");
                }
                else { /*pass*/ }
            }
        }
        static void Quest_Start(int quest_Battle)
        {
            if(quest_Battle > 5)
            {
                quest_Battle = 5;
            }

            Console.SetCursorPosition(80, 10);
            Console.WriteLine("\t\t  § 퀘스트 내용 §");
            Console.SetCursorPosition(80, 12);
            Console.WriteLine(" §낑낑을 도와 풀숲에 출현하는 몬스터 5마리 처치하기§ ");
            Console.SetCursorPosition(80, 13);
            Console.WriteLine(" ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
            Console.SetCursorPosition(80, 14);
            Console.WriteLine("\t\t              처치한 몬스터 [ {0} / 5 ] ", quest_Battle);
        }
    }
}





    