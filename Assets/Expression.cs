using System.Collections.Generic;


namespace GifTalk.JSON {
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Trigger {
        public bool case_sensitive { get; set; } = false;
        public string pattern { get; set; } = "NonePattern";
    }
    public class Expression {
        public string name { get; set; } = "DEFAULT_EXPRESSION";
        public string src { get; set; } = "test.webm";
        public double duration { get; set; } = 3.0;
        public List<Trigger> triggers { get; set; }
        public List<string> canvases { get; set; } = new List<string>() {  };
    }
}
