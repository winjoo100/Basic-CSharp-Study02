// 상점과 인벤토리 시스템 만들기
// - 아이템을 저장하고 있는 컬렉션을 만들고
// - 상점을 열먼 위의 컬렉션에서 랜덤으로 3가지 종류의 아이템을 출력한다.
// - 상점을 일정시간(or 열 때마다) 아이템의 종류가 바뀐다.
// - 플레이어는 가지고 있는 골드의 범위 안에서 아이템을 구매할 수 있다.
// - 구매한 아이템은 플레이어의 인벤토리로 들어가고, 출력해서 볼 수 있다.
// - 아이템 갯수 제한은 따로 없음

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Program
    {
        static void Main(string[] args)
        {
            int itemPool = 3;                            // 상점에 출력될 아이템 갯수
            string[] market = new string[3];             // 상점
            string[] marketTemp = new string[3];         // 상점 출력용
            int[] sameNumcheck = new int[itemPool];      // 상점 아이템 갯수
            string[] removeCheck = new string[itemPool]; // 구매한 아이템은 창고에서 삭제
            int storageRandomItem;                       // 창고에서 무작위로 아이템을 선정할 변수
            int playerMoney = 3000000;                        // 플레이어의 초기 소지금

            // 내 가방 만들기
            List<string> myInventory = new List<string>();
            myInventory.Add("텅텅빈 통장");

            // 아이템 목록 도감
            Dictionary<string, int> itemDictionary = new Dictionary<string, int>();
            itemDictionary.Add("불타는 검", 2000);
            itemDictionary.Add("마법 지팡이", 1000);
            itemDictionary.Add("강화된 방패", 2000);
            itemDictionary.Add("회복 물약", 500);
            itemDictionary.Add("철검", 1000);
            itemDictionary.Add("도적의 가면", 3000);
            itemDictionary.Add("천갑옷", 1500);
            itemDictionary.Add("번개의 망토", 7000);
            itemDictionary.Add("황금 장화", 10000);
            itemDictionary.Add("마법서", 5000);
            itemDictionary.Add("독화살", 5000);
            itemDictionary.Add("철제 갑옷", 4000);
            itemDictionary.Add("회복의 성배", 20000);
            itemDictionary.Add("어둠의 검은 돌", 3000);
            itemDictionary.Add("신속한 신발", 5000);
            itemDictionary.Add("화염 저항 망토", 7000);
            itemDictionary.Add("신비한 마법서", 20000);
            itemDictionary.Add("독사의 독침", 30000);
            itemDictionary.Add("천둥의 망치", 100000);
            itemDictionary.Add("얼음 방패", 50000);
            itemDictionary.Add("빛나는 보석", 20000);
            itemDictionary.Add("암흑의 외투", 30000);


            // 창고 만들기
            List<string> storage = new List<string>();
            storage.Add("불타는 검");
            storage.Add("마법 지팡이");
            storage.Add("강화된 방패");
            storage.Add("회복 물약");
            storage.Add("철검");
            storage.Add("도적의 가면");
            storage.Add("천갑옷");
            storage.Add("번개의 망토");
            storage.Add("황금 장화");
            storage.Add("마법서");
            storage.Add("독화살");
            storage.Add("철제 갑옷");
            storage.Add("회복의 성배");
            storage.Add("어둠의 검은 돌");
            storage.Add("신속한 신발");
            storage.Add("화염 저항 망토");
            storage.Add("신비한 마법서");
            storage.Add("독사의 독침");
            storage.Add("천둥의 망치");
            storage.Add("얼음 방패");
            storage.Add("빛나는 보석");
            storage.Add("암흑의 외투");

            // 게임 시작
            while (true)
            {
                // 인벤토리 만들기
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■인벤토리■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("플레이어의 현재 소지금 : [ {0} ]", playerMoney);

                int countInven_ = 0;
                foreach (string Inventroy in myInventory)
                {
                    Console.Write("§{0} ", Inventroy);
                    countInven_++;
                    if (countInven_ % 7 == 0)    // 가방 아이템의 갯수가 7이상이면 한칸 띄우기
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■창고■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine();


                // 창고 출력
                int countStorage_ = 0;
                foreach (string itemstorage in storage)
                {
                    Console.Write("§{0} ", itemstorage);
                    countStorage_++;
                    if (countStorage_ % 7 == 0)    // 창고 아이템의 갯수가 7이상이면 한칸 띄우기
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine();

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
                Console.WriteLine("상점 아이템 목록");
                int[] itemPrice = new int[itemPool];

                // 상점 아이템이름, 가격 출력하기 (딕셔너리에서 출력)
                for (int i = 0; i < itemPool; i++)
                {
                    // 아이템딕셔너리의 키값을 새로운 키값으로 돌려보면서 market[i]와 키값이 같다면 출력합니다.
                    foreach (string key in itemDictionary.Keys)
                    {
                        if (key == market[i])
                        {
                            int price = itemDictionary[key];  // 해당 아이템의 가격을 가져옴
                            itemPrice[i] = price;             // 해당 아이템의 가격을 저장해서 아이템을 구매할 때 비교할 때 사용
                            Console.WriteLine("{0} : 아이템 이름: {1}, 가격: {2}", i + 1, key, price);
                            break;
                        }
                    }
                }
                Console.WriteLine();

                // { 아이템 구매 로직
                bool quitMarket_ = false;       // 상점을 종료하기 위한 논리값
                int[] sameBuy = new int[4];     // 중복된 숫자를 또 누를 경우를 방지
                int buyCount = 0;               // 구매횟수를 확인

                while (quitMarket_ == false)
                {
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
                    }

                    // buyCount가 3이상이면 상점 나가기
                    if (buyCount >= 3)
                    {
                        Console.WriteLine("모든 아이템을 구매했습니다.");
                        Console.WriteLine("아무키나 눌러 상점을 나갑니다....");
                        Console.WriteLine();
                        Console.ReadKey();
                        quitMarket_ = true;
                    }
                }       // } 아이템 구매 로직

                // 창고의 개수가 3개 미만일 경우 게임 종료
                if (storage.Count < 3)
                {
                    break;
                }

                Console.Clear();

            }       // 게임 전체 while()
        }       // Main()
    }
}
