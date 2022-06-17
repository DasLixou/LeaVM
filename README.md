# LeaVM

## All Instructions

|OpCode|Operands               |Description                                                                   |
|------|-----------------------|------------------------------------------------------------------------------|
|`PUSH`|`Constant` or `Address`|Pushes the value onto the stack                                               |
|`POP` |`Address`              |Removes and stores the latest value in the address                            |
|`PEEK`|`Address`              |Stores the latest value in the address without removing                       |
|`ADD` |                       |Adds the last to values from the stack and pushes the result                  |
|`SUB` |                       |Subtracts the last to values from the stack and pushes the result             |
|`MUL` |                       |Multiplies the last to values from the stack and pushes the result            |
|`DIV` |                       |Divides the last to values from the stack and pushes the result               |
|`CEQ` |                       |Compares `==`, pushes 1 when 2 stackvalues equals, else 0                     |
|`CNE` |                       |Compares `!=`, pushes 1 when 2 stackvalues not equals, else 0                 |
|`CGT` |                       |Compares `>`, pushes 1 when stackvalue is greater then other, else 0          |
|`CGE` |                       |Compares `>=`, pushes 1 when stackvalue is greater/equals then other, else 0  |
|`CLT` |                       |Compares `<`, pushes 1 when stackvalue is smaller then other, else 0          |
|`CLE` |                       |Compares `<=`, pushes 1 when stackvalue is smaller/equals then other, else 0  |
|`JMP` |`Constant`             |Jumps to the byte of the constant                                             |
|`JMB` |`Constant`             |Only jumps to the byte of the constant when the last value from stack is 1    |

## Create ByteCode

```csharp
// First, import these namespaces
using LeaVM.Builder;
using LeaVM.Core;
using LeaVM.Core.Operands;

// Then create a LeaBuilder
var builder = new LeaBuilder();

// now you can create instructions like this
builder.Emit(new Instruction(OpCodes.PUSH, new ConstantOperand(2)));
// you can also make emit little bit more readable
builder.Emit(OpCodes.PUSH, 1);

// different instructions need different amounts and types of operands
builder.Emit(OpCodes.ADD);

// you can store values in memory with AddressOperands
builder.Emit(new Instruction(OpCodes.POP, Operand.Address(12))); // stores result of 1 + 2 in Address(12)
// new AddressOperand(12) would also work

// now generate the bytes
var bytes = builder.AsBytes();
```

## Running ByteCode

```csharp
// First, import this namespace
using LeaVM;

// Then create a new runtime instance
var runtime = new Runtime();

// now put your bytes in the runtime and let it run the instructions
runtime.Run(bytes) // you can use the bytes from "Create ByteCode"

// I like to make a unused instruction where i can create a breakpoint to see the result of the runtime
var i = 1; // create a breakpoint here and look up the runtime variable and it's memory and stack
```
