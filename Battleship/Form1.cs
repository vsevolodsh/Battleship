namespace Battleship
{
    public partial class Form1 : Form
    {
        Panel[,] HumanPanels = new Panel[10, 10];
        static Ship ship40 = new(4);
        static Ship ship30 = new(3);
        static Ship ship31 = new(3);
        static Ship[] ships = new Ship[] { ship40, ship30, ship31 };
        int shipsIndex = 0;
        int click = 0;
        bool first = true;
        static Field humanfield = new Field();
        static Field AiField = new Field();
        Human human = new(humanfield, AiField, ships);
        AI ai = new(humanfield, AiField, ships);

        public Form1()
        {
            const int panelSize = 30;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    HumanPanels[i, j] = new Panel();
                    HumanPanels[i, j].Location = new Point(85 + j * panelSize + 5, 65 + i * panelSize + 5);
                    HumanPanels[i, j].Size = new Size(panelSize - 5, panelSize - 5);
                    HumanPanels[i, j].BackColor = Color.White;
                    HumanPanels[i, j].Click += new System.EventHandler(humanPanel_Click);
                    Controls.Add(HumanPanels[i, j]);
                }
            }
            InitializeComponent();
        }
        private void humanPanel_Click(object sender, EventArgs e)
        {
            label3.Text = $"Расположите корабль с {ships[shipsIndex].countDecks} палубами";
            Panel pnl = sender as Panel;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (HumanPanels[i, j].Equals(pnl))
                    {
                        if (first && humanfield.weightArr[i, j] != 0)
                        {
                            humanfield.weightArr[i, j] = 10;
                            first = false;
                        }
                        if (human.setShips(new[] { i, j }, shipsIndex))
                        {
                            pnl.BackColor = Color.Blue;
                            click++;
                        }
                        else
                        {
                            MessageBox.Show("В этом поле разместить корабль нельзя!");
                        }
                        if (click == ships[shipsIndex].countDecks && shipsIndex != ships.Count())
                        {
                            shipsIndex++;
                            click = 0;
                            first = true;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] panelCoord;
            bool hit;
            ai.makeShot(out panelCoord, out hit);
            if (hit)
            {
                HumanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Red;
            }
            else
            {
                HumanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Gray;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            humanfield.weightArr = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
        };
        }
    }
}