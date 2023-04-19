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
        Ship[] AiShipArr;
        Ship[] humanShipArr;

        public AI(Field humanField, Field AiField, Ship[] AiShipArr, Ship[] humanShipArr)
        {
            this.humanField = humanField;
            this.AiField = AiField;
            this.AiShipArr = AiShipArr;
            this.humanShipArr = humanShipArr;
        }

        public void SetShips()
        {
            foreach (var ship in AiShipArr)
            {
                Random rnd = new();
                for (int i = 0; i < ship.countDecks; i++)
                {
                    AiField.fillWeightDict();
                    AiField.weightDict.TryGetValue(rnd.Next(0, AiField.weightDict.Count() - 1), out int[] value);
                    ship.ListOfCoordinates.Add(value);
                    AiField.updateFieldWeight(value, ship, false);
                    AiField.weightDict.Clear();
                }
            }
        }

        public void MakeShot(out int[] value, out bool hit, out bool wound, out Ship shipPaint)
        {
            wound = false;
            shipPaint = AiShipArr[0];
            hit = false;
            humanField.fillWeightDict();
            Random rnd = new();
            bool fight = true;
            humanField.weightDict.TryGetValue(rnd.Next(0, humanField.weightDict.Count() - 1), out value);
            foreach (var ship in humanShipArr)
            {
                if (ship.IsGetNewShot(value))
                {

                    shipPaint = ship;
                    if (!ship.isAlive)
                        fight = false;
                    else 
                        wound = true;
                    //попали - вес ячейки делаем 0, все ячейки по диагонали на 1 делаем 0, остальные ячейки вокруг нее вес - 10
                    humanField.updateFieldWeight(value, ship, fight);
                    hit = true;
                    /*if (!ship.isAlive)
                    {
                        //вес ячеек вокруг потопленного корабля делаем 0  
                        if (ship.countDecks == 1)
                        {
                            humanField.weightArr[value[0] - 1, value[1]] *= 0;
                            humanField.weightArr[value[0] + 1, value[1]] *= 0;
                            humanField.weightArr[value[0], value[1] - 1] *= 0;
                            humanField.weightArr[value[0], value[1] + 1] *= 0;
                        }
                    }*/
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

