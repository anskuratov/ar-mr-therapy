using System.Collections;
using UnityEngine;
using UnityEngine.VR;

namespace Sources.Behaviours {
	public class DeviceManager : MonoBehaviour {

		[SerializeField] private bool _enableCardboard;

		private void Awake() {
			if (_enableCardboard) {
				StartCoroutine(LoadDevice("Cardboard", true));
			}
		}

		private IEnumerator LoadDevice(string deviceName, bool enable) {
			VRSettings.LoadDeviceByName(deviceName);
			yield return null;
			VRSettings.enabled = enable;
		}
	}
}
