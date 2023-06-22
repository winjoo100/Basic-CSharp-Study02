using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittlePoketMon_Game
{
    public class Monster : MonsterBase
    {
        public Monster(string name, int hp, int def, int damage)
        {
            monster_Name = name;
            monster_Hp = hp;
            monster_Def = def;
            monster_Damage = damage;
        }

        // 추가적인 몬스터 속성 및 동작을 정의합니다.
    }
}
