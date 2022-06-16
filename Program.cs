using LeaVM;
using LeaVM.Builder;
using LeaVM.Core;
using LeaVM.Core.Operands;

var builder = new LeaBuilder();

builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(1)));
builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(2)));
builder.Emit(new Instruction(OpCodes.ADD));
builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(3)));
builder.Emit(new Instruction(OpCodes.MUL));
builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(10)));
builder.Emit(new Instruction(OpCodes.CLT));

var result = builder.AsBytes();

var runtime = new Runtime();
runtime.Run(result);

var a = 1;