using System.Drawing;
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

        int liveHumanShipsCount = 10;
        int liveAiShipsCount = 10;

        int humanShipsCount = 0;
        int click = 0;
        bool first = true;
        bool isHumanOrAiShot = true;
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

        private async void aiPanel_Click(object sender, EventArgs e)
        {
            Panel pnl = sender as Panel;
            bool wound;
            Ship shipPaint;
            bool isHumanShot = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (pnl.Equals(aiPanels[i, j]))
                    {
                        isHumanShot = human.MakeShot(new int[] { i, j }, out wound, out shipPaint);
                        if (isHumanShot)
                        {
                            if (wound)
                            {
                                aiPanels[i, j].BackColor = Color.Orange;
                                aiPanels[i, j].Enabled = false;
                            }
                            else
                            {
                                liveAiShipsCount--;
                                foreach (var coord in shipPaint.ListOfCoordinates)
                                {
                                    aiPanels[coord[0], coord[1]].BackColor = Color.Red;
                                    aiPanels[coord[0], coord[1]].Enabled = false;
                                }
                                for (int k = 0; k < 10; k++)
                                {
                                    for (int l = 0; l < 10; l++)
                                    {
                                        if (AiField.weightArr[k, l] == 0 && aiPanels[k, l].BackColor != Color.Red
                                            && aiPanels[k, l].BackColor != Color.Orange)
                                        {
                                            aiPanels[k, l].BackColor = Color.Gray;
                                            aiPanels[k, l].Enabled = false;
                                        }
                                    }
                                }
                                if (liveAiShipsCount == 0)
                                {
                                    MessageBox.Show("Бой окончен. Вы победили!");
                                    foreach (var panel in aiPanels)
                                        panel.Enabled = false;
                                    return;
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
            if (!isHumanShot)
            {
                foreach (var panel in aiPanels)
                    panel.Enabled = false;
                bool cont;
                do
                {
                    await Task.Delay(500);
                    cont = aiShot();
                } while (cont);
                foreach (var panel in aiPanels)
                    panel.Enabled = true;
            }
        }

        private bool aiShot()
        {
            int[] panelCoord;
            bool hit;
            bool wound;
            Ship shipPaint;
            ai.MakeShot(out panelCoord, out hit, out wound, out shipPaint);
            if (hit)
            {
                if (wound)
                {
                    humanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Orange;
                }
                else
                {
                    liveHumanShipsCount--;
                    foreach (var coord in shipPaint.ListOfCoordinates)
                    {
                        humanPanels[coord[0], coord[1]].BackColor = Color.Red;
                    }
                    if (liveHumanShipsCount == 0)
                    {
                        MessageBox.Show("Бой окончен.Вы проиграли!");
                        foreach (var panel in aiPanels)
                            panel.Enabled = false;
                        return false;
                    }
                }
                return true;
            }
            else
            {
                humanPanels[panelCoord[0], panelCoord[1]].BackColor = Color.Gray;
                return false;
            }

        }



        private void humanPanel_Click(object sender, EventArgs e)
        {

            label3.Text = $"Расположите корабль с {humanShips[humanShipsCount].countDecks} палубами";
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
                            MessageBox.Show("В этом поле разместить корабль нельзя!");
                        }
                        if (click == humanShips[humanShipsCount].countDecks && humanShipsCount != humanShips.Count())
                        {
                            humanShipsCount++;
                            click = 0;
                            first = true;
                        }
                        if (humanShipsCount == humanShips.Count())
                        {
                            label3.Text = "Расстановка окончена!\nДля начала боя нажмите кнопку \"Начать бой\".";
                            buttonStartBattle.Enabled = true;
                            for (int k = 0; k < 10; k++)
                            {
                                for (int l = 0; l < 10; l++)
                                {
                                    humanPanels[k, l].Enabled = false;
                                }
                            }
                            ai.SetShips(); // расстановка кораблей бота.
                        }
                        return;
                    }
                }
            }
        }

        private void newGame()
        {
            humanShipsCount = 0;
            liveAiShipsCount = 10;
            liveHumanShipsCount = 10;
            first = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    humanPanels[i, j].BackColor = Color.White;
                    aiPanels[i, j].BackColor = Color.White;
                    AiField.weightArr[i, j] = 1;
                    humanfield.weightArr[i, j] = 1;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                humanShips[i] = new Ship(humanShips[i].countDecks);
                aiShips[i] = new Ship(aiShips[i].countDecks);
            }
        }

        private void начатьНовуюИгруСоСлучайнойРасстановкойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
            ai.SetShips(); // расстановка кораблей бота.
            human.AutoSetShips();
            foreach (var ship in humanShips)
            {
                for (int i = 0; i < ship.ListOfCoordinates.Count; i++)
                {
                    humanPanels[ship.ListOfCoordinates[i][0], ship.ListOfCoordinates[i][1]].BackColor = Color.Blue;
                }
            }
            buttonStartBattle.Enabled = true;
            label3.Text = "Расстановка окончена!\nДля начала боя нажмите кнопку \"Начать бой\".";
        }

        private void начатьИгруСПользовательскойРасстановкойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
            foreach (var panel in humanPanels)
            {
                panel.Enabled = true;
                label3.Text = $"Расположите корабли в порядке убываний палуб";
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
                    AiField.weightArr[i, j] = 1;
                }
            }
            humanfield.weightArr = new int[,]
            {
            {5,1,5,1,1,1,4,1,4,1},
            {1,5,1,5,1,1,1,4,1,4},
            {5,1,5,1,1,1,4,1,4,1},
            {1,5,1,5,1,1,1,4,1,4},
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1},
            {1,3,1,3,1,1,2,1,2,1},
            {3,1,3,1,1,1,1,2,1,2},
            {1,3,1,3,1,1,2,1,2,1},
            {3,1,3,1,1,1,1,2,1,2},
            };
        }
    }
}