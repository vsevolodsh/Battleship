using System.Threading;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form1 : Form
    {
        Panel[,] humanPanels = new Panel[10, 10];
        Panel[,] aiPanels = new Panel[10, 10];
        static Ship humanShip40 = new(4);
        static Ship humanShip30 = new(3);
        static Ship humanShip31 = new(3);
        static Ship humanShip20 = new(2);
        static Ship humanShip21 = new(2);
        static Ship humanShip22 = new(2);
        static Ship humanShip10 = new(1);
        static Ship humanShip11 = new(1);
        static Ship humanShip12 = new(1);
        static Ship humanShip13 = new(1);

        static Ship aiShip40 = new(4);
        static Ship aiShip30 = new(3);
        static Ship aiShip31 = new(3);
        static Ship aiShip20 = new(2);
        static Ship aiShip21 = new(2);
        static Ship aiShip22 = new(2);
        static Ship aiShip10 = new(1);
        static Ship aiShip11 = new(1);
        static Ship aiShip12 = new(1);
        static Ship aiShip13 = new(1);


        static Ship[] humanShips = new Ship[] { humanShip40, humanShip30, humanShip31, humanShip20, humanShip21, humanShip22,
            humanShip10, humanShip11, humanShip12, humanShip13 };


        static Ship[] aiShips = new Ship[] { aiShip40, aiShip30, aiShip31, aiShip20, aiShip21, aiShip22,
            aiShip10, aiShip11, aiShip12, aiShip13 };

        int humanShipsCount = 0;
        int click = 0;
        bool first = true;
        static Field humanfield = new Field();
        static Field AiField = new Field();
        Human human = new(humanfield, AiField, aiShips, humanShips);
        AI ai = new(humanfield, AiField, aiShips, humanShips);
        const int panelSize = 30;

        public Form1()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    humanPanels[i, j] = new Panel();
                    humanPanels[i, j].Location = new Point(85 + j * panelSize + 5, 120 + i * panelSize + 5);
                    humanPanels[i, j].Size = new Size(panelSize - 5, panelSize - 5);
                    humanPanels[i, j].BackColor = Color.White;
                    humanPanels[i, j].Enabled = false;
                    humanPanels[i, j].Click += new EventHandler(humanPanel_Click);
                    Controls.Add(humanPanels[i, j]);

                    aiPanels[i, j] = new Panel();
                    aiPanels[i, j].Location = new Point(620 + j * panelSize + 5, 120 + i * panelSize + 5);
                    aiPanels[i, j].Size = new Size(panelSize - 5, panelSize - 5);
                    aiPanels[i, j].BackColor = Color.White;
                    aiPanels[i, j].Enabled = false;
                    aiPanels[i, j].Click += new EventHandler(aiPanel_Click);
                    Controls.Add(aiPanels[i, j]);
                }
            }
            InitializeComponent();
        }

        private void aiPanel_Click(object sender, EventArgs e)
        {
            Panel pnl = sender as Panel;
            bool wound;
            Ship shipPaint;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (pnl.Equals(aiPanels[i, j]))
                    {
                        if (human.MakeShot(new int[] { i, j }, out wound, out shipPaint))
                        {
                            if (wound)
                            {
                                aiPanels[i, j].BackColor = Color.Orange;
                                aiPanels[i, j].Enabled = false;
                            }
                            else
                            {
                                foreach (var coord in shipPaint.ListOfCoordinates)
                                {
                                    aiPanels[coord[0], coord[1]].BackColor = Color.Red;
                                    aiPanels[coord[0], coord[1]].Enabled = false;
                                }
                                for (int k = 0; k < 10; k++)
                                {
                                    for (int l = 0; l < 10; l++)
                                    {
                                        if (AiField.weightArr[k, l] == 0 && aiPanels[k, l].BackColor != Color.Red)
                                        {
                                            aiPanels[k, l].BackColor = Color.Gray;
                                            aiPanels[k, l].Enabled = false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            aiPanels[i, j].BackColor = Color.Gray;
                            aiPanels[i, j].Enabled = false;
                        }
                    }
                }
            }
            int[] panelCoord;
            bool hit;
            ai.MakeShot(out panelCoord, out hit, out wound, out shipPaint);
            if (hit)
            {
                if (wound)
                    humanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Orange;
                else
                {
                    foreach (var coord in shipPaint.ListOfCoordinates)
                    {
                        humanPanels[coord[0], coord[1]].BackColor = Color.Red;
                    }
                }
            }
            else
            {
                humanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Gray;
            }
        }

        private void humanPanel_Click(object sender, EventArgs e)
        {

            label3.Text = $"����������� ������� � {humanShips[humanShipsCount].countDecks} ��������";
            Panel pnl = sender as Panel;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (humanPanels[i, j].Equals(pnl))
                    {
                        if (first && humanfield.weightArr[i, j] != 0)
                        {
                            humanfield.weightArr[i, j] = 10;
                            first = false;
                        }
                        if (human.setShips(new[] { i, j }, humanShipsCount))
                        {
                            pnl.BackColor = Color.Blue;
                            click++;
                        }
                        else
                        {
                            MessageBox.Show("� ���� ���� ���������� ������� ������!");
                        }
                        if (click == humanShips[humanShipsCount].countDecks && humanShipsCount != humanShips.Count())
                        {
                            humanShipsCount++;
                            click = 0;
                            first = true;
                        }
                        if (humanShipsCount == humanShips.Count())
                        {
                            label3.Text = "����������� ��������!\n��� ������ ��� ������� ������ \"������ ���\".";
                            buttonStartBattle.Enabled = true;
                            for (int k = 0; k < 10; k++)
                            {
                                for (int l = 0; l < 10; l++)
                                {
                                    humanPanels[k, l].Enabled = false;
                                }
                            }
                            ai.SetShips(); // ����������� �������� ����.
                        }
                        return;
                    }
                }
            }
        }

        private void newGame()
        {
            humanShipsCount = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    humanPanels[i, j].BackColor = Color.White;
                    aiPanels[i, j].BackColor = Color.White;
                    humanfield.weightArr[i, j] = 1;
                    AiField.weightArr[i, j] = 1;
                }
            }
            foreach (var item in humanShips)
            {
                item.ListOfCoordinates.Clear();
            }
            foreach (var item in aiShips)
            {
                item.ListOfCoordinates.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] panelCoord;
            bool hit;
            /* ai.MakeShot(out panelCoord, out hit);
             if (hit)
             {
                 humanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Red;
             }
             else
             {
                 humanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Gray;
             }*/

        }

        private void ��������������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
            ai.SetShips(); // ����������� �������� ����.
            human.AutoSetShips();
            foreach (var ship in humanShips)
            {
                for (int i = 0; i < ship.ListOfCoordinates.Count; i++)
                {
                    humanPanels[ship.ListOfCoordinates[i][0], ship.ListOfCoordinates[i][1]].BackColor = Color.Blue;
                }
            }
            buttonStartBattle.Enabled = true;
        }

        private void ���������������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
            foreach (var panel in humanPanels)
            {
                panel.Enabled = true;
                label3.Text = $"����������� ������� � ������� �������� �����";
            }
            buttonStartBattle.Enabled = true;
        }

        private void buttonStartBattle_Click(object sender, EventArgs e)
        {
            foreach (var panel in aiPanels)
            {
                panel.Enabled = true;
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    humanfield.weightArr[i, j] = 1;
                    AiField.weightArr[i, j] = 1;
                }
            }
        }
    }
}