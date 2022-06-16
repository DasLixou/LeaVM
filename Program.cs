using LeaVM.Builder;
using LeaVM.Core;
using LeaVM.Core.Operands;

var builder = new LeaBuilder();

builder.Emit(new Instruction(OpCodes.ADD, new ConstantOperand(1), new ConstantOperand(2)));

var a = 1;