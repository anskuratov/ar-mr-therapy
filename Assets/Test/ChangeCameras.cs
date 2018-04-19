using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameras : MonoBehaviour {

	[SerializeField] private Camera _tangoCamera;
	[SerializeField] private RenderTexture _renderTexture;
	[SerializeField] private GameObject _splitScreenObjects;

	private void Start () {
		Invoke("EnableSplitCameras", 10);
	}

	private void EnableSplitCameras() {
		_tangoCamera.targetTexture = _renderTexture;
		_splitScreenObjects.SetActive(true);
	}
}
