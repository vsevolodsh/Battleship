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
        //Ship[] shipArr;
       public Dictionary<int, int[]> weightDict = new();

        //public Field(Ship[] shipArr)
        //{
        //    this.shipArr = shipArr;
        //}


        public void fillWeightDict()
        {
            int key = 0;
           bool isDictNotEmpty =  weightDict.TryGetValue(key, out int[] value);
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
    }
}
