using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client.Models;


namespace GifTalk {
    public class App : MonoBehaviour {
        private static string CONFIGURATION_PATH = "app_config.json";
        private static string EXPRESSION_MAP_PATH = "expressions.json";

        private Config config;
        private Client client;
        private ConnectionCredentials credentials;


        void Start() {
            Debug.Log("App starting");
            config = new Config(CONFIGURATION_PATH, EXPRESSION_MAP_PATH);
            credentials = new ConnectionCredentials(Secrets.bot_name, Secrets.bot_access_token);
            client = new Client();
            client.Initialize(credentials, config.settings.streamer);

            client.OnMessageReceived += Client_OnMessageReceived;

            client.Connect();
            Debug.Log("Client connected");
        }


        private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e) {
            Debug.Log("Message received " + e);
        }


        void Update() {

        }
    }
}
