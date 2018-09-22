using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sources.Behaviours.AnimationsControllers {
    public class RatAnimationsController : MonoBehaviour {
        private const float timeToHesitate = 6f;
        private const float moveSpeed = 0.2f;
        
        [SerializeField] private Transform _lookingAnchor;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _hesitatingAudioSource;

        private bool _isMoving;
        private float hesitatingTiming = 0f;

        private void FixedUpdate() {
            _lookingAnchor.position = new Vector3(Camera.main.transform.position.x,
                gameObject.transform.position.y,
                Camera.main.transform.position.z);

            if (_isMoving) {
                gameObject.transform.position +=
                    (_lookingAnchor.transform.position - gameObject.transform.position).normalized * moveSpeed *
                    Time.deltaTime;
                gameObject.transform.LookAt(_lookingAnchor);
                gameObject.transform.Rotate(0f, 180f, 0f);
            }
            else {
                HesitatingControl();
            }
        }

        public void StartMove() {
            _animator.SetBool("Move", true);
            _isMoving = true;
        }

        public void EndMove() {
            _animator.SetBool("Move", false);
            _isMoving = false;
        }

        private void HesitatingControl() {
            hesitatingTiming += Time.deltaTime;

            if (hesitatingTiming >= timeToHesitate) {
                var rnd = new System.Random();
                var expectation = rnd.Next(0, 10);

                if (expectation >= 6) {
                    _animator.SetTrigger("Hesitating");
                    _hesitatingAudioSource.Play();
                }
                hesitatingTiming = 0;
            }
        }
    }
}