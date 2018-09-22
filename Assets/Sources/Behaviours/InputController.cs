using System.Collections;
using Tango;
using UnityEngine;

namespace Sources.Behaviours {

    public class InputController : MonoBehaviour, ITangoLifecycle, ITangoDepth {

        [SerializeField] private RectTransform _prefabTouchEffect;
        [SerializeField] private TangoPointCloud _pointCloud;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private SpawningType _spawningType;
        [SerializeField] private GameObject[] _prefabsToSpawn; // 1 - spider, 2 - snake, 3 - rat

        private const float UI_BUTTON_SIZE_X = 250.0f;
        private const float UI_BUTTON_SIZE_Y = 90.0f;
        private const float UI_BUTTON_GAP_X = 5.0f;
        private const float UI_BUTTON_GAP_Y = 0.0f;
        private const float UI_BETWEEN_BUTTON_GAP_X = 250.0f;

        private bool _findPlaneWaitingForDepth;
        private bool _showDebug;
        private string _tangoServiceVersion;

        private Rect _hideAllRect;
        private Rect _selectedRect;
        private Rect _splitRect;
        private Rect _spiderRect;
        private Rect _snakeRect;
        private Rect _ratRect;

        private ARObjectBehaviour _selectedMarker;
        private ParametersKeeper _parametersKeeper;
        private SceneLoaderBehaviour _sceneLoader;
        private TangoApplication _tangoApplication;
        private TangoARPoseController _tangoPose;
        private GameObject _prefabMarker;
        private GameObject _objectInstance;

        private void Start() {
            _objectInstance = null;

            _parametersKeeper = FindObjectOfType<ParametersKeeper>();
            _sceneLoader = FindObjectOfType<SceneLoaderBehaviour>();
            
            _tangoPose = FindObjectOfType<TangoARPoseController>();
            if (_tangoPose == null) {
                Debug.LogError("InputController: TangoPose is null!");
            }

            _tangoServiceVersion = TangoApplication.GetTangoServiceVersion();
            if (_tangoServiceVersion == null) {
                Debug.LogError("InputController: TangoServiceVersion is null!");
            }


            _tangoApplication = FindObjectOfType<TangoApplication>();
            if (_tangoApplication == null) {
                Debug.LogError("InputController: TangoApplication is null!");
            }
            else {
                _tangoApplication.Register(this);
            }
            
            setupRects();
            selectCurrentPrefab();
        }

        private void Update() {

            _UpdateLocationMarker();

            if (Input.GetKey(KeyCode.Escape))
                AndroidHelper.AndroidQuit();
        }

        private void OnDestroy() {
            _tangoApplication.Unregister(this);
        }

        public void OnTangoDepthAvailable(TangoUnityDepth tangoDepth) {
            _findPlaneWaitingForDepth = false;
        }

        public void OnTangoPermissions(bool permissionsGranted) {
        }

        public void OnTangoServiceConnected() {
            _tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.DISABLED);
        }

        public void OnTangoServiceDisconnected() {
        }

        private void setupRects() {
            _hideAllRect = new Rect(Screen.width - UI_BUTTON_SIZE_X - UI_BUTTON_GAP_X,
                Screen.height - UI_BUTTON_SIZE_Y - UI_BUTTON_GAP_X,
                UI_BUTTON_SIZE_X,
                UI_BUTTON_SIZE_Y);

            _splitRect = new Rect((Screen.width - UI_BUTTON_SIZE_X) / 2,
                Screen.height - UI_BUTTON_SIZE_Y - UI_BUTTON_GAP_Y,
                UI_BUTTON_SIZE_X,
                UI_BUTTON_SIZE_Y);
            
            _spiderRect = new Rect((Screen.width - UI_BUTTON_SIZE_X) / 2 - UI_BETWEEN_BUTTON_GAP_X,
                UI_BUTTON_GAP_Y,
                UI_BUTTON_SIZE_X,
                UI_BUTTON_SIZE_Y);
            
            _snakeRect = new Rect((Screen.width - UI_BUTTON_SIZE_X) / 2,
                UI_BUTTON_GAP_Y,
                UI_BUTTON_SIZE_X,
                UI_BUTTON_SIZE_Y);
            
            _ratRect = new Rect((Screen.width - UI_BUTTON_SIZE_X) / 2 + UI_BETWEEN_BUTTON_GAP_X,
                UI_BUTTON_GAP_Y,
                UI_BUTTON_SIZE_X,
                UI_BUTTON_SIZE_Y);
        }

