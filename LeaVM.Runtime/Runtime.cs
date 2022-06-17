﻿using LeaVM.Core;
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
                result = new ConstantOperand(BitConverter.ToInt32(bytes, cursor));
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
        private void add() => binaryOp((l, r) => l.Value + r.Value);
        private void sub() => binaryOp((l, r) => l.Value - r.Value);
        private void mul() => binaryOp((l, r) => l.Value * r.Value);
        private void div() => binaryOp((l, r) => l.Value / r.Value);
        private void ceq() => boolBinaryOp((l, r) => l.Value == r.Value);
        private void cne() => boolBinaryOp((l, r) => l.Value != r.Value);
        private void cgt() => boolBinaryOp((l, r) => l.Value > r.Value);
        private void cge() => boolBinaryOp((l, r) => l.Value >= r.Value);
        private void clt() => boolBinaryOp((l, r) => l.Value < r.Value);
        private void cle() => boolBinaryOp((l, r) => l.Value <= r.Value);
        private void jmp(Operand operand)
        {
            if(operand is ConstantOperand cop)
            {
                cursor = cop.Value;
            } else
            {
                Console.Error.WriteLine("Can't jump to value from AddressOperand - use ConstantOperand instead.");
            }
        }
        private void jmb(Operand operand)
        {
            if (operand is ConstantOperand cop)
            {
                var value = ((ConstantOperand)stack.Pop()).Value == 1 ? true : false;
                if(value) cursor = cop.Value;
            }
            else
            {
                Console.Error.WriteLine("Can't jump to value from AddressOperand - use ConstantOperand instead.");
            }
        }

        private void binaryOp(Func<ConstantOperand, ConstantOperand, int> call)
        {
            var right = stack.Pop();
            var left = stack.Pop();
            if(left is ConstantOperand leftC && right is ConstantOperand rightC)
            {
                stack.Push(new ConstantOperand(call(leftC, rightC)));
            } else
            {
                Console.Error.WriteLine("Executed Add Operation but Operands weren't Constants.");
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
