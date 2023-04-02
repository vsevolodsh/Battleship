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
        Ship[] shipArr;

        public AI(Field humanField, Field AiField, Ship[] shipArr)
        {
            this.humanField = humanField;
            this.AiField = AiField;
            this.shipArr = shipArr;
        }

        //сделать метод расставляющий корабли
        public void setShips()
        {
            foreach (var ship in shipArr)
            {
                Random rnd = new();
                for (int i = 0; i < ship.countDecks; i++)
                {
                    AiField.fillWeightDict();
                    AiField.weightDict.TryGetValue(rnd.Next(0, AiField.weightDict.Count() - 1), out int[] value);
                    ship.ListOfCoordinates.Add(value);
                    updateFieldWeight(AiField, value);
                    AiField.weightDict.Clear();
                }
            }
        }

        public void makeShot()
        {
            humanField.fillWeightDict();
            Random rnd = new();
            humanField.weightDict.TryGetValue(rnd.Next(0, humanField.weightDict.Count() - 1), out int[] value);
            foreach (var ship in shipArr)
            {
                if (ship.IsGetNewShot(value))
                {
                    //попали - вес ячейки делаем 0, все ячейки по диагонали на 1 делаем 0, остальные ячейки вокруг нее вес - 10
                    updateFieldWeight(humanField, value);
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
                }
                else
                {
                    humanField.weightArr[value[0], value[1]] *= 0; //не попали - делаем вес ячейки -
                }
            }
            humanField.weightDict.Clear();
        }

        private void updateFieldWeight(Field field, int[] XYcoordinates)
        {
            field.weightArr[XYcoordinates[0], XYcoordinates[1]] *= 0;
            field.weightArr[XYcoordinates[0] - 1, XYcoordinates[1] - 1] *= 0;
            field.weightArr[XYcoordinates[0] + 1, XYcoordinates[1] - 1] *= 0;
            field.weightArr[XYcoordinates[0] - 1, XYcoordinates[1] + 1] *= 0;
            field.weightArr[XYcoordinates[0] + 1, XYcoordinates[1] + 1] *= 0;
            field.weightArr[XYcoordinates[0] - 1, XYcoordinates[1]] *= 10;
            field.weightArr[XYcoordinates[0] + 1, XYcoordinates[1]] *= 10;
            field.weightArr[XYcoordinates[0], XYcoordinates[1] - 1] *= 10;
            field.weightArr[XYcoordinates[0], XYcoordinates[1] + 1] *= 10;
        }
    }
}
