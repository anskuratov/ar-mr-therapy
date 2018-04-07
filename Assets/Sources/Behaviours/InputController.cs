using System.Collections;
using Tango;
using UnityEngine;

namespace Sources.Behaviours {
    public class InputController : MonoBehaviour, ITangoLifecycle, ITangoDepth {
        
        [SerializeField] private GameObject _prefabMarker;
        [SerializeField] private RectTransform _prefabTouchEffect;
        [SerializeField] private TangoPointCloud _pointCloud;
        [SerializeField] private Canvas _canvas;

        private const float UI_LABEL_START_X = 15.0f;
        private const float UI_LABEL_START_Y = 15.0f;
        private const float UI_LABEL_SIZE_X = 1920.0f;
        private const float UI_LABEL_SIZE_Y = 35.0f;
        private const float UI_LABEL_GAP_Y = 3.0f;
        private const float UI_BUTTON_SIZE_X = 250.0f;
        private const float UI_BUTTON_SIZE_Y = 130.0f;
        private const float UI_BUTTON_GAP_X = 5.0f;
        private const float UI_LABEL_OFFSET = UI_LABEL_GAP_Y + UI_LABEL_SIZE_Y;
        private const float UI_FPS_LABEL_START_Y = UI_LABEL_START_Y + UI_LABEL_OFFSET;
        private const float UI_EVENT_LABEL_START_Y = UI_FPS_LABEL_START_Y + UI_LABEL_OFFSET;
        private const float UI_POSE_LABEL_START_Y = UI_EVENT_LABEL_START_Y + UI_LABEL_OFFSET;

        private const string UI_FLOAT_FORMAT = "F3";
        private const string UI_FONT_SIZE = "<size=25>";
        private const string UX_TANGO_SERVICE_VERSION = "Tango service version: {0}";
        private const string UX_TARGET_TO_BASE_FRAME = "Target->{0}, Base->{1}:";
        private const string UX_STATUS = "\tstatus: {0}, count: {1}, position (m): [{2}], orientation: [{3}]";

        private const float FPS_UPDATE_FREQUENCY = 1.0f;
        private float _accumulation;
        private float _currentTime;
        private int _currentFPS;
        private int _framesSinceUpdate;
        private bool _findPlaneWaitingForDepth;
        private bool _showDebug;
        private string _fpsText;
        private string _tangoServiceVersion;

        private Rect _hideAllRect;
        private Rect _selectedRect;
        
        private ARMarker _selectedMarker;
        private ARCameraPostProcess _arCameraPostProcess;
        private TangoApplication _tangoApplication;
        private TangoARPoseController _tangoPose;

        private void Start() {
            _currentFPS = 0;
            _framesSinceUpdate = 0;
            _currentTime = 0.0f;
            _fpsText = "FPS = Calculating";
            _tangoApplication = FindObjectOfType<TangoApplication>();
            _tangoPose = FindObjectOfType<TangoARPoseController>();
            _arCameraPostProcess = FindObjectOfType<ARCameraPostProcess>();
            _tangoServiceVersion = TangoApplication.GetTangoServiceVersion();

            _tangoApplication.Register(this);
        }

