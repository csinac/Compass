using System.Collections.Generic;
using UnityEngine;
using RectangleTrainer.Compass.World;

namespace RectangleTrainer.Compass.UI
{
    public abstract class ACompass : MonoBehaviour
    {
        [SerializeField] protected float range = 90;
        [Space]
        [SerializeField] protected bool fadeOutOfRangeIcons = false;
        [SerializeField] protected float fadeRange = 5;

        protected static ACompass instance;
        protected Dictionary<int, ATrackableIcon> icons;
        
        private void Awake() {
            if (instance && instance != this) {
                Destroy(this);
                return;
            }

            icons = new Dictionary<int, ATrackableIcon>();
            instance = this;
        }

        public static void AddTrackable(Trackable trackable) {
            if (instance == null)
                return;

            if (!instance.icons.ContainsKey(trackable.GetInstanceID())) {
                ATrackableIcon iconInstance = Instantiate(trackable.Icon);
                iconInstance.transform.SetParent(instance.transform);
                instance.icons.Add(trackable.GetInstanceID(), iconInstance);
            }
        }

        public static void RemoveTrackable(Trackable trackable) {
            if (instance == null)
                return;

            if (instance.icons.ContainsKey(trackable.GetInstanceID())) {
                ATrackableIcon icon = instance.icons[trackable.GetInstanceID()];
                Destroy(icon.gameObject);
                
                instance.icons.Remove(trackable.GetInstanceID());
            }
        }
        
        public static void UpdateIcon(Trackable trackable, float angles, Vector3 delta) {
            if (instance == null)
                return;
            
            ATrackableIcon icon = instance.icons[trackable.GetInstanceID()];
            if (icon == null)
                return;
            
            instance.UpdateIconPosition(icon, angles);
            icon.UpdateDistance(delta.magnitude);
        }


        private void UpdateIconPosition(ATrackableIcon icon, float angles) {
            float absAngle = Mathf.Abs(angles);
            bool visible = absAngle < range / 2;

            icon.Toggle(visible);
            
            if (visible) {
                float compassPosition = GetIconPosition(angles);

                if (fadeOutOfRangeIcons) {
                    FadeIcon(absAngle, icon);
                }
                
                PositionIcon(compassPosition, icon);
            }
        }

        protected abstract float GetIconPosition(float angles);
        protected abstract void FadeIcon(float absoluteAngle, ATrackableIcon icon);
        protected abstract void PositionIcon(float xPos, ATrackableIcon icon);
    }
}
