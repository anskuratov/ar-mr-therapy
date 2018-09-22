using UnityEngine;

namespace Sources.Behaviours.AnimationsControllers {
    public class SpiderAnimationsController : MonoBehaviour {

        [SerializeField] private Transform _lookingAnchor;
        [SerializeField] private Animator _animator;
        
        private bool _isMoving;
        private const float _moveSpeed = 0.12f;

        private void FixedUpdate() {
            _lookingAnchor.position = new Vector3(Camera.main.transform.position.x,
                gameObject.transform.position.y,
                Camera.main.transform.position.z);

            if (!_isMoving) return;
            gameObject.transform.position += (_lookingAnchor.transform.position - gameObject.transform.position).normalized * _moveSpeed * Time.deltaTime;
            gameObject.transform.LookAt(_lookingAnchor);
        }

        public void StartFear() {
            _animator.SetBool("Fear", true);
        }

        public void EndFear() {
            _animator.SetBool("Fear", false);
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