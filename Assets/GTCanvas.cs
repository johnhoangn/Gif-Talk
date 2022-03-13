using UnityEngine;
using UnityEngine.Video;


namespace GifTalk {
    public class GTCanvas {
        // Camera at 1920, 1080
        private static Vector3 CANVAS_ANCHOR = new Vector3(0, 0, 0);

        private VideoPlayer video;
        private Config config;
        private string originalExpression;
        private string name;
        private Vector2 size;

        public GameObject screen;


        public GTCanvas(string canvasName, string expressionName, GameObject baseCanvas, Config _config) {
            originalExpression = expressionName;
            name = canvasName;
            screen = baseCanvas;
            video = (VideoPlayer)screen.GetComponent("VideoPlayer");
            config = _config;
            setFile(config.getExpressionFile(expressionName));
            setSize(24, 24);
        }


        // Tall boi
        public GTCanvas(
            string canvasName,
            string resourceName,
            GameObject baseCanvas,
            Config config,
            int sx, int sy,
            int px, int py) : this(
                canvasName,
                resourceName,
                baseCanvas,
                config) {

            setSize(sx, sy);
            setPosition(px, py);
        }


        public void setPosition(int x, int y) {
            // Screen origin is bottom left
            // Object origin is center
            screen.transform.position = CANVAS_ANCHOR + new Vector3(
                x + size.x / 2,
                -(y + size.y / 2),
                0
            );
        }


        public void setSize(int x, int y) {
            screen.transform.localScale = new Vector2(x, y);
            size = new Vector2(x, y);
        }

        public void setLayer(int z) {
            screen.transform.position += new Vector3(
                0, 0, z - screen.transform.position.z
            );
        }


        public void setFile(string fileName) {
            Debug.Log("Set file " + App.MEDIA_DIRECTORY + fileName);
            video.url = App.MEDIA_DIRECTORY + fileName;
            video.Play();
        }


        /**
         * Resets expression
         */
        public void reset() {
            setFile(originalExpression);
        }
    }
}
