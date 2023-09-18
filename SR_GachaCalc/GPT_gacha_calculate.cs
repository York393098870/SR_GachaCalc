using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SR_GachaCalc
{
    public class Program
    {
        private const double baseProbability = 0.006;
        private const double increaseProbability = 0.06;

        public static void CalculateGacha()
        {
            var maxTries = 100;
            Dictionary<int, double> probabilityDistribution = new Dictionary<int, double>();

            for (int tries = 1; tries <= maxTries; tries++)
            {
                double cumulativeProbability = SimulateGacha(tries);
                probabilityDistribution[tries] = cumulativeProbability;
            }

            SaveProbabilityDistribution(probabilityDistribution);
            Console.WriteLine($"Graph saved to desktop.");
            Console.ReadKey();
        }

        static double SimulateGacha(int tries)
        {
            double cumulativeProbability = 0;
            int m = 0;
            int n = 0;

            for (int i = 0; i < tries; i++)
            {
                double currentProbability = GetProbability(n);
                cumulativeProbability += (1 - cumulativeProbability) * currentProbability;

                m++;
                n++;

                if (Math.Abs(currentProbability - 1.0) < 0.01 || (new Random().NextDouble() < currentProbability))
                {
                    n = 0;
                }
            }

            return cumulativeProbability;
        }

        static double GetProbability(int n)
        {
            if (n >= 0 && n <= 73)
            {
                return baseProbability;
            }
            else if (n >= 74 && n <= 89)
            {
                return baseProbability + (n - 73) * increaseProbability;
            }
            else if (n == 90)
            {
                return 1.0;
            }
            else
            {
                throw new Exception("Invalid value of n.");
            }
        }

        static void SaveProbabilityDistribution(Dictionary<int, double> probabilityDistribution)
        {
            int width = 800;
            int height = 600;
            using (Bitmap bmp = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);

                    // Draw the axes
                    g.DrawLine(Pens.Black, 50, 50, 50, height - 50);
                    g.DrawLine(Pens.Black, 50, height - 50, width - 50, height - 50);

                    // Draw the curve
                    Point[] points = probabilityDistribution
                        .Select(kv => new Point(50 + kv.Key * 7, height - 50 - (int)(kv.Value * 500))).ToArray();
                    g.DrawLines(Pens.Blue, points);

                    // Save to desktop
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    bmp.Save($"{desktopPath}\\ProbabilityDistribution.png");
                }
            }
        }
    }
}