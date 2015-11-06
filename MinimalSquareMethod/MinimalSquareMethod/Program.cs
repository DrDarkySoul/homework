using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquareMethod
{
    class Program
    {
        public class Line_func
        {
            private Double a_;
            private Double b_;

            public Double coord_a_
            {
                get { return a_; }
                set { a_ = value; }
            }
            public Double coord_b_
            {
                get { return b_; }
                set { b_ = value; }
            }

            public Line_func()
            {
                coord_a_ = 0;
                coord_b_ = 0;
            }

            public Line_func(List<Double> x_a, List<Double> y_a)
            {
                if (x_a.Count != y_a.Count)
                    throw new Exception(" Массивы должны быть одной длинны!");
                else
                {
                    Double x_mid = 0;
                    Double y_mid = 0;
                    Double xx_mid = 0;
                    Double xy_mid = 0;

                    Int64 x_sum = 0;
                    Int64 y_sum = 0;
                    Int64 xx_sum = 0;
                    Int64 xy_sum = 0;

                    Int32 N = x_a.Count;
                    Double[] xx_a = new Double[N];
                    Double[] xy_a = new Double[N];

                    for (int i = 0; i < N; i++)
                    {
                        xx_a[i] = Math.Pow(x_a[i], 2);
                        xy_a[i] = x_a[i] * y_a[i];

                        x_sum += (Int64)x_a[i];
                        y_sum += (Int64)y_a[i];
                        xx_sum += (Int64)xx_a[i];
                        xy_sum += (Int64)xy_a[i];
                    }

                    x_mid = (Double)x_sum / N;
                    y_mid = (Double)y_sum / N;
                    xx_mid = (Double)xx_sum / N;
                    xy_mid = (Double)xy_sum / N;

                    coord_a_ = (xy_mid - (y_mid * x_mid)) / (xx_mid - Math.Pow(x_mid, 2));
                    coord_b_ = ((xx_mid * y_mid) - (x_mid * xy_mid)) / (xx_mid - Math.Pow(x_mid, 2));
                }
            }
            public void Print()
            {
                Console.WriteLine(" {0:F} * X + {1:F}", a_, b_); 
            }
        }


        static void Main(string[] args)
        {

            int[] x_a = { 167, 169, 170, 170, 172, 173, 174, 175, 179, 180 };
            int[] y_a = { 169, 171, 166, 172, 180, 176, 177, 182, 182, 186 };

            List<Double> coordinates_x = new List<Double>() { 167, 169, 170, 170, 172, 173, 174, 175, 179, 180 };
            List<Double> coordinates_y = new List<Double>() { 169, 171, 166, 172, 180, 176, 177, 182, 182, 186 };

            Line_func line = new Line_func(coordinates_x, coordinates_y);
            line.Print();
        }
    }
}
