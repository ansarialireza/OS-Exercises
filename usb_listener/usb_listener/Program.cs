using System;
using System.Management;
using System.Diagnostics;

class Program
{
    static bool paintOpened = false;

    static void Main(string[] args)
    {
        Console.WriteLine("USB Listener started. Press Ctrl+C to exit.");

        // Continuously monitor for USB devices
        while (true)
        {
            // Check for USB devices
            if (IsUsbDriveConnected())
            {
                // Open MS Paint if a USB device is connected and MS Paint hasn't been opened yet
                if (!paintOpened)
                {
                    OpenMSPaint();
                    paintOpened = true;
                }

                // Wait for a few seconds before checking again
                System.Threading.Thread.Sleep(5000);
            }
            else
            {
                // Reset the flag if no USB device is connected
                paintOpened = false;

                // Wait for a short time before checking again
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    static bool IsUsbDriveConnected()
    {
        // Create a WMI query to select USB devices
        string query = "SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%USB%'";

        // Initialize a ManagementObjectSearcher to execute the query
        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
        {
            // Get the collection of USB devices
            ManagementObjectCollection collection = searcher.Get();

            // Check if any USB device is connected
            foreach (ManagementObject device in collection)
            {
                string deviceName = (string)device["Name"];

                // Check if the device is a USB mass storage device
                if (deviceName.Contains("USB Mass Storage") || deviceName.Contains("Flash Drive"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    static void OpenMSPaint()
    {
        try
        {
            Console.WriteLine("Opening MS Paint...");
            Process.Start("mspaint.exe");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening MS Paint: {ex.Message}");
        }
    }
}
