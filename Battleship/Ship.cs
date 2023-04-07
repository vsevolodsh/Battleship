namespace Battleship
{
    internal class Ship
    {
        public int countDecks;
        int hp;
        public List<int[]> ListOfCoordinates = new();
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
        public Ship(int countDecks)
        {
            this.countDecks = countDecks;
            hp = countDecks;
        }

        public bool IsGetNewShot(int[] coordinates)
        {
            foreach (var item in ListOfCoordinates)
            {
                if (Enumerable.SequenceEqual(item, coordinates))
                {
                    hp--;
                    return true;
                }
            }
            return false;
        }
    }

}
