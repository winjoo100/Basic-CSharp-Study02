using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCoin_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 변수 선언
            int mapsize;                                            // 맵 크기
            string userDecideMapSize;                               // 유저가 맵 크기를 입력하기 위한 변수
            char firstInput;                                        // 입력받은 값 중에 첫 번째 입력값만 받는 변수
            //string playerMove;                                      // 플레이어가 asdw를 입력해서 움직이게 하기 위한 변수
            string temp;
            int verticalMove;                                       // 세로 이동을 위한 변수
            int horizonMove;                                        // 가로 이동을 위한 변수
            int moveCount = 0;
            int gameScore = 0;

            // 플레이어의 입력을 받고 맵 크기 정하기
            Console.WriteLine("맵의 크기 = x * x ");
            Console.Write("맵의 크기를 입력하세요 : ");
            userDecideMapSize = Console.ReadLine();                 // ReadLine을 통해 유저로부터 값을 입력받기
            mapsize = int.Parse(userDecideMapSize) + 2;             // 앞글자를 int형으로 변경하기
            Console.WriteLine(mapsize);

            // 맵 출력
            string[,] map = new string[mapsize, mapsize];           // 맵 크기를 정하기 위한 변수
            StartMapSetting(map, mapsize);
            

            verticalMove = (mapsize / 2);
            horizonMove = (mapsize / 2);


            while (true)
            {
                Console.Clear();
                Console.WriteLine("verticalMove {0} / horizonMove {0}",verticalMove, horizonMove);
                Console.WriteLine("총 움직인 횟수 : {0}", moveCount);
                Console.WriteLine("플레이어의 현재 점수 : {0}", gameScore);
                Map(map, mapsize);

                // 플레이어가 a,s,d,w로 이동하기
                ConsoleKey playerMove = Console.ReadKey(true).Key;
                switch (playerMove)
                {
                    case ConsoleKey.A:
                        {
                            if (horizonMove > 1)
                            {
                                if (map[verticalMove, horizonMove - 1] == "# ")  // 돌을 밀 때
                                {
                                    if (!(map[verticalMove, horizonMove - 2] == "# ") || (map[verticalMove, horizonMove - 2] != map[verticalMove, 1]))
                                    {
                                        temp = map[verticalMove, horizonMove - 1];
                                        map[verticalMove, horizonMove - 1] = map[verticalMove, horizonMove - 2];
                                        map[verticalMove, horizonMove - 2] = temp;
                                    }
                                    break;
                                }

                                if (map[verticalMove, horizonMove - 1] == "@ ")
                                {
                                    map[verticalMove, horizonMove - 1] = "* ";
                                    gameScore += 1;

                                    temp = map[verticalMove, horizonMove];
                                    map[verticalMove, horizonMove] = map[verticalMove, horizonMove - 1];
                                    map[verticalMove, horizonMove - 1] = temp;
                                    --horizonMove;
                                    moveCount += 1;
                                }
                                else
                                {
                                    temp = map[verticalMove, horizonMove];
                                    map[verticalMove, horizonMove] = map[verticalMove, horizonMove - 1];
                                    map[verticalMove, horizonMove - 1] = temp;
                                    --horizonMove;
                                    moveCount += 1;
                                }
                            }
                            break;
                        }

                    case ConsoleKey.D:
                        {
                            if (playerMove == ConsoleKey.D)
                            {
                                if (horizonMove < mapsize - 2)
                                {
                                    if (map[verticalMove, horizonMove + 1] == "# ")  // 돌을 밀 때
                                    {
                                        if ((map[verticalMove, horizonMove + 2] != "# ") || (horizonMove < 7))
                                        {
                                            temp = map[verticalMove, horizonMove + 1];
                                            map[verticalMove, horizonMove + 1] = map[verticalMove, horizonMove + 2];
                                            map[verticalMove, horizonMove + 2] = temp;
                                        }
                                        continue;
                                    }

                                    if (map[verticalMove, horizonMove + 1] == "@ ")
                                    {
                                        map[verticalMove, horizonMove + 1] = "* ";
                                        gameScore += 1;

                                        temp = map[verticalMove, horizonMove];
                                        map[verticalMove, horizonMove] = map[verticalMove, horizonMove + 1];
                                        map[verticalMove, horizonMove + 1] = temp;
                                        horizonMove++;
                                        moveCount += 1;
                                    }
                                    else
                                    {
                                        temp = map[verticalMove, horizonMove];
                                        map[verticalMove, horizonMove] = map[verticalMove, horizonMove + 1];
                                        map[verticalMove, horizonMove + 1] = temp;
                                        horizonMove++;
                                        moveCount += 1;
                                    }
                                }
                            }
                            break;
                        }
                    case ConsoleKey.W:
                    {
                        if (playerMove == ConsoleKey.W)
                        {
                            if (verticalMove > 1)
                            {
                                if (map[verticalMove - 1, horizonMove] == "# ")  // 돌을 밀 때
                                {
                                    if ((map[verticalMove - 2, horizonMove] != "# ") && (map[verticalMove - 2, horizonMove] != map[0, horizonMove]))
                                    {
                                        temp = map[verticalMove - 1, horizonMove];
                                        map[verticalMove - 1, horizonMove] = map[verticalMove - 2, horizonMove];
                                        map[verticalMove - 2, horizonMove] = temp;
                                    }
                                    continue;
                                }

                                if (map[verticalMove - 1, horizonMove] == "@ ")
                                {
                                    map[verticalMove - 1, horizonMove] = "* ";
                                    gameScore += 1;

                                    temp = map[verticalMove, horizonMove];
                                    map[verticalMove, horizonMove] = map[verticalMove - 1, horizonMove];
                                    map[verticalMove - 1, horizonMove] = temp;
                                    verticalMove--;
                                    moveCount += 1;
                                }
                                else
                                {
                                    temp = map[verticalMove, horizonMove];
                                    map[verticalMove, horizonMove] = map[verticalMove - 1, horizonMove];
                                    map[verticalMove - 1, horizonMove] = temp;
                                    verticalMove--;
                                    moveCount += 1;
                                }
                            }
                        }
                        break;
                    }
                    case ConsoleKey.S:
                        if (playerMove == ConsoleKey.S)
                        {
                            if (verticalMove < mapsize - 2)
                            {
                                if (map[verticalMove + 1, horizonMove] == "# ")  // 돌을 밀 때
                                {
                                    if ((map[verticalMove + 2, horizonMove] != "# ") || (map[verticalMove + 2, horizonMove] != map[mapsize, horizonMove]))
                                    {
                                        temp = map[verticalMove + 1, horizonMove];
                                        map[verticalMove + 1, horizonMove] = map[verticalMove + 2, horizonMove];
                                        map[verticalMove + 2, horizonMove] = temp;
                                    }
                                    continue;
                                }

                                if (map[verticalMove + 1, horizonMove] == "@ ")
                                {
                                    map[verticalMove + 1, horizonMove] = "* ";
                                    gameScore += 1;

                                    temp = map[verticalMove, horizonMove];
                                    map[verticalMove, horizonMove] = map[verticalMove + 1, horizonMove];
                                    map[verticalMove + 1, horizonMove] = temp;
                                    verticalMove++;
                                    moveCount += 1;
                                }
                                else
                                {
                                    temp = map[verticalMove, horizonMove];
                                    map[verticalMove, horizonMove] = map[verticalMove + 1, horizonMove];
                                    map[verticalMove + 1, horizonMove] = temp;
                                    verticalMove++;
                                    moveCount += 1;
                                }
                            }
                        }
                        break;
                }           
                    
                // 3번 이동하면 무작위의 곳에 코인이 생김 (플레이어나 벽이 있는 곳에는 생기지 않게 하기)
                if (moveCount % 3 == 0)
                {
                    restart:
                    Random random = new Random();
                    int x_ = random.Next(0, mapsize);
                    int y_ = random.Next(0, mapsize);

                    if (map[x_, y_] == "* ")
                    {
                        map[x_, y_] = "@ ";
                    }
                    else
                    {
                        goto restart;
                    }
                }
            }
        }       // Main()

        static void StartMapSetting(string[,] map, int mapsize)
        {
            // 벽만들기
            for (int i = 0; i < mapsize; i++)
            {
                for (int j = 0; j < mapsize; j++)
                {
                    map[i, j] = "■";
                }
            }

            Console.Clear();
            for (int y = 1; y < mapsize - 1; y++)
            {
                for (int x = 1; x < mapsize - 1; x++)
                {
                    if ((x == (mapsize / 2)) && (y == (mapsize / 2)))   // 맵 가운데에 플레이어의 위치 찍기
                    {
                        map[y, x] = "& ";
                    }
                    else
                    {
                        map[y, x] = "* ";                           // 2차원 배열 * 찍기
                    }
                }
            }

            // 돌만들기
            int stone = 0;
            int num1, num2;
            while (stone < mapsize)
            {
                Random random = new Random();
                num1 = random.Next(1, mapsize - 1);
                num2 = random.Next(1, mapsize - 1);

                if (map[num1, num2] == "* ")
                {
                    map[num1, num2] = "# ";
                    stone += 1;
                }
            }
            

        }

        static void Map(string[,] map, int mapsize)
        {
            for (int y = 0; y < mapsize; y++)
            {
                for (int x = 0; x < mapsize; x++)
                {
                    if (map[y, x] == "@ ")      // 코인 색깔 다르게하기
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    if (map[y, x] == "& ")      // 플레이어 색깔 다르게하기
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    if (map[y, x] == "# ")      // 플레이어 색깔 다르게하기
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    if ((y == 0) || (y == mapsize - 1) || (x == 0) || (x == mapsize - 1))   // 벽 색깔 다르게 하기
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }

                    Console.Write(map[y, x]);                   // 맵 출력하기
                    
                }
                Console.WriteLine();
                
            }
        }       // Map()

        static void AppearCoin(ref string[,] map, int mapsize)
        {
            restart:
            Random random = new Random();
            int x_ = random.Next(0, mapsize);
            int y_ = random.Next(0, mapsize);

            if(map[x_, y_] == "*")
            {
                map[x_, y_] = "@";
            }
            else
            {
                goto restart;
            }
            

        }

        static void Coin()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⊙");
            Console.WriteLine("");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }       // Coin()
    }
}
