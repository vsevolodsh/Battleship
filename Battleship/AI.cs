using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class AI
    {
        Field field;
        Ship[] shipArr;

        public AI(Field field, Ship[] shipArr)
        {
            this.field = field;
            this.shipArr = shipArr;
        }

        //сделать метод расставляющий корабли

        public void makeShot()
        {
            field.fillWeightDict();
            Random rnd = new();
            field.weightDict.TryGetValue(rnd.Next(0, field.weightDict.Count() - 1), out int[] value);
            if (value[0] == 6 && value[1] == 9)
            {

            }
            foreach (var ship in shipArr)
            {
                if (ship.IsGetNewShot(value))
                {
                    //попали - вес ячейки делаем 0, все ячейки по диагонали на 1 делаем 0, остальные ячейки вокруг нее вес - 10
                    field.weightArr[value[0], value[1]] *= 0;
                    field.weightArr[value[0] - 1, value[1] - 1] *= 0;
                    field.weightArr[value[0] + 1, value[1] - 1] *= 0;
                    field.weightArr[value[0] - 1, value[1] + 1] *= 0;
                    field.weightArr[value[0] + 1, value[1] + 1] *= 0;
                    field.weightArr[value[0] - 1, value[1]] *= 10;
                    field.weightArr[value[0] + 1, value[1]] *= 10;
                    field.weightArr[value[0], value[1] - 1] *= 10;
                    field.weightArr[value[0], value[1] + 1] *= 10;
                    if (!ship.isAlive)
                    {    
                        //вес ячеек вокруг потопленного корабля делаем 0  
                    }
                }
                else
                {
                    field.weightArr[value[0], value[1]] *= 0; //не попали - делаем вес ячейки -
                }
            }
            field.weightDict.Clear();
        }
    }
}
