namespace Battleship
{
    internal class Human
    {
        Field humanField;
        Field AiField;
       Ship[] AiShipArr;
        Ship[] humanShipArr;

        public Human(Field humanField, Field AiField, Ship[] AiShipArr, Ship[] humanShipArr)
        {
            this.humanField = humanField;
            this.AiField = AiField;
            this.AiShipArr = AiShipArr;
            this.humanShipArr = humanShipArr;
        }

        public bool MakeShot(int[] xyCoordinates)
        {
            foreach (var ship in AiShipArr)
            {
                if (ship.IsGetNewShot(xyCoordinates))
                {
                    return true;
                }
            } 
            return false;
        }

        public bool setShips(int[] xyCoordinates, int shipIndex)
        {
            if (humanField.weightArr[xyCoordinates[0], xyCoordinates[1]] == 10)
            {
                humanShipArr[shipIndex].ListOfCoordinates.Add(xyCoordinates);
                humanField.updateFieldWeight(xyCoordinates, humanShipArr[shipIndex], false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AutoSetShips()
        {
            foreach (var ship in humanShipArr)
            {
                Random rnd = new();
                for (int i = 0; i < ship.countDecks; i++)
                {
                    humanField.fillWeightDict();
                    humanField.weightDict.TryGetValue(rnd.Next(0, humanField.weightDict.Count() - 1), out int[] value);
                    ship.ListOfCoordinates.Add(value);
                    humanField.updateFieldWeight(value, ship, false);
                    humanField.weightDict.Clear();
                }
            }
        }
    }
}
