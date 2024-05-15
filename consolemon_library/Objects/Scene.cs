using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolemon_console
{
    internal class Scene
    {
        public int sceneIndex { get; }
        public string map { get; }

        public Scene(int sceneIndex, string map) 
        { 
            this.sceneIndex = sceneIndex;
            this.map = map;
        }
    }
}
