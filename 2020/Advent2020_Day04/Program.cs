using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Advent2020_Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            int validpp = 0;
            List<Match> pp = new List<Match>();
            List<List<Match>> pps = new List<List<Match>>();
            //string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day04\\Sample.txt";
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day04\\Input.txt";
            using (StreamReader file = new StreamReader(textFile))
            {
                string ln;
                Regex rex = new Regex(@"(\w{3}):(\S*)");
                while ((ln = file.ReadLine()) != null)
                {
                    if (ln != "")
                    {
                        foreach (Match match in rex.Matches(ln))
                        {
                            if (match.Groups[1].Value != "cid") pp.Add(match);
                        }
                    }
                    else // End of passport
                    {
                        if (pp.Count == 7) {
                            bool valid = true;
                            bool aa = false, bb = false, cc = false, dd = false, ee = false, ff = false, gg = false;
                            foreach (Match item in pp)
                            {
                                switch (item.Groups[1].Value)
                                {
                                    case "byr":
                                        int byr = int.Parse(item.Groups[2].Value);
                                        if (1920 > byr | byr > 2002) valid = false;
                                        aa = true;
                                        break;
                                    case "iyr":
                                        int iyr = int.Parse(item.Groups[2].Value);
                                        if (2010 > iyr | iyr > 2020) valid = false;
                                        bb = true;
                                        break;
                                    case "eyr":
                                        int eyr = int.Parse(item.Groups[2].Value);
                                        if (2020 > eyr | eyr > 2030) valid = false;
                                        cc = true;
                                        break;
                                    case "hgt":
                                        string type = item.Groups[2].Value.Substring(item.Groups[2].Value.Length - 2,2);
                                        if (type == "cm")
                                        {
                                            int hgt = int.Parse(item.Groups[2].Value.Substring(0, item.Groups[2].Value.Length - 2));
                                            if (150 > hgt | hgt > 193) valid = false;
                                        }
                                        else if (type == "in")
                                        {
                                            int hgt = int.Parse(item.Groups[2].Value.Substring(0, item.Groups[2].Value.Length - 2));
                                            if (59 > hgt | hgt > 76) valid = false;
                                        }
                                        else valid = false;
                                        dd = true;
                                        break;
                                    case "hcl":
                                        string hex = item.Groups[2].Value;
                                        if (hex.Length != 7 & Regex.IsMatch(hex.Substring(1, hex.Length - 1), @"#[0-9a-f]{6}") == false) valid = false;
                                        ee = true;
                                        break;
                                    case "ecl":
                                        string ecl = item.Groups[2].Value;
                                        if (ecl != "amb" & ecl != "blu" & ecl != "brn" & ecl != "gry" & ecl != "grn" & ecl != "hzl" & ecl != "oth") valid = false;
                                        ff = true;
                                        break;
                                    case "pid":
                                        string pid = item.Groups[2].Value;
                                        if (pid.Length == 9)
                                        {
                                            if (Regex.IsMatch(pid, @"[0-9]{9}") == false) valid = false;
                                        }
                                        else valid = false;
                                        gg = true;
                                        break;
                                    default: break;
                                }        
                            }
                            if (valid & aa & bb & cc & dd & ee & ff & gg)
                            {
                                validpp++;
                                pps.Add(pp);
                            }
                        }
                        pp = new List<Match>();
                    }
                }
                pp.Clear();
                file.Close();
            }
            Console.WriteLine($"{validpp} valid passports");
            foreach (List<Match> q in pps)
            {
                foreach (Match w in q)
                {
                    if (w.Groups[1].Value == "pid") Console.WriteLine(w.Value);
                }
            }
            Console.ReadKey();
        }
    }
}
