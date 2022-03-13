using System.Collections.Generic;
using UnityEngine;


namespace GifTalk {
    class MainRenderer {
        private static GameObject CANVAS_PREFAB;
        private Dictionary<string, GTCanvas> canvases = new Dictionary<string, GTCanvas>();
        private Config config;


        public MainRenderer(Config _config, GameObject canvasPrefab) {
            CANVAS_PREFAB = canvasPrefab;
            config = _config;
        }


        /**
         *  Size + Position constructor
         */
        public GTCanvas addCanvas(string canvasName, string expressionName, int sx, int sy, int px, int py) {
            GTCanvas newCanvas = new GTCanvas(canvasName, expressionName, MonoBehaviour.Instantiate(CANVAS_PREFAB), config);

            newCanvas.screen.transform.SetParent(GameObject.Find("GifTalk").transform);
            newCanvas.setSize(sx, sy);
            newCanvas.setPosition(px, py);
            canvases.Add(canvasName, newCanvas);

            Debug.Log("Added canvas! " + canvasName + " " + expressionName
                + newCanvas.screen.transform.localPosition + " | " 
                + newCanvas.screen.transform.localScale + " | "
                + sx + " " + sy + " " + px + " " + py);

            return newCanvas;
        }


        public void setCanvasPosition(string canvasName, int x, int y) {
            if (canvases.TryGetValue(canvasName, out GTCanvas canvas)) {
                canvas.setPosition(x, y);
            } else {
                Debug.LogError(string.Format("Canvas of name {0} does not exist!", canvasName));
            }
        }


        public void setCanvasSize(string canvasName, int x, int y) {
            if (canvases.TryGetValue(canvasName, out GTCanvas canvas)) {
                canvas.setSize(x, y);
            } else {
                Debug.LogError(string.Format("Canvas of name {0} does not exist!", canvasName));
            }
        }


        public void setCanvasLayer(string canvasName, int z) {
            if (canvases.TryGetValue(canvasName, out GTCanvas canvas)) {
                canvas.setLayer(z);
            } else {
                Debug.LogError(string.Format("Canvas of name {0} does not exist!", canvasName));
            }
        }


        public void setCanvasFile(string canvasName, string fileName) {
            if (canvases.TryGetValue(canvasName, out GTCanvas canvas)) {
                Debug.Log("Setting file of " + canvasName + " to " + fileName);
                canvas.setFile(fileName);
            } else {
                Debug.LogError(string.Format("Canvas of name {0} does not exist!", canvasName));
            }
        }


        public void update() {

        }
    }
}
