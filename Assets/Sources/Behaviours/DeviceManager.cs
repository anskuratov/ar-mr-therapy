using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class DeviceManager : MonoBehaviour {

	[SerializeField] private bool _enableCardboard = false;
	
	void Awake() {
		if (_enableCardboard)
			StartCoroutine(LoadDevice("Cardboard", true));
	}

	private IEnumerator LoadDevice(string deviceName, bool enable) {
		VRSettings.LoadDeviceByName(deviceName);
		yield return null;
		VRSettings.enabled = enable;
	}
}
