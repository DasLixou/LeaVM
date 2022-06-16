namespace LeaVM.Core.Operands
{
    public class AddressOperand : Operand
    {
        public int Address { get; set; }

        public AddressOperand(int address)
        {
            Address = address;
        }

        public override byte[] AsByte()
        {
            var result = new byte[1 + 4];
            result[0] = 0;
            BitConverter.GetBytes(Address).CopyTo(result, 1);
            return result;
        }
    }
}
