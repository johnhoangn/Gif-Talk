using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GifTalk {
    using UnityEngine;

    public class Logger {
        private static string timestamp = GetTimestamp(DateTime.Now);

        public static void Log(string logString, string stackTrace, LogType type) {
            string filename = App.LOG_DIRECTORY + timestamp + ".txt";
            System.IO.File.AppendAllText(filename, logString + "\n");
        }


        private static String GetTimestamp(DateTime value) {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
