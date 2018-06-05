using UnityEngine;

namespace Sources.Behaviours.AnimationsControllers {
    public class SpiderAnimationsController : MonoBehaviour {

        [SerializeField] private Transform _lookingAnchor;
        [SerializeField] private Animator _animator;
        
        private bool _isMoving;
        private bool _isFearing;
        private const float _moveSpeed = 0.002f;

        private void FixedUpdate() {
            _lookingAnchor.position = new Vector3(Camera.main.transform.position.x,
                gameObject.transform.position.y,
                Camera.main.transform.position.z);
            
            if (_isMoving) {
                gameObject.transform.position += (_lookingAnchor.transform.position - gameObject.transform.position).normalized * _moveSpeed;
                gameObject.transform.LookAt(_lookingAnchor);
            }
            else if (_isFearing) {
                gameObject.transform.LookAt(_lookingAnchor);
            }
        }

        public void StartFear() {
            _animator.SetBool("Fear", true);
            _isFearing = true;
        }

        public void EndFear() {
            _animator.SetBool("Fear", false);
            _isFearing = false;
        }

        public void StartMove() {
            _animator.SetBool("Move", true);
            _isMoving = true;
        }

        public void EndMove() {
            _animator.SetBool("Move", false);
            _isMoving = false;
        }
    }
}