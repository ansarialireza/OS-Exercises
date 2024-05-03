
using System;
using System.Net;
using System.Threading;

class Program
{
    public static void Main(string[] args)
    {
        string dir = "C:/Users/T460s/Desktop/Os Lab/firstHW/fourthHW/DownloadedFiles";
        string[] downloadLinks = new string[]
                {
                    "https://irsv.upmusics.com/AliBZ/Zang%20Bzn%20Ambulaance%20(320).mp3",
                    //"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Operating_system_placement.svg/1200px-Operating_system_placement.svg.png",
                    "https://cdn-ckjba.nitrocdn.com/XvHIXtRQMUYzLjoXbfBpiwAcydcSSOVj/assets/images/optimized/rev-d85625b/www.ciat.edu/wp-content/uploads/2022/12/c-sharp-blog.jpeg",
                    "https://dls.music-fa.com/tagdl/downloads/Mohsen%20Lorestani%20-%20Bache%20Nane%20(128).mp3",
                    "https://code.visualstudio.com/assets/docs/languages/csharp/csharp-hero.png"

                };


        foreach (string url in downloadLinks)
        {
            string fileName = GetFileNameFromUrl(url);
            DownloadFile(url, dir + "\\" + fileName);
        }
        Console.ReadLine();
    }

    public static string[] GetUrls(int counter)
    {
        string[] urls = new string[counter];
        for (int i = 0; i < counter; i++)
        {
            Console.WriteLine($"URL {i + 1}: ");
            urls[i] = Console.ReadLine();
            Console.Clear();
        }
        Console.Clear();
        return urls;
    }

    public static void DownloadFile(string url, string fileName)
    {
        Thread thread = new Thread(() => DownloadProcess(url, fileName));
        thread.Start();
    }

    public static void DownloadProcess(string url, string fileName)
    {
        try
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(new Uri(url), fileName);
                client.DownloadProgressChanged += (sender, e) =>
                {
                    Console.WriteLine(e.ProgressPercentage);
                };
            }
            Console.WriteLine($"Download of '{fileName}' completed!");
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred for '{url}'! Error message: {e.Message}");
        }
    }

    
    public static string GetFileNameFromUrl(string url)
    {
        Uri uri = new Uri(url);
        string fileName = System.IO.Path.GetFileName(uri.LocalPath);
        return fileName;
    }
}
