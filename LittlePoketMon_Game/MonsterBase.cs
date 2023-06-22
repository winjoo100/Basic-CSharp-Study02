using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace LittlePoketMon_Game
{
    public class MonsterBase
    {
        public string monster_Name;
        public int monster_Hp;
        public int monster_Def;
        public int monster_Damage;

        public void Monster_Info()
        {
            Console.WriteLine("몬스터 이　름  : {0}", monster_Name);
            Console.WriteLine("몬스터 체　력  : {0}", monster_Hp);
            Console.WriteLine("몬스터 방어력  : {0}", monster_Def);
            Console.WriteLine("몬스터 공격력  : {0}", monster_Damage);
        }

        public void Monster_DecreaseHP(int playerDamage)
        {
            this.monster_Hp = this.monster_Hp - playerDamage;
        }
    }
}
