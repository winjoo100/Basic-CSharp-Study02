using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal_Map
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 변수 선언
            int mapSize = 25;
            int User_Position_Y = mapSize / 2;
            int User_Position_X = mapSize / 2;
            string[,] map = new string[mapSize, mapSize];
            // 변수 선언 end

            // 초기 맵 세팅
            Set_Map(ref map, mapSize);
            Set_maze(ref map, mapSize);

            // 게임 시작
            while (true)
            {
                Print_Map(map, mapSize);
                User_Move(ref map, ref User_Position_Y, ref User_Position_X, mapSize);
            }
        }       // Main 함수 end

        // 초기 맵 세팅
        static void Set_Map(ref string[,] map , int mapSize)
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

            // 포탈 생성
            for (int y = 0;y < mapSize;y++)
            {
                for(int x = 0; x < mapSize;x++)
                {
                    // 왼쪽
                    if ((y == (mapSize / 2) + 1) && (x == 0))
                    {
                        map[y, x] = "▣";
                        map[y - 1, x] = "▣";
                        map[y - 2, x] = "▣";
                        continue;
                    }
                    // 오른쪽
                    if ((y == (mapSize / 2) + 1) && (x == mapSize - 1))
                    {
                        map[y, x] = "▣";
                        map[y - 1, x] = "▣";
                        map[y - 2, x] = "▣";
                        continue;
                    }
                    // 위쪽
                    if ((y == 0) && (x == (mapSize / 2) + 1))
                    {
                        map[y, x] = "▣";
                        map[y, x - 1] = "▣";
                        map[y, x - 2] = "▣";
                        continue;
                    }
                    // 아래쪽
                    if ((y == mapSize - 1) && (x == (mapSize / 2) + 1))
                    {
                        map[y, x] = "▣";
                        map[y, x - 1] = "▣";
                        map[y, x - 2] = "▣";
                        continue;
                    }
                }
            }           // 포탈 생성 end
        }           // 초기 맵 세팅 end

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
        }           // 미로 돌 생성 end

        // 미로 돌 초기화
        static void Reset_maze(ref string[,] map, int mapSize)
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    if (map[x, y] == "# ")
                    {
                        map[x, y] = "* ";
                    }
                }
            }
        }

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
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 플레이어 색상 변경
                    if (map[y, x] == "& ")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    // 포탈 색상 변경
                    if (map[y, x] == "▣")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
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

                    Console.Write(map[y, x]);
                }
                Console.WriteLine();
            }
        }           // 맵 출력 end

        // 플레이어 이동
        static void User_Move(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize)
        {
            ConsoleKey user_Move = Console.ReadKey(true).Key;

            // A 입력 (왼쪽이동)
            if(user_Move == ConsoleKey.A)
            {
                Swap_Move_A(ref map, ref User_Position_Y, ref User_Position_X, mapSize);
            }

            // D 입력 (오른쪽이동)
            else if (user_Move == ConsoleKey.D)
            {
                Swap_Move_D(ref map, ref User_Position_Y, ref User_Position_X, mapSize);
            }

            // W 입력 (위쪽이동)
            else if (user_Move == ConsoleKey.W)
            {
                Swap_Move_W(ref map, ref User_Position_Y, ref User_Position_X, mapSize);
            }

            // S 입력 (아래쪽이동)
            else if (user_Move == ConsoleKey.S)
            {
                Swap_Move_S(ref map, ref User_Position_Y, ref User_Position_X, mapSize);
            }
        }

        // 플레이어 이동 시 스왑
        #region

        // A 이동
        static void Swap_Move_A(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize)
        {
            // 벽이 아닐 경우 && 돌이 아닐 경우
            if ((map[User_Position_Y, User_Position_X - 1] != "■") && (map[User_Position_Y, User_Position_X - 1] != "# "))
            {
                // 포탈일 때
                if (map[User_Position_Y, User_Position_X - 1] == "▣")
                {
                    // 플레이어 위치 반대편 포탈 앞으로 스왑
                    string temp_ = map[User_Position_Y, User_Position_X];
                    map[User_Position_Y, User_Position_X] = map[User_Position_Y,mapSize - 2];
                    map[User_Position_Y, mapSize - 2] = temp_;

                    User_Position_X = mapSize - 2;
                    Reset_maze(ref map, mapSize);
                    Set_maze(ref map, mapSize);
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
        static void Swap_Move_D(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize)
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
                    Reset_maze(ref map, mapSize);
                    Set_maze(ref map, mapSize);
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
        static void Swap_Move_W(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize)
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
                    Reset_maze(ref map, mapSize);
                    Set_maze(ref map, mapSize);
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
        static void Swap_Move_S(ref string[,] map, ref int User_Position_Y, ref int User_Position_X, int mapSize)
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
                    Reset_maze(ref map, mapSize);
                    Set_maze(ref map, mapSize);
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

        #endregion
    }
}
