using System;
using System.Collections.Generic;
using UnityEngine;
using RectangleTrainer.Compass.World;
using Unity.VisualScripting;

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
        protected List<ADirectionIcon> directionIcons;
        
        private void Awake() {
            if (instance && instance != this) {
                Destroy(this);
                return;
            }

            icons = new Dictionary<int, ATrackableIcon>();
            directionIcons = new List<ADirectionIcon>();
            instance = this;
        }

        public static void AddTrackable(Trackable trackable) {
            if (instance == null)
                return;

            if (!instance.icons.ContainsKey(trackable.GetInstanceID())) {
                if (trackable.Icon == null) {
                    Debug.LogWarning($"Trackable {trackable.name} doesn't have an icon. It will not be tracked on the compass.");
                    return;
                }
    
                ATrackableIcon iconInstance = Instantiate(trackable.Icon);
                iconInstance.transform.SetParent(instance.transform);
                instance.icons.Add(trackable.GetInstanceID(), iconInstance);
            }
        }

        public static void RemoveTrackable(Trackable trackable) {
            if (trackable.Icon == null) {
                Debug.LogWarning($"Trackable {trackable.name} doesn't have an icon. Ignoring 'RemoveTrackable'.");
                return;
            }

            if (instance.icons.ContainsKey(trackable.GetInstanceID())) {
                if(trackable.Icon == null)
                    return;

                ATrackableIcon icon = instance.icons[trackable.GetInstanceID()];
                if(icon)
                    Destroy(icon.gameObject);
                
                instance.icons.Remove(trackable.GetInstanceID());
            }
        }
        
        public static void AddCardinalDirection(ADirectionIcon iconPF, CardinalDirections.Direction info) {
            if (instance == null)
                return;

            if (!instance.directionIcons.Contains(iconPF)) {
                ADirectionIcon iconInstance = Instantiate(iconPF);
                iconInstance.Initialize(info);
                iconInstance.transform.SetParent(instance.transform);

                instance.directionIcons.Add(iconInstance);
            }
        }

        public static void UpdateIcon(Trackable trackable, float angles, Vector3 delta) {
            if (instance == null)
                return;

            if (!instance.icons.ContainsKey(trackable.GetInstanceID()))
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

        protected virtual void Update() {
            foreach (ADirectionIcon icon in directionIcons) {
                float degrees = icon.Degrees - Tracker.Direction + North.Offset;

                while (degrees >  180) degrees -= 360;
                while (degrees < -180) degrees += 360;

                instance.UpdateIconPosition(icon, degrees);
            }
        }

        protected abstract float GetIconPosition(float angles);
        protected abstract void FadeIcon(float absoluteAngle, ATrackableIcon icon);
        protected abstract void PositionIcon(float xPos, ATrackableIcon icon);
    }
}
