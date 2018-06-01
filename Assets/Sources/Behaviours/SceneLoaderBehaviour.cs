using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Behaviours {
    
    public class SceneLoaderBehaviour : MonoBehaviour {

        [SerializeField] private SceneType _sceneType;

        private void Start() {
            LoadScene(_sceneType);
        }
        
        public void LoadScene(SceneType _sceneType) {
            switch(_sceneType) {
                case SceneType.Base: {
                    SceneManager.LoadScene(SceneNames.Base, LoadSceneMode.Additive);
                    break;
                }
                    
                case SceneType.SplitScreen: {
                    SceneManager.LoadScene(SceneNames.Split, LoadSceneMode.Additive);
                    break;
                }
            }
        }

        public void UnloadScene(SceneType _sceneType) {
            switch(_sceneType) {
                case SceneType.Base: {
                    SceneManager.UnloadSceneAsync(SceneNames.Base);
                    break;
                }
                    
                case SceneType.SplitScreen: {
                    SceneManager.UnloadSceneAsync(SceneNames.Split);
                    break;
                }
            }
        }
    }
}