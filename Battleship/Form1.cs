namespace Battleship
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Ship ship = new(4);
            Ship[] ships = new Ship[] { ship };
            Field Humanfield = new Field();
            Field AiField = new Field();
            AI ai = new(Humanfield, AiField, ships);
            ai.setShips();
            //for (int i = 0; i < 100; i++)
            //{
            //    ai.makeShot();
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("sda");
        }
    }
}