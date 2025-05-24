using OpenCvSharp;

public class Crypter
{
    public static Mat CryptTextInImg(Mat image, string text)
    {
        int letter = 0;
        Mat newImage = image.Clone();
        text = "'" + text + "'";
        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                if (letter < text.Length)
                {
                    Vec3b pixel = image.At<Vec3b>(y, x); 

                    byte red = pixel.Item2;
                    byte green = pixel.Item1;
                    byte blue = pixel.Item0;  

                    
                    List<int> newColors = ReplaceBits(red, green, blue, (int)text[letter]); 
                    byte newRed = (byte)newColors[0];
                    byte newGreen = (byte)newColors[1];
                    byte newBlue = (byte)newColors[2];
                   
                    newImage.Set(y, x, new Vec3b(newBlue, newGreen, newRed)); 
                    letter++;
                }

            }
        }

        return newImage;
    }
    public static List<int> ReplaceBits(int a, int b, int c, int d)
    {
        string binary = Convert.ToString(d, 2).PadLeft(8, '0'); 
        string A = Convert.ToString(a, 2).PadLeft(8, '0');
        string B = Convert.ToString(b, 2).PadLeft(8, '0');
        string C = Convert.ToString(c, 2).PadLeft(8, '0');

        string Apart = binary.Substring(0, 3);
        string Bpart = binary.Substring(3, 2);
        string Cpart = binary.Substring(5);


        string newA = A.Substring(0, 5) + Apart;
        string newB = B.Substring(0, 6) + Bpart;
        string newC = C.Substring(0, 5) + Cpart;
        return new List<int> { Convert.ToInt32(newA, 2), Convert.ToInt32(newB, 2), Convert.ToInt32(newC, 2) };
    }

    public static void Main(string[] args)
    {
        
        Mat image = new Mat(@"C:\PhotoCrypt\hampter.png", ImreadModes.Color); // указать фото для шифрования(png)
        if (image.Empty()) 
        {
            Console.WriteLine("Не удалось открыть или найти изображение!");
            return;
        }

        string textToEncrypt = "HITS{NEPLOXO-VOT-FLAG}";  // указать текст для шифрования(без использования ')


        Mat encryptedImage = CryptTextInImg(image, textToEncrypt);

        if (encryptedImage != null) 
        {
            encryptedImage.SaveImage("C:/PhotoCrypt/hampterCrypted.png"); 
            Console.WriteLine("Изображение успешно зашифровано и сохранено!");
        }
        else
        {
            Console.WriteLine("Произошла ошибка при шифровании изображения.");
        }

        image.Dispose();         
        encryptedImage.Dispose();


    }
}

