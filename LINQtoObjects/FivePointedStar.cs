using System;

namespace LINQtoObjects
{
    public class FivePointedStar : IComparable<FivePointedStar>
    {
        public double SideLength { get; }

        public double Area { get; private set; }

        public double Perimeter { get; private set; }

        public FivePointedStar(double sideLength)
        {
            if (sideLength <= 0)
            {
                throw new ArgumentException("Side length can't be negative or 0.");
            }

            SideLength = sideLength;
            CountPerimeter();
            CountArea();
        }

        private void CountPerimeter()
        {
            Perimeter = 10 * SideLength;
        }

        // Нахождение площади правильной пятиконечной звезды как суммы площадей 10-ти
        // треугольников и правильного пятиугольника
        private void CountArea()
        {
            double area = 5 * TriangleArea(36, SideLength, SideLength);
            double pentagonSide = ThirdSideOfTriangle(36, SideLength, SideLength);
            area += RegularPentagonArea(pentagonSide);
            Area = area;

            // Нахождение площади треугольника
            // Формула: площадь треугольника равна половине произведения его сторон 
            // на синус угла между ними
            static double TriangleArea(double angle, double firstSideLength,
                double secondSideLength)
            {
                return (1d / 2) * firstSideLength * secondSideLength * Math.Sin(Math.PI * angle / 180);
            }

            // Нахождение неизвестной стороны треугольника по теореме косинусов
            static double ThirdSideOfTriangle(double oppositeAngle, double firstSideLength,
                double secondSideLength)
            {
                return Math.Pow(Math.Pow(firstSideLength, 2) + Math.Pow(secondSideLength, 2) -
                    2 * firstSideLength * secondSideLength * Math.Cos(Math.PI * oppositeAngle / 180), 0.5);
            }

            // Нахождение площади правильного пятиугольника через его сторону
            static double RegularPentagonArea(double sideLength)
            {
                return 5 * Math.Pow(sideLength, 2) / (4 * Math.Tan(Math.PI * 36 / 180));
            }
        }

        public override string ToString()
        {
            return $"Five-pointed star with side length of {SideLength} cm, " +
                $"area of {Area} cm\u00b2 and perimeter of {Perimeter} cm.";
        }

        public int CompareTo(FivePointedStar other)
        {
            if (SideLength > other.SideLength)
            {
                return 1;
            }
            if (SideLength < other.SideLength)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
