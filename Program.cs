using LeaVM.Builder;
using LeaVM.Core;
using LeaVM.Core.Operands;

var builder = new LeaBuilder();

builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(1)));
builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(2)));
builder.Emit(new Instruction(OpCodes.ADD));

var result = builder.AsBytes();

var a = 1;