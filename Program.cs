using System.Drawing;
using System.Text.RegularExpressions;
using Aspose.BarCode.BarCodeRecognition;
using BarcodeLib;

namespace BarcodeGenerator;
class Program
{
    static void Main(string[] args)
    {
        bool control = true;
        int selection;
        string barcodeNumber;

        Barcode barcode = new Barcode();

        int imageWidth = 250;
        int imageHeight = 150;

        Color foreColor = Color.Black;
        Color backColor = Color.White;
        
        string regexFormat = @"^[0-9]{12}$";
        Regex regex = new Regex(regexFormat);

        do
        {
            Console.WriteLine("*Barcode Generator*");
            Console.WriteLine("-------------------");
            Console.WriteLine("");
            Console.WriteLine("Please press (1) to generate barcode");
            Console.WriteLine("Please press (2) to read barcode");
            Console.WriteLine("Please press (0) to exit");
    
            bool selectionController;
            do
            {
                selectionController = int.TryParse(Console.ReadLine(), out selection);
                if (selection < 0 || selection > 2)                
                    selectionController = false;
    
                if (selectionController == false)
                    Console.Write("Please make a valid selection : ");
            } while (selectionController == false);
    
            switch (selection)
            {
                case 1:
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Generate Barcode");
                        Console.WriteLine("----------------");
                        Console.WriteLine("");
    
                        Console.Write("Please enter the barcode number you want to generate (EAN13 - 12 digits) : ");            
                        barcodeNumber = Console.ReadLine();
                        Console.WriteLine("");  
    
                        if(regex.IsMatch(barcodeNumber) == false)
                        {
                            control = false;                
                            Console.WriteLine("The barcode number that you entered is invalid.");
                            Console.WriteLine("");
                            Console.WriteLine("Please press any key to try again.");
                            Console.ReadKey();
                            Console.Clear();
                        }
    
                    } while (regex.IsMatch(barcodeNumber) == false);
    
                    Console.Write("Please enter the barcode name : ");
                    string barcodeName = Console.ReadLine();
    
                    Image barcodeImage = barcode.Encode(TYPE.EAN13, barcodeNumber, foreColor, backColor, imageWidth, imageHeight);
    
                    string path = AppDomain.CurrentDomain.BaseDirectory + "\\barcodes\\" + barcodeName + ".png";
    
                    barcode.SaveImage(path,SaveTypes.PNG);
    
                    Console.WriteLine("");
                    Console.WriteLine($"The barcode named '{barcodeName}.png' has been successfully generated and saved.");
                    ReturnMenu();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Read Barcode");
                    Console.WriteLine("------------");
                    Console.WriteLine("");

                    Console.Write("Please enter the barcode name : ");
                    barcodeName = Console.ReadLine();

                    path = AppDomain.CurrentDomain.BaseDirectory + "\\barcodes\\" + barcodeName + ".png";

                    BarCodeReader barcodeReader = new BarCodeReader(path, DecodeType.EAN13);

                    foreach (BarCodeResult result in barcodeReader.ReadBarCodes())
                    {
                        Console.WriteLine("Barcode Type : " + result.CodeType);
                        Console.WriteLine("Barcode Number : " + result.CodeText);
                    }

                    ReturnMenu();

                    break;
                default:
                    break;
            }
        } while (selection != 0);
    }

    private static void ReturnMenu()
    {
        Console.WriteLine("");
        Console.WriteLine("Please press any key to continue.");
        Console.ReadKey();
        Console.Clear();
    }
}
