namespace LeaVM.Core
{
    public class LeaValue
    {
        public LeaValue(ReadOnlySpan<byte> value)
        {
            Value = BitConverter.ToInt32(value);
        }

        public LeaValue(int value)
        {
            Value = value;
        }

        public static implicit operator LeaValue(int value)
        {
            return new LeaValue(value);
        }

        public int Value { get; set; }

        public byte[] GetBytes()
        {
            return BitConverter.GetBytes(Value);
        }
    }
}
