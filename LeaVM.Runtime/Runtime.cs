using LeaVM.Core;
using LeaVM.Core.Operands;

namespace LeaVM.Runtime
{
    public class Runtime
    {
        public Stack<object> stack = new();
        public int cursor = 0;
        public object[] memory = new object[64];

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
                case OpCodes.POP: pop(NextOperand(bytes)); break;
                case OpCodes.PEEK: peek(NextOperand(bytes)); break;
                case OpCodes.ADD: add(); break;
                case OpCodes.SUB: sub(); break;
                case OpCodes.MUL: mul(); break;
                case OpCodes.DIV: div(); break;
                case OpCodes.CEQ: ceq(); break;
                case OpCodes.CNE: cne(); break;
                case OpCodes.CGT: cgt(); break;
                case OpCodes.CGE: cge(); break;
                case OpCodes.CLT: clt(); break;
                case OpCodes.CLE: cle(); break;
                case OpCodes.AND: and(); break;
                case OpCodes.OR: or(); break;
                case OpCodes.XOR: xor(); break;
                case OpCodes.NOT: not(); break;
                case OpCodes.NAND: nand(); break;
                case OpCodes.NOR: nor(); break;
                case OpCodes.XNOR: xnor(); break;
                case OpCodes.JMP: jmp(NextOperand(bytes)); break;
                case OpCodes.JMB: jmb(NextOperand(bytes)); break;
                default: throw new Exception($"Unknown Instruction: {op}");
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
                length = 4;
            } else
            {
                result = new ConstantOperand(new LeaValue(new ReadOnlySpan<byte>(bytes, cursor, length)));
            }
            cursor += length;
            return result;
        }

        private void push(Operand operand)
        {
            if(operand is ConstantOperand cOp)
            {
                stack.Push(cOp);
            } else if(operand is AddressOperand cAd)
            {
                stack.Push(memory[cAd.Address]);
            }
        }

        private void pop(Operand operand)
        {
            if(operand is AddressOperand address)
            {
                memory[address.Address] = stack.Pop();
            } else
            {
                Console.Error.WriteLine("Can't pop value into ConstantOperand - use AddressOperand instead.");
            }
        }
        private void peek(Operand operand)
        {
            if (operand is AddressOperand address)
            {
                memory[address.Address] = stack.Peek();
            }
            else
            {
                Console.Error.WriteLine("Can't pop value into ConstantOperand - use AddressOperand instead.");
            }
        }
        private void add() => binaryOp((l, r) => l.Value.Value + r.Value.Value);
        private void sub() => binaryOp((l, r) => l.Value.Value - r.Value.Value);
        private void mul() => binaryOp((l, r) => l.Value.Value * r.Value.Value);
        private void div() => binaryOp((l, r) => l.Value.Value / r.Value.Value);
        private void ceq() => boolBinaryOp((l, r) => l.Value.Value == r.Value.Value);
        private void cne() => boolBinaryOp((l, r) => l.Value.Value != r.Value.Value);
        private void cgt() => boolBinaryOp((l, r) => l.Value.Value > r.Value.Value);
        private void cge() => boolBinaryOp((l, r) => l.Value.Value >= r.Value.Value);
        private void clt() => boolBinaryOp((l, r) => l.Value.Value < r.Value.Value);
        private void cle() => boolBinaryOp((l, r) => l.Value.Value <= r.Value.Value);
        private void and() => binaryOp((l, r) => l.Value.Value & r.Value.Value);
        private void or() => binaryOp((l, r) => l.Value.Value | r.Value.Value);
        private void xor() => binaryOp((l, r) => l.Value.Value ^ r.Value.Value);
        private void not() => unaryOp(element => ~element.Value.Value);
        private void nand() { and(); not(); }
        private void nor() { or(); not(); }
        private void xnor() { xor(); not(); }
        private void jmp(Operand operand)
        {
            if(operand is ConstantOperand cop)
            {
                cursor = cop.Value.Value;
            } else
            {
                Console.Error.WriteLine("Can't jump to value from AddressOperand - use ConstantOperand instead.");
            }
        }
        private void jmb(Operand operand)
        {
            if (operand is ConstantOperand cop)
            {
                var value = ((ConstantOperand)stack.Pop()).Value.Value == 1 ? true : false;
                if(value) cursor = cop.Value.Value;
            }
            else
            {
                Console.Error.WriteLine("Can't jump to value from AddressOperand - use ConstantOperand instead.");
            }
        }

        private void unaryOp(Func<ConstantOperand, int> call)
        {
            var element = stack.Pop();
            if (element is ConstantOperand elementC)
            {
                stack.Push(new ConstantOperand(new LeaValue(call(elementC))));
            }
            else
            {
                Console.Error.WriteLine("Executed Unary Operation but Operands weren't Constants.");
            }
        }

        private void binaryOp(Func<ConstantOperand, ConstantOperand, int> call)
        {
            var right = stack.Pop();
            var left = stack.Pop();
            if(left is ConstantOperand leftC && right is ConstantOperand rightC)
            {
                stack.Push(new ConstantOperand(new LeaValue(call(leftC, rightC))));
            } else
            {
                Console.Error.WriteLine("Executed Binary Operation but Operands weren't Constants.");
            }
        }

        private void boolBinaryOp(Func<ConstantOperand, ConstantOperand, bool> call)
        {
            binaryOp((l, r) =>
            {
                var result = call(l, r);
                return result ? 1 : 0;
            });
        }
    }
}
