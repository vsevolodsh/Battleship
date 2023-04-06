namespace Battleship
{
    internal class Human
    {
        Field humanField;
        Field AiField;
        //Ship[] AiShipArr;
        Ship[] humanShipArr;

        public Human(Field humanField, Field AiField, Ship[] humanShipArr)
        {
            this.humanField = humanField;
            this.AiField = AiField;
            //this.AiShipArr = AiShipArr;
            this.humanShipArr = humanShipArr;
        }
        public bool setShips(int[] xyCoordinates, int shipIndex)
        {
            if (humanField.weightArr[xyCoordinates[0], xyCoordinates[1]] == 10)
            {
                humanShipArr[shipIndex].ListOfCoordinates.Add(xyCoordinates);
                humanField.updateFieldWeight(xyCoordinates);
                return true;
            }
            else
            {
                return false;   
            }
        }
    }
}
