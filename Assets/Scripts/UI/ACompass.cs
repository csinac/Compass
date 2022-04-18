using System.Collections.Generic;
using UnityEngine;
using RectangleTrainer.Compass.World;

namespace RectangleTrainer.Compass.UI
{
    public abstract class ACompass : MonoBehaviour
    {
        [SerializeField] protected float range = 90;
        [SerializeField] protected float distanceVisibilityRange = 20;
        [Space]
        [SerializeField] protected bool fadeOutOfRangeIcons = false;
        [SerializeField] protected float fadeRange = 5;
        [SerializeField] protected float maxDistance = 1000;

        protected static ACompass instance;
        protected Dictionary<int, ATrackableIcon> icons;
        protected List<ADirectionIcon> directionIcons;

        protected virtual void Awake() {
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

                ACompassElement icon = instance.icons[trackable.GetInstanceID()];
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

            float distance = delta.magnitude;
            
            instance.UpdateIconPosition(icon, angles, distance, trackable.IconPersistent);
            icon.UpdateDistance(distance, Mathf.Abs(angles) < instance.distanceVisibilityRange);
            icon.Highlight(trackable.Focused);
        }


        private void UpdateIconPosition(ACompassElement icon, float angles, float distance, bool persistent) {
            float halfRange = range / 2;

            if(persistent)
                angles = Mathf.Clamp(angles, -halfRange, halfRange);

            float absAngle = Mathf.Abs(angles);
            bool visible = persistent || (distance < maxDistance && absAngle < halfRange);

            icon.Toggle(visible);
            
            if (visible) {
                float compassPosition = GetIconPosition(angles);
                FadeIcon(absAngle, icon, persistent);
                PositionIcon(compassPosition, icon);
            }
        }

        private void UpdateDirectionIcon(ADirectionIcon icon, float angles) => UpdateIconPosition(icon, angles, -1, false);

        protected virtual void Update() {
            foreach (ADirectionIcon icon in directionIcons) {
                float degrees = icon.Degrees - Tracker.Direction + North.Offset;

                while (degrees >  180) degrees -= 360;
                while (degrees < -180) degrees += 360;

                instance.UpdateDirectionIcon(icon, degrees);
            }
        }

        protected abstract float GetIconPosition(float angles);
        protected abstract void FadeIcon(float absoluteAngle, ACompassElement icon, bool forced);
        protected abstract void PositionIcon(float xPos, ACompassElement icon);
    }
}
