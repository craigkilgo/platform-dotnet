using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public partial class Sigma
    {
        public List<Nodes> nodes { get; set; }
        public List<Edges> edges { get; set; }

        public int[] ToVertices(){
            List<int> rtn = new List<int>();

            foreach(Nodes n in nodes){
                rtn.Add(Int32.Parse(n.id));
            }

            return rtn.ToArray();
        }

        public Tuple<int, int>[] ToTuples(){
            List<Tuple<int, int>> rtn = new List<Tuple<int, int>>();

            foreach(Edges e in edges){
                rtn.Add(Tuple.Create(Int32.Parse(e.source),Int32.Parse(e.target)));
            }

            return rtn.ToArray();
        }
    }
}