        private void Update() {
            _currentTime += Time.deltaTime;
            ++_framesSinceUpdate;
            _accumulation += Time.timeScale / Time.deltaTime;
            if (_currentTime >= FPS_UPDATE_FREQUENCY) {
                _currentFPS = (int) (_accumulation / _framesSinceUpdate);
                _currentTime = 0.0f;
                _framesSinceUpdate = 0;
                _accumulation = 0.0f;
                _fpsText = "FPS: " + _currentFPS;
            }

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

        public void OnGUI() {
//        Rect distortionButtonRec = new Rect(UI_BUTTON_GAP_X,
//                                            Screen.height - UI_BUTTON_SIZE_Y - UI_BUTTON_GAP_X,
//                                            UI_BUTTON_SIZE_X,
//                                            UI_BUTTON_SIZE_Y);
//        string isOn = m_arCameraPostProcess.enabled ? "Off" : "On";
//        if (GUI.Button(distortionButtonRec,
//                       UI_FONT_SIZE + "Turn Distortion " + isOn + "</size>"))
//        {
//            m_arCameraPostProcess.enabled = !m_arCameraPostProcess.enabled;
//        }

            if (_showDebug && _tangoApplication.HasRequiredPermissions) {
                var oldColor = GUI.color;
                GUI.color = Color.white;

                GUI.color = Color.black;
                GUI.Label(new Rect(UI_LABEL_START_X,
                        UI_LABEL_START_Y,
                        UI_LABEL_SIZE_X,
                        UI_LABEL_SIZE_Y),
                    UI_FONT_SIZE + string.Format(UX_TANGO_SERVICE_VERSION, _tangoServiceVersion) + "</size>");

                GUI.Label(new Rect(UI_LABEL_START_X,
                        UI_FPS_LABEL_START_Y,
                        UI_LABEL_SIZE_X,
                        UI_LABEL_SIZE_Y),
                    UI_FONT_SIZE + _fpsText + "</size>");

                GUI.Label(new Rect(UI_LABEL_START_X,
                        UI_POSE_LABEL_START_Y - UI_LABEL_OFFSET,
                        UI_LABEL_SIZE_X,
                        UI_LABEL_SIZE_Y),
                    UI_FONT_SIZE + string.Format(UX_TARGET_TO_BASE_FRAME, "Device", "Start") + "</size>");

                var pos = _tangoPose.transform.position;
                var quat = _tangoPose.transform.rotation;
                var positionString = pos.x.ToString(UI_FLOAT_FORMAT) + ", " +
                                     pos.y.ToString(UI_FLOAT_FORMAT) + ", " +
                                     pos.z.ToString(UI_FLOAT_FORMAT);
                var rotationString = quat.x.ToString(UI_FLOAT_FORMAT) + ", " +
                                     quat.y.ToString(UI_FLOAT_FORMAT) + ", " +
                                     quat.z.ToString(UI_FLOAT_FORMAT) + ", " +
                                     quat.w.ToString(UI_FLOAT_FORMAT);
                var statusString = string.Format(UX_STATUS,
                    _GetLoggingStringFromPoseStatus(_tangoPose.m_poseStatus),
                    _GetLoggingStringFromFrameCount(_tangoPose.m_poseCount),
                    positionString, rotationString);
                GUI.Label(new Rect(UI_LABEL_START_X,
                        UI_POSE_LABEL_START_Y,
                        UI_LABEL_SIZE_X,
                        UI_LABEL_SIZE_Y),
                    UI_FONT_SIZE + statusString + "</size>");
                GUI.color = oldColor;
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

            if (FindObjectOfType<ARMarker>() != null) {
                _hideAllRect = new Rect(Screen.width - UI_BUTTON_SIZE_X - UI_BUTTON_GAP_X,
                    Screen.height - UI_BUTTON_SIZE_Y - UI_BUTTON_GAP_X,
                    UI_BUTTON_SIZE_X,
                    UI_BUTTON_SIZE_Y);
                if (GUI.Button(_hideAllRect, "<size=30>Hide All</size>"))
                    foreach (var marker in FindObjectsOfType<ARMarker>())
                        marker.SendMessage("Hide");
            }
            else {
                _hideAllRect = new Rect(0, 0, 0, 0);
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

                if (_selectedRect.Contains(guiPosition) || _hideAllRect.Contains(guiPosition)) {
                }
                else if (Physics.Raycast(cam.ScreenPointToRay(t.position), out hitInfo)) {
                    var tapped = hitInfo.collider.gameObject;
                    if (!tapped.GetComponent<Animation>().isPlaying) _selectedMarker = tapped.GetComponent<ARMarker>();
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

//            if (Input.touchCount == 2) {
//                var t0 = Input.GetTouch(0);
//                var t1 = Input.GetTouch(1);    
//
//                if (t0.phase != TouchPhase.Began && t1.phase != TouchPhase.Began) return;
//
//                _showDebug = !_showDebug;
//            }
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

            Instantiate(_prefabMarker, planeCenter, Quaternion.LookRotation(forward, up));
            _selectedMarker = null;
        }
    }
}