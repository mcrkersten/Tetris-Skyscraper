using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Version3D {
    public class InitManager : MonoBehaviour {

        private Tower tower;
        private BlockQueue blockQueue;
        private PlayerController controller;

        public GameObject killScreen;
        public Camera cam;
        public GameObject[] tetrisModels;
        public GameObject[] buildingModels;
        public GameObject snapSystem;
        public Text scoreText;
        public Transform followCamTransform;

        public int lifes = 3;
        public List<Image> imageLives;


        private static InitManager instance = null;
        public static InitManager Instance
        {
            get {
                if (instance == null) {
                    instance = FindObjectOfType(typeof(InitManager)) as InitManager;
                }
                if (instance == null) {
                    //HAS TO EXSIST
                    throw new System.ArgumentException("FATAL ERROR: Init manager has to exsist, you did something stupid didnt ya?");
                }
                return instance;
            }
        }

        private void Awake() {
            Init();
        }


        private void Update() {
            int i = 0;
            foreach(Image image in imageLives) {
                if(lifes < i) {
                    image.enabled = false;
                }
                i++;
            }
            if(lifes <= 0) {
                killScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }


        private void Init() {
            controller = PlayerController.Instance;
            tower = Tower.Instance;
            blockQueue = BlockQueue.Instance;
        }
    }
}
