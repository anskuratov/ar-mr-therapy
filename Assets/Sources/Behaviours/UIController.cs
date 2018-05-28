using UnityEngine;

namespace Sources.Behaviours {
    public class UIController : MonoBehaviour {

        [SerializeField] private ApplicationMode _applicationMode;
        
        private const float UI_LABEL_START_X = 15.0f;
        private const float UI_FPS_LABEL_START_Y = 30.0f;
        private const float UI_LABEL_SIZE_X = 1920.0f;
        private const float UI_LABEL_SIZE_Y = 35.0f;
        private const string UI_FONT_SIZE = "<size=25>";
        
        private const float FPS_UPDATE_FREQUENCY = 1.0f;
        
        private string _fpsText;
        private float _currentTime;
        private float _accumulation = 0;
        private int _framesSinceUpdate = 0;
        private int _currentFps = 0;

        private void Update() {
            if (_applicationMode == ApplicationMode.Development) {
            CountFps();
        }
    }

        private void CountFps() {
            _currentTime += Time.deltaTime;
            ++_framesSinceUpdate;
            _accumulation += Time.timeScale / Time.deltaTime;
            if (_currentTime >= FPS_UPDATE_FREQUENCY) {
                _currentFps = (int) (_accumulation / _framesSinceUpdate);
                _currentTime = 0.0f;
                _framesSinceUpdate = 0;
                _accumulation = 0.0f;
                _fpsText = "FPS: " + _currentFps;
            }
        }

        public void OnGui() {
            if (_applicationMode == ApplicationMode.Development) {
                GUI.color = Color.white;
                GUI.Label(new Rect(UI_LABEL_START_X,
                        UI_FPS_LABEL_START_Y,
                        UI_LABEL_SIZE_X,
                        UI_LABEL_SIZE_Y),
                    UI_FONT_SIZE + _fpsText + "</size>");
            }
            
            var _hideAllRect = new Rect(250.0f, 130.0f, 250.0f, 130.0f);
            if (GUI.Button(_hideAllRect, "<size=30>Hide All</size>"))
                foreach (var marker in FindObjectsOfType<ARObjectBehaviour>())
                    marker.SendMessage("Hide");
        }
    }
}