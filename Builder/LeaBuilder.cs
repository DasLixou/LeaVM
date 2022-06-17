using LeaVM.Core;

namespace LeaVM.Builder
{
    public class LeaBuilder
    {
        public List<Instruction> instructions = new();

        public byte[] AsBytes()
        {
            var result = new List<byte>();
            instructions.ForEach(_ => result.AddRange(_.AsBytes()));
            return result.ToArray();
        }

        public void Emit(Instruction instruction)
        {
            instructions.Add(instruction);
        }

        public void Emit(OpCodes opcode)
        {
            Emit(new Instruction(opcode));
        }

        public void Emit(OpCodes opcode, Operand operand)
        {
            Emit(new Instruction(opcode, operand));
        }

        public void Emit(OpCodes opcode, Operand operand1, Operand operand2)
        {
            Emit(new Instruction(opcode, operand1, operand2));
        }
    }
}