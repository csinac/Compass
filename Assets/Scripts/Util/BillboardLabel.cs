using RectangleTrainer.Compass.World;
using UnityEngine;
using UnityEngine.UI;

namespace RectangleTrainer.Compass.Util
{
    public class BillboardLabel : MonoBehaviour
    {
        [SerializeField] private Text label;
        private Camera mainCam;
        
        private void Start() {
            Trackable trackable = GetComponentInParent<Trackable>();
            label.text = trackable ? trackable.name : "Untitled";
            mainCam = Camera.main;
        }

        private void Update() {
            transform.LookAt(mainCam.transform);
        }
    }
}
