using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Behaviours.Triggers {
	public class AnimalTriggerBehaviour : MonoBehaviour {

		[SerializeField] private TriggerType _triggerType;
		
		private TriggersController _triggersController;
		
		void Start() {
			_triggersController = FindObjectOfType<TriggersController>();
		}

		private void OnTriggerEnter(Collider other) {
			if (!other.gameObject.CompareTag("MainCamera")) {
				return;
			}
			
			switch (_triggerType) {
				case TriggerType.Inner: {
					_triggersController.ActivateInnerAction();
					break;
				}
				case TriggerType.Outter: {
					_triggersController.ActivateOutterAction();
					break;
				}
			}
		}

		private void OnTriggerExit(Collider other) {
			if (!other.gameObject.CompareTag("MainCamera")) {
				return;
			}
			
			switch (_triggerType) {
				case TriggerType.Inner: {
					_triggersController.EndInnerAction();
					break;
				}
				case TriggerType.Outter: {
					_triggersController.EndOutterAction();
					break;
				}
			}
		}

		private enum TriggerType {
			Inner,
			Outter
		}
	}
}