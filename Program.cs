using LeaVM;
using LeaVM.Builder;
using LeaVM.Core;
using LeaVM.Core.Operands;

var builder = new LeaBuilder();

var counterAddress = new AddressOperand(2);
var condition = new AddressOperand(1);

builder.Emit(OpCodes.PUSH, 2);
builder.Emit(OpCodes.PUSH, Operand.Constant(1));
builder.Emit(OpCodes.ADD);
builder.Emit(OpCodes.PEEK, counterAddress);
builder.Emit(OpCodes.PUSH, 10);
builder.Emit(OpCodes.CLT);
builder.Emit(OpCodes.POP, condition);
builder.Emit(OpCodes.PUSH, counterAddress);
builder.Emit(OpCodes.PUSH, condition);
builder.Emit(OpCodes.JMB, 6);

var result = builder.AsBytes();

var runtime = new Runtime();
runtime.Run(result);

var a = 1;