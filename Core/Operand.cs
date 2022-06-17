using LeaVM.Core.Operands;

namespace LeaVM.Core
{
    public abstract class Operand
    {
        public static implicit operator Operand(int value)
        {
            return new ConstantOperand(value);
        }

        public abstract byte[] AsByte();
    }
}