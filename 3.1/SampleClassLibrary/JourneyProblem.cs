using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleClassLibrary
{
    public class Location
    {
        public string Name { get; set; }
        public List<Distance> Distances { get; set; }
    }

    public class Distance
    {
        public Distance(Location a, Location b, int miles)
        {
            Locations = new List<Location> { a, b };
            MileAway = miles;
        }
        public int MileAway { get; set; }
        public List<Location> Locations { get; set; }
    }

    public class JourneyProblem
    {
        public List<Location> PrepareData()
        {
            var a = new Location { Name = "Bei Jing" };
            var b = new Location { Name = "Tian Jin" };
            var c = new Location { Name = "Shang Hai" };
            var d = new Location { Name = "Wu Han" };
            var e = new Location { Name = "Cheng Du" };

            var ab = new Distance(a, b, 120);
            var ac = new Distance(a, c, 1200);
            var ad = new Distance(a, d, 900);
            var ae = new Distance(a, e, 1300);
            var bc = new Distance(b, c, 1100);
            var bd = new Distance(b, d, 800);
            var be = new Distance(b, e, 1400);
            var cd = new Distance(c, d, 400);
            var ce = new Distance(c, e, 1000);
            var de = new Distance(d, e, 800);

            a.Distances = new List<Distance> { ab, ac, ad, ae };
            b.Distances = new List<Distance> { ab, bc, bd, be };
            c.Distances = new List<Distance> { ac, bc, cd, ce };
            d.Distances = new List<Distance> { ad, bd, cd, de };
            e.Distances = new List<Distance> { ae, be, ce, de };

            return new List<Location> { a, b, c, d, e };
        }

        public List<Distance> GetShortestFullPath(List<Location> Locations, Location BeginFrom = null)
        {
            var paths = new List<List<Distance>>();

            if (BeginFrom != null)
            {
                if (!Locations.Contains(BeginFrom))
                {
                    throw new Exception("We need a start location in all locations");
                }

                paths = GetDistanceList(BeginFrom, Locations);
            }
            else
            {
                paths = Locations.SelectMany(l => GetDistanceList(l, Locations)).ToList();
            }

            var index = 0;
            foreach (var path in paths)
            {
                index++;
                Console.WriteLine($"{index}: {string.Join(", ", path.SelectMany(i => i.Locations).Select(j => j.Name))}");
            }

            var min = paths.Min(i => i.Sum(j => j.MileAway));
            var shortestPath = paths.Where(i => i.Sum(j => j.MileAway) == min).FirstOrDefault();

            return shortestPath;
        }

        public List<List<Distance>> GetDistanceList(Location from, List<Location> locations, List<List<Distance>> paths = null , List<Distance> path = null)
        {
            paths = paths ?? new List<List<Distance>>();
            path = path ?? new List<Distance>();

            foreach (var distance in from.Distances)
            {
                var currentPath = new List<Distance>();
                currentPath.AddRange(path);
                var to = distance.Locations.Except(new[] { from }).SingleOrDefault();

                if (!currentPath.SelectMany(i => i.Locations).Contains(to))
                {
                    currentPath.Add(distance);
                    GetDistanceList(to, locations, paths, currentPath);
                }
                else if (currentPath.SelectMany(i => i.Locations).Count() == (locations.Count() - 1) * 2)
                {
                    paths.Add(currentPath);
                    break;
                }
            }

            return paths;
        }
    }
}