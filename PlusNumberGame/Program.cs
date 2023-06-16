using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PlusNumberGame
{
    internal class Program
    {
        const string WALL_SHAPE = "■";
        const string FLOAR_SHAPE = "* ";
        const string GOAL_SHAPE = "@ ";
        const string PLAYER_SHAPE = "& ";
        const string NUMBER_SHAPE = "1 ";
        
        static void Main(string[] args)
        {
            int player_Pos_Y = 1;
            int player_Pos_X = 1;
            int temp_Pos_Y = default;
            int temp_Pos_X = default;
            string temp = default;


            string[,] map = new string[10, 15]; // 초기 맵 사이즈
            string[,] number_Ball = new string[10, 15];  // 숫자 공의 맵 좌표

            // 초기 맵 생성
            Setting_Map(map);

            while (true)
            {

                // 맵 출력
                Console.SetCursorPosition(0, 0);
                Print_Map(map);

                // 플레이어 이동
                Console.WriteLine("\nWASD를 입력해 캐릭터를 움직이세요.");
                Console.WriteLine("\n숫자 왼쪽에서 Q를 입력해 숫자를 차세요!");
                Console.WriteLine("\nR을 입력해 숫자를 생성하세요!");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                ConsoleKey playerMove = keyInfo.Key;

                // A를 눌렀을 때,
                if ((playerMove == ConsoleKey.A) && 
                    (map[player_Pos_Y, player_Pos_X - 1] != WALL_SHAPE) && 
                    (map[player_Pos_Y, player_Pos_X - 1] != NUMBER_SHAPE))
                {
                    // 플레이어 위치 pos_x - 1 하기
                    temp = map[player_Pos_Y, player_Pos_X];
                    map[player_Pos_Y, player_Pos_X] = map[player_Pos_Y, player_Pos_X - 1];
                    map[player_Pos_Y, player_Pos_X - 1] = temp;
                    player_Pos_X--;
                }

                // D를 눌렀을 때,
                else if ((playerMove == ConsoleKey.D) && 
                    (map[player_Pos_Y, player_Pos_X + 1] != WALL_SHAPE) &&
                    (map[player_Pos_Y, player_Pos_X + 1] != NUMBER_SHAPE))
                {
                    // 플레이어 위치 pos_x + 1 하기
                    temp = map[player_Pos_Y, player_Pos_X];
                    map[player_Pos_Y, player_Pos_X] = map[player_Pos_Y, player_Pos_X + 1];
                    map[player_Pos_Y, player_Pos_X + 1] = temp;
                    player_Pos_X++;
                }

                // W를 눌렀을 때,
                else if ((playerMove == ConsoleKey.W) && 
                    (map[player_Pos_Y - 1, player_Pos_X] != WALL_SHAPE) &&
                    (map[player_Pos_Y - 1, player_Pos_X] != NUMBER_SHAPE))
                {
                    // 플레이어 위치 pos_y - 1 하기
                    temp = map[player_Pos_Y, player_Pos_X];
                    map[player_Pos_Y, player_Pos_X] = map[player_Pos_Y - 1, player_Pos_X];
                    map[player_Pos_Y - 1, player_Pos_X] = temp;
                    player_Pos_Y--;
                }

                // S를 눌렀을 때,
                else if ((playerMove == ConsoleKey.S) && 
                    (map[player_Pos_Y + 1, player_Pos_X] != WALL_SHAPE) &&
                    (map[player_Pos_Y + 1, player_Pos_X] != NUMBER_SHAPE))
                {
                    // 플레이어 위치 pos_y + 1 하기
                    temp = map[player_Pos_Y, player_Pos_X];
                    map[player_Pos_Y, player_Pos_X] = map[player_Pos_Y + 1, player_Pos_X];
                    map[player_Pos_Y + 1, player_Pos_X] = temp;
                    player_Pos_Y++;
                }

                // Q를 눌렀을 때,
                if((playerMove == ConsoleKey.Q) && (map[player_Pos_Y, player_Pos_X + 1] == NUMBER_SHAPE))
                {
                    // 숫자 공의 좌표 새로 지정
                    temp_Pos_Y = player_Pos_Y;
                    temp_Pos_X = player_Pos_X;

                    // 골라인에 도착할 때 까지,
                    while(true)
                    {
                        // 이동하다가 골라인에 닿으면
                        if (map[temp_Pos_Y, temp_Pos_X + 2] == GOAL_SHAPE)
                        {
                            // 맵 좌표에 새로 넘버를 넣음
                            map[temp_Pos_Y, temp_Pos_X + 1] = FLOAR_SHAPE;
                            map[temp_Pos_Y, temp_Pos_X + 2] = NUMBER_SHAPE;
                            break;
                        }

                        if (int.TryParse(map[temp_Pos_Y, temp_Pos_X + 1], out int number2) &&
                            int.TryParse(map[temp_Pos_Y, temp_Pos_X + 2], out int number3))
                        {
                            // 만난 위치의 숫자들을 바닥 모양으로 초기화
                            map[temp_Pos_Y, temp_Pos_X + 1] = FLOAR_SHAPE;
                            map[temp_Pos_Y, temp_Pos_X + 2] = FLOAR_SHAPE;

                            // 더한 숫자로 스왑
                            int sum = number2 + number3 - 1;

                            // 골라인에 숫자가 있다면,
                            if (int.TryParse(map[temp_Pos_Y, 13], out int number_))
                            {
                                number_ += sum;
                                map[temp_Pos_Y, 13] = number_.ToString();
                            }
                            // 골라인에 숫자가 없다면,
                            else
                            {
                                map[temp_Pos_Y, 13] = sum.ToString();
                            }
                        }

                        // 골라인에 이미 숫자가 있다면
                        // 골라인에 있는 string 넘버를 int로 변경한 다음 int 1을 더한 다음 다시 string으로 변경하기
                        if (int.TryParse(map[temp_Pos_Y, temp_Pos_X + 2], out int number))
                        {
                            // 숫자면
                            // 숫자를 1 증가시키고
                            number++;
                            // 다시 string으로 형변환해서 배열에 대입
                            map[temp_Pos_Y, temp_Pos_X + 1] = FLOAR_SHAPE;
                            map[temp_Pos_Y, temp_Pos_X + 2] = number.ToString() + " ";
                            break;
                        }

                        // 계속 왼쪽으로 이동함
                        temp = map[temp_Pos_Y, temp_Pos_X + 1];
                        map[temp_Pos_Y, temp_Pos_X + 1] = map[temp_Pos_Y, temp_Pos_X + 2];
                        map[temp_Pos_Y, temp_Pos_X + 2] = temp;
                        temp_Pos_X++;

                    }
                }
                // R을 눌렀을 때,
                if (playerMove == ConsoleKey.R)
                {
                    Random_Postion(map);
                }       
            }
        }

        // 초기 맵 세팅하는 함수
        static void Setting_Map(string[,] map)
        {
            //string wall_Shape = "■";
            //string floar_Shape = "* ";
            //string goal_Shape = "@ ";
            //string player_Shape = "& ";

            // 2차원 맵 세팅하기
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    // 벽 세팅
                    if (((y == 0) || (x == 0)) || ((y == 9) || (x == 14)))
                    {
                        map[y, x] = WALL_SHAPE;
                        continue;
                    }
                    // 골 라인 세팅
                    else if (((y != 0) && (y != 9)) && (x == 13))
                    {
                        map[y, x] = GOAL_SHAPE;
                        continue;
                    }
                    // 플레이어 위치 세팅
                    if ((y == 1) && (x == 1))
                    {
                        map[y, x] = PLAYER_SHAPE;
                        continue;
                    }
                    // 바닥 세팅
                    map[y, x] = FLOAR_SHAPE;
                }
            }
            // 랜덤 좌표에 1이 나오게끔
            Random_Postion(map);
        }

        // 맵 출력하는 함수
        static void Print_Map(string[,] map)
        {
            // 2차원 맵 출력하기
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    // 출력할 때 x 인덱스가 0으로 돌아올 때 마다 줄넘김하기
                    if (x == 0)
                    {
                        Console.WriteLine();
                    }

                    // 출력할 때 색 변경하기
                    // 벽 색상 변경
                    if (map[y, x] == WALL_SHAPE)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }
                    // 골라인 색상 변경
                    else if (map[y, x] == GOAL_SHAPE)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }
                    // 플레이어 색상 변경
                    else if (map[y, x] == PLAYER_SHAPE)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }
                    // 숫자 1 색상 변경
                    else if (map[y, x] == NUMBER_SHAPE)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                        continue;
                    }   // 출력할 때 색 변경하기


                    Console.Write(map[y, x]);
                }
            }
        }

        // 랜덤한 세개의 좌표를 설정하는 함수
        static void Random_Postion(string[,] map)
        {
            Random random = new Random();

            // 바닥에 랜덤한 숫자 생성
            for (int i = 0; i < 3; i++)
            {
                // 랜덤 범위를 2, 8로 한 이유는 1이 나오는 위치가 벽 인덱스 -1이 되게끔 하기 위함.
                int randomy_ = random.Next(2, 8);
                int randomX_ = random.Next(2, 8);
                if (map[randomy_, randomX_] != PLAYER_SHAPE)
                {
                    map[randomy_, randomX_] = NUMBER_SHAPE;
                }
                else
                {
                    i--;
                }
                
            }
        }
    }
}
