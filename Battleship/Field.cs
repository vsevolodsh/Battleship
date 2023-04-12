using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Field
    {
        public int[,] weightArr = new int[,]
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
        public Dictionary<int, int[]> weightDict = new();


        public void fillWeightDict()
        {
            int key = 0;
            bool isDictNotEmpty = weightDict.TryGetValue(key, out int[] value);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (!isDictNotEmpty)
                    {
                        weightDict.Add(key, new int[] { i, j });
                        weightDict.TryGetValue(key, out value);
                        isDictNotEmpty = true;
                    }
                    else if (weightArr[i, j] > weightArr[value[0], value[1]])
                    {
                        weightDict.Clear();
                        key = 0;
                        weightDict.Add(key, new int[] { i, j });
                        weightDict.TryGetValue(key, out value);
                    }
                    else if (weightArr[i, j] == weightArr[value[0], value[1]])
                    {
                        key++;
                        weightDict.Add(key, new int[] { i, j });
                    }
                }
            }
        }

        public void updateFieldWeight(int[] XYcoordinates, Ship ship, bool fight)
        {
            weightArr[XYcoordinates[0], XYcoordinates[1]] *= 0;

            if (XYcoordinates[0] != 0 && XYcoordinates[0] != 9 && XYcoordinates[1] != 0 && XYcoordinates[1] != 9)
            {
                weightArr[XYcoordinates[0] - 1, XYcoordinates[1] - 1] *= 0;
                weightArr[XYcoordinates[0] + 1, XYcoordinates[1] - 1] *= 0;
                weightArr[XYcoordinates[0] - 1, XYcoordinates[1] + 1] *= 0;
                weightArr[XYcoordinates[0] + 1, XYcoordinates[1] + 1] *= 0;
                weightArr[XYcoordinates[0] - 1, XYcoordinates[1]] *= 10;
                weightArr[XYcoordinates[0] + 1, XYcoordinates[1]] *= 10;
                weightArr[XYcoordinates[0], XYcoordinates[1] - 1] *= 10;
                weightArr[XYcoordinates[0], XYcoordinates[1] + 1] *= 10;
            }
            else if (XYcoordinates[1] == 0)
            {
                weightArr[XYcoordinates[0], XYcoordinates[1] + 1] *= 10;

                if (XYcoordinates[0] != 0 && XYcoordinates[0] != 9)
                {
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1] + 1] *= 0;
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1] + 1] *= 0;
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1]] *= 10;
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1]] *= 10;
                }
                else if (XYcoordinates[0] == 0)
                {
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1] + 1] *= 0;
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1]] *= 10;
                }
                else if (XYcoordinates[0] == 9)
                {
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1] + 1] *= 0;
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1]] *= 10;
                }
            }
            else if (XYcoordinates[1] == 9)
            {
                weightArr[XYcoordinates[0], XYcoordinates[1] - 1] *= 10;

                if (XYcoordinates[0] != 0 && XYcoordinates[0] != 9)
                {
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1] - 1] *= 0;
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1] - 1] *= 0;
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1]] *= 10;
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1]] *= 10;
                }
                else if (XYcoordinates[0] == 0)
                {
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1] - 1] *= 0;
                    weightArr[XYcoordinates[0] + 1, XYcoordinates[1]] *= 10;
                }
                else if (XYcoordinates[0] == 9)
                {
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1] - 1] *= 0;
                    weightArr[XYcoordinates[0] - 1, XYcoordinates[1]] *= 10;
                }
            }
            else if (XYcoordinates[0] == 0)
            {
                weightArr[XYcoordinates[0] + 1, XYcoordinates[1] - 1] *= 0;
                weightArr[XYcoordinates[0] + 1, XYcoordinates[1] + 1] *= 0;
                weightArr[XYcoordinates[0] + 1, XYcoordinates[1]] *= 10;
                weightArr[XYcoordinates[0], XYcoordinates[1] - 1] *= 10;
                weightArr[XYcoordinates[0], XYcoordinates[1] + 1] *= 10;
            }
            else if (XYcoordinates[0] == 9)
            {
                weightArr[XYcoordinates[0] - 1, XYcoordinates[1] - 1] *= 0;
                weightArr[XYcoordinates[0] - 1, XYcoordinates[1] + 1] *= 0;
                weightArr[XYcoordinates[0] - 1, XYcoordinates[1]] *= 10;
                weightArr[XYcoordinates[0], XYcoordinates[1] - 1] *= 10;
                weightArr[XYcoordinates[0], XYcoordinates[1] + 1] *= 10;
            }
            if ((ship.countDecks == ship.ListOfCoordinates.Count() || !ship.isAlive) && !fight)
            {
                if (ship.ListOfCoordinates[0][0] == ship.ListOfCoordinates[1][0])
                {
                    for(int i = 0; i < ship.ListOfCoordinates.Count(); i++)
                    {
                        if (ship.ListOfCoordinates[i][1] != 0 && ship.ListOfCoordinates[i][1] != 9)
                        {
                            weightArr[ship.ListOfCoordinates[i][0], ship.ListOfCoordinates[i][1] + 1] *= 0;
                            weightArr[ship.ListOfCoordinates[i][0], ship.ListOfCoordinates[i][1] - 1] *= 0;
                        }
                        else if (ship.ListOfCoordinates[i][1] == 0)
                        {
                            weightArr[ship.ListOfCoordinates[i][0], ship.ListOfCoordinates[i][1] + 1] *= 0;
                        }
                        else if (ship.ListOfCoordinates[i][1] == 9)
                        {
                            weightArr[ship.ListOfCoordinates[i][0], ship.ListOfCoordinates[i][1] - 1] *= 0;
                        }
                    }
                }
                else if (ship.ListOfCoordinates[0][1] == ship.ListOfCoordinates[1][1])
                {
                    for (int i = 0; i < ship.ListOfCoordinates.Count(); i++)
                    {
                        if (ship.ListOfCoordinates[i][0] != 0 && ship.ListOfCoordinates[i][0] != 9)
                        {
                            weightArr[ship.ListOfCoordinates[i][0] - 1, ship.ListOfCoordinates[i][1]] *= 0;
                            weightArr[ship.ListOfCoordinates[i][0] + 1, ship.ListOfCoordinates[i][1]] *= 0;
                        }
                        else if (ship.ListOfCoordinates[i][0] == 0)
                        {
                            weightArr[ship.ListOfCoordinates[i][0] + 1, ship.ListOfCoordinates[i][1]] *= 0;
                        }
                        else if (ship.ListOfCoordinates[i][0] == 9)
                        {
                            weightArr[ship.ListOfCoordinates[i][0] - 1, ship.ListOfCoordinates[i][1]] *= 0;
                        }
                    }
                }
            }
        }
    }
}
