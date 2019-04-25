using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockstep
{
    public class PosData
    {
        public int x = 200;
        public int y = 0;
        public int d = 1;
    }
    public class LevelData
    {
        public int minX = -450;
        public int maxX = 450;
        public int maxY = 250; 
    }

    public class LevelDataWrapper
    {
        public int id;
        public LevelData info = new LevelData();
    }

}
