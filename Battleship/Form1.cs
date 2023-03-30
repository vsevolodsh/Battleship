namespace Battleship
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Ship ship = new(4);
            ship.ListOfCoordinates.Add(new[] { 6, 8 });
            ship.ListOfCoordinates.Add(new[] { 6, 9 });
            Ship[] ships = new Ship[] { ship };
            Field field = new Field();
            AI ai = new(field, ships);
            for (int i = 0; i < 100; i++)
            {
                ai.makeShot();
            }
        }
    }
}