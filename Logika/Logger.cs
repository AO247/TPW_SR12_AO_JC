using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Timers;

namespace Logika
{
    internal class Logger
    {
        private static List<CircleLogic> circles;
        private System.Timers.Timer timer;
        private bool enable = false;
        FileStream stream;


        public List<CircleLogic> Circles
        {
            get { return circles; }
            set { circles = value; }
        }
        public bool Enable
        {
            get => enable;
        }
        public Logger(List<CircleLogic> circles)
        {
            Circles = circles;
            startLogger(1000);
        }
        public Logger() { }


        public void startLogger(int interval)
        {
            string directoryPath = "Logs";
            try
            {
                // Create the directory
                if (Directory.Exists(directoryPath))
                {
                    string fileName = ".\\Logs\\log.json";
                    stream = File.Create(fileName);
                }
                else
                {
                    Directory.CreateDirectory(directoryPath);
                    string fileName = ".\\Logs\\log.json";
                    stream = File.Create(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            timer.Interval = 1000; // 1000 ms is one second
            timer.Start();
        }

       

        public  void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            
            // code here will run every second
            foreach (CircleLogic circleLogic in circles)
            {
                JsonSerializer.SerializeAsync(stream, circleLogic);
                Console.WriteLine("Tak Logger sie wykonuje..");
            }
        }
    }
}
