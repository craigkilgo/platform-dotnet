using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class Edges
    {
        public int id { get; set; }
        public string source { get; set; }
        public string target { get; set; }
    
        public Edges(int id,string source, string target){
            this.id = id;
            this.source = source;
            this.target = target;
        }
    
    }
}
