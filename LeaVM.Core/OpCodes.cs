namespace LeaVM.Core
{
    public enum OpCodes : byte
    {
        PUSH, POP, PEEK,
        ADD, SUB, MUL, DIV,
        /*==*/CEQ, /*!=*/CNE, /*>*/CGT, /*>=*/CGE, /*<*/CLT, /*<=*/CLE,
        AND, OR, XOR, NOT, NAND, NOR, XNOR,
        JMP, JMB,
        CALL,
    }
}