        public void OnGUI() {
            if (GUI.Button(_splitRect, "<size=30>Split</size>")) {
                LoadAnotherScene();
            }

            if (GUI.Button(_spiderRect, "<size=30>Spider</size>")) {
                _parametersKeeper.currentAnimal = AnimalType.Spider;
            }
            
            if (GUI.Button(_snakeRect, "<size=30>Snake</size>")) {
                _parametersKeeper.currentAnimal = AnimalType.Snake;
            }

            if (GUI.Button(_ratRect, "<size=30>Rat</size>")) {
                _parametersKeeper.currentAnimal = AnimalType.Rat;
            }
            
            if (_selectedMarker != null) {
                var selectedRenderer = _selectedMarker.GetComponent<Renderer>();

                var screenRect = WorldBoundsToScreen(Camera.main, selectedRenderer.bounds);
                var yMin = Screen.height - screenRect.yMin;
                var yMax = Screen.height - screenRect.yMax;
                screenRect.yMin = Mathf.Min(yMin, yMax);
                screenRect.yMax = Mathf.Max(yMin, yMax);

                if (GUI.Button(screenRect, "<size=30>Hide</size>")) {
                    _selectedMarker.SendMessage("Hide");
                    _selectedMarker = null;
                    _selectedRect = new Rect();
                }
                else {
                    _selectedRect = screenRect;
                }
            }
            else {
                _selectedRect = new Rect();
            }

            if (FindObjectOfType<ARObjectBehaviour>() != null) {
                if (GUI.Button(_hideAllRect, "<size=30>Hide</size>"))
                    foreach (var marker in FindObjectsOfType<ARObjectBehaviour>())
                        marker.SendMessage("Hide");
            }
        }

        private void LoadAnotherScene() {
            switch (_parametersKeeper.currentScene) {
                case SceneType.Base: {
                    _sceneLoader.UnloadScene(SceneType.Base);
                    _sceneLoader.LoadScene(SceneType.SplitScreen);
                    _parametersKeeper.currentScene = SceneType.SplitScreen;
                    break;
                }
                case SceneType.SplitScreen: {
                    _sceneLoader.UnloadScene(SceneType.SplitScreen);
                    _sceneLoader.LoadScene(SceneType.Base);
                    _parametersKeeper.currentScene = SceneType.Base;
                    break;
                }
            }
        }

