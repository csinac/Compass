
namespace RectangleTrainer.Compass
{
    public class CardinalDirections
    {
        public static Direction[] directions = {
            new() {Name = "N" , Degrees =   0},
            new() {Name = "NE", Degrees =  45},
            new() {Name = "E" , Degrees =  90},
            new() {Name = "SE", Degrees = 135},
            new() {Name = "S" , Degrees = 180},
            new() {Name = "SW", Degrees = 225},
            new() {Name = "W" , Degrees = 270},
            new() {Name = "NW", Degrees = 315}
        };
            
        public struct Direction
        {
            public float Degrees;
            public string Name;
        }
    }
}