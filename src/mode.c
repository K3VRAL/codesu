#include "mode.h"

char *etsCommand(instruction com) {
    return  com == inpDig   ?   "inp,"  :
            com == inpAsc   ?   "inp;"  :
            com == jmpStr   ?   "jmp["  :
            com == jmpEnd   ?   "jmp]"  :
            com == pntLft   ?   "pnt<"  :
            com == pntRgt   ?   "pnt>"  :
            com == inc      ?   "inc+"  :
            com == dec      ?   "dec-"  :
            com == ran      ?   "rnd~"  :
            com == mulc     ?   "mul*"  :
            com == divc     ?   "div/"  :
            com == outDig   ?   "out."  :
            com == outAsc   ?   "out:"  :
                                "isnull";
}

char *etsType(types typ) {
    return  typ == circle   ?   "cirle"   :
            typ == slider   ?   "slider"  :
            typ == spinner  ?   "spinner" :
                                "isnull";
}