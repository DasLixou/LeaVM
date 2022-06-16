using LeaVM.Core;

namespace LeaVM.Builder
{
    public class LeaBuilder
    {
        public List<Instruction> instructions = new();

        public void Emit(Instruction instruction)
        {
            instructions.Add(instruction);
        }
    }
}
