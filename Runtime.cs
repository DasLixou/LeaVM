using LeaVM.Core;
using LeaVM.Core.Operands;

namespace LeaVM
{
    public class Runtime
    {
        public Stack<object> stack = new();
        public int cursor = 0;

        public void Run(byte[] bytes)
        {
            while(cursor < bytes.Length)
            {
                ExecuteNextInstruction(bytes);
            }
        }

        public void ExecuteNextInstruction(byte[] bytes)
        {
            OpCodes op = (OpCodes)bytes[cursor];
            cursor++;
            switch (op)
            {
                case OpCodes.PUSH: push(NextOperand(bytes)); break;
                case OpCodes.ADD: add(); break;
                case OpCodes.SUB: sub(); break;
                case OpCodes.MUL: mul(); break;
                case OpCodes.DIV: div(); break;
                default: Console.Error.WriteLine($"Unknown Instruction: {op}"); break;
            }
        }

        private Operand NextOperand(byte[] bytes)
        {
            byte length = bytes[cursor];
            cursor++;

            Operand result;
            if(length == 0)
            {
                result = new AddressOperand(BitConverter.ToInt32(bytes, cursor));
            } else
            {
                result = new ConstantOperand(BitConverter.ToInt32(bytes, cursor));
            }
            cursor += length;
            return result;
        }

        private void push(Operand operand) => stack.Push(operand);
        private void add() => binaryOp((l, r) => l.Value + r.Value);
        private void sub() => binaryOp((l, r) => l.Value - r.Value);
        private void mul() => binaryOp((l, r) => l.Value * r.Value);
        private void div() => binaryOp((l, r) => l.Value / r.Value);

        private void binaryOp(Func<ConstantOperand, ConstantOperand, int> call)
        {
            var right = stack.Pop();
            var left = stack.Pop();
            if(left is ConstantOperand leftC && right is ConstantOperand rightC)
            {
                stack.Push(new ConstantOperand(call.Invoke(leftC, rightC)));
            } else
            {
                Console.Error.WriteLine("Executed Add Operation but Operands weren't Constants.");
            }
        }
    }
}
