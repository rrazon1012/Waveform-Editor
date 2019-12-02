using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace WindowsFormsApp2
{
    class DFT
    {
        double[] real;
        double[] imag;

        public double[] dft(double[] samples)
        {
            int N = samples.Length;
            real = new double[N];
            imag = new double[N];
            double[] result = new double[N];
            double pi = 2 * Math.PI / N;
            for (int f = 0; f < N; f++)
            {
                double a = f * pi;
                for (int t = 0; t < N; t++)
                {
                    real[f] += samples[t] * Math.Cos(a * t);
                    imag[f] -= samples[t] * Math.Sin(a * t);
                }

                result[f] = Math.Sqrt((real[f] * real[f]) + (imag[f] * imag[f])) / N;
                //Console.WriteLine("Frequency = " + f);
                //Console.WriteLine("Amplitude = " + result[f]);
                //Console.WriteLine("Phase Shift = " + tan(real[f], imag[f]));
                //Console.WriteLine(real[f] + " " + imag[f]);
            }
            return result;
        }

        public double[] paralleldft(double[] samples) {
            int N = samples.Length;
            real = new double[N];
            imag = new double[N];
            double[] result = new double[N];
            double pi = 2 * Math.PI / N;
            Parallel.For(0, N, f =>
            {
                double a = f * pi;
                for (int t = 0; t < N; t++)
                {
                    real[f] += samples[t] * Math.Cos(a * t);
                    imag[f] -= samples[t] * Math.Sin(a * t);
                }

                result[f] = Math.Sqrt((real[f] * real[f]) + (imag[f] * imag[f])) / N;
            }
            );
            return result;
        }

        public static double[] bdft(double[] samples)
        {
            int N = samples.Length;
            int m = N;
            double[] real = new double[N];
            double[] imag = new double[N];
            double[] result = new double[N];
            double pi = 2 * Math.PI / N;
            for (int f = 0; f < N - 1; f++)
            {
                double a = f * pi;
                for (int t = 0; t < N - 1; t++)
                {
                    real[f] += samples[t] * Math.Cos(a * t);
                    imag[f] -= samples[t] * Math.Sin(a * t);
                }
                real[f] = real[f] / N;
                imag[f] = real[f] / N;
                double amp = Amplitude(real[f], imag[f]);
                double phase = tan(real[f], imag[f]);
                result[f] = Math.Sqrt((real[f] * real[f]) + (imag[f] * imag[f]));

            }
            return result;
        }

        public static double[] iidft(double[] samples)
        {
            int N = samples.Length;
            int m = N;
            double[] real = new double[N];
            double[] imag = new double[N];
            double[] result = new double[N];
            double pi = 2 * Math.PI / N;
            for (int f = 0; f < N; f++)
            {
                double a = f * pi;
                for (int t = 0; t < N; t++)
                {
                    real[f] += samples[t] * Math.Cos(a * t) / N;
                    imag[f] -= samples[t] * Math.Sin(a * t) / N;
                }
                result[f] = Math.Sqrt(real[f] * real[f] + imag[f] * imag[f]);
            }
            //result = idft(result);
            return result;
        }

        public double[] idft(double[] a)
        {
            int N = a.Length;
            double[] samples = new double[N];
            double pi = 2 * Math.PI / N;
            Parallel.For(0, N, f =>
            {
                double c = f * pi;
                for (int t = 0; t < N; t++)
                {
                    samples[f] += a[t] * (Math.Cos(c * t) + Math.Sin(c * t));
                }
            });
            return samples;
        }

        public static double Amplitude(double real, double imag)
        {
            double amp = Math.Sqrt(real * real + imag * imag);
            return amp;
        }
        public static double tan(double real, double imag)
        {
            double tan = Math.Atan(imag / real) * 180 / Math.PI;
            return tan;
        }

    }
}
