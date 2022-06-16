namespace LeaVM.Core.Operands
{
    public class ConstantOperand : Operand
    {
        public int Value { get; set; }

        public ConstantOperand(int value)
        {
            Value = value;
        }

        public override byte[] AsByte()
        {
            var result = new byte[1 + 4];
            result[0] = 0b0000001;
            BitConverter.GetBytes(Value).CopyTo(result, 1);
            return result;
        }
    }
}
