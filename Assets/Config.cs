using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace GifTalk {
    public class Config {
        public JSON.Settings settings;
        public Dictionary<string, JSON.Expression> expressions = new Dictionary<string, JSON.Expression>();
        public List<string> expressionNames = new List<string>();


        public Config(string basedir, string app_config, string expression_map) {
            Debug.Log("Loading configs...");

            settings = JsonConvert.DeserializeObject<JSON.Settings>(readFile(basedir, app_config));
            List<JSON.Expression> expressionList
                = JsonConvert.DeserializeObject<List<JSON.Expression>>(readFile(basedir, expression_map));

            try {
                expressionList.ForEach((expression) => {
                    Debug.Log("Adding " + expression.name);
                    expressions.Add(expression.name, expression);
                    expressionNames.Add(expression.name);
                    Debug.Log("Added " + expression.name);
                });
            } catch (System.Exception e) {
                Debug.LogError("Error parsing expression list!\n" + e.StackTrace);
                //Application.Quit();
            }

            Debug.Log("Configs parsed!");
        }


        public static void writeDefaults(string basedir, string app_config, string expression_map) {
            StreamWriter settingsFile = File.CreateText(basedir + app_config);
            StreamWriter expressionsFile = File.CreateText(basedir + expression_map);

            settingsFile.Write(JsonConvert.SerializeObject(new JSON.Settings(), Formatting.Indented));
            expressionsFile.Write(JsonConvert.SerializeObject(new List<JSON.Expression>() {
                new JSON.Expression()
            }, Formatting.Indented));

            settingsFile.Close();
            expressionsFile.Close();
        }


        public string getExpressionFile(string expressionName) {
            try {
                expressions.TryGetValue(expressionName, out JSON.Expression expression);
                return expression.src;
            } catch {
                Debug.LogError("No expression by the name " + expressionName);
            }
            return null;
        }


        private string readFile(string basedir, string fileName) {
            return File.ReadAllText(basedir + fileName);
        }
    }
}
