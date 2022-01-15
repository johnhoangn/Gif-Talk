using UnityEngine;
using System.IO;
using Newtonsoft.Json;


namespace GifTalk {
    public class Config {
        public JSON.Settings settings;
        public JSON.Expressions expressions;


        public Config(string app_config, string expression_map) {
            Debug.Log("Loading configurations");

            settings = JsonConvert.DeserializeObject<JSON.Settings>(readFile(app_config));
            expressions = JsonConvert.DeserializeObject<JSON.Expressions>(readFile(expression_map));
            Debug.Log("Configs parsed");
        }


        private string readFile(string fileName) {
            return File.ReadAllText(Application.dataPath + "/Config/" + fileName);
        }
    }
}
