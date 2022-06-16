namespace LeaVM.Core
{
    public enum OpCodes : byte
    {
        PUSH, POP,
        ADD, SUB, MUL, DIV,
        /*==*/CEQ, /*!=*/CNE, /*>*/CGT, /*>=*/CGE, /*<*/CLT, /*<=*/CLE,
        CALL,
    }
}
