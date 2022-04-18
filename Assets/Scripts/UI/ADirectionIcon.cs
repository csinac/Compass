namespace RectangleTrainer.Compass.UI
{
    public abstract class ADirectionIcon : ACompassElement
    {
        protected float degrees;
        public float Degrees => degrees;
        public abstract void Initialize(CardinalDirections.Direction dir);
    }
}