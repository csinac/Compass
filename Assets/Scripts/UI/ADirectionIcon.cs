namespace RectangleTrainer.Compass.UI
{
    public abstract class ADirectionIcon : ATrackableIcon
    {
        protected float degrees;
        public float Degrees => degrees;
        
        public override void UpdateDistance(float distance) {}
        public abstract void Initialize(CardinalDirections.Direction dir);
    }
}