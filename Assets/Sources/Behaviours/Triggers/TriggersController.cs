using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Behaviours.Triggers {
	public class TriggersController : MonoBehaviour {
		
		[SerializeField] private UnityEvent _onInnerEnter;
		[SerializeField] private UnityEvent _onInnerExit;
		[SerializeField] private UnityEvent _onOutterEnter;
		[SerializeField] private UnityEvent _onOutterExit;

		public void ActivateInnerAction() {
			_onInnerEnter.Invoke();
		}

		public void EndInnerAction() {
			_onInnerExit.Invoke();	
		}
		
		public void ActivateOutterAction() {
			_onOutterEnter.Invoke();
		}
		
		public void EndOutterAction() {
			_onOutterExit.Invoke();
		}
	}
}