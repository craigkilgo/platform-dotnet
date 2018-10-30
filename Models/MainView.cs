using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using dotnet.Models;


namespace dotnet.Models.ViewModels {


    public class IndexViewModel
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public bool hashPresent { get; set; }
        public List<int> values { get; set; }
        public List<FiftyValuesView> fifty { get; set; }
        public List<Names> names { get; set; }
        public Sigma s { get; set; }
        public string path { get; set; }
    }


}