using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LINQtoObjects
{
    class Program
    {
        static void Main()
        {
            LinkedList<FivePointedStar> starsFirst = new();

            starsFirst.AddLast(new FivePointedStar(25));
            starsFirst.AddLast(new FivePointedStar(15));
            starsFirst.AddLast(new FivePointedStar(20));
            starsFirst.AddLast(new FivePointedStar(5));
            starsFirst.AddLast(new FivePointedStar(30));

            starsFirst.Sort();

            starsFirst.WriteToFile("data.json", true);

            LinkedList<FivePointedStar> starsSecond = new();

            starsSecond.AddLast(new FivePointedStar(1));
            starsSecond.AddLast(new FivePointedStar(3));

            LinkedList<FivePointedStar> starsThird = new(starsSecond);

            starsSecond.AddLast(starsFirst[3]);
            starsThird.AddLast(new FivePointedStar(5));

            LinkedList<FivePointedStar>[] listsOfStars = { starsFirst, starsSecond, starsThird };

            var orderedLists = listsOfStars.OrderBy(x => x.Size).ToList();

            Console.WriteLine("Минимальная коллекция: ");

            foreach (var item in orderedLists.First())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Максимальная коллекция: ");

            foreach (var item in orderedLists.Last())
            {
                Console.WriteLine(item);
            }

            Console.Write("Введите длину стороны звезды: ");

            int sideLength = Convert.ToInt32(Console.ReadLine());

            int count = (from list in listsOfStars
                         where list.Any(star => star.SideLength == sideLength)
                         select list)
                         .Count();

            Console.WriteLine("Количество коллекций, в которых есть звезда с длиной " +
                $"стороны {sideLength} см, равно {count}.");

            var secondQuery = starsFirst
                .Where(star => star.SideLength >= 10)
                .OrderByDescending(star => star.SideLength)
                .Skip(1)
                .Take(2)
                .Select(star => star.SideLength);

            Console.WriteLine("Второй запрос:");

            foreach (var length in secondQuery)
            {
                Console.WriteLine(length);
            }

            var maxArea = starsSecond.Union(starsThird).Except(starsFirst).Max(star => star.Area);

            Console.WriteLine("Третий запрос: ");
            Console.WriteLine($"Площадь: {maxArea}");
        }
    }
}
