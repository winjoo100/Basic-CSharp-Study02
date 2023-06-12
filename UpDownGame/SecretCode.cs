// 컴퓨터가 숨기고 있는 비밀 코드를 추측하는 게임
// 1. 컴퓨터는 비밀 코드를 하나 랜덤하게 선정한다. 비밀 코드는 아스키 코드(영문 대문자)로 이루어져 있다.
// 2. 유저는 input 입력해서 비밀 코드가 어떤 것인지 추측한다.
// 3. 컴퓨터는 유저의 추측을 보고 비밀 코드가 유저의 추측보다 앞에 있는지, 뒤에 있는지 알려준다.
// 4. 몇 번 만에 맞추는지에 따라서 점수를 부여한다.
// 5. 유저가 코드를 맞추면 게임을 종료한다.
// - 예외조건 클래스를 활용해서 구현한다.
// - ex) SecretCode 클래스를 생성 SecretCode mySecretCode = new SecretCode();
//                              mySecretCode.Play();

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpDownGame
{
    public class SecretCode
    {
        public void UpDownGame()
        {
            // 컴퓨터가 숨기고 있는 비밀코드
            Random random = new Random();
            int secretCode = random.Next(65, 90);
            char asciiCode = (char)secretCode;
            Console.WriteLine(asciiCode);

            bool gameEnd = false;
            int findCount = 0;
            while(gameEnd == false)
            {
                Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                Console.WriteLine("A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z");
                Console.WriteLine("컴퓨터의 비밀코드를 맞춰보세요 (비밀코드 == 영문 대문자)");
                Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");

                // 유저의 입력을 받기
                string userInput = Console.ReadLine();
                // 첫 번째 문자 반환
                char ch = userInput[0];

                if ('A' <= ch && ch <= 'Z')
                {
                    Console.Clear();


                    Console.WriteLine("유저가 입력한 값 : {0}", ch);
                    Console.WriteLine();

                    // 유저의 입력과 비밀코드 비교하기
                    if (ch == asciiCode)
                    {
                        Console.WriteLine("승리");
                        gameEnd = true;
                    }
                    else if (ch > asciiCode)
                    {
                        ++findCount;
                        Console.WriteLine("입력하신 값{0}보다 비밀코드가 작습니다.", ch);
                        Console.WriteLine("현재까지 찾은 횟수 = {0}", findCount);
                    }
                    else if (ch < asciiCode)
                    {
                        ++findCount;
                        Console.WriteLine("입력하신 값{0}보다 비밀코드가 큽니다.", ch);
                        Console.WriteLine("현재까지 찾은 횟수 = {0}", findCount);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("유저가 입력한 값 : {0}", userInput);                    
                    Console.WriteLine("영문 대문자가 아닙니다. 다시 입력하세요...");
                }
                
            }
            


        }
    }
}
