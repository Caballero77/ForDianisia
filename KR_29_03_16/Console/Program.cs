using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleC
{
    class Program
    {
        static void Main(string[] args)
        {
            Palette MainPalette = Palette.ReadFromFile(@"c:\users\андрій\documents\visual studio 2015\Projects\KR_29_03_16\Console\ColorFile.txt");

            Console.WriteLine("All colors in palette: ");

            MainPalette.Sort();

            foreach (Color c in MainPalette)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("The most intensity color: {0}", MainPalette[MainPalette.Colors.Count - 1]);

            Console.WriteLine("Choose 2 colors in palette to mix: ");

            Console.WriteLine(BColor.Mix(MainPalette[Convert.ToInt32(Console.ReadLine())], MainPalette[Convert.ToInt32(Console.ReadLine())]));

            Console.ReadLine();
        }

        private enum BaseColor
        {
            red,
            green,
            blue
        }

        private class Color:IComparable //255 britness
        {
            public Dictionary<BaseColor, double> Colors { get; set; }

            public Color(double red = 0, double green = 0, double blue = 0)
            {
                Colors = new Dictionary<BaseColor, double>();
                Colors.Add(BaseColor.red, red);
                Colors.Add(BaseColor.green, green);
                Colors.Add(BaseColor.blue, blue);
            }

            public double Intensity
            {
                get
                {
                    return (Colors[BaseColor.red] + Colors[BaseColor.green] + Colors[BaseColor.blue]) / 3;
                }
            }

            public static Color operator +(Color c1, Color c2)
            {
                return Mix(c1, c2);
            }

            public static Color Mix(params Color[] colors)
            {

                Color res = new BColor();
                foreach (Color c in colors)
                {
                    res.Colors[BaseColor.red] += c.Colors[BaseColor.red];
                    res.Colors[BaseColor.green] += c.Colors[BaseColor.green];
                    res.Colors[BaseColor.blue] += c.Colors[BaseColor.blue];
                }
                res.Colors[BaseColor.red] /= colors.Length;
                res.Colors[BaseColor.green] /= colors.Length;
                res.Colors[BaseColor.blue] /= colors.Length;
                return res;
            }

            public int CompareTo(object obj)
            {
                return Math.Sign(Intensity - ((Color)obj).Intensity);
            }

            public override string ToString()
            {
                return string.Format("red: {0} green: {1} blue: {2} intensity: {3}", Colors[BaseColor.red], Colors[BaseColor.green], Colors[BaseColor.blue],Intensity);
            }
        }

        private class Palette:IEnumerable<Color>
        {
            public List<Color> Colors { get; set; }

            public Palette()
            {
                Colors = new List<Color>();
            }

            public IEnumerator<Color> GetEnumerator()
            {
                return Colors.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Color this[int i]
            {
                get
                {
                    return Colors[i];
                }
                set
                {
                    Colors[i] = value;
                }
            }

            public static Palette ReadFromFile(string filePath)
            {
                Palette ResPal = new Palette();
                using (StreamReader sr = File.OpenText(filePath))
                {
                    while(!sr.EndOfStream)
                    {
                        string str = sr.ReadLine();
                        var s = str.Split(' ');
                        if(s.Length == 3)
                            ResPal.Add(new Color(Convert.ToDouble(s[0]), Convert.ToDouble(s[1]), Convert.ToDouble(s[2])));
                        if(s.Length == 4)
                            ResPal.Add(new BColor(Convert.ToDouble(s[0]), Convert.ToDouble(s[1]), Convert.ToDouble(s[2]), Convert.ToDouble(s[3])));
                    }
                }
                return ResPal;
            }

            public void Add(Color c)
            {
                Colors.Add(c);
            }

            public void Sort()
            {
                Colors.Sort();
            }
        }

        private class BColor:Color
        {
            public double Britness { get; set; }

            public BColor(double red = 0, double green = 0, double blue = 0,double britness = 0)
                :base(red,green,blue)
            {
                Britness = britness;
            }

            public override string ToString()
            {
                return base.ToString() + string.Format(" britness: {0}", Britness);
            }

            public static new BColor Mix(params Color[] colors)
            {

                BColor res = new BColor();
                foreach (Color c in colors)
                {
                    res.Colors[BaseColor.red] += c.Colors[BaseColor.red];
                    res.Colors[BaseColor.green] += c.Colors[BaseColor.green];
                    res.Colors[BaseColor.blue] += c.Colors[BaseColor.blue];
                    if(c is BColor)
                    {
                        res.Britness += (c as BColor).Britness;
                    }
                    else
                    {
                        res.Britness += 255;
                    }
                }
                res.Colors[BaseColor.red] /= colors.Length;
                res.Colors[BaseColor.green] /= colors.Length;
                res.Colors[BaseColor.blue] /= colors.Length;
                res.Britness /= colors.Length;
                return res;
            }

            public static BColor operator +(BColor c1, Color c2)
            {
                return Mix(c1, c2);
            }
        }
    }
}
