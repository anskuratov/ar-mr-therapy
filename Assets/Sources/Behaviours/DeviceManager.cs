using System.Collections;
using UnityEngine;
using UnityEngine.VR;

namespace Sources.Behaviours {
	public class DeviceManager : MonoBehaviour {

		[SerializeField] private ScreenOrientation _screenOrientation;

		private void Awake() {
			Screen.orientation = _screenOrientation;
		}
	}
}
