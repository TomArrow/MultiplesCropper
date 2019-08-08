using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;

namespace MultiplesCropper
{
    class Program
    {
        static void Main(string[] args)
        {
            int multiple = 4;
            string suffix = "_multiplescrop";

            List<string> filesToProcess = new List<string>();

            for(int i = 0; i<args.Length; i++)
            {

                // TODO Add possibility to set cropping alignment (left, center, right, top, middle, bottom)
                // TODO Maybe make separate vertical and horizontal settings
                if (args[i] == "-m" || args[i] == "--multiple") {
                    i++;
                    multiple = int.Parse(args[i]);
                }
                else if (args[i] == "-s" || args[i] == "--suffix")
                {
                    i++;
                    suffix = args[i];
                } 
                else
                {
                    string filename = args[i];

                    // Process wildcard
                    if (filename.IndexOf("*") != -1) { 

                        string pattern = Path.GetFileName(filename);
                        string path = Path.GetDirectoryName(filename);

                        path = path == "" ? "." : path;

                        Console.WriteLine("Found wildcard. Processing "+pattern+" in "+path);
                    
                        // Search files mathing the pattern
                        string[] files = Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly);

                        foreach(string file in files)
                        {

                            filesToProcess.Add(file);
                            Console.WriteLine("Will process " + file);
                        }

                    } else
                    {

                        filesToProcess.Add(filename);
                        Console.WriteLine("Will process " + filename);
                    }

                }
            }

            foreach(string filename in filesToProcess)
            {

                Console.WriteLine("Processing " + filename);
                Image image =  Bitmap.FromFile(filename);
                string extension = Path.GetExtension(filename);
                string name = Path.GetFileNameWithoutExtension(filename);
                string path = Path.GetDirectoryName(filename);
                string newPath = path + "\\" + name + suffix + extension;

                int width = image.Width;
                int height = image.Height;

                // Width
                while(Math.Round((double)width/(double)multiple)*multiple != width)
                {
                    width--;
                }
                // Height
                while (Math.Round((double)height / (double)multiple) * multiple != height)
                {
                    height--;
                }

                Rectangle cropBoundaries = new Rectangle(0, 0, width, height);
                Image croppedImage = cropImage(image, cropBoundaries);
                croppedImage.Save(newPath);
            }
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }
    }
}
