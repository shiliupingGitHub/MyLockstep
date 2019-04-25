using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class Game:SingleTon<Game>
    {
        public void Init()
        {
            JsonMapper.RegisterExporter<float>((obj, writer) => writer.Write(Convert.ToDouble(obj)));
            JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
            lockstep.Controller.Instance.Init();
            lockstep.ConfigManager.Instance.Load();
        }
    }
}
