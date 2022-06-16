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

        public byte[] AsBytes()
        {
            var result = new List<byte>();
            instructions.ForEach(_ => result.AddRange(_.AsBytes()));
            return result.ToArray();
        }
    }
}
