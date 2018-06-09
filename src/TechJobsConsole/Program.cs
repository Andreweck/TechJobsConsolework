using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            
            
                StreamReader needabook = new StreamReader(@"c:\Users\usr\source\repos\TechJobsConsole\src\TechJobsConsole\job_data.csv");
            
                List<string> preList = new List<string>();

                List<string[]> final_list = new List<string[]>();

                List<string> listValue = new List<string> { };

                StringBuilder arrayValue = new StringBuilder();

                bool isinQuotes = false;

                List<Dictionary<string, string>> finalData = new List<Dictionary<string, string>>();

                Dictionary<string, string> dictValue = new Dictionary<string, string>();

            List<string> searchList = new List<string>();

                while (needabook.Peek() > 0)
                {
                    preList.Add(needabook.ReadLine());
                }

            foreach (string i in preList)
            {
                foreach (char a in i)
                {
                    if (a.Equals('"'))
                    {
                        isinQuotes = !isinQuotes;
                        arrayValue.Append(a);
                    }
                    else if (a.Equals(','))
                    {
                        if (isinQuotes == false)
                        {
                            listValue.Add(arrayValue.ToString());
                            arrayValue.Clear();
                        }
                        else
                        {
                            arrayValue.Append(a);
                        }
                    }
                    else
                    {
                        arrayValue.Append(a);
                    }

                }
                listValue.Add(arrayValue.ToString());
                arrayValue.Clear();
                final_list.Add(listValue.ToArray());
                listValue = new List<string>();
            }

            string[] firstPostList = final_list[0];

            final_list.Remove(firstPostList);

            foreach (string[] i in final_list)

            {

                for (int a = 0; a < i.Count(); a++)

                {

                    dictValue.Add(firstPostList[a], i[a]);


                }
                finalData.Add(dictValue);
                dictValue = new Dictionary<string, string>();

            }
            if (someJobs.Count.Equals(0))
            {
                Console.WriteLine("There are no results to display!");
            }
            else
            {
                for (int z = 0; z < someJobs.Count(); z++)
                {
                    //if (someJobs.Contains(finalData[z]))
                    //{
                    List<string> lines = new List<string>(someJobs[z].Values);
                    List<string> lineKeys = new List<string>(someJobs[z].Keys);
                    for (int i = 0; i < lineKeys.Count(); i++)
                    {
                        Console.WriteLine(lineKeys[i] + ": " + lines[i]);
                    }
                    Console.WriteLine("**************************************");
                    //} 
                }

            }

            }
        }
}
