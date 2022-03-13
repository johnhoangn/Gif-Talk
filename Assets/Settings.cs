using System.Collections.Generic;

namespace GifTalk.JSON {
    public class Panel {
        public string name { get; set; } = "DEFAULT_PANEL";
        public string default_expression { get; set; } = "DEFAULT_EXPRESSION";
        public int[] size { get; set; } = { 0, 0 };
        public int[] position { get; set; } = { 0, 0 };
    }

    public class Settings {
        public string streamer { get; set; } = "totouri";
        public float reset_delay { get; set; } = 3;
        public int history_length { get; set; } = 3;
        public List<Panel> panels { get; set; } = new List<Panel>() { };
    }
}
