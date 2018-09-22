using UnityEngine;

namespace Sources.Behaviours {
    public class ParametersKeeper : MonoBehaviour {
        public SceneType currentScene;
        public AnimalType currentAnimal;

        private void Awake() {
            currentAnimal = AnimalType.Spider;
            currentScene = SceneType.Base;
        }
    }
}