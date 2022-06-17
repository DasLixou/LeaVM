namespace LeaVM.Core
{
    public class Instruction
    {
        public OpCodes OpCode { get; set; }
        public List<Operand> Operands { get; set; }

        public Instruction(OpCodes opCode, params Operand[] operands)
        {
            OpCode = opCode;
            Operands = operands.ToList();
        }

        public byte[] AsBytes()
        {
            var result = new List<byte>();

            result.Add((byte)OpCode);
            for (int i = 0; i < Operands.Count; i++)
            {
                result.AddRange(Operands[i].AsByte());
            }

            return result.ToArray();
        }
    }
}
