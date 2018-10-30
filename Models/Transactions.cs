using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Item { get; set; }
        public int? Price { get; set; }
        public DateTimeOffset? InsertTime { get; set; }
    }
}
