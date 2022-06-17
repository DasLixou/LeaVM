namespace LeaVM.Core.Operands
{
    public class ConstantOperand : Operand
    {
        public ConstantOperand(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public static implicit operator ConstantOperand(int value)
        {
            return new ConstantOperand(value);
        }

        public override byte[] AsByte()
        {
            var valueBytes = BitConverter.GetBytes(Value);
            var result = new byte[1 + valueBytes.Length];
            result[0] = (byte)valueBytes.Length;
            valueBytes.CopyTo(result, 1);
            return result;
        }
    }
}