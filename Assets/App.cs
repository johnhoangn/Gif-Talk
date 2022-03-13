using System;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using System.Text.RegularExpressions;


namespace GifTalk {
    public class App : MonoBehaviour {
        private Config config;
        private Client client;
        private ConnectionCredentials credentials;

        private MainRenderer renderWindow;

        public static string LOG_DIRECTORY = "/Logs/";
        public static string CONFIGURATION_FILE = "/app_config.json";
        public static string EXPRESSION_MAP_FILE = "/expressions.json";
        public static string CONFIG_DIRECTORY = "/Config/";
        public static string MEDIA_DIRECTORY = "/Media/";

        public GameObject canvasPrefab;


        // Automatically write logs to file by hooking into the Application log event
        private void OnEnable() {
            Application.logMessageReceived += Logger.Log;
        }
        private void OnDisable() {
            Application.logMessageReceived -= Logger.Log;
        }


        void Start() {
            Debug.Log("App starting...");

            // Prepare configurations/media
            init();

            config = new Config(CONFIG_DIRECTORY, CONFIGURATION_FILE, EXPRESSION_MAP_FILE);
            credentials = new ConnectionCredentials(Secrets.bot_name, Secrets.bot_access_token);

            Debug.Log("Starting renderer...");

            renderWindow = new MainRenderer(config, canvasPrefab);

            foreach (JSON.Panel panelData in config.settings.panels) {
                renderWindow.addCanvas(
                    panelData.name,
                    panelData.default_expression,
                    panelData.size[0],
                    panelData.size[1],
                    panelData.position[0],
                    panelData.position[1]
                );
            }

            Debug.Log("Renderer started!");
            Debug.Log("Client connecting...");

            client = new Client();
            client.Initialize(credentials, config.settings.streamer);
            client.OnMessageReceived += Client_OnMessageReceived;
            client.Connect();

            Debug.Log("Client connected!");
            Debug.Log("Gif-Talk started!");
        }

        private void init() {
            LOG_DIRECTORY = Application.dataPath + LOG_DIRECTORY;
            CONFIG_DIRECTORY = Application.dataPath + CONFIG_DIRECTORY;
            MEDIA_DIRECTORY = Application.dataPath + MEDIA_DIRECTORY;

            if (!Directory.Exists(CONFIG_DIRECTORY)) {
                Directory.CreateDirectory(LOG_DIRECTORY);
                Directory.CreateDirectory(CONFIG_DIRECTORY);
                Directory.CreateDirectory(MEDIA_DIRECTORY);
                Config.writeDefaults(CONFIG_DIRECTORY, CONFIGURATION_FILE, EXPRESSION_MAP_FILE);
            }
        }


        private bool triggerMatches(string message, JSON.Trigger trigger) {
            return new Regex(
                trigger.pattern,
                RegexOptions.Compiled 
                    | (trigger.case_sensitive ? RegexOptions.IgnoreCase : 0)
            ).Matches(message).Count > 0;
        }


        private JSON.Expression getMatchingExpression(string message) {
            foreach (string name in config.expressionNames) {
                Debug.LogError("Trying " + name);
                config.expressions.TryGetValue(name, out JSON.Expression exprData);
                if (exprData.triggers is null) continue; // Only default should have no triggers

                foreach (JSON.Trigger trigger in exprData.triggers) {
                    if (triggerMatches(message, trigger)) {
                        return exprData;
                    }
                }
            }

            return config.expressions[config.expressionNames[config.expressionNames.Count - 1]];
        }


        private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e) {
            // if (!e.ChatMessage.Username.Equals(config.settings.streamer)) return;

            Debug.Log("Message received " + e.ChatMessage.UserId + " " + e.ChatMessage.Username + " " + e.ChatMessage.Message);
            JSON.Expression exprData = getMatchingExpression(e.ChatMessage.Message);
            Debug.LogError(e.ChatMessage.Message + " matched " + exprData.name);

            foreach (string canvasName in exprData.canvases) {
                renderWindow.setCanvasFile(canvasName, exprData.src);
            }
        }


        void Update() {
            //renderWindow.update();
        }
    }
}
