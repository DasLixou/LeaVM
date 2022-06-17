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
