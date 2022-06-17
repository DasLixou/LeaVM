using LeaVM.Core.Operands;

namespace LeaVM.Core
{
    public abstract class Operand
    {
        public static implicit operator Operand(int value)
        {
            return new ConstantOperand(value);
        }

        public static ConstantOperand Constant(int value) => new ConstantOperand(value);
        public static AddressOperand Address(int value) => new AddressOperand(value);

        public abstract byte[] AsByte();
    }
}