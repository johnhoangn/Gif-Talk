namespace GifTalk.JSON {
    // JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Settings {
        public string streamer { get; set; }
        public float reset_delay { get; set; }
        public int history_length { get; set; }
    }
}
