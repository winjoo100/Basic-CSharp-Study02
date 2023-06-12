// 카드 뽑기 게임
// 카드는 총 52장 문양별로 존재함.
// 컴퓨터가 카드를 2장 뽑아서 보여준다.
// 플레이어는 카드를 보고 배팅한다.
// 플레이어는 카드를 한 장 뽑는다.
// 플레이어의 카드가 컴퓨터 카드 2장 사이에 존재하는 수라면, 플레이어는 한 번의 게임을 승리하여 베팅 점수의 2배를 받는다.
// 플레이어의 카드가 컴퓨터 카드 2장 사이에 존재하지 않는 경우, 플레이어는 한 번의 게임을 패배하여 베팅 점수 만큼 잃는다.
// 플레이어는 일정 점수를 획득하면 게임을 최종 승리하며, 0점 이하인 경우 게임을 최종 패배한다. 이 경우에 게임을 종료한다.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CardGame_2_CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 초기 플레이어의 소지금
            int playerMoney = 20000;

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
                int joinGameMoney = 1000;

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
                    string yesOrNo = Console.ReadLine();

                    // { 베팅을 수락했을 경우
                    if (yesOrNo == "y" || yesOrNo == "Y")
                    {
                        bool yesno = true;
                        while (yesno)
                        {

                            // 베팅 금액 여부
                            Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                            Console.WriteLine("\t얼마 베팅 하시겠습니까? 승리시 베팅금액의 2배 획득\n");
                            Console.WriteLine("\t베팅 최소금액 1000원 이상\n");
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
                                else if (betValue < 1000)
                                {
                                    Console.WriteLine("\t낙장불입! 1000원 이상을 베팅해야합니다. 다시 입력하세요");
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
                        Console.WriteLine("\t게임 참여비 {0}원 지불하셨습니다.\n", joinGameMoney);
                        playerMoney = playerMoney - joinGameMoney;
                        Console.WriteLine("\t컴퓨터가 다시 카드를 뽑습니다.");
                        Console.WriteLine("\t...아무키나 눌러 게임을 다시 시작하세요.\n");
                        Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ\n");
                        gameEnd = true;
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
    }
}
