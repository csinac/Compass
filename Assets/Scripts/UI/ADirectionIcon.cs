namespace RectangleTrainer.Compass.UI
{
    public abstract class ADirectionIcon : ATrackableIcon
    {
        protected float degrees;
        public float Degrees => degrees;
        
        public override void UpdateDistance(float distance, bool visible) {}
        public abstract void Initialize(CardinalDirections.Direction dir);
    }
}