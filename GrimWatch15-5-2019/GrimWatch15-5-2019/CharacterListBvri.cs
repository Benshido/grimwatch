using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimWatch15_5_2019
{
    //Class for all the characters
    class CharacterListBvri
    {
        private string characterIdBvri;
        private string characterNameBvri;
        private string characterHealthBvri;
        private string characterDamageBvri;
        private string characterDefenseBvri;
        private string characterImageBvri;

        public CharacterListBvri(string c_characterIdBvri,
                                 string c_characterNameBvri, 
                                 string c_characterHealthBvri, 
                                 string c_characterDamageBvri, 
                                 string c_characterDefenseBvri,
                                 string c_characterImageBvri)
        {

            characterIdBvri = c_characterIdBvri;
            characterNameBvri = c_characterNameBvri;
            characterHealthBvri = c_characterHealthBvri;
            characterDamageBvri = c_characterDamageBvri;
            characterDefenseBvri = c_characterDefenseBvri;
            characterImageBvri = c_characterImageBvri;
        }

        public string CharacterIdBvri
        {
            get { return characterIdBvri; }
            set { characterIdBvri = value; }
        }

        public string CharacterNameBvri
        {
            get { return characterNameBvri; }
            set { characterNameBvri = value; }
        }

        public string CharacterHealthBvri 
        {
            get { return characterHealthBvri; }
            set { characterHealthBvri = value; }
        }

        public string CharacterDamageBvri
        {
            get { return characterDamageBvri; }
            set { characterDamageBvri = value; }
        }

        public string CharacterDefenseBvri
        {
            get { return characterDefenseBvri; }
            set { characterDefenseBvri = value; }
        }

        public string CharacterImageBvri
        {
            get { return characterImageBvri; }
            set { characterImageBvri = value; }
        }
    }
}
