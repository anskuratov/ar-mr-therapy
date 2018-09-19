using UnityEngine;

namespace Sources.Behaviours {
	public class ARObjectBehaviour : MonoBehaviour {

		private void Hide() {
			DestroySelf();
		}
		
		private void DestroySelf() {
		 	gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
}
