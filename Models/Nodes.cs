using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class Nodes
    {
        public string id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int size { get; set; }

        public Nodes(string id){
            Random rnd = new Random();

            this.id = id;
            this.x = rnd.Next(1,100);
            this.y = rnd.Next(1,100);
            this.size = 1;
        }

    }


}
