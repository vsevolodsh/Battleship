using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Ship
    {
        int hp;
        List<int[,]> ListOfCoordinates = new();
        public bool isAlive
        {
            get
            {
                if (hp == 0)
                    return false;
                else
                    return true;
            }
        }
        public Ship(int hp, List<int[,]> ListOfCoordinates)
        {
            this.hp = hp;
            this.ListOfCoordinates = ListOfCoordinates;
        }

        public bool IsGetNewShot(int[,] coordinates)
        {
            foreach (var item in ListOfCoordinates)
            {
                if (item == coordinates)
                {
                    hp--;
                    return true;
                }
            }
            return false;
        }
    }

}
