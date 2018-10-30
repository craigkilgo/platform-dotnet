using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using dotnet.Models.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace dotnet.Controllers
{
    public class HomeController : Controller
    {

        private readonly mainContext _context;
        public HomeController(mainContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<int> primes = sieve();
            ViewBag.primes = primes;

            dotnet.Models.ViewModels.IndexViewModel model = new dotnet.Models.ViewModels.IndexViewModel();

            //20 random values and min + max
            model.values = twentyRandom();
            int max = 0;
            int min = 10000000;
            foreach(int r in model.values){
                if(r > max){
                    max = r;
                }
                if(r<min){
                    min = r;
                }
            }
            model.Max = max;
            model.Min = min;

            //hash of values from database
            var fiftyValues = _context.Fiftyvalues.ToList();
            model.fifty = new List<FiftyValuesView>();

              foreach(Fiftyvalues v in fiftyValues){
                   FiftyValuesView fv = new FiftyValuesView();
                    fv.Value = v.Value;
                    fv.Id = v.Id;
                    fv.Hash = ComputeSha256Hash(v.Value);
                    model.fifty.Add(fv);
                }

            //names
            var names = _context.Names.ToList();
            var sorted_names = MergeSort(names);
            model.names = new List<Names>();
            model.names = sorted_names;

            //make sigma for display on page
            Sigma s = new Sigma();
            s.nodes = new List<Nodes>();
            s.edges = new List<Edges>();

            int i = 1;
            foreach(Names n in names){

                s.nodes.Add(new Nodes(n.Id.ToString()));
                string[] friends = n.Friends.Split(",");  
                foreach (string f in friends){
                    s.edges.Add(new Edges(i,n.Id.ToString(),f));
                    i++;
                }  
            }
            model.s = s;

            //bfs for shortest path


            //string pairs
            var hash = "acb80281e4e94213c7452a81fa08f61893eff5ffa62d50876da8d1fed4710d95";
            var strings = new List<Tuple<string, string>>();
            strings.Add(Tuple.Create("ethereal", "front"));
            strings.Add(Tuple.Create("ask", "release"));
            strings.Add(Tuple.Create("bucket", "unique"));
            strings.Add(Tuple.Create("plug", "average"));
            strings.Add(Tuple.Create("trade", "weather"));
            strings.Add(Tuple.Create("card", "wide"));
            strings.Add(Tuple.Create("numberless", "copper"));
            strings.Add(Tuple.Create("fruit", "example"));
            strings.Add(Tuple.Create("slap", "pause"));
            strings.Add(Tuple.Create("jittery", "confused"));

            model.hashPresent = checkForHash(hash, strings);


            //insert statement
            Transactions tx = new Transactions();
            tx.Customer = "Ben Hogan";
            tx.Price = 900;
            tx.Item = "Cleek";

            _context.Transactions.Add(tx);
            _context.SaveChanges();

            return View(model);
        }
        private static bool checkForHash(string Hash, List<Tuple<string, string>> pairs){
            bool rtn = false;
            foreach(Tuple<string, string> pair in pairs)
            {
                if(ComputeSha256Hash(String.Concat(pair.Item1,pair.Item2))==Hash){
                    rtn = true;
                }
            }
            return rtn;
        }
        private static List<Names> MergeSort(List<Names> unsorted)
        {
            if (unsorted.Count <= 1)
                return unsorted;

            List<Names> left = new List<Names>();
            List<Names> right = new List<Names>();

            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle;i++)  //Dividing the unsorted list
            {
                left.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                right.Add(unsorted[i]);
            }

            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }

        private static List<Names> Merge(List<Names> left, List<Names> right)
        {
            List<Names> result = new List<Names>();

            while(left.Count > 0 || right.Count>0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left[0].Name.CompareTo(right[0].Name)<1)  //Comparing First two elements to see which is smaller
                    {
                        result.Add(left.First());
                        left.Remove(left.First());      //Rest of the list minus the first element
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if(left.Count>0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());

                    right.Remove(right.First());
                }
            }
            return result;
        }


        public static List<int> sieve(){
            List<int> primes = new List<int>();

            //long sum = 0;
            long n = 500;
            bool[] e = new bool[n];//by default they're all false
            for (int i = 2; i < n; i++)
            {
                e[i] = true;//set all numbers to true
            }
            //weed out the non primes by finding mutiples
            for (int j = 2; j < n; j++)
            {
            if (e[j])//is true
            {
                for (long p = 2; (p*j) < n; p++)
                {
                    e[p * j] = false;
                }
            }
            }

            for (int k = 0; k < e.Length; k++){
                if(e[k]){
                    primes.Add(k);
                }
            }
            return primes;

        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static List<int> twentyRandom(){
            List<int> twentyRandom = new List<int>();
            Random rnd = new Random();
            for (int k = 0; k < 20; k++){
                twentyRandom.Add(rnd.Next(1,10000000));
            }


            return twentyRandom;
        }

    }
}
