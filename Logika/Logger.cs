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

using Dane;

namespace Logika
{
    internal class Logger
    {
        public static CircleList circleList = new CircleList();

        private System.Timers.Timer timer;
        private bool enable = false;
        FileStream stream;


        public CircleList CircleList
        {
            get { return circleList; }
            set { circleList = value; }
        }
        public bool Enable
        {
            get => enable;
        }
        public Logger(CircleList circleList)
        {
            CircleList = circleList;
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



        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {

            // code here will run every second
            foreach (Circle circle in circleList)
            {
                JsonSerializer.SerializeAsync(stream, circle);
                Console.WriteLine("Tak Logger sie wykonuje..");
            }
        }
    }
}

