using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class AI
    {
        Field humanField;
        Field AiField;
        //Ship[] AiShipArr;
        Ship[] humanShipArr;

        public AI(Field humanField, Field AiField, Ship[] humanShipArr)
        {
            this.humanField = humanField;
            this.AiField = AiField;
            //this.AiShipArr = AiShipArr;
            this.humanShipArr = humanShipArr;
        }

        //сделать метод расставляющий корабли
        //public void setShips()
        //{
        //    foreach (var ship in AiShipArr)
        //    {
        //        Random rnd = new();
        //        for (int i = 0; i < ship.countDecks; i++)
        //        {
        //            AiField.fillWeightDict();
        //            AiField.weightDict.TryGetValue(rnd.Next(0, AiField.weightDict.Count() - 1), out int[] value);
        //            ship.ListOfCoordinates.Add(value);
        //            humanField.updateFieldWeight(value);
        //            AiField.weightDict.Clear();
        //        }
        //    }
        //}

        public void makeShot(out int[] value, out bool hit)
        {
            hit = false;
            humanField.fillWeightDict();
            Random rnd = new();
            bool fight = true;
            humanField.weightDict.TryGetValue(rnd.Next(0, humanField.weightDict.Count() - 1), out value);
            foreach (var ship in humanShipArr)
            {
                if (ship.IsGetNewShot(value))
                {
                    if (!ship.isAlive) fight = false;
                    //попали - вес ячейки делаем 0, все ячейки по диагонали на 1 делаем 0, остальные ячейки вокруг нее вес - 10
                    humanField.updateFieldWeight(value, ship, fight);
                    hit = true;
                    if (!ship.isAlive)
                    {
                        //вес ячеек вокруг потопленного корабля делаем 0  
                        if (ship.countDecks == 1)
                        {
                            humanField.weightArr[value[0] - 1, value[1]] *= 0;
                            humanField.weightArr[value[0] + 1, value[1]] *= 0;
                            humanField.weightArr[value[0], value[1] - 1] *= 0;
                            humanField.weightArr[value[0], value[1] + 1] *= 0;
                        }
                    }
                    break;
                }
            }
            if (!hit)
            {
                humanField.weightArr[value[0], value[1]] *= 0; //не попали - делаем вес ячейки -
            }
            humanField.weightDict.Clear();
        }


    }
}

