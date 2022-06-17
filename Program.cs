using LeaVM;
using LeaVM.Builder;
using LeaVM.Core;
using LeaVM.Core.Operands;

var builder = new LeaBuilder();

var counterAddress = new AddressOperand(2);
var condition = new AddressOperand(1);

builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(2)));
builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(1)));
builder.Emit(new Instruction(OpCodes.ADD));
builder.Emit(new Instruction(OpCodes.PEEK, counterAddress));
builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(10)));
builder.Emit(new Instruction(OpCodes.CLT));
builder.Emit(new Instruction(OpCodes.POP, condition));
builder.Emit(new Instruction(OpCodes.PUSH, counterAddress));
builder.Emit(new Instruction(OpCodes.PUSH, condition));
builder.Emit(new Instruction(OpCodes.JMB, new ConstantOperand(6)));

var result = builder.AsBytes();

var runtime = new Runtime();
runtime.Run(result);

var a = 1;