using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2009.QualificationRound
{
    public class AAlienLanguage
    {
        public enum InputType
        {
            Small,
            Large
        }
        string[] input;
        int L, D, N;
        InputType inputType;
        string fileName;

        public AAlienLanguage()
        {
            inputType = InputType.Large;
            switch(inputType)
            {
                case InputType.Small:
                    fileName = "A-small-practice";
                    break;
                case InputType.Large:
                    fileName = "A-large-practice";
                    break;
            }
            
            input = File.ReadAllLines($"2009/QualificationRound/{fileName}.in");
            ProcessPuzzle();
        }

        class Node
        {
            public char text;
            public List<Node> next;
            public bool isEnd;
            public Node()
            {
                next = new List<Node>();
            }
        }

        static class Lang
        {
            static List<Node> dictionary = new List<Node>();

            public static void AddWord(string input)
            {
                var coll = dictionary;
                Node newLetter = new Node();
                foreach (char letter in input)
                {
                    newLetter = new Node();
                    var matchingLetter = coll.Where(l => l.text == letter);
                    if (matchingLetter.Count() == 0)
                    {
                        newLetter.text = letter;
                        coll.Add(newLetter);
                        coll = newLetter.next;
                    }
                    else
                    {
                        coll = matchingLetter.First().next;
                    }
                }
                newLetter.isEnd = true;
            }

            public static int PatternMatch(string pattern)
            {
                bool isCollection = false;
                IEnumerable<Node> matchedPattern;
                List<List<char>> letterColl = new List<List<char>>();
                List<char> charList = new List<char>();
                
                foreach (char letter in pattern)
                {
                    if (letter == '(')
                    {
                        isCollection = true;
                        if (charList.Count != 0)
                        {
                            letterColl.Add(charList);
                        }
                        charList = new List<char>();
                        continue;
                    }

                    if (letter == ')')
                    {
                        isCollection = false;
                        if (charList.Count != 0)
                        {
                            letterColl.Add(charList);
                        }
                        charList = new List<char>();
                        continue;
                    }

                    charList.Add(letter);
                    if (!isCollection)
                    {
                        if (charList.Count != 0)
                        {
                            letterColl.Add(charList);
                        }
                        charList = new List<char>();
                    }
                }

                var coll = dictionary;
                int result = 0;

                foreach (var item in letterColl)
                {
                    matchedPattern = coll.Where(n => item.Contains(n.text));
                    if (matchedPattern.Count() == 0)
                    {
                        return 0;
                    }
                    coll = matchedPattern.SelectMany(m => m.next).ToList();
                    result = matchedPattern.Count();
                }
                return result;
            }
        }

        private void ProcessPuzzle()
        {
            int i = 0;
            var casesInfo = input[i].Split(new char[] { ' ' });
            L = Convert.ToInt32(casesInfo[0]);
            D = Convert.ToInt32(casesInfo[1]);
            N = Convert.ToInt32(casesInfo[2]);

            for (int j = 1; j <= D; j++)
            {
                Lang.AddWord(input[++i]);
            }

            StringBuilder result = new StringBuilder();
            for (int j = 1; j <= N; j++)
            {
                var output = string.Format("Case #{0}: {1}", j, Lang.PatternMatch(input[++i]));
                result.AppendLine(output);
                Console.WriteLine(output);
                
            }
            File.WriteAllText($"2009/QualificationRound/{fileName}.out", result.ToString());
            Console.ReadLine();
        }
    }
}

