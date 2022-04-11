using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.Compass.World
{
    public class Tracker : MonoBehaviour
    {
        [SerializeField, HideInInspector] private List<Trackable> trackables;
        public static float Direction => instance ? instance.transform.eulerAngles.y : 0;
        
        private static Tracker instance;
        private readonly static float DEGREETORADIAN = Mathf.PI / 180;

        private void Awake() {
            if (instance && instance != this) {
                Destroy(this);
                return;
            }

            instance = this;
            trackables = new List<Trackable>();
        }
        
        private float Angle(Vector3 delta) {
            Vector2 deltaRotated = RotateXZ(delta, transform.eulerAngles.y);
            return Mathf.Atan2(deltaRotated.x, deltaRotated.y) / Mathf.PI * 180;
        }

        Vector2 RotateXZ(Vector3 vector, float angle) {
            float radians = angle * DEGREETORADIAN;
            float cos = Mathf.Cos(radians);
            float sin = Mathf.Sin(radians);
            
            return new Vector2 (
                vector.x * cos - vector.z * sin,
                vector.z * cos + vector.x * sin
            );
        }

        public static void AddTrackable(Trackable trackable) {
            if (instance == null)
                return;

            if (!instance.trackables.Contains(trackable)) {
                instance.trackables.Add(trackable);
                UI.ACompass.AddTrackable(trackable);
            }
        }

        public static void RemoveTrackable(Trackable trackable) {
            if (instance == null)
                return;

            if (instance.trackables.Contains(trackable)) {
                instance.trackables.Remove(trackable);
                UI.ACompass.RemoveTrackable(trackable);
            }
        }
        void Update()
        {
            foreach (var trackable in trackables) {
                Vector3 delta = trackable.Position - transform.position;
                UI.ACompass.UpdateIcon(trackable, Angle(delta), delta);
            }
        }
    }
}