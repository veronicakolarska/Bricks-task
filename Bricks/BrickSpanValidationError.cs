namespace Bricks
{
    // errors for brick span more than 2
    public class BrickSpanValidationError
    {
        public int BrickNumber { get; set; }

        public int BrickSpan { get; set; }

        public BrickSpanValidationError(int brickNumber, int brickSpan)
        {
            this.BrickNumber = brickNumber;
            this.BrickSpan = brickSpan;
        }

        public override string ToString()
        {
            return $"Brick {this.BrickNumber} is spanning {this.BrickSpan} squares!";
        }
    }
}