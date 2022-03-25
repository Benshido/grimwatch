using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimWatch15_5_2019
{
    //Class for all the attacks
    class AttackListBvri
    {
        private string attackNameBvri;
        private string characterIdBvri;
        private string minAttackDamageBvri;
        private string maxAttackDamageBvri;

        public AttackListBvri(string c_attackNameBvri, 
                              string c_characterIdBvri,
                              string c_minAttackDamageBvri,
                              string c_maxAttackDamageBvri)
        {
            attackNameBvri = c_attackNameBvri;
            characterIdBvri = c_characterIdBvri;
            minAttackDamageBvri = c_minAttackDamageBvri;
            maxAttackDamageBvri = c_maxAttackDamageBvri;
        }

        public string AttackNameBvri
        {
            get { return attackNameBvri; }
            set { attackNameBvri = value; }
        }

        public string CharacterIdBvri
        {
            get { return characterIdBvri; }
            set { characterIdBvri = value; }
        }

        public string MinAttackDamageBvri
        {
            get { return minAttackDamageBvri; }
            set { minAttackDamageBvri = value; }
        }

        public string MaxAttackDamageBvri
        {
            get { return maxAttackDamageBvri; }
            set { maxAttackDamageBvri = value; }
        }
    }
}
