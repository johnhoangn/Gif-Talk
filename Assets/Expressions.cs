using System.Collections.Generic;


namespace GifTalk.JSON {
    // JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Expressions {
        public string name { get; set; }
        public string src { get; set; }
        public float duration { get; set; }
        public List<string> triggers { get; set; }
    }

}
