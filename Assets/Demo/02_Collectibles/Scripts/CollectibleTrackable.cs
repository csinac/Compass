using RectangleTrainer.Compass.World;
using System;
using UnityEngine;

namespace RectangleTrainer.Compass.Demo
{
    public class CollectibleTrackable : Trackable
    {
        public Action<GameObject> OnCollected;
        private bool collectBegun = false;
        private float initialScale;
        private float collectTime = 1;
        private float collectionSpeed = 0.5f;

        protected override void Start() {
            base.Start();
            initialScale = transform.localScale.x;
        }

        private void OnTriggerEnter(Collider other) {
            collectBegun = true;
        }

        private void Update() {
            if (collectBegun) {
                collectTime -= Time.deltaTime / collectionSpeed;
                transform.localScale = Vector3.one * initialScale * collectTime;

                if (collectTime <= 0) {
                    OnCollected?.Invoke(gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}