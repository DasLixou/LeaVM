namespace LeaVM.Core.Operands
{
    public class ConstantOperand : Operand
    {
        public ConstantOperand(LeaValue value)
        {
            Value = value;
        }

        public LeaValue Value { get; set; }

        public static implicit operator ConstantOperand(LeaValue value)
        {
            return new ConstantOperand(value);
        }

        public static implicit operator ConstantOperand(int value)
        {
            return new ConstantOperand(value);
        }

        public override byte[] AsByte()
        {
            var valueBytes = Value.GetBytes();
            var result = new byte[1 + valueBytes.Length];
            result[0] = (byte)valueBytes.Length;
            valueBytes.CopyTo(result, 1);
            return result;
        }
    }
}