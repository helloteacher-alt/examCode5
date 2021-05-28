using System;

namespace examCode5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Address of image data :");
            string imageDataAddress = Console.ReadLine();
            double[,] imagedataArray = ReadImageDataFromFile(imageDataAddress);
            Console.Write("Address of convolution kernel :");
            string convolutionKernel = Console.ReadLine();
            ReadImageDataFromFile(convolutionKernel);
            double[,] convolutionArray = ReadImageDataFromFile(convolutionKernel);
            Console.Write("Address of output :");
            string outputAddress = Console.ReadLine();

            int ArrayCollumn = imagedataArray.GetLength(0) + convolutionArray.GetLength(0) - 1;
            int ArrayRow = imagedataArray.GetLength(1) + convolutionArray.GetLength(1) - 1;
            double[,] expandedDataArray = writeExpandedDataArray(imagedataArray, ArrayCollumn, ArrayRow, imagedataArray.GetLength(0), imagedataArray.GetLength(1), convolutionArray.GetLength(0), convolutionArray.GetLength(1));
            double[,] outputImageData = Concolution(expandedDataArray, convolutionArray, imagedataArray.GetLength(0), imagedataArray.GetLength(1));
            WriteImageDataToFile(outputAddress, outputImageData);
        }
        static double[,] writeExpandedDataArray(double[,] imageDataArray, int expandedCollumn, int expandedRow, int dataArrayCollumn, int dataArrayRow, int convolutionCollumn, int convolutionRow)
        {
            double[,] expandedDataArray = new double[expandedCollumn, expandedRow];
            for (int i = 0; i < expandedCollumn; i++)
            {
                for (int j = 0; j < expandedRow; j++)
                {
                    expandedDataArray[i, j] = imageDataArray[(i + (dataArrayCollumn - 1)) % dataArrayCollumn, (j = (dataArrayRow - 1)) % dataArrayRow];
                }
            }
            return expandedDataArray;
        }
        static double[,] Concolution(double[,] expandedDataArray,double[,] convolutionArray, int dataArrayCollumn,int dataArrayRow)
        {
            double[,] outputImageArray = new double[dataArrayCollumn, dataArrayRow];
            for (int i = 0; i<dataArrayCollumn; i++)
            {
                for (int j = 0; j < dataArrayRow; j++)
                {
                    for(int k = 0; k < convolutionArray.GetLength(0); k++)
                    {
                        for(int m = 0; 1 < convolutionArray.GetLength(1); m++)
                        {
                            outputImageArray[i, j] += expandedDataArray[i + k, j + m] * convolutionArray[k, m];
                        }
                    }
                }
            }return outputImageArray;
        }
        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }
        static void WriteImageDataToFile(string imageDataFilePath,double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }
    }
}
