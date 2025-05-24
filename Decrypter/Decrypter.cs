using OpenCvSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
public class Decrypter
{
    public static void Main(string[] args)
    {
        string answer = "";
        
        Mat image = new Mat(@"C:\PhotoCrypt\amogusCrypted.png", ImreadModes.Color); //указать файл для расшифровки
        if (image.Empty()) 
        {
            Console.WriteLine("Не удалось открыть или найти изображение!");
            return;
        }

        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                Vec3b pixel = image.At<Vec3b>(y, x); 
                byte red = pixel.Item2; 
                byte green = pixel.Item1; 
                byte blue = pixel.Item0;

                string redBits = Convert.ToString(red, 2).PadLeft(8, '0').Substring(5);
                string greenBits = Convert.ToString(green, 2).PadLeft(8, '0').Substring(6);
                string blueBits = Convert.ToString(blue, 2).PadLeft(8, '0').Substring(5);

                string combinedBits = redBits + greenBits + blueBits;
                
                int symb = Convert.ToInt32(combinedBits, 2);
                if (symb == 39 && answer.Length!=0)
                {
                    answer += (char)symb;
                    Console.WriteLine(answer);
                    image.Dispose();
                    Environment.Exit(0);
                }
                else
                {
                    answer +=(char)symb;
                    
                }
            }
        }
        
        image.Dispose();
    }
}