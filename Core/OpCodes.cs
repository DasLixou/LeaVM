namespace LeaVM.Core
{
    public enum OpCodes : byte
    {
        PUSH, POP, PEEK,
        ADD, SUB, MUL, DIV,
        /*==*/CEQ, /*!=*/CNE, /*>*/CGT, /*>=*/CGE, /*<*/CLT, /*<=*/CLE,
        JMP, JMB,
        CALL,
    }
}
