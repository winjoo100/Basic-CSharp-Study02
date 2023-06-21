using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidEnemy_Game
{
    public class Program
    {
        // Main 함수
        static void Main(string[] args)
        {
            // 변수 선언
            int mapSize = 25;
            int User_Position_Y = mapSize / 2;
            int User_Position_X = mapSize / 2;
            string[,] map = new string[mapSize, mapSize];

            List<int> enemyPostion_Y = new List<int>();
            List<int> enemyPostion_X = new List<int>();

            int moveCount = 20;
            int enemyCount = 0;
            int coinCount = 0;

            bool gameEnd = true;
            // 변수 선언 end

            // 초기 맵 세팅
            Set_Map(ref map, mapSize);
            Set_maze(ref map, mapSize);

            // 게임 시작
            while (gameEnd)
            {
                Create_Coin(ref map, mapSize, moveCount);
                Make_Enemy(ref map, mapSize, ref enemyPostion_Y, ref enemyPostion_X, ref moveCount, ref enemyCount);
                Print_Map(map, mapSize);

                Console.WriteLine("현재 획득한 코인! [{0}]", coinCount);
                Console.WriteLine("플레이어의 좌표 {0:D2},{1:D2}", User_Position_Y, User_Position_X);
                for (int i = 0; i < enemyCount; i++)
                {
                    Console.WriteLine("enemy{0} 의 좌표 {1:D2},{2:D2}", i, enemyPostion_Y[i], enemyPostion_X[i]);
                }

                Move_EnemyPos(ref map, ref User_Position_Y, ref User_Position_X, ref enemyPostion_Y, ref enemyPostion_X, ref enemyCount);

                for (int i = 0; i < enemyCount; i++)
                {
                    if ((User_Position_Y == enemyPostion_Y[i]) && (User_Position_X == enemyPostion_X[i]))
                    {
                        Console.Clear();
                        Print_Map(map, mapSize);
                        Console.WriteLine("앗! 적과 부딪혔습니다...");
                        Console.WriteLine("Game End!!");
                        gameEnd = false;
                        break;
                    }
                }

                User_Move(ref map, ref User_Position_Y, ref User_Position_X, mapSize, ref coinCount, ref enemyPostion_Y, ref enemyPostion_X);

                //Console.WriteLine("{0:D2}", moveCount);
                moveCount++;
            }
        }
                // Main 함수 end

        // 초기 맵 세팅
        static void Set_Map(ref string[,] map, int mapSize)
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    // 벽 생성
                    if (((y == 0) || (y == mapSize - 1)) || ((x == 0) || (x == mapSize - 1)))
                    {
                        map[y, x] = "■";
                        continue;
                    }

                    // 플레이어 생성
                    if ((y == mapSize / 2) && (x == mapSize / 2))
                    {
                        map[y, x] = "& ";
                        continue;
                    }

                    // 바닥 생성
                    map[y, x] = "* ";
                }
            }

            //// 포탈 생성
            //for (int y = 0; y < mapSize; y++)
            //{
            //    for (int x = 0; x < mapSize; x++)
            //    {
            //        // 왼쪽
            //        if ((y == (mapSize / 2) + 1) && (x == 0))
            //        {
            //            map[y, x] = "▣";
            //            map[y - 1, x] = "▣";
            //            map[y - 2, x] = "▣";
            //            continue;
            //        }
            //        // 오른쪽
            //        if ((y == (mapSize / 2) + 1) && (x == mapSize - 1))
            //        {
            //            map[y, x] = "▣";
            //            map[y - 1, x] = "▣";
            //            map[y - 2, x] = "▣";
            //            continue;
            //        }
            //        // 위쪽
            //        if ((y == 0) && (x == (mapSize / 2) + 1))
            //        {
            //            map[y, x] = "▣";
            //            map[y, x - 1] = "▣";
            //            map[y, x - 2] = "▣";
            //            continue;
            //        }
            //        // 아래쪽
            //        if ((y == mapSize - 1) && (x == (mapSize / 2) + 1))
            //        {
            //            map[y, x] = "▣";
            //            map[y, x - 1] = "▣";
            //            map[y, x - 2] = "▣";
            //            continue;
            //        }
            //    }
            //}           // 포탈 생성 end
        }
                // 초기 맵 세팅 end

        // 코인 생성
        static void Create_Coin(ref string[,] map, int mapSize , int moveCount)
        {
            bool isCoinCreated = true;
            Random random = new Random();
            if (moveCount > 20)
            {
                while (isCoinCreated)
                {
                    int randomY_ = random.Next(1, mapSize - 2);
                    int randomX_ = random.Next(1, mapSize - 2);

                    if (map[randomY_, randomX_] == "* ")
                    {
                        map[randomY_, randomX_] = "◈";
                        isCoinCreated = false;
                    }

                    else {/*pass*/ }
                }
                
            }
            else {/*pass*/ }

        }
                // 코인 생성 end

        // 미로 돌 생성
        static void Set_maze(ref string[,] map, int mapSize)
        {

            Random random = new Random();
            for (int i = 0; i < mapSize * 2; i++)
            {
                int randomY_ = random.Next(1, mapSize - 2);
                int randomX_ = random.Next(1, mapSize - 2);

                // 별이 하나라도 있어야함
                if ((map[randomY_, randomX_ + 1] == "* ") ||
                        (map[randomY_, randomX_ - 1] == "* ") ||
                        (map[randomY_ + 1, randomX_] == "* ") ||
                        (map[randomY_ - 1, randomX_] == "* "))
                // 플레이어의 위치에 생성되면 안됨.
                {
                    if (map[randomY_, randomX_] != "& ")
                    {
                        map[randomY_, randomX_] = "# ";
                    }
                    else
                    {
                        i--;
                    }
                }

                else
                {
                    i--;
                }
            }
        }
                // 미로 돌 생성 end

        // 적 생성
        static void Make_Enemy(ref string[,] map, int mapSize, ref List<int> enemyPostion_Y, ref List<int> enemyPostion_X, ref int moveCount, ref int enemyCount)
        {
            bool create_Enemy = true;

            if ( moveCount > 20)
            {
                while (create_Enemy)
                {
                    Random random = new Random();
                    int randomY_ = random.Next(2, mapSize - 2);
                    int randomX_ = random.Next(2, mapSize - 2);

                    // 생성되는 위치가 별이여야함
                    if (map[randomY_, randomX_] == "* ")
                    {
                        map[randomY_, randomX_] = "@ ";

                        enemyPostion_Y.Add(randomY_);
                        enemyPostion_X.Add(randomX_);

                        enemyCount++;
                        moveCount = 0;
                        create_Enemy = false;
                    }

                    else { }
                }
            }
        }
                // 적 생성 end

        // 맵 출력
        static void Print_Map(string[,] map, int mapSize)
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
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
                    if (map[y, x] == "& ")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 포탈 색상 변경
                    if (map[y, x] == "▣")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 미로 돌 색상 변경
                    if (map[y, x] == "# ")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 코인 색상 변경
                    if (map[y, x] == "+ ")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 에너미 색상 변경
                    if (map[y, x] == "@ ")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    Console.Write(map[y, x]);
                    // 벽 삭제
                    //if (map[y, x] == "* ")
                    //{
                    //    map[y, x] = "　";
                    //    Console.Write(map[y, x]);
                    //    continue;
                    //}
                }
                Console.WriteLine();
            }
        }
                // 맵 출력 end

        // 플레이어 이동
        static void User_Move(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize, ref int coinCount, ref List<int> enemyPostion_Y, ref List<int> enemyPostion_X)
        {
            ConsoleKey user_Move = Console.ReadKey(true).Key;

            // A 입력 (왼쪽이동)
            if (user_Move == ConsoleKey.A)
            {
                Swap_Move_A(ref map, ref User_Position_Y, ref User_Position_X, mapSize, ref coinCount, ref enemyPostion_Y, ref enemyPostion_X);
            }

            // D 입력 (오른쪽이동)
            else if (user_Move == ConsoleKey.D)
            {
                Swap_Move_D(ref map, ref User_Position_Y, ref User_Position_X, mapSize, ref coinCount, ref enemyPostion_Y, ref enemyPostion_X);
            }

            // W 입력 (위쪽이동)
            else if (user_Move == ConsoleKey.W)
            {
                Swap_Move_W(ref map, ref User_Position_Y, ref User_Position_X, mapSize, ref coinCount, ref enemyPostion_Y, ref enemyPostion_X);
            }

            // S 입력 (아래쪽이동)
            else if (user_Move == ConsoleKey.S)
            {
                Swap_Move_S(ref map, ref User_Position_Y, ref User_Position_X, mapSize, ref coinCount, ref enemyPostion_Y, ref enemyPostion_X);
            }
        }   
                // 플레이어 이동 end

        // 플레이어 이동 시 스왑
        #region
        // A 이동
        static void Swap_Move_A(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize, ref int coinCount, ref List<int> enemyPostion_Y, ref List<int> enemyPostion_X)
        {
            // 벽이 아닐 경우 && 돌이 아닐 경우
            if ((map[User_Position_Y, User_Position_X - 1] != "■") && (map[User_Position_Y, User_Position_X - 1] != "# "))
            {
                // 포탈일 때
                if (map[User_Position_Y, User_Position_X - 1] == "▣")
                {
                    // 플레이어 위치 반대편 포탈 앞으로 스왑
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y, mapSize - 2];
                    map[User_Position_Y, mapSize - 2] = temp_;

                    User_Position_X = mapSize - 2;
                    //Reset_maze(ref map, mapSize);
                    //Set_maze(ref map, mapSize);
                }

                // 코인 일 때,
                else if(map[User_Position_Y, User_Position_X - 1] == "◈")
                {
                    map[User_Position_Y, User_Position_X - 1] = "* ";

                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y, User_Position_X - 1];
                    map[User_Position_Y, User_Position_X - 1] = temp_;
                    User_Position_X--;

                    coinCount++;
                }

                // 포탈이 아닐 때,
                else
                {
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y, User_Position_X - 1];
                    map[User_Position_Y, User_Position_X - 1] = temp_;
                    User_Position_X--;
                }
            }

            else { }
        }
        // D 이동
        static void Swap_Move_D(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize, ref int coinCount, ref List<int> enemyPostion_Y, ref List<int> enemyPostion_X)
        {
            // 벽이 아닐 경우 && 돌이 아닐 경우
            if ((map[User_Position_Y, User_Position_X + 1] != "■") && (map[User_Position_Y, User_Position_X + 1] != "# "))
            {
                // 포탈일 때
                if (map[User_Position_Y, User_Position_X + 1] == "▣")
                {
                    // 플레이어 위치 반대편 포탈 앞으로 스왑
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y, 1];
                    map[User_Position_Y, 1] = temp_;

                    User_Position_X = 1;
                    //Reset_maze(ref map, mapSize);
                    //Set_maze(ref map, mapSize);
                }

                // 코인 일 때,
                else if (map[User_Position_Y, User_Position_X + 1] == "◈")
                {
                    map[User_Position_Y, User_Position_X + 1] = "* ";

                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y, User_Position_X + 1];
                    map[User_Position_Y, User_Position_X + 1] = temp_;
                    User_Position_X++;

                    coinCount++;
                }

                // 포탈이 아닐 때,
                else
                {
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y, User_Position_X + 1];
                    map[User_Position_Y, User_Position_X + 1] = temp_;
                    User_Position_X++;
                }
            }
            else { }
        }
        // W 이동
        static void Swap_Move_W(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize, ref int coinCount, ref List<int> enemyPostion_Y, ref List<int> enemyPostion_X)
        {
            // 벽이 아닐 경우 && 돌이 아닐 경우
            if ((map[User_Position_Y - 1, User_Position_X] != "■") && (map[User_Position_Y - 1, User_Position_X] != "# "))
            {
                // 포탈일 때
                if (map[User_Position_Y - 1, User_Position_X] == "▣")
                {
                    // 플레이어 위치 반대편 포탈 앞으로 스왑
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[mapSize - 2, User_Position_X];
                    map[mapSize - 2, User_Position_X] = temp_;

                    User_Position_Y = mapSize - 2;
                    //Reset_maze(ref map, mapSize);
                    //Set_maze(ref map, mapSize);
                }

                // 코인 일 때,
                else if (map[User_Position_Y - 1, User_Position_X] == "◈")
                {
                    map[User_Position_Y - 1, User_Position_X] = "* ";

                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y - 1, User_Position_X];
                    map[User_Position_Y - 1, User_Position_X] = temp_;
                    User_Position_Y--;

                    coinCount++;
                }

                // 포탈이 아닐 때,
                else
                {
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y - 1, User_Position_X];
                    map[User_Position_Y - 1, User_Position_X] = temp_;
                    User_Position_Y--;
                }
            }
            else { }
        }
        // S 이동
        static void Swap_Move_S(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize, ref int coinCount, ref List<int> enemyPostion_Y, ref List<int> enemyPostion_X)
        {
            // 벽이 아닐 경우 && 돌이 아닐 경우
            if ((map[User_Position_Y + 1, User_Position_X] != "■") && (map[User_Position_Y + 1, User_Position_X] != "# "))
            {
                // 포탈일 때
                if (map[User_Position_Y + 1, User_Position_X] == "▣")
                {
                    // 플레이어 위치 반대편 포탈 앞으로 스왑
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[1, User_Position_X];
                    map[1, User_Position_X] = temp_;

                    User_Position_Y = 1;
                    //Reset_maze(ref map, mapSize);
                    //Set_maze(ref map, mapSize);
                }

                // 코인 일 때,
                else if (map[User_Position_Y + 1, User_Position_X] == "◈")
                {
                    map[User_Position_Y + 1, User_Position_X] = "* ";

                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y + 1, User_Position_X];
                    map[User_Position_Y + 1, User_Position_X] = temp_;
                    User_Position_Y++;

                    coinCount++;
                }

                // 포탈이 아닐 때,
                else
                {
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y + 1, User_Position_X];
                    map[User_Position_Y + 1, User_Position_X] = temp_;
                    User_Position_Y++;
                }
            }
            else { }
        }
        #endregion      // 플레이어 이동 시 스왑 end

        // 에너미가 플레이어를 따라오는 로직
        static void Move_EnemyPos(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, ref List<int> enemyPostion_Y, ref List<int> enemyPostion_X, ref int enemyCount)
        {
            Random random = new Random();
            int logicStart = random.Next(1, 5);
            for (int i = 0; i < enemyCount; i++)
            {
                if (logicStart == 1)
                {
                    // 에너미 x 위치가 플레이어 x 위치보다 클 경우
                    if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, w쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, s쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, D쪽이 벽이 아니면 
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // 에너미 x 위치가 플레이어 x 위치보다 작을 경우
                    else if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // 에너미 Y 위치가 플레이어 Y 위치보다 클 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }
                    // W 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }
                    // D 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // 에너미 Y 위치가 플레이어 Y 위치보다 작을 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }
                }
                if (logicStart == 2)
                {
                    // 에너미 x 위치가 플레이어 x 위치보다 작을 경우
                    if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // 에너미 Y 위치가 플레이어 Y 위치보다 클 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }
                    // W 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }
                    // D 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // 에너미 Y 위치가 플레이어 Y 위치보다 작을 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // 에너미 x 위치가 플레이어 x 위치보다 클 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, w쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, s쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, D쪽이 벽이 아니면 
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }
                }
                if (logicStart == 3)
                {
                    // 에너미 Y 위치가 플레이어 Y 위치보다 클 경우
                    if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }
                    // W 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }
                    // D 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // 에너미 Y 위치가 플레이어 Y 위치보다 작을 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }
                    // 에너미 x 위치가 플레이어 x 위치보다 클 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, w쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, s쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, D쪽이 벽이 아니면 
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // 에너미 x 위치가 플레이어 x 위치보다 작을 경우
                    else if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }
                }
                if (logicStart == 4)
                {
                    // 에너미 Y 위치가 플레이어 Y 위치보다 작을 경우
                    if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] < User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }
                    // 에너미 x 위치가 플레이어 x 위치보다 클 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, w쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, s쪽이 벽이 아니면
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, D쪽이 벽이 아니면 
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // 에너미 x 위치가 플레이어 x 위치보다 작을 경우
                    else if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }

                    // D 쪽이 벽이고, W 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] < User_Position_X) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }

                    // W 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }

                    // S 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_X[i] > User_Position_X) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // 에너미 Y 위치가 플레이어 Y 위치보다 클 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] != "■"))
                    {
                        // W 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] - 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] - 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]--;
                    }
                    // W 쪽이 벽이고, A 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] - 1] != "■"))
                    {
                        // A 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] - 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] - 1] = temp_;
                        enemyPostion_X[i]--;
                    }

                    // A 쪽이 벽이고, D 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "# ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "@ ") && (map[enemyPostion_Y[i], enemyPostion_X[i] + 1] != "■"))
                    {
                        // D 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i], enemyPostion_X[i] + 1];
                        map[enemyPostion_Y[i], enemyPostion_X[i] + 1] = temp_;
                        enemyPostion_X[i]++;
                    }
                    // D 쪽이 벽이고, S 쪽이 벽이 아닐 경우
                    else if ((enemyPostion_Y[i] > User_Position_Y) && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "# ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "@ ") && (map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] != "■"))
                    {
                        // S 쪽으로 이동
                        string temp_ = map[enemyPostion_Y[i], enemyPostion_X[i]];
                        map[enemyPostion_Y[i], enemyPostion_X[i]] = map[enemyPostion_Y[i] + 1, enemyPostion_X[i]];
                        map[enemyPostion_Y[i] + 1, enemyPostion_X[i]] = temp_;
                        enemyPostion_Y[i]++;
                    }
                }
            }
        }
                // 에너미가 플레이어를 따라오는 로직 end





        // 미로 돌 초기화
        //static void Reset_maze(ref string[,] map, int mapSize)
        //{
        //    for (int y = 0; y < mapSize; y++)
        //    {
        //        for (int x = 0; x < mapSize; x++)
        //        {
        //            if (map[x, y] == "# ")
        //            {
        //                map[x, y] = "* ";
        //            }
        //        }
        //    }
        //}
                // 미로 돌 초기화 end
    }
}