        private Rect WorldBoundsToScreen(Camera cam, Bounds bounds) {
            var center = bounds.center;
            var extents = bounds.extents;
            var screenBounds = new Bounds(cam.WorldToScreenPoint(center), Vector3.zero);

            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(+extents.x, +extents.y, +extents.z)));
            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(+extents.x, +extents.y, -extents.z)));
            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(+extents.x, -extents.y, +extents.z)));
            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(+extents.x, -extents.y, -extents.z)));
            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(-extents.x, +extents.y, +extents.z)));
            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(-extents.x, +extents.y, -extents.z)));
            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(-extents.x, -extents.y, +extents.z)));
            screenBounds.Encapsulate(cam.WorldToScreenPoint(center + new Vector3(-extents.x, -extents.y, -extents.z)));
            return Rect.MinMaxRect(screenBounds.min.x, screenBounds.min.y, screenBounds.max.x, screenBounds.max.y);
        }

        private string _GetLoggingStringFromPoseStatus(TangoEnums.TangoPoseStatusType status) {
            string statusString;
            switch (status) {
                case TangoEnums.TangoPoseStatusType.TANGO_POSE_INITIALIZING:
                    statusString = "initializing";
                    break;
                case TangoEnums.TangoPoseStatusType.TANGO_POSE_INVALID:
                    statusString = "invalid";
                    break;
                case TangoEnums.TangoPoseStatusType.TANGO_POSE_UNKNOWN:
                    statusString = "unknown";
                    break;
                case TangoEnums.TangoPoseStatusType.TANGO_POSE_VALID:
                    statusString = "valid";
                    break;
                default:
                    statusString = "N/A";
                    break;
            }

            return statusString;
        }

        private string _GetLoggingStringFromFrameCount(int frameCount) {
            if (frameCount == -1.0)
                return "N/A";
            return frameCount.ToString();
        }

        private void _UpdateLocationMarker() {
            if (Input.touchCount == 1) {
                var t = Input.GetTouch(0);
                var guiPosition = new Vector2(t.position.x, Screen.height - t.position.y);
                var cam = Camera.main;
                RaycastHit hitInfo;

                if (t.phase != TouchPhase.Began)
                    return;

                if (_selectedRect.Contains(guiPosition) 
                    || _hideAllRect.Contains(guiPosition)
                    || _splitRect.Contains(guiPosition)
                    || _spiderRect.Contains(guiPosition)
                    || _snakeRect.Contains(guiPosition)
                    || _ratRect.Contains(guiPosition)) {

                }
                else if (Physics.Raycast(cam.ScreenPointToRay(t.position), out hitInfo)) {
                    var tapped = hitInfo.collider.gameObject;
                    _selectedMarker = tapped.GetComponent<ARObjectBehaviour>();
                }
                else {
                    _selectedMarker = null;
                    StartCoroutine(_WaitForDepthAndFindPlane(t.position));

                    var touchEffectRectTransform = Instantiate(_prefabTouchEffect);
                    touchEffectRectTransform.transform.SetParent(_canvas.transform, false);
                    var normalizedPosition = t.position;
                    normalizedPosition.x /= Screen.width;
                    normalizedPosition.y /= Screen.height;
                    touchEffectRectTransform.anchorMin = touchEffectRectTransform.anchorMax = normalizedPosition;
                }
            }
        }

        private IEnumerator _WaitForDepthAndFindPlane(Vector2 touchPosition) {
            _findPlaneWaitingForDepth = true;

            _tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.MAXIMUM);
            while (_findPlaneWaitingForDepth) yield return null;

            _tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.DISABLED);

            var cam = Camera.main;
            Vector3 planeCenter;
            Plane plane;
            if (!_pointCloud.FindPlane(cam, touchPosition, out planeCenter, out plane)) yield break;

            var up = plane.normal;
            Vector3 forward;
            if (Vector3.Angle(plane.normal, cam.transform.forward) < 175) {
                var right = Vector3.Cross(up, cam.transform.forward).normalized;
                forward = Vector3.Cross(right, up).normalized;
            }
            else {
                forward = Vector3.Cross(up, cam.transform.right);
            }
            
            SpawnObject(planeCenter, forward, up);
        }

        private void SpawnObject(Vector3 planeCenter, Vector3 forward, Vector3 up) {
            selectCurrentPrefab();
            
            switch (_spawningType) {
                case SpawningType.Single: {
                    if (_objectInstance != null) {
                        _objectInstance.GetComponent<ARObjectBehaviour>().SendMessage("Hide");
                    }
                    _objectInstance = Instantiate(_prefabMarker, planeCenter, Quaternion.LookRotation(forward, up));
                    
                    if (_parametersKeeper.currentAnimal == AnimalType.Spider)
                        _objectInstance.transform.Rotate(0f, 180f, 0f);
                    
                    break;
                }
                case SpawningType.Multiple: {
                    Instantiate(_prefabMarker, planeCenter, Quaternion.LookRotation(forward, up));
                    break;
                }
            }
            
            _selectedMarker = null;
        }

        private void selectCurrentPrefab() {
            if (_parametersKeeper == null) {
                _prefabMarker = _prefabsToSpawn[0];
            }
            
            switch (_parametersKeeper.currentAnimal) {
                case AnimalType.Spider: {
                    _prefabMarker = _prefabsToSpawn[0];
                    break;
                }
                case AnimalType.Snake: {
                    _prefabMarker = _prefabsToSpawn[1];
                    break;
                }
                case AnimalType.Rat: {
                    _prefabMarker = _prefabsToSpawn[2];
                    break;
                }
            }
        }
    }
}