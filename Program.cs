using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchfight
{
    public class Program
    {
        public const string BASE_URI = "https://www.googleapis.com/customsearch/v1?";
        public const string API_KEY = "AIzaSyBFJO24RZyDVPkh5Lgve5us8zEZws3KEzA";

        public static void Main(string[] args)
        {
            //google variables
            string googleWinner = "";
            long googleResults = 0;
            long googleTotal = 0;
            string cxGoogle = "017517188320842375054:kud1ri4uwg7"; //google search engine ID

            //bing variables
            string bingWinner = "";
            long bingResults = 0;
            long bingTotal = 0;
            string cxBing = "017517188320842375054:vrm5pgrcyb9"; //bing search engine ID

            //yahoo variables
            string yahooWinner = "";
            long yahooResults = 0;
            long yahooTotal = 0;
            string cxYahoo = "017517188320842375054:f9zjx77jfkk"; //yahoo search engine ID

            //total variables
            string totalWinner = "";
            long queryTotal = 0;

            if (args.Length > 0)
            {
                //initialize rest client
                RestClient.InitializeClient();

                //query search engines
                char[] charsToTrim = { '"' };
                Task<long> task;

                try
                {
                    foreach (String str in args)
                    {
                        //trim double quotes
                        string queryText = str.Trim(charsToTrim);

                        //google search
                        QueryProcessor oQueryGoogle = new QueryProcessor(BASE_URI, API_KEY, cxGoogle, queryText);
                        task = oQueryGoogle.GetResults();
                        googleResults = task.Result;
                        if (googleResults > googleTotal) //check google winner
                        {
                            googleTotal = googleResults;
                            googleWinner = queryText;
                        }

                        //bing search
                        QueryProcessor oQueryBing = new QueryProcessor(BASE_URI, API_KEY, cxBing, queryText);
                        task = oQueryBing.GetResults();
                        bingResults = task.Result;
                        if (bingResults > bingTotal) //check bing winner
                        {
                            bingTotal = bingResults;
                            bingWinner = queryText;
                        }

                        //yahoo search
                        QueryProcessor oQueryYahoo = new QueryProcessor(BASE_URI, API_KEY, cxYahoo, queryText);
                        task = oQueryYahoo.GetResults();
                        yahooResults = task.Result;
                        if (yahooResults > yahooTotal) //check yahoo winner
                        {
                            yahooTotal = yahooResults;
                            yahooWinner = queryText;
                        }

                        if (googleResults + bingResults + yahooResults > queryTotal) //check total winner
                        {
                            queryTotal = googleResults + bingResults + yahooResults;
                            totalWinner = queryText;
                        }

                        printResultsByWord(queryText, googleResults, bingResults, yahooResults);

                    }

                    printWinners(googleWinner, bingWinner, yahooWinner, totalWinner);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No arguments found.");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();
        }

        public static void printResultsByWord(string text, long googleResults, long bingResults, long yahooResults)
        {
            Console.WriteLine(text + ":");
            Console.WriteLine("Google: " + String.Format("{0:n0}", googleResults));
            Console.WriteLine("Bing: " + String.Format("{0:n0}", bingResults));
            Console.WriteLine("Yahoo: " + String.Format("{0:n0}", yahooResults));
            Console.WriteLine("##################################################");
        }

        public static void printWinners(string googleWinner, string bingWinner, string yahooWinner, string totalWinner)
        {
            Console.WriteLine("Google winner: " + googleWinner);
            Console.WriteLine("Bing winner: " + bingWinner);
            Console.WriteLine("Yahoo winner: " + yahooWinner);
            Console.WriteLine("\nTotal winner: " + totalWinner);
        }
    }
}
