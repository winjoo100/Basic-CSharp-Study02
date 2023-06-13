// TicTacToe 게임
// 승리조건
// (가로) 123, 456, 789
// (세로) 147, 258, 369
// (대각) 159, 357

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            // { 변수 선언부
            int gameCount = 0;
            int[] boardIdx = new int[9];
            string[,] print_board = new string[5, 11] 
            {
                {" ", " ", " ", "ㅣ", " "," ", " ", "ㅣ", " ", " ", " " },
                {"-", "-", "-", "ㅣ", "-","-", "-", "ㅣ", "-", "-", "-" },
                {" ", " ", " ", "ㅣ", " "," ", " ", "ㅣ", " ", " ", " " },
                {"-", "-", "-", "ㅣ", "-","-", "-", "ㅣ", "-", "-", "-" },
                {" ", " ", " ", "ㅣ", " "," ", " ", "ㅣ", " ", " ", " " }
            };
            string playerCheck;
            int playerCheckNum;
            int computerNum;

            // 게임시작
            Console.WriteLine("게임을 시작합니다.");
            while (true)  // 게임카운트가 10이 되면 게임 종료
            {
                restart:
                Console.WriteLine();
                print_Board(print_board);
                Console.WriteLine();

                // { 컴퓨터의 승리 조건
                // { 가로
                if ((print_board[0, 1] == "0") && (print_board[0, 5] == "0") && (print_board[0, 9] == "0"))     // 1, 2, 3
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[2, 1] == "0") && (print_board[2, 5] == "0") && (print_board[2, 9] == "0"))     // 4, 5, 6
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[4, 1] == "0") && (print_board[4, 5] == "0") && (print_board[4, 9] == "0"))     // 7, 8, 9
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }       // } 가로

                // { 세로
                if ((print_board[0, 1] == "0") && (print_board[2, 1] == "0") && (print_board[4, 1] == "0"))     // 1, 4, 7
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[0, 5] == "0") && (print_board[2, 5] == "0") && (print_board[4, 5] == "0"))     // 2, 5, 8
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[0, 9] == "0") && (print_board[2, 9] == "0") && (print_board[4, 9] == "0"))     // 3, 6, 9
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }       // } 세로

                //  { 대각
                if ((print_board[0, 1] == "0") && (print_board[2, 5] == "0") && (print_board[4, 9] == "0"))     // 1, 5, 9
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[0, 9] == "0") && (print_board[2, 5] == "0") && (print_board[4, 1] == "0"))     // 3, 5, 7
                {
                    Console.Clear();
                    print_Board(print_board);
                    Console.WriteLine("컴퓨터의 승리입니다.");
                    Console.ReadKey();
                    break;
                }       // } 대각

                // 플레이어의 차례
                // 플레이어의 입력을 받는 로직
                Console.WriteLine("원하는 위치에 체크하세요. (1 ~ 9) 사이의 숫자를 입력하세요.");
                playerCheck = Console.ReadLine();

                // { 1~9를 잘 입력했는지 여부
                if ((playerCheck == "1") || (playerCheck == "2") || (playerCheck == "3") || (playerCheck == "4") || (playerCheck == "5")
                                            || (playerCheck == "6") || (playerCheck == "7") || (playerCheck == "8") || (playerCheck == "9"))
                {
                    playerCheckNum = (int.Parse(playerCheck));
                    if (playerCheckNum == 1)
                    {
                        if (print_board[0, 1] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[0, 1] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 2)
                    {
                        if (print_board[0, 5] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[0, 5] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 3)
                    {
                        if (print_board[0, 9] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[0, 9] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 4)
                    {
                        if (print_board[2, 1] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[2, 1] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 5)
                    {
                        if (print_board[2, 5] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[2, 5] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 6)
                    {
                        if (print_board[2, 9] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[2, 9] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 7)
                    {
                        if (print_board[4, 1] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[4, 1] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 8)
                    {
                        if (print_board[4, 5] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[4, 5] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                    else if (playerCheckNum == 9)
                    {
                        if (print_board[4, 9] == " ")
                        {
                            Console.Clear();
                            Console.WriteLine();
                            print_board[4, 9] = "X";
                            gameCount++;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 체크가 된 곳입니다. 다른 곳을 찍어주세요.");
                            goto restart;
                        }
                    }
                }       // } 1~9를 잘 입력했는지 여부

                // 1~9 외의 다른 입력을 받았을 때
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못입력하셨습니다. 1 ~ 9 사이의 숫자를 입력하세요.");
                    Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                    goto restart;
                }

                // { 플레이어의 승리 조건
                // { 가로
                if ((print_board[0, 1] == "X") && (print_board[0, 5] == "X") && (print_board[0, 9] == "X"))     // 1, 2, 3
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[2, 1] == "X") && (print_board[2, 5] == "X") && (print_board[2, 9] == "X"))     // 4, 5, 6
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[4, 1] == "X") && (print_board[4, 5] == "X") && (print_board[4, 9] == "X"))     // 7, 8, 9
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }       // } 가로

                // { 세로
                if ((print_board[0, 1] == "X") && (print_board[2, 1] == "X") && (print_board[4, 1] == "X"))     // 1, 4, 7
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[0, 5] == "X") && (print_board[2, 5] == "X") && (print_board[4, 5] == "X"))     // 2, 5, 8
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[0, 9] == "X") && (print_board[2, 9] == "X") && (print_board[4, 9] == "X"))     // 3, 6, 9
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }       // } 세로

                //  { 대각
                if ((print_board[0, 1] == "X") && (print_board[2, 5] == "X") && (print_board[4, 9] == "X"))     // 1, 5, 9
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }
                if ((print_board[0, 9] == "X") && (print_board[2, 5] == "X") && (print_board[4, 1] == "X"))     // 3, 5, 7
                {
                    print_Board(print_board);
                    Console.WriteLine("플레이어의 승리입니다.");
                    Console.ReadKey();
                    break;
                }       // } 대각

                // } 게임승리조건

                // 만약 무승부라면?
                if (gameCount >= 9)
                {
                    print_Board(print_board);
                    Console.WriteLine("비겼습니다.");
                    Console.ReadKey();
                    break;
                }

                // 컴퓨터의 출력
                while (true)
                {
                    //ComputerRestart:
                    Random random = new Random();
                    computerNum = random.Next(1, 10);

                    if ((computerNum == 1) && (print_board[0, 1] == " "))
                    {
                        Console.Clear();
                        print_board[0, 1] = "0";
                        gameCount++;
                        break;
                    }
                    
                    else if ((computerNum == 2) && (print_board[0, 5] == " "))
                    {
                        Console.Clear();
                        print_board[0, 5] = "0";
                        gameCount++;
                        break;
                    }

                    else if ((computerNum == 3) && (print_board[0, 9] == " "))
                    {
                        Console.Clear();
                        print_board[0, 9] = "0";
                        gameCount++;
                        break;
                    }

                    else if ((computerNum == 4) && (print_board[2, 1] == " "))
                    {
                        Console.Clear();
                        print_board[2, 1] = "0";
                        gameCount++;
                        break;
                    }

                    else if ((computerNum == 5) && (print_board[2, 5] == " "))
                    {
                        Console.Clear();
                        print_board[2, 5] = "0";
                        gameCount++;
                        break;
                    }

                    else if ((computerNum == 6) && (print_board[2, 9] == " "))
                    {
                        Console.Clear();
                        print_board[2, 9] = "0";
                        gameCount++;
                        break;
                    }

                    else if ((computerNum == 7) && (print_board[4, 1] == " "))
                    {
                        Console.Clear();
                        print_board[4, 1] = "0";
                        gameCount++;
                        break;
                    }

                    else if ((computerNum == 8) && (print_board[4, 5] == " "))
                    {
                        Console.Clear();
                        print_board[4, 5] = "0";
                        gameCount++;
                        break;
                    }

                    else if ((computerNum == 9) && (print_board[4, 9] == " "))
                    {
                        Console.Clear();
                        print_board[4, 9] = "0";
                        gameCount++;
                        break;
                    }

                }
            }
        }       // Main()

        // { 보드판 출력
        static void print_Board(string[,] print_board)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Console.Write(print_board[i, j]);
                    if (j == 10)
                    {
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine();
        }       // } print_Board()
    }
}
