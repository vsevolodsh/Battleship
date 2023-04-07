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
                /*if(humanShipArr[shipIndex].ListOfCoordinates.Count() != 0)
                {
                    if (humanShipArr[shipIndex].ListOfCoordinates[0][0] > xyCoordinates[0])
                    {

                    }
                }*/
                humanShipArr[shipIndex].ListOfCoordinates.Add(xyCoordinates);
                //humanShipArr[shipIndex].ListOfCoordinates.Sort();
                humanField.updateFieldWeight(xyCoordinates, humanShipArr[shipIndex], false);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
