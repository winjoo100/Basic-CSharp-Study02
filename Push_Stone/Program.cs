using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Push_Stone
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int score = 0;              // 성공횟수!

            string userDecideMapSize;   // 맵의 크기를 유저가 정하기 위한 변수
            int mapsize;                // 맵 크기를 저장하는 변수
            int moveCount = 0;          // 플레이어의 이동 횟수 체크하는 변수

            int verticalMove;           // 플레이어의 세로 이동을 위한 변수
            int horizonMove;            // 플레이어의 가로 이동을 위한 변수

            int tempVerticalMove;
            int tempHorizonMove;

            string temp;           // 스왑을 위한 변수

            // 플레이어의 입력을 받고 맵 크기 정하기
            Console.WriteLine("맵의 크기 = x * x ");
            Console.Write("맵의 크기를 입력하세요 : ");
            userDecideMapSize = Console.ReadLine();                 // ReadLine을 통해 유저로부터 값을 입력받기
            mapsize = int.Parse(userDecideMapSize) + 2;             // 앞글자를 int형으로 변경하기
            Console.WriteLine(mapsize);

            string[,] map = new string[mapsize, mapsize];               // 2차원 배열 선언
            StartMapSetting(map, mapsize); // 2차원 배열 안에 맵 생성

            // 플레이어의 초기 생성 위치
            verticalMove = (mapsize / 2);
            horizonMove = (mapsize / 2);

            while(true)
            {
                // 맵 출력
                Console.SetCursorPosition(0, 0);
                Map(map, mapsize);

                // vertical horizon 출력
                //Console.Write(verticalMove);
                //Console.WriteLine( horizonMove);
                Console.WriteLine("클리어 횟수 : {0}",score);

                // 플레이어가 a,s,d,w로 이동하기
                ConsoleKey playerMove = Console.ReadKey(true).Key;
                
                // A키를 눌렀을 때,,,
                if(playerMove == ConsoleKey.A)
                {
                    // 왼쪽의 끝 부분이 아닐 경우
                    if (horizonMove > 2)    // 왼쪽의 인덱스가 1 이하일땐 스왑이 안되게...
                    {
                        // 돌을 밀었을 때
                        if (map[verticalMove, horizonMove - 1] == "# ")
                        {
                            // 돌 두개가 겹쳐있다면 밀지 못하도록
                            if (map[verticalMove, horizonMove - 2] != "# ") 
                            {
                                // 플레이어의 좌표값은 바뀌지 않게... 
                                tempHorizonMove = horizonMove;

                                if (tempHorizonMove > 3)
                                {
                                    for (int i = horizonMove; i > 3; i--)
                                    {
                                        //돌 끼리 서로 만났을 때
                                        if (map[verticalMove, tempHorizonMove - 2] == "# ")
                                        {
                                            break;
                                        }

                                        //아니라면 서로 스왑
                                        map[verticalMove, tempHorizonMove - 2] = "* ";

                                        temp = map[verticalMove, tempHorizonMove - 1];
                                        map[verticalMove, tempHorizonMove - 1] = map[verticalMove, tempHorizonMove - 2];
                                        map[verticalMove, tempHorizonMove - 2] = temp;

                                        tempHorizonMove--;

                                        Thread.Sleep(100);

                                        Console.SetCursorPosition(0, 0);
                                        Map(map, mapsize);
                                    }
                                    // 게임 승리 조건
                                    // (가로로 3개가 겹쳤을 때)
                                    if((map[verticalMove, tempHorizonMove - 1] == "# ") && (map[verticalMove, tempHorizonMove - 2] == "# ") && (map[verticalMove, tempHorizonMove - 3] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (세로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 위에 있을 때)
                                    if ((map[verticalMove + 1, tempHorizonMove-1] == "# ") && (map[verticalMove + 2, tempHorizonMove-1] == "# ") && (map[verticalMove, tempHorizonMove-1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (왼쪽 세로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 가운데에 있을 때)
                                    if ((map[verticalMove - 1, tempHorizonMove-1] == "# ") && (map[verticalMove, tempHorizonMove-1] == "# ") && (map[verticalMove + 1, tempHorizonMove-1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (왼쪽 세로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 아래에 때)
                                    if ((map[verticalMove - 2, tempHorizonMove-1] == "# ") && (map[verticalMove - 1 , tempHorizonMove-1] == "# ") && (map[verticalMove, tempHorizonMove-1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                }
                            }
                                
                            else
                            {
                                continue;
                            }
                        }

                        // 돌을 밀지 않았을 때
                        else
                        {
                            // 그냥 이동하기
                            temp = map[verticalMove, horizonMove];
                            map[verticalMove, horizonMove] = map[verticalMove, horizonMove - 1];
                            map[verticalMove, horizonMove - 1] = temp;
                            horizonMove--;
                            moveCount += 1;
                        }  
                    }
                }

                // D키를 눌렀을 때,,,
                else if (playerMove == ConsoleKey.D)
                {
                    // 오른쪽의 끝 부분이 아닐 경우
                    if (horizonMove < mapsize - 3)    // 오른쪽의 인덱스가 맵 크기 - 2 (벽 - 1) 이하일땐 스왑이 안되게...
                    {
                        // 돌을 밀었을 때
                        if (map[verticalMove, horizonMove + 1] == "# ")
                        {
                            // 돌 두개가 겹쳐있다면 밀지 못하도록
                            if (map[verticalMove, horizonMove + 2] != "# ")
                            {
                                // 플레이어의 좌표값은 바뀌지 않게... 
                                tempHorizonMove = horizonMove;

                                if (tempHorizonMove < mapsize - 4)
                                {
                                    for (int i = horizonMove; i < mapsize - 4; i++)
                                    {
                                        //돌 끼리 서로 만났을 때
                                        if (map[verticalMove, tempHorizonMove + 2] == "# ")
                                        {
                                            break;
                                        }

                                        //아니라면 스왑
                                        map[verticalMove, tempHorizonMove + 2] = "* ";

                                        temp = map[verticalMove, tempHorizonMove + 1];
                                        map[verticalMove, tempHorizonMove + 1] = map[verticalMove, tempHorizonMove + 2];
                                        map[verticalMove, tempHorizonMove + 2] = temp;

                                        tempHorizonMove++;

                                        Thread.Sleep(100);

                                        Console.SetCursorPosition(0, 0);
                                        Map(map, mapsize);
                                    }
                                    // 게임 승리 조건
                                    // (위쪽 가로로 3개가 겹쳤을 때)
                                    if ((map[verticalMove, tempHorizonMove + 1] == "# ") && (map[verticalMove, tempHorizonMove + 2] == "# ") && (map[verticalMove, tempHorizonMove + 3] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (오른쪽 세로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 위에 있을 때)
                                    else if ((map[verticalMove + 1, tempHorizonMove + 1] == "# ") && (map[verticalMove + 2, tempHorizonMove + 1] == "# ") && (map[verticalMove, tempHorizonMove + 1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (오른쪽 세로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 가운데에 있을 때)
                                    else if ((map[verticalMove - 1, tempHorizonMove + 1] == "# ") && (map[verticalMove, tempHorizonMove + 1] == "# ") && (map[verticalMove + 1, tempHorizonMove + 1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (오른쪽 세로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 아래에 때)
                                    else if ((map[verticalMove - 2, tempHorizonMove + 1] == "# ") && (map[verticalMove - 1, tempHorizonMove + 1] == "# ") && (map[verticalMove, tempHorizonMove + 1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        // 돌을 밀지 않았을 때
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

                // W키를 눌렀을 때,,,
                else if (playerMove == ConsoleKey.W)
                {
                    // 위쪽의 끝 부분이 아닐 경우
                    if (verticalMove > 2)    // 위쪽의 인덱스가 1 이하일땐 스왑이 안되게...
                    {
                        // 돌을 밀었을 때
                        if (map[verticalMove - 1, horizonMove] == "# ")
                        {
                            // 돌 두개가 겹쳐있다면 밀지 못하도록
                            if (map[verticalMove - 2, horizonMove] != "# ")
                            {
                                // 플레이어의 좌표값은 바뀌지 않게... 
                                tempVerticalMove = verticalMove;

                                if (tempVerticalMove > 3)
                                {
                                    for (int i = verticalMove; i > 3; i--)
                                    {
                                        //돌 끼리 서로 만났을 때
                                        if (map[tempVerticalMove - 2, horizonMove] == "# ")
                                        {
                                            break;
                                        }

                                        //아니라면 스왑
                                        map[tempVerticalMove - 2, horizonMove] = "* ";

                                        temp = map[tempVerticalMove - 1, horizonMove];
                                        map[tempVerticalMove - 1, horizonMove] = map[tempVerticalMove - 2, horizonMove];
                                        map[tempVerticalMove - 2, horizonMove] = temp;

                                        tempVerticalMove--;

                                        Thread.Sleep(100);

                                        Console.SetCursorPosition(0, 0);
                                        Map(map, mapsize);

                                    }
                                    // 게임 승리 조건
                                    // (가로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 왼쪽에 있을 때)
                                    if ((map[tempVerticalMove - 1, horizonMove + 1] == "# ") && (map[tempVerticalMove - 1, horizonMove + 2] == "# ") && (map[tempVerticalMove - 1, horizonMove] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (가로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 가운데에 있을 때)
                                    else if ((map[tempVerticalMove - 1, horizonMove - 1] == "# ") && (map[tempVerticalMove - 1, horizonMove] == "# ") && (map[tempVerticalMove - 1, horizonMove + 1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (가로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 오른쪽에 있을 때)
                                    else if ((map[tempVerticalMove - 1, horizonMove - 2] == "# ") && (map[tempVerticalMove - 1, horizonMove - 1] == "# ") && (map[tempVerticalMove - 1, horizonMove] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (세로로 3개가 겹쳤을 때)
                                    else if ((map[tempVerticalMove - 3, horizonMove] == "# ") && (map[tempVerticalMove - 2, horizonMove] == "# ") && (map[tempVerticalMove - 1, horizonMove] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        // 돌을 밀지 않았을 때
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

                // S키를 눌렀을 때,,,
                else if (playerMove == ConsoleKey.S)
                {
                    // 아래쪽의 끝 부분이 아닐 경우
                    if (verticalMove < mapsize - 3)    // 아래의 인덱스가 맵 -3 미만일땐 스왑이 안되게...
                    {
                        // 돌을 밀었을 때
                        if (map[verticalMove + 1, horizonMove] == "# ")
                        {
                            // 돌 두개가 겹쳐있다면 밀지 못하도록
                            if (map[verticalMove + 2, horizonMove] != "# ")
                            {
                                // 플레이어의 좌표값은 바뀌지 않게... 
                                tempVerticalMove = verticalMove;

                                if (tempVerticalMove < mapsize - 4)
                                {
                                    for (int i = verticalMove; i < mapsize - 4; i++)
                                    {
                                        //돌 끼리 서로 만났을 때
                                        if (map[tempVerticalMove + 2, horizonMove] == "# ")
                                        {
                                            break;
                                        }

                                        //아니라면 스왑
                                        map[tempVerticalMove + 2, horizonMove] = "* ";

                                        temp = map[tempVerticalMove + 1, horizonMove];
                                        map[tempVerticalMove + 1, horizonMove] = map[tempVerticalMove + 2, horizonMove];
                                        map[tempVerticalMove + 2, horizonMove] = temp;

                                        tempVerticalMove++;

                                        Thread.Sleep(100);

                                        Console.SetCursorPosition(0, 0);
                                        Map(map, mapsize);

                                    }
                                    // 게임 승리 조건
                                    // (가로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 왼쪽에 있을 때)
                                    if ((map[tempVerticalMove + 1, horizonMove + 1] == "# ") && (map[tempVerticalMove+1, horizonMove + 2] == "# ") && (map[tempVerticalMove+1, horizonMove + 3] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (가로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 가운데에 있을 때)
                                    else if ((map[tempVerticalMove+1, horizonMove - 1] == "# ") && (map[tempVerticalMove+1, horizonMove] == "# ") && (map[tempVerticalMove+1, horizonMove + 1] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (가로로 3개가 겹쳤을 때) (플레이어가 마지막으로 굴린 돌이 제일 오른쪽에 있을 때)
                                    else if ((map[tempVerticalMove+1, horizonMove - 2] == "# ") && (map[tempVerticalMove+1, horizonMove - 1] == "# ") && (map[tempVerticalMove+1, horizonMove] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    // (세로로 3개가 겹쳤을 때)
                                    else if ((map[tempVerticalMove + 3, horizonMove] == "# ") && (map[tempVerticalMove + 2, horizonMove] == "# ") && (map[tempVerticalMove + 1, horizonMove] == "# "))
                                    {
                                        Console.WriteLine("돌 3개 겹치기 성공!!");
                                        Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                                        Console.ReadKey();
                                        // 플레이어의 초기 생성 위치
                                        verticalMove = (mapsize / 2);
                                        horizonMove = (mapsize / 2);
                                        score++;
                                        StartMapSetting(map, mapsize);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        // 돌을 밀지 않았을 때
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

                else if (playerMove == ConsoleKey.R)
                {
                    Console.WriteLine("게임을 재시작합니다! 아무키나 눌러 재시작하세요...");
                    Console.ReadKey();

                    // 플레이어의 초기 생성 위치
                    verticalMove = (mapsize / 2);
                    horizonMove = (mapsize / 2);

                    StartMapSetting(map, mapsize);
                }

                else
                {
                    Console.WriteLine("잘못입력하셨습니다!");
                    break;
                }

            }   
        }

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
            for (int y = 2; y < mapsize - 2; y++)
            {
                for (int x = 2; x < mapsize - 2; x++)
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
            while (stone < 3)
            {
                Random random = new Random();
                num1 = random.Next(3, mapsize - 3);
                num2 = random.Next(3, mapsize - 3);

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

                    if (map[y, x] == "# ")      // 돌 색깔 다르게하기
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

                    Console.Write(map[y, x]);  // 맵 출력하기

                }
                Console.WriteLine();

            }
        }
    }
}
