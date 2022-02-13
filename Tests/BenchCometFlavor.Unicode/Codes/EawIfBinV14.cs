using CometFlavor.Unicode;
namespace BenchCometFlavor.Unicode.Codes;

public static class EawIfBinV14
{
    public static EastAsianWidth GetEastAsianWidth(int code)
    {
        if (code < 0x0026BF)
        {
            if (code < 0x00124A)
            {
                if (code < 0x000870)
                {
                    if (code < 0x000168)
                    {
                        if (code < 0x0000F1)
                        {
                            if (code < 0x0000BB)
                            {
                                if (code < 0x0000A9)
                                {
                                    if (code < 0x0000A1)
                                    {
                                        if (code < 0x000020)
                                        {
                                            if (0x000000 <= code && code <= 0x00001F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00007E) return EastAsianWidth.Narrow;
                                        else
                                        {
                                            if (0x00007F <= code && code <= 0x0000A0) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0000A1) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x0000A4)
                                        {
                                            if (0x0000A2 <= code && code <= 0x0000A3) return EastAsianWidth.Narrow;
                                        }
                                        else if (code <= 0x0000A4) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0000A5)
                                            {
                                            }
                                            else if (code <= 0x0000A6) return EastAsianWidth.Narrow;
                                            else
                                            {
                                                if (0x0000A7 <= code && code <= 0x0000A8) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0000A9) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0000AD)
                                    {
                                        if (code < 0x0000AB)
                                        {
                                            if (0x0000AA <= code && code <= 0x0000AA) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0000AB) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0000AC <= code && code <= 0x0000AC) return EastAsianWidth.Narrow;
                                        }
                                    }
                                    else if (code <= 0x0000AE) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x0000B0)
                                        {
                                            if (0x0000AF <= code && code <= 0x0000AF) return EastAsianWidth.Narrow;
                                        }
                                        else if (code <= 0x0000B4) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0000B5)
                                            {
                                            }
                                            else if (code <= 0x0000B5) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0000B6 <= code && code <= 0x0000BA) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x0000BB) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x0000DE)
                                {
                                    if (code < 0x0000C7)
                                    {
                                        if (code < 0x0000C0)
                                        {
                                            if (0x0000BC <= code && code <= 0x0000BF) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0000C5) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0000C6 <= code && code <= 0x0000C6) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0000CF) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0000D1)
                                        {
                                            if (0x0000D0 <= code && code <= 0x0000D0) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0000D6) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0000D7)
                                            {
                                            }
                                            else if (code <= 0x0000D8) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0000D9 <= code && code <= 0x0000DD) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0000E1) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x0000E8)
                                    {
                                        if (code < 0x0000E6)
                                        {
                                            if (0x0000E2 <= code && code <= 0x0000E5) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0000E6) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x0000E7 <= code && code <= 0x0000E7) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0000EA) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x0000EC)
                                        {
                                            if (0x0000EB <= code && code <= 0x0000EB) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0000ED) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0000EE)
                                            {
                                            }
                                            else if (code <= 0x0000EF) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0000F0 <= code && code <= 0x0000F0) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x0000F1) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x000128)
                            {
                                if (code < 0x000101)
                                {
                                    if (code < 0x0000FB)
                                    {
                                        if (code < 0x0000F4)
                                        {
                                            if (0x0000F2 <= code && code <= 0x0000F3) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0000F6) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0000F7 <= code && code <= 0x0000FA) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0000FB) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0000FD)
                                        {
                                            if (0x0000FC <= code && code <= 0x0000FC) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0000FD) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0000FE)
                                            {
                                            }
                                            else if (code <= 0x0000FE) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0000FF <= code && code <= 0x000100) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000101) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x000113)
                                    {
                                        if (code < 0x000111)
                                        {
                                            if (0x000102 <= code && code <= 0x000110) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000111) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x000112 <= code && code <= 0x000112) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000113) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x00011B)
                                        {
                                            if (0x000114 <= code && code <= 0x00011A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00011B) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x00011C)
                                            {
                                            }
                                            else if (code <= 0x000125) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000126 <= code && code <= 0x000127) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x00012A) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x000144)
                                {
                                    if (code < 0x000134)
                                    {
                                        if (code < 0x00012C)
                                        {
                                            if (0x00012B <= code && code <= 0x00012B) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x000130) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000131 <= code && code <= 0x000133) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x000137) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000139)
                                        {
                                            if (0x000138 <= code && code <= 0x000138) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x00013E) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00013F)
                                            {
                                            }
                                            else if (code <= 0x000142) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x000143 <= code && code <= 0x000143) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000144) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x00014D)
                                    {
                                        if (code < 0x000148)
                                        {
                                            if (0x000145 <= code && code <= 0x000147) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00014B) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x00014C <= code && code <= 0x00014C) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00014D) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x000152)
                                        {
                                            if (0x00014E <= code && code <= 0x000151) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000153) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x000154)
                                            {
                                            }
                                            else if (code <= 0x000165) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000166 <= code && code <= 0x000167) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x00016A) return EastAsianWidth.Neutral;
                    else
                    {
                        if (code < 0x0002DE)
                        {
                            if (code < 0x0001DD)
                            {
                                if (code < 0x0001D4)
                                {
                                    if (code < 0x0001CF)
                                    {
                                        if (code < 0x00016C)
                                        {
                                            if (0x00016B <= code && code <= 0x00016B) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0001CD) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0001CE <= code && code <= 0x0001CE) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0001CF) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0001D1)
                                        {
                                            if (0x0001D0 <= code && code <= 0x0001D0) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0001D1) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0001D2)
                                            {
                                            }
                                            else if (code <= 0x0001D2) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0001D3 <= code && code <= 0x0001D3) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0001D4) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x0001D8)
                                    {
                                        if (code < 0x0001D6)
                                        {
                                            if (0x0001D5 <= code && code <= 0x0001D5) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0001D6) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x0001D7 <= code && code <= 0x0001D7) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0001D8) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x0001DA)
                                        {
                                            if (0x0001D9 <= code && code <= 0x0001D9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0001DA) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0001DB)
                                            {
                                            }
                                            else if (code <= 0x0001DB) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0001DC <= code && code <= 0x0001DC) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x000250) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x0002C9)
                                {
                                    if (code < 0x000262)
                                    {
                                        if (code < 0x000252)
                                        {
                                            if (0x000251 <= code && code <= 0x000251) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x000260) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000261 <= code && code <= 0x000261) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0002C3) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0002C5)
                                        {
                                            if (0x0002C4 <= code && code <= 0x0002C4) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0002C6) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0002C7)
                                            {
                                            }
                                            else if (code <= 0x0002C7) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0002C8 <= code && code <= 0x0002C8) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0002CB) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x0002D0)
                                    {
                                        if (code < 0x0002CD)
                                        {
                                            if (0x0002CC <= code && code <= 0x0002CC) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0002CD) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x0002CE <= code && code <= 0x0002CF) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0002D0) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x0002D8)
                                        {
                                            if (0x0002D1 <= code && code <= 0x0002D7) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0002DB) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0002DC)
                                            {
                                            }
                                            else if (code <= 0x0002DC) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0002DD <= code && code <= 0x0002DD) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x0002DE) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x000410)
                            {
                                if (code < 0x000391)
                                {
                                    if (code < 0x000370)
                                    {
                                        if (code < 0x0002E0)
                                        {
                                            if (0x0002DF <= code && code <= 0x0002DF) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0002FF) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000300 <= code && code <= 0x00036F) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x000377) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000384)
                                        {
                                            if (0x00037A <= code && code <= 0x00037F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00038A) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00038C)
                                            {
                                            }
                                            else if (code <= 0x00038C) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00038E <= code && code <= 0x000390) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0003A1) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x0003C2)
                                    {
                                        if (code < 0x0003AA)
                                        {
                                            if (0x0003A3 <= code && code <= 0x0003A9) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0003B0) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0003B1 <= code && code <= 0x0003C1) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0003C2) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0003CA)
                                        {
                                            if (0x0003C3 <= code && code <= 0x0003C9) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x000400) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000401)
                                            {
                                            }
                                            else if (code <= 0x000401) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x000402 <= code && code <= 0x00040F) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x00044F) return EastAsianWidth.Ambiguous;
                            else
                            {
                                if (code < 0x0005EF)
                                {
                                    if (code < 0x000531)
                                    {
                                        if (code < 0x000451)
                                        {
                                            if (0x000450 <= code && code <= 0x000450) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000451) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x000452 <= code && code <= 0x00052F) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000556) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00058D)
                                        {
                                            if (0x000559 <= code && code <= 0x00058A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00058F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000591)
                                            {
                                            }
                                            else if (code <= 0x0005C7) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0005D0 <= code && code <= 0x0005EA) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0005F4) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0007FD)
                                    {
                                        if (code < 0x00070F)
                                        {
                                            if (0x000600 <= code && code <= 0x00070D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00074A) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00074D)
                                            {
                                            }
                                            else if (code <= 0x0007B1) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0007C0 <= code && code <= 0x0007FA) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                    else if (code <= 0x00082D) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000840)
                                        {
                                            if (0x000830 <= code && code <= 0x00083E) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00085B) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00085E)
                                            {
                                            }
                                            else if (code <= 0x00085E) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000860 <= code && code <= 0x00086A) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (code <= 0x00088E) return EastAsianWidth.Neutral;
                else
                {
                    if (code < 0x000BCA)
                    {
                        if (code < 0x000AAA)
                        {
                            if (code < 0x000A0F)
                            {
                                if (code < 0x0009BC)
                                {
                                    if (code < 0x00098F)
                                    {
                                        if (code < 0x000898)
                                        {
                                            if (0x000890 <= code && code <= 0x000891) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000983) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000985 <= code && code <= 0x00098C) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000990) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0009AA)
                                        {
                                            if (0x000993 <= code && code <= 0x0009A8) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0009B0) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0009B2)
                                            {
                                            }
                                            else if (code <= 0x0009B2) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0009B6 <= code && code <= 0x0009B9) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0009C4) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0009DC)
                                    {
                                        if (code < 0x0009CB)
                                        {
                                            if (0x0009C7 <= code && code <= 0x0009C8) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0009CE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0009D7 <= code && code <= 0x0009D7) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0009DD) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0009E6)
                                        {
                                            if (0x0009DF <= code && code <= 0x0009E3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0009FE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000A01)
                                            {
                                            }
                                            else if (code <= 0x000A03) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000A05 <= code && code <= 0x000A0A) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x000A10) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x000A4B)
                                {
                                    if (code < 0x000A35)
                                    {
                                        if (code < 0x000A2A)
                                        {
                                            if (0x000A13 <= code && code <= 0x000A28) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000A30) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000A32 <= code && code <= 0x000A33) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000A36) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000A3C)
                                        {
                                            if (0x000A38 <= code && code <= 0x000A39) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000A3C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000A3E)
                                            {
                                            }
                                            else if (code <= 0x000A42) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000A47 <= code && code <= 0x000A48) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000A4D) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x000A66)
                                    {
                                        if (code < 0x000A59)
                                        {
                                            if (0x000A51 <= code && code <= 0x000A51) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000A5C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000A5E <= code && code <= 0x000A5E) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000A76) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000A85)
                                        {
                                            if (0x000A81 <= code && code <= 0x000A83) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000A8D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000A8F)
                                            {
                                            }
                                            else if (code <= 0x000A91) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000A93 <= code && code <= 0x000AA8) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x000AB0) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x000B47)
                            {
                                if (code < 0x000AF9)
                                {
                                    if (code < 0x000AC7)
                                    {
                                        if (code < 0x000AB5)
                                        {
                                            if (0x000AB2 <= code && code <= 0x000AB3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000AB9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000ABC <= code && code <= 0x000AC5) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000AC9) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000AD0)
                                        {
                                            if (0x000ACB <= code && code <= 0x000ACD) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000AD0) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000AE0)
                                            {
                                            }
                                            else if (code <= 0x000AE3) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000AE6 <= code && code <= 0x000AF1) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000AFF) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x000B13)
                                    {
                                        if (code < 0x000B05)
                                        {
                                            if (0x000B01 <= code && code <= 0x000B03) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000B0C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000B0F <= code && code <= 0x000B10) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000B28) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000B32)
                                        {
                                            if (0x000B2A <= code && code <= 0x000B30) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000B33) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000B35)
                                            {
                                            }
                                            else if (code <= 0x000B39) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000B3C <= code && code <= 0x000B44) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x000B48) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x000B92)
                                {
                                    if (code < 0x000B5F)
                                    {
                                        if (code < 0x000B55)
                                        {
                                            if (0x000B4B <= code && code <= 0x000B4D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000B57) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000B5C <= code && code <= 0x000B5D) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000B63) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000B82)
                                        {
                                            if (0x000B66 <= code && code <= 0x000B77) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000B83) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000B85)
                                            {
                                            }
                                            else if (code <= 0x000B8A) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000B8E <= code && code <= 0x000B90) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000B95) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x000BA3)
                                    {
                                        if (code < 0x000B9C)
                                        {
                                            if (0x000B99 <= code && code <= 0x000B9A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000B9C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000B9E <= code && code <= 0x000B9F) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000BA4) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000BAE)
                                        {
                                            if (0x000BA8 <= code && code <= 0x000BAA) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000BB9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000BBE)
                                            {
                                            }
                                            else if (code <= 0x000BC2) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000BC6 <= code && code <= 0x000BC8) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x000BCD) return EastAsianWidth.Neutral;
                    else
                    {
                        if (code < 0x000D81)
                        {
                            if (code < 0x000C92)
                            {
                                if (code < 0x000C46)
                                {
                                    if (code < 0x000C00)
                                    {
                                        if (code < 0x000BD7)
                                        {
                                            if (0x000BD0 <= code && code <= 0x000BD0) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000BD7) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000BE6 <= code && code <= 0x000BFA) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000C0C) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000C12)
                                        {
                                            if (0x000C0E <= code && code <= 0x000C10) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000C28) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000C2A)
                                            {
                                            }
                                            else if (code <= 0x000C39) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000C3C <= code && code <= 0x000C44) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000C48) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x000C5D)
                                    {
                                        if (code < 0x000C55)
                                        {
                                            if (0x000C4A <= code && code <= 0x000C4D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000C56) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000C58 <= code && code <= 0x000C5A) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000C5D) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000C66)
                                        {
                                            if (0x000C60 <= code && code <= 0x000C63) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000C6F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000C77)
                                            {
                                            }
                                            else if (code <= 0x000C8C) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000C8E <= code && code <= 0x000C90) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x000CA8) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x000CE6)
                                {
                                    if (code < 0x000CC6)
                                    {
                                        if (code < 0x000CB5)
                                        {
                                            if (0x000CAA <= code && code <= 0x000CB3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000CB9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000CBC <= code && code <= 0x000CC4) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000CC8) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000CD5)
                                        {
                                            if (0x000CCA <= code && code <= 0x000CCD) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000CD6) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000CDD)
                                            {
                                            }
                                            else if (code <= 0x000CDE) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000CE0 <= code && code <= 0x000CE3) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000CEF) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x000D12)
                                    {
                                        if (code < 0x000D00)
                                        {
                                            if (0x000CF1 <= code && code <= 0x000CF2) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000D0C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000D0E <= code && code <= 0x000D10) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000D44) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000D4A)
                                        {
                                            if (0x000D46 <= code && code <= 0x000D48) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000D4F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000D54)
                                            {
                                            }
                                            else if (code <= 0x000D63) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000D66 <= code && code <= 0x000D7F) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x000D83) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x000EA5)
                            {
                                if (code < 0x000DD8)
                                {
                                    if (code < 0x000DBD)
                                    {
                                        if (code < 0x000D9A)
                                        {
                                            if (0x000D85 <= code && code <= 0x000D96) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000DB1) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000DB3 <= code && code <= 0x000DBB) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000DBD) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000DCA)
                                        {
                                            if (0x000DC0 <= code && code <= 0x000DC6) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000DCA) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000DCF)
                                            {
                                            }
                                            else if (code <= 0x000DD4) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000DD6 <= code && code <= 0x000DD6) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000DDF) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x000E3F)
                                    {
                                        if (code < 0x000DF2)
                                        {
                                            if (0x000DE6 <= code && code <= 0x000DEF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000DF4) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000E01 <= code && code <= 0x000E3A) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000E5B) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000E84)
                                        {
                                            if (0x000E81 <= code && code <= 0x000E82) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000E84) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000E86)
                                            {
                                            }
                                            else if (code <= 0x000E8A) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000E8C <= code && code <= 0x000EA3) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x000EA5) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x000F71)
                                {
                                    if (code < 0x000EC8)
                                    {
                                        if (code < 0x000EC0)
                                        {
                                            if (0x000EA7 <= code && code <= 0x000EBD) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000EC4) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x000EC6 <= code && code <= 0x000EC6) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x000ECD) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x000EDC)
                                        {
                                            if (0x000ED0 <= code && code <= 0x000ED9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000EDF) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000F00)
                                            {
                                            }
                                            else if (code <= 0x000F47) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x000F49 <= code && code <= 0x000F6C) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x000F97) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0010C7)
                                    {
                                        if (code < 0x000FBE)
                                        {
                                            if (0x000F99 <= code && code <= 0x000FBC) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x000FCC) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x000FCE)
                                            {
                                            }
                                            else if (code <= 0x000FDA) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x001000 <= code && code <= 0x0010C5) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                    else if (code <= 0x0010C7) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0010D0)
                                        {
                                            if (0x0010CD <= code && code <= 0x0010CD) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0010FF) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x001100)
                                            {
                                            }
                                            else if (code <= 0x00115F) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x001160 <= code && code <= 0x001248) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (code <= 0x00124D) return EastAsianWidth.Neutral;
            else
            {
                if (code < 0x0021D4)
                {
                    if (code < 0x001FDD)
                    {
                        if (code < 0x001930)
                        {
                            if (code < 0x0013F8)
                            {
                                if (code < 0x0012C0)
                                {
                                    if (code < 0x001260)
                                    {
                                        if (code < 0x001258)
                                        {
                                            if (0x001250 <= code && code <= 0x001256) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001258) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x00125A <= code && code <= 0x00125D) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x001288) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x001290)
                                        {
                                            if (0x00128A <= code && code <= 0x00128D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0012B0) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0012B2)
                                            {
                                            }
                                            else if (code <= 0x0012B5) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0012B8 <= code && code <= 0x0012BE) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0012C0) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x001312)
                                    {
                                        if (code < 0x0012C8)
                                        {
                                            if (0x0012C2 <= code && code <= 0x0012C5) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0012D6) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0012D8 <= code && code <= 0x001310) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x001315) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00135D)
                                        {
                                            if (0x001318 <= code && code <= 0x00135A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00137C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x001380)
                                            {
                                            }
                                            else if (code <= 0x001399) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0013A0 <= code && code <= 0x0013F5) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x0013FD) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x001780)
                                {
                                    if (code < 0x00171F)
                                    {
                                        if (code < 0x0016A0)
                                        {
                                            if (0x001400 <= code && code <= 0x00169C) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0016F8) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x001700 <= code && code <= 0x001715) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x001736) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x001760)
                                        {
                                            if (0x001740 <= code && code <= 0x001753) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00176C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00176E)
                                            {
                                            }
                                            else if (code <= 0x001770) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x001772 <= code && code <= 0x001773) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0017DD) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x001820)
                                    {
                                        if (code < 0x0017F0)
                                        {
                                            if (0x0017E0 <= code && code <= 0x0017E9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0017F9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x001800 <= code && code <= 0x001819) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x001878) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0018B0)
                                        {
                                            if (0x001880 <= code && code <= 0x0018AA) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0018F5) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x001900)
                                            {
                                            }
                                            else if (code <= 0x00191E) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x001920 <= code && code <= 0x00192B) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x00193B) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x001C3B)
                            {
                                if (code < 0x001A60)
                                {
                                    if (code < 0x001980)
                                    {
                                        if (code < 0x001944)
                                        {
                                            if (0x001940 <= code && code <= 0x001940) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00196D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x001970 <= code && code <= 0x001974) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0019AB) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0019D0)
                                        {
                                            if (0x0019B0 <= code && code <= 0x0019C9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0019DA) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0019DE)
                                            {
                                            }
                                            else if (code <= 0x001A1B) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x001A1E <= code && code <= 0x001A5E) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x001A7C) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x001AB0)
                                    {
                                        if (code < 0x001A90)
                                        {
                                            if (0x001A7F <= code && code <= 0x001A89) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001A99) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x001AA0 <= code && code <= 0x001AAD) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x001ACE) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x001B50)
                                        {
                                            if (0x001B00 <= code && code <= 0x001B4C) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001B7E) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x001B80)
                                            {
                                            }
                                            else if (code <= 0x001BF3) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x001BFC <= code && code <= 0x001C37) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x001C49) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x001F50)
                                {
                                    if (code < 0x001CD0)
                                    {
                                        if (code < 0x001C90)
                                        {
                                            if (0x001C4D <= code && code <= 0x001C88) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001CBA) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x001CBD <= code && code <= 0x001CC7) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x001CFA) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x001F18)
                                        {
                                            if (0x001D00 <= code && code <= 0x001F15) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001F1D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x001F20)
                                            {
                                            }
                                            else if (code <= 0x001F45) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x001F48 <= code && code <= 0x001F4D) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x001F57) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x001F5F)
                                    {
                                        if (code < 0x001F5B)
                                        {
                                            if (0x001F59 <= code && code <= 0x001F59) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001F5B) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x001F5D <= code && code <= 0x001F5D) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x001F7D) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x001FB6)
                                        {
                                            if (0x001F80 <= code && code <= 0x001FB4) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001FC4) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x001FC6)
                                            {
                                            }
                                            else if (code <= 0x001FD3) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x001FD6 <= code && code <= 0x001FDB) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x001FEF) return EastAsianWidth.Neutral;
                    else
                    {
                        if (code < 0x0020AA)
                        {
                            if (code < 0x002032)
                            {
                                if (code < 0x00201A)
                                {
                                    if (code < 0x002010)
                                    {
                                        if (code < 0x001FF6)
                                        {
                                            if (0x001FF2 <= code && code <= 0x001FF4) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x001FFE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002000 <= code && code <= 0x00200F) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002010) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002013)
                                        {
                                            if (0x002011 <= code && code <= 0x002012) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002016) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002017)
                                            {
                                            }
                                            else if (code <= 0x002017) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002018 <= code && code <= 0x002019) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00201B) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x002023)
                                    {
                                        if (code < 0x00201E)
                                        {
                                            if (0x00201C <= code && code <= 0x00201D) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x00201F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002020 <= code && code <= 0x002022) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x002023) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002028)
                                        {
                                            if (0x002024 <= code && code <= 0x002027) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x00202F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002030)
                                            {
                                            }
                                            else if (code <= 0x002030) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x002031 <= code && code <= 0x002031) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x002033) return EastAsianWidth.Ambiguous;
                            else
                            {
                                if (code < 0x002074)
                                {
                                    if (code < 0x00203B)
                                    {
                                        if (code < 0x002035)
                                        {
                                            if (0x002034 <= code && code <= 0x002034) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002035) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x002036 <= code && code <= 0x00203A) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00203B) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x00203E)
                                        {
                                            if (0x00203C <= code && code <= 0x00203D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00203E) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x00203F)
                                            {
                                            }
                                            else if (code <= 0x002064) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002066 <= code && code <= 0x002071) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x002074) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x002081)
                                    {
                                        if (code < 0x00207F)
                                        {
                                            if (0x002075 <= code && code <= 0x00207E) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00207F) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x002080 <= code && code <= 0x002080) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002084) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002090)
                                        {
                                            if (0x002085 <= code && code <= 0x00208E) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00209C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0020A0)
                                            {
                                            }
                                            else if (code <= 0x0020A8) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0020A9 <= code && code <= 0x0020A9) return EastAsianWidth.Half;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x0020AB) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x002127)
                            {
                                if (code < 0x002109)
                                {
                                    if (code < 0x002100)
                                    {
                                        if (code < 0x0020AD)
                                        {
                                            if (0x0020AC <= code && code <= 0x0020AC) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0020C0) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0020D0 <= code && code <= 0x0020F0) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002102) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002104)
                                        {
                                            if (0x002103 <= code && code <= 0x002103) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002104) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002105)
                                            {
                                            }
                                            else if (code <= 0x002105) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x002106 <= code && code <= 0x002108) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x002109) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x002116)
                                    {
                                        if (code < 0x002113)
                                        {
                                            if (0x00210A <= code && code <= 0x002112) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002113) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x002114 <= code && code <= 0x002115) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002116) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002121)
                                        {
                                            if (0x002117 <= code && code <= 0x002120) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002122) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002123)
                                            {
                                            }
                                            else if (code <= 0x002125) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002126 <= code && code <= 0x002126) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x00212A) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x002170)
                                {
                                    if (code < 0x002155)
                                    {
                                        if (code < 0x00212C)
                                        {
                                            if (0x00212B <= code && code <= 0x00212B) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002152) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002153 <= code && code <= 0x002154) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x00215A) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00215F)
                                        {
                                            if (0x00215B <= code && code <= 0x00215E) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x00215F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002160)
                                            {
                                            }
                                            else if (code <= 0x00216B) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x00216C <= code && code <= 0x00216F) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x002179) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x00219A)
                                    {
                                        if (code < 0x002189)
                                        {
                                            if (0x00217A <= code && code <= 0x002188) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002189) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x00218A)
                                            {
                                            }
                                            else if (code <= 0x00218B) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002190 <= code && code <= 0x002199) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                    else if (code <= 0x0021B7) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0021BA)
                                        {
                                            if (0x0021B8 <= code && code <= 0x0021B9) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0021D1) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0021D2)
                                            {
                                            }
                                            else if (code <= 0x0021D2) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0021D3 <= code && code <= 0x0021D3) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (code <= 0x0021D4) return EastAsianWidth.Ambiguous;
                else
                {
                    if (code < 0x002460)
                    {
                        if (code < 0x00224C)
                        {
                            if (code < 0x00221A)
                            {
                                if (code < 0x002209)
                                {
                                    if (code < 0x002200)
                                    {
                                        if (code < 0x0021E7)
                                        {
                                            if (0x0021D5 <= code && code <= 0x0021E6) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0021E7) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x0021E8 <= code && code <= 0x0021FF) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002200) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002202)
                                        {
                                            if (0x002201 <= code && code <= 0x002201) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002203) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002204)
                                            {
                                            }
                                            else if (code <= 0x002206) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002207 <= code && code <= 0x002208) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00220A) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x002210)
                                    {
                                        if (code < 0x00220C)
                                        {
                                            if (0x00220B <= code && code <= 0x00220B) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x00220E) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x00220F <= code && code <= 0x00220F) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x002210) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002212)
                                        {
                                            if (0x002211 <= code && code <= 0x002211) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002214) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002215)
                                            {
                                            }
                                            else if (code <= 0x002215) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x002216 <= code && code <= 0x002219) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x00221A) return EastAsianWidth.Ambiguous;
                            else
                            {
                                if (code < 0x00222D)
                                {
                                    if (code < 0x002223)
                                    {
                                        if (code < 0x00221D)
                                        {
                                            if (0x00221B <= code && code <= 0x00221C) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002220) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x002221 <= code && code <= 0x002222) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002223) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002225)
                                        {
                                            if (0x002224 <= code && code <= 0x002224) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002225) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002226)
                                            {
                                            }
                                            else if (code <= 0x002226) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002227 <= code && code <= 0x00222C) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00222D) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x002238)
                                    {
                                        if (code < 0x00222F)
                                        {
                                            if (0x00222E <= code && code <= 0x00222E) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002233) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002234 <= code && code <= 0x002237) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x00223B) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00223E)
                                        {
                                            if (0x00223C <= code && code <= 0x00223D) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002247) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002248)
                                            {
                                            }
                                            else if (code <= 0x002248) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x002249 <= code && code <= 0x00224B) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x00224C) return EastAsianWidth.Ambiguous;
                        else
                        {
                            if (code < 0x002299)
                            {
                                if (code < 0x00226C)
                                {
                                    if (code < 0x002260)
                                    {
                                        if (code < 0x002252)
                                        {
                                            if (0x00224D <= code && code <= 0x002251) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002252) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x002253 <= code && code <= 0x00225F) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002261) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002264)
                                        {
                                            if (0x002262 <= code && code <= 0x002263) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002267) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002268)
                                            {
                                            }
                                            else if (code <= 0x002269) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00226A <= code && code <= 0x00226B) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00226D) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x002284)
                                    {
                                        if (code < 0x002270)
                                        {
                                            if (0x00226E <= code && code <= 0x00226F) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002281) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002282 <= code && code <= 0x002283) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x002285) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002288)
                                        {
                                            if (0x002286 <= code && code <= 0x002287) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002294) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002295)
                                            {
                                            }
                                            else if (code <= 0x002295) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x002296 <= code && code <= 0x002298) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x002299) return EastAsianWidth.Ambiguous;
                            else
                            {
                                if (code < 0x00231C)
                                {
                                    if (code < 0x0022BF)
                                    {
                                        if (code < 0x0022A5)
                                        {
                                            if (0x00229A <= code && code <= 0x0022A4) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0022A5) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x0022A6 <= code && code <= 0x0022BE) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0022BF) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002312)
                                        {
                                            if (0x0022C0 <= code && code <= 0x002311) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002312) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002313)
                                            {
                                            }
                                            else if (code <= 0x002319) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00231A <= code && code <= 0x00231B) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x002328) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0023F0)
                                    {
                                        if (code < 0x00232B)
                                        {
                                            if (0x002329 <= code && code <= 0x00232A) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x0023E8) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0023E9)
                                            {
                                            }
                                            else if (code <= 0x0023EC) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x0023ED <= code && code <= 0x0023EF) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                    else if (code <= 0x0023F0) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x0023F3)
                                        {
                                            if (0x0023F1 <= code && code <= 0x0023F2) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0023F3) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x0023F4)
                                            {
                                            }
                                            else if (code <= 0x002426) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002440 <= code && code <= 0x00244A) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x0024E9) return EastAsianWidth.Ambiguous;
                    else
                    {
                        if (code < 0x002609)
                        {
                            if (code < 0x0025BC)
                            {
                                if (code < 0x002596)
                                {
                                    if (code < 0x002550)
                                    {
                                        if (code < 0x0024EB)
                                        {
                                            if (0x0024EA <= code && code <= 0x0024EA) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00254B) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x00254C <= code && code <= 0x00254F) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002573) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x002580)
                                        {
                                            if (0x002574 <= code && code <= 0x00257F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00258F) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002590)
                                            {
                                            }
                                            else if (code <= 0x002591) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002592 <= code && code <= 0x002595) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00259F) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0025AA)
                                    {
                                        if (code < 0x0025A2)
                                        {
                                            if (0x0025A0 <= code && code <= 0x0025A1) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0025A2) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0025A3 <= code && code <= 0x0025A9) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0025B1) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0025B4)
                                        {
                                            if (0x0025B2 <= code && code <= 0x0025B3) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0025B5) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0025B6)
                                            {
                                            }
                                            else if (code <= 0x0025B7) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0025B8 <= code && code <= 0x0025BB) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x0025BD) return EastAsianWidth.Ambiguous;
                            else
                            {
                                if (code < 0x0025D2)
                                {
                                    if (code < 0x0025C6)
                                    {
                                        if (code < 0x0025C0)
                                        {
                                            if (0x0025BE <= code && code <= 0x0025BF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0025C1) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x0025C2 <= code && code <= 0x0025C5) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0025C8) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x0025CB)
                                        {
                                            if (0x0025C9 <= code && code <= 0x0025CA) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0025CB) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0025CC)
                                            {
                                            }
                                            else if (code <= 0x0025CD) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0025CE <= code && code <= 0x0025D1) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0025E1) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0025F0)
                                    {
                                        if (code < 0x0025E6)
                                        {
                                            if (0x0025E2 <= code && code <= 0x0025E5) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0025EE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0025EF <= code && code <= 0x0025EF) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0025FC) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0025FF)
                                        {
                                            if (0x0025FD <= code && code <= 0x0025FE) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x002604) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002605)
                                            {
                                            }
                                            else if (code <= 0x002606) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x002607 <= code && code <= 0x002608) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x002609) return EastAsianWidth.Ambiguous;
                        else
                        {
                            if (code < 0x002663)
                            {
                                if (code < 0x00261F)
                                {
                                    if (code < 0x002614)
                                    {
                                        if (code < 0x00260E)
                                        {
                                            if (0x00260A <= code && code <= 0x00260D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00260F) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x002610 <= code && code <= 0x002613) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002615) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x00261C)
                                        {
                                            if (0x002616 <= code && code <= 0x00261B) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00261C) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x00261D)
                                            {
                                            }
                                            else if (code <= 0x00261D) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00261E <= code && code <= 0x00261E) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00263F) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x002643)
                                    {
                                        if (code < 0x002641)
                                        {
                                            if (0x002640 <= code && code <= 0x002640) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x002641) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002642 <= code && code <= 0x002642) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x002647) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002654)
                                        {
                                            if (0x002648 <= code && code <= 0x002653) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x00265F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002660)
                                            {
                                            }
                                            else if (code <= 0x002661) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x002662 <= code && code <= 0x002662) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x002665) return EastAsianWidth.Ambiguous;
                            else
                            {
                                if (code < 0x002680)
                                {
                                    if (code < 0x00266C)
                                    {
                                        if (code < 0x002667)
                                        {
                                            if (0x002666 <= code && code <= 0x002666) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00266A) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x00266B <= code && code <= 0x00266B) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00266D) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x00266F)
                                        {
                                            if (0x00266E <= code && code <= 0x00266E) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00266F) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x002670)
                                            {
                                            }
                                            else if (code <= 0x00267E) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00267F <= code && code <= 0x00267F) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x002692) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0026A1)
                                    {
                                        if (code < 0x002694)
                                        {
                                            if (0x002693 <= code && code <= 0x002693) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x00269D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00269E)
                                            {
                                            }
                                            else if (code <= 0x00269F) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0026A0 <= code && code <= 0x0026A0) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                    else if (code <= 0x0026A1) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x0026AA)
                                        {
                                            if (0x0026A2 <= code && code <= 0x0026A9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0026AB) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x0026AC)
                                            {
                                            }
                                            else if (code <= 0x0026BC) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0026BD <= code && code <= 0x0026BE) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (code <= 0x0026BF) return EastAsianWidth.Ambiguous;
        else
        {
            if (code < 0x011600)
            {
                if (code < 0x00FE30)
                {
                    if (code < 0x002DD0)
                    {
                        if (code < 0x002756)
                        {
                            if (code < 0x0026FA)
                            {
                                if (code < 0x0026E3)
                                {
                                    if (code < 0x0026CE)
                                    {
                                        if (code < 0x0026C4)
                                        {
                                            if (0x0026C0 <= code && code <= 0x0026C3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0026C5) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x0026C6 <= code && code <= 0x0026CD) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x0026CE) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x0026D4)
                                        {
                                            if (0x0026CF <= code && code <= 0x0026D3) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0026D4) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x0026D5)
                                            {
                                            }
                                            else if (code <= 0x0026E1) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x0026E2 <= code && code <= 0x0026E2) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0026E3) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x0026EB)
                                    {
                                        if (code < 0x0026E8)
                                        {
                                            if (0x0026E4 <= code && code <= 0x0026E7) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0026E9) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x0026EA <= code && code <= 0x0026EA) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x0026F1) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x0026F4)
                                        {
                                            if (0x0026F2 <= code && code <= 0x0026F3) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x0026F4) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0026F5)
                                            {
                                            }
                                            else if (code <= 0x0026F5) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x0026F6 <= code && code <= 0x0026F9) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x0026FA) return EastAsianWidth.Wide;
                            else
                            {
                                if (code < 0x002728)
                                {
                                    if (code < 0x002700)
                                    {
                                        if (code < 0x0026FD)
                                        {
                                            if (0x0026FB <= code && code <= 0x0026FC) return EastAsianWidth.Ambiguous;
                                        }
                                        else if (code <= 0x0026FD) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x0026FE <= code && code <= 0x0026FF) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x002704) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002706)
                                        {
                                            if (0x002705 <= code && code <= 0x002705) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x002709) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00270A)
                                            {
                                            }
                                            else if (code <= 0x00270B) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x00270C <= code && code <= 0x002727) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x002728) return EastAsianWidth.Wide;
                                else
                                {
                                    if (code < 0x00274C)
                                    {
                                        if (code < 0x00273D)
                                        {
                                            if (0x002729 <= code && code <= 0x00273C) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00273D) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x00273E <= code && code <= 0x00274B) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00274C) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x00274E)
                                        {
                                            if (0x00274D <= code && code <= 0x00274D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00274E) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x00274F)
                                            {
                                            }
                                            else if (code <= 0x002752) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002753 <= code && code <= 0x002755) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x002756) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x002B51)
                            {
                                if (code < 0x0027BF)
                                {
                                    if (code < 0x002780)
                                    {
                                        if (code < 0x002758)
                                        {
                                            if (0x002757 <= code && code <= 0x002757) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x002775) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002776 <= code && code <= 0x00277F) return EastAsianWidth.Ambiguous;
                                        }
                                    }
                                    else if (code <= 0x002794) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002798)
                                        {
                                            if (0x002795 <= code && code <= 0x002797) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x0027AF) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x0027B0)
                                            {
                                            }
                                            else if (code <= 0x0027B0) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x0027B1 <= code && code <= 0x0027BE) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0027BF) return EastAsianWidth.Wide;
                                else
                                {
                                    if (code < 0x002985)
                                    {
                                        if (code < 0x0027E6)
                                        {
                                            if (0x0027C0 <= code && code <= 0x0027E5) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0027ED) return EastAsianWidth.Narrow;
                                        else
                                        {
                                            if (0x0027EE <= code && code <= 0x002984) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002986) return EastAsianWidth.Narrow;
                                    else
                                    {
                                        if (code < 0x002B1B)
                                        {
                                            if (0x002987 <= code && code <= 0x002B1A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002B1C) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x002B1D)
                                            {
                                            }
                                            else if (code <= 0x002B4F) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002B50 <= code && code <= 0x002B50) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x002B54) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x002D30)
                                {
                                    if (code < 0x002B76)
                                    {
                                        if (code < 0x002B56)
                                        {
                                            if (0x002B55 <= code && code <= 0x002B55) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x002B59) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x002B5A <= code && code <= 0x002B73) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002B95) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002CF9)
                                        {
                                            if (0x002B97 <= code && code <= 0x002CF3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002D25) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002D27)
                                            {
                                            }
                                            else if (code <= 0x002D27) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002D2D <= code && code <= 0x002D2D) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x002D67) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x002DA8)
                                    {
                                        if (code < 0x002D7F)
                                        {
                                            if (0x002D6F <= code && code <= 0x002D70) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002D96) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002DA0 <= code && code <= 0x002DA6) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x002DAE) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x002DB8)
                                        {
                                            if (0x002DB0 <= code && code <= 0x002DB6) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002DBE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x002DC0)
                                            {
                                            }
                                            else if (code <= 0x002DC6) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x002DC8 <= code && code <= 0x002DCE) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x002DD6) return EastAsianWidth.Neutral;
                    else
                    {
                        if (code < 0x00A980)
                        {
                            if (code < 0x003250)
                            {
                                if (code < 0x00303F)
                                {
                                    if (code < 0x002E9B)
                                    {
                                        if (code < 0x002DE0)
                                        {
                                            if (0x002DD8 <= code && code <= 0x002DDE) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x002E5D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x002E80 <= code && code <= 0x002E99) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x002EF3) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x002FF0)
                                        {
                                            if (0x002F00 <= code && code <= 0x002FD5) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x002FFB) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x003000)
                                            {
                                            }
                                            else if (code <= 0x003000) return EastAsianWidth.Full;
                                            else
                                            {
                                                if (0x003001 <= code && code <= 0x00303E) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00303F) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x003131)
                                    {
                                        if (code < 0x003099)
                                        {
                                            if (0x003041 <= code && code <= 0x003096) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x0030FF) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x003105 <= code && code <= 0x00312F) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x00318E) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x0031F0)
                                        {
                                            if (0x003190 <= code && code <= 0x0031E3) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x00321E) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x003220)
                                            {
                                            }
                                            else if (code <= 0x003247) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x003248 <= code && code <= 0x00324F) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x004DBF) return EastAsianWidth.Wide;
                            else
                            {
                                if (code < 0x00A7D5)
                                {
                                    if (code < 0x00A4D0)
                                    {
                                        if (code < 0x004E00)
                                        {
                                            if (0x004DC0 <= code && code <= 0x004DFF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00A48C) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x00A490 <= code && code <= 0x00A4C6) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x00A62B) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00A700)
                                        {
                                            if (0x00A640 <= code && code <= 0x00A6F7) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00A7CA) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00A7D0)
                                            {
                                            }
                                            else if (code <= 0x00A7D1) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00A7D3 <= code && code <= 0x00A7D3) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00A7D9) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x00A880)
                                    {
                                        if (code < 0x00A830)
                                        {
                                            if (0x00A7F2 <= code && code <= 0x00A82C) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00A839) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x00A840 <= code && code <= 0x00A877) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00A8C5) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00A8E0)
                                        {
                                            if (0x00A8CE <= code && code <= 0x00A8D9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00A953) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00A95F)
                                            {
                                            }
                                            else if (code <= 0x00A95F) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00A960 <= code && code <= 0x00A97C) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x00A9CD) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x00D7CB)
                            {
                                if (code < 0x00AB09)
                                {
                                    if (code < 0x00AA40)
                                    {
                                        if (code < 0x00A9DE)
                                        {
                                            if (0x00A9CF <= code && code <= 0x00A9D9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00A9FE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x00AA00 <= code && code <= 0x00AA36) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00AA4D) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00AA5C)
                                        {
                                            if (0x00AA50 <= code && code <= 0x00AA59) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00AAC2) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00AADB)
                                            {
                                            }
                                            else if (code <= 0x00AAF6) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00AB01 <= code && code <= 0x00AB06) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00AB0E) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x00AB30)
                                    {
                                        if (code < 0x00AB20)
                                        {
                                            if (0x00AB11 <= code && code <= 0x00AB16) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00AB26) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x00AB28 <= code && code <= 0x00AB2E) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00AB6B) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00ABF0)
                                        {
                                            if (0x00AB70 <= code && code <= 0x00ABED) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00ABF9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00AC00)
                                            {
                                            }
                                            else if (code <= 0x00D7A3) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x00D7B0 <= code && code <= 0x00D7C6) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x00D7FB) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x00FB40)
                                {
                                    if (code < 0x00FB00)
                                    {
                                        if (code < 0x00E000)
                                        {
                                            if (0x00D800 <= code && code <= 0x00DFFF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00F8FF) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x00F900 <= code && code <= 0x00FAFF) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x00FB06) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00FB1D)
                                        {
                                            if (0x00FB13 <= code && code <= 0x00FB17) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00FB36) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00FB38)
                                            {
                                            }
                                            else if (code <= 0x00FB3C) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00FB3E <= code && code <= 0x00FB3E) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00FB41) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x00FDCF)
                                    {
                                        if (code < 0x00FB46)
                                        {
                                            if (0x00FB43 <= code && code <= 0x00FB44) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00FBC2) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x00FBD3)
                                            {
                                            }
                                            else if (code <= 0x00FD8F) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x00FD92 <= code && code <= 0x00FDC7) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                    else if (code <= 0x00FDCF) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00FE00)
                                        {
                                            if (0x00FDF0 <= code && code <= 0x00FDFF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00FE0F) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x00FE10)
                                            {
                                            }
                                            else if (code <= 0x00FE19) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x00FE20 <= code && code <= 0x00FE2F) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (code <= 0x00FE52) return EastAsianWidth.Wide;
                else
                {
                    if (code < 0x0109BC)
                    {
                        if (code < 0x01039F)
                        {
                            if (code < 0x010028)
                            {
                                if (code < 0x00FFCA)
                                {
                                    if (code < 0x00FE76)
                                    {
                                        if (code < 0x00FE68)
                                        {
                                            if (0x00FE54 <= code && code <= 0x00FE66) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x00FE6B) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x00FE70 <= code && code <= 0x00FE74) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x00FEFC) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x00FF01)
                                        {
                                            if (0x00FEFF <= code && code <= 0x00FEFF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00FF60) return EastAsianWidth.Full;
                                        else
                                        {
                                            if (code < 0x00FF61)
                                            {
                                            }
                                            else if (code <= 0x00FFBE) return EastAsianWidth.Half;
                                            else
                                            {
                                                if (0x00FFC2 <= code && code <= 0x00FFC7) return EastAsianWidth.Half;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x00FFCF) return EastAsianWidth.Half;
                                else
                                {
                                    if (code < 0x00FFE8)
                                    {
                                        if (code < 0x00FFDA)
                                        {
                                            if (0x00FFD2 <= code && code <= 0x00FFD7) return EastAsianWidth.Half;
                                        }
                                        else if (code <= 0x00FFDC) return EastAsianWidth.Half;
                                        else
                                        {
                                            if (0x00FFE0 <= code && code <= 0x00FFE6) return EastAsianWidth.Full;
                                        }
                                    }
                                    else if (code <= 0x00FFEE) return EastAsianWidth.Half;
                                    else
                                    {
                                        if (code < 0x00FFFD)
                                        {
                                            if (0x00FFF9 <= code && code <= 0x00FFFC) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x00FFFD) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x010000)
                                            {
                                            }
                                            else if (code <= 0x01000B) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01000D <= code && code <= 0x010026) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x01003A) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x0101A0)
                                {
                                    if (code < 0x010080)
                                    {
                                        if (code < 0x01003F)
                                        {
                                            if (0x01003C <= code && code <= 0x01003D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01004D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x010050 <= code && code <= 0x01005D) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0100FA) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x010107)
                                        {
                                            if (0x010100 <= code && code <= 0x010102) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010133) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010137)
                                            {
                                            }
                                            else if (code <= 0x01018E) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x010190 <= code && code <= 0x01019C) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0101A0) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0102E0)
                                    {
                                        if (code < 0x010280)
                                        {
                                            if (0x0101D0 <= code && code <= 0x0101FD) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01029C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0102A0 <= code && code <= 0x0102D0) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0102FB) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01032D)
                                        {
                                            if (0x010300 <= code && code <= 0x010323) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01034A) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010350)
                                            {
                                            }
                                            else if (code <= 0x01037A) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x010380 <= code && code <= 0x01039D) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x0103C3) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x010760)
                            {
                                if (code < 0x01057C)
                                {
                                    if (code < 0x0104B0)
                                    {
                                        if (code < 0x010400)
                                        {
                                            if (0x0103C8 <= code && code <= 0x0103D5) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01049D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0104A0 <= code && code <= 0x0104A9) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0104D3) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x010500)
                                        {
                                            if (0x0104D8 <= code && code <= 0x0104FB) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010527) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010530)
                                            {
                                            }
                                            else if (code <= 0x010563) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01056F <= code && code <= 0x01057A) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01058A) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0105A3)
                                    {
                                        if (code < 0x010594)
                                        {
                                            if (0x01058C <= code && code <= 0x010592) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010595) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x010597 <= code && code <= 0x0105A1) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0105B1) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0105BB)
                                        {
                                            if (0x0105B3 <= code && code <= 0x0105B9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0105BC) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010600)
                                            {
                                            }
                                            else if (code <= 0x010736) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x010740 <= code && code <= 0x010755) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x010767) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x01083F)
                                {
                                    if (code < 0x010800)
                                    {
                                        if (code < 0x010787)
                                        {
                                            if (0x010780 <= code && code <= 0x010785) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0107B0) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0107B2 <= code && code <= 0x0107BA) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x010805) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01080A)
                                        {
                                            if (0x010808 <= code && code <= 0x010808) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010835) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010837)
                                            {
                                            }
                                            else if (code <= 0x010838) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01083C <= code && code <= 0x01083C) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x010855) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0108F4)
                                    {
                                        if (code < 0x0108A7)
                                        {
                                            if (0x010857 <= code && code <= 0x01089E) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0108AF) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x0108E0 <= code && code <= 0x0108F2) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0108F5) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01091F)
                                        {
                                            if (0x0108FB <= code && code <= 0x01091B) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010939) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01093F)
                                            {
                                            }
                                            else if (code <= 0x01093F) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x010980 <= code && code <= 0x0109B7) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x0109CF) return EastAsianWidth.Neutral;
                    else
                    {
                        if (code < 0x0110D0)
                        {
                            if (code < 0x010C00)
                            {
                                if (code < 0x010A60)
                                {
                                    if (code < 0x010A15)
                                    {
                                        if (code < 0x010A05)
                                        {
                                            if (0x0109D2 <= code && code <= 0x010A03) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010A06) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x010A0C <= code && code <= 0x010A13) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x010A17) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x010A38)
                                        {
                                            if (0x010A19 <= code && code <= 0x010A35) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010A3A) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010A3F)
                                            {
                                            }
                                            else if (code <= 0x010A48) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x010A50 <= code && code <= 0x010A58) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x010A9F) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x010B39)
                                    {
                                        if (code < 0x010AEB)
                                        {
                                            if (0x010AC0 <= code && code <= 0x010AE6) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010AF6) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x010B00 <= code && code <= 0x010B35) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x010B55) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x010B78)
                                        {
                                            if (0x010B58 <= code && code <= 0x010B72) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010B91) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010B99)
                                            {
                                            }
                                            else if (code <= 0x010B9C) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x010BA9 <= code && code <= 0x010BAF) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x010C48) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x010F00)
                                {
                                    if (code < 0x010D30)
                                    {
                                        if (code < 0x010CC0)
                                        {
                                            if (0x010C80 <= code && code <= 0x010CB2) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010CF2) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x010CFA <= code && code <= 0x010D27) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x010D39) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x010E80)
                                        {
                                            if (0x010E60 <= code && code <= 0x010E7E) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010EA9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x010EAB)
                                            {
                                            }
                                            else if (code <= 0x010EAD) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x010EB0 <= code && code <= 0x010EB1) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x010F27) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x010FE0)
                                    {
                                        if (code < 0x010F70)
                                        {
                                            if (0x010F30 <= code && code <= 0x010F59) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x010F89) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x010FB0 <= code && code <= 0x010FCB) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x010FF6) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x011052)
                                        {
                                            if (0x011000 <= code && code <= 0x01104D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011075) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01107F)
                                            {
                                            }
                                            else if (code <= 0x0110C2) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0110CD <= code && code <= 0x0110CD) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x0110E8) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x01130F)
                            {
                                if (code < 0x011280)
                                {
                                    if (code < 0x011150)
                                    {
                                        if (code < 0x011100)
                                        {
                                            if (0x0110F0 <= code && code <= 0x0110F9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011134) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x011136 <= code && code <= 0x011147) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x011176) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0111E1)
                                        {
                                            if (0x011180 <= code && code <= 0x0111DF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0111F4) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011200)
                                            {
                                            }
                                            else if (code <= 0x011211) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011213 <= code && code <= 0x01123E) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x011286) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01129F)
                                    {
                                        if (code < 0x01128A)
                                        {
                                            if (0x011288 <= code && code <= 0x011288) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01128D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01128F <= code && code <= 0x01129D) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0112A9) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0112F0)
                                        {
                                            if (0x0112B0 <= code && code <= 0x0112EA) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0112F9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011300)
                                            {
                                            }
                                            else if (code <= 0x011303) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011305 <= code && code <= 0x01130C) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x011310) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x011357)
                                {
                                    if (code < 0x011335)
                                    {
                                        if (code < 0x01132A)
                                        {
                                            if (0x011313 <= code && code <= 0x011328) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011330) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x011332 <= code && code <= 0x011333) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x011339) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x011347)
                                        {
                                            if (0x01133B <= code && code <= 0x011344) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011348) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01134B)
                                            {
                                            }
                                            else if (code <= 0x01134D) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011350 <= code && code <= 0x011350) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x011357) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01145D)
                                    {
                                        if (code < 0x011366)
                                        {
                                            if (0x01135D <= code && code <= 0x011363) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01136C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011370)
                                            {
                                            }
                                            else if (code <= 0x011374) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011400 <= code && code <= 0x01145B) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                    else if (code <= 0x011461) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0114D0)
                                        {
                                            if (0x011480 <= code && code <= 0x0114C7) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0114D9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011580)
                                            {
                                            }
                                            else if (code <= 0x0115B5) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x0115B8 <= code && code <= 0x0115DD) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (code <= 0x011644) return EastAsianWidth.Neutral;
            else
            {
                if (code < 0x01EE00)
                {
                    if (code < 0x016FF0)
                    {
                        if (code < 0x011D3F)
                        {
                            if (code < 0x0119A0)
                            {
                                if (code < 0x0118A0)
                                {
                                    if (code < 0x0116C0)
                                    {
                                        if (code < 0x011660)
                                        {
                                            if (0x011650 <= code && code <= 0x011659) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01166C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x011680 <= code && code <= 0x0116B9) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x0116C9) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01171D)
                                        {
                                            if (0x011700 <= code && code <= 0x01171A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01172B) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011730)
                                            {
                                            }
                                            else if (code <= 0x011746) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011800 <= code && code <= 0x01183B) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x0118F2) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x011915)
                                    {
                                        if (code < 0x011909)
                                        {
                                            if (0x0118FF <= code && code <= 0x011906) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011909) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01190C <= code && code <= 0x011913) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x011916) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x011937)
                                        {
                                            if (0x011918 <= code && code <= 0x011935) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011938) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01193B)
                                            {
                                            }
                                            else if (code <= 0x011946) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011950 <= code && code <= 0x011959) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x0119A7) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x011C50)
                                {
                                    if (code < 0x011A50)
                                    {
                                        if (code < 0x0119DA)
                                        {
                                            if (0x0119AA <= code && code <= 0x0119D7) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0119E4) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x011A00 <= code && code <= 0x011A47) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x011AA2) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x011C00)
                                        {
                                            if (0x011AB0 <= code && code <= 0x011AF8) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011C08) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011C0A)
                                            {
                                            }
                                            else if (code <= 0x011C36) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011C38 <= code && code <= 0x011C45) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x011C6C) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x011D00)
                                    {
                                        if (code < 0x011C92)
                                        {
                                            if (0x011C70 <= code && code <= 0x011C8F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011CA7) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x011CA9 <= code && code <= 0x011CB6) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x011D06) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x011D0B)
                                        {
                                            if (0x011D08 <= code && code <= 0x011D09) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011D36) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011D3A)
                                            {
                                            }
                                            else if (code <= 0x011D3A) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011D3C <= code && code <= 0x011D3D) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x011D47) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x014400)
                            {
                                if (code < 0x011FB0)
                                {
                                    if (code < 0x011D6A)
                                    {
                                        if (code < 0x011D60)
                                        {
                                            if (0x011D50 <= code && code <= 0x011D59) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011D65) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x011D67 <= code && code <= 0x011D68) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x011D8E) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x011D93)
                                        {
                                            if (0x011D90 <= code && code <= 0x011D91) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x011D98) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x011DA0)
                                            {
                                            }
                                            else if (code <= 0x011DA9) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x011EE0 <= code && code <= 0x011EF8) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x011FB0) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x012470)
                                    {
                                        if (code < 0x011FFF)
                                        {
                                            if (0x011FC0 <= code && code <= 0x011FF1) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x012399) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x012400 <= code && code <= 0x01246E) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x012474) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x012F90)
                                        {
                                            if (0x012480 <= code && code <= 0x012543) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x012FF2) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x013000)
                                            {
                                            }
                                            else if (code <= 0x01342E) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x013430 <= code && code <= 0x013438) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x014646) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x016B50)
                                {
                                    if (code < 0x016A6E)
                                    {
                                        if (code < 0x016A40)
                                        {
                                            if (0x016800 <= code && code <= 0x016A38) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x016A5E) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x016A60 <= code && code <= 0x016A69) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x016ABE) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x016AD0)
                                        {
                                            if (0x016AC0 <= code && code <= 0x016AC9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x016AED) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x016AF0)
                                            {
                                            }
                                            else if (code <= 0x016AF5) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x016B00 <= code && code <= 0x016B45) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x016B59) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x016E40)
                                    {
                                        if (code < 0x016B63)
                                        {
                                            if (0x016B5B <= code && code <= 0x016B61) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x016B77) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x016B7D <= code && code <= 0x016B8F) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x016E9A) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x016F4F)
                                        {
                                            if (0x016F00 <= code && code <= 0x016F4A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x016F87) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x016F8F)
                                            {
                                            }
                                            else if (code <= 0x016F9F) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x016FE0 <= code && code <= 0x016FE4) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x016FF1) return EastAsianWidth.Wide;
                    else
                    {
                        if (code < 0x01D507)
                        {
                            if (code < 0x01CF50)
                            {
                                if (code < 0x01B164)
                                {
                                    if (code < 0x01AFF0)
                                    {
                                        if (code < 0x018800)
                                        {
                                            if (0x017000 <= code && code <= 0x0187F7) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x018CD5) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x018D00 <= code && code <= 0x018D08) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x01AFF3) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x01AFFD)
                                        {
                                            if (0x01AFF5 <= code && code <= 0x01AFFB) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01AFFE) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01B000)
                                            {
                                            }
                                            else if (code <= 0x01B122) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x01B150 <= code && code <= 0x01B152) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01B167) return EastAsianWidth.Wide;
                                else
                                {
                                    if (code < 0x01BC80)
                                    {
                                        if (code < 0x01BC00)
                                        {
                                            if (0x01B170 <= code && code <= 0x01B2FB) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01BC6A) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01BC70 <= code && code <= 0x01BC7C) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01BC88) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01BC9C)
                                        {
                                            if (0x01BC90 <= code && code <= 0x01BC99) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01BCA3) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01CF00)
                                            {
                                            }
                                            else if (code <= 0x01CF2D) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01CF30 <= code && code <= 0x01CF46) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x01CFC3) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x01D456)
                                {
                                    if (code < 0x01D200)
                                    {
                                        if (code < 0x01D100)
                                        {
                                            if (0x01D000 <= code && code <= 0x01D0F5) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01D126) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01D129 <= code && code <= 0x01D1EA) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01D245) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01D300)
                                        {
                                            if (0x01D2E0 <= code && code <= 0x01D2F3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01D356) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01D360)
                                            {
                                            }
                                            else if (code <= 0x01D378) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01D400 <= code && code <= 0x01D454) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01D49C) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01D4A9)
                                    {
                                        if (code < 0x01D4A2)
                                        {
                                            if (0x01D49E <= code && code <= 0x01D49F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01D4A2) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01D4A5 <= code && code <= 0x01D4A6) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01D4AC) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01D4BB)
                                        {
                                            if (0x01D4AE <= code && code <= 0x01D4B9) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01D4BB) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01D4BD)
                                            {
                                            }
                                            else if (code <= 0x01D4C3) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01D4C5 <= code && code <= 0x01D505) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x01D50A) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x01E026)
                            {
                                if (code < 0x01D6A8)
                                {
                                    if (code < 0x01D53B)
                                    {
                                        if (code < 0x01D516)
                                        {
                                            if (0x01D50D <= code && code <= 0x01D514) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01D51C) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01D51E <= code && code <= 0x01D539) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01D53E) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01D546)
                                        {
                                            if (0x01D540 <= code && code <= 0x01D544) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01D546) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01D54A)
                                            {
                                            }
                                            else if (code <= 0x01D550) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01D552 <= code && code <= 0x01D6A5) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01D7CB) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01DF00)
                                    {
                                        if (code < 0x01DA9B)
                                        {
                                            if (0x01D7CE <= code && code <= 0x01DA8B) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01DA9F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01DAA1 <= code && code <= 0x01DAAF) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01DF1E) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01E008)
                                        {
                                            if (0x01E000 <= code && code <= 0x01E006) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01E018) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01E01B)
                                            {
                                            }
                                            else if (code <= 0x01E021) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01E023 <= code && code <= 0x01E024) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x01E02A) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x01E7E8)
                                {
                                    if (code < 0x01E14E)
                                    {
                                        if (code < 0x01E130)
                                        {
                                            if (0x01E100 <= code && code <= 0x01E12C) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01E13D) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01E140 <= code && code <= 0x01E149) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01E14F) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01E2C0)
                                        {
                                            if (0x01E290 <= code && code <= 0x01E2AE) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01E2F9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01E2FF)
                                            {
                                            }
                                            else if (code <= 0x01E2FF) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01E7E0 <= code && code <= 0x01E7E6) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01E7EB) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01E900)
                                    {
                                        if (code < 0x01E7F0)
                                        {
                                            if (0x01E7ED <= code && code <= 0x01E7EE) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01E7FE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01E800)
                                            {
                                            }
                                            else if (code <= 0x01E8C4) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01E8C7 <= code && code <= 0x01E8D6) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                    else if (code <= 0x01E94B) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01E95E)
                                        {
                                            if (0x01E950 <= code && code <= 0x01E959) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01E95F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01EC71)
                                            {
                                            }
                                            else if (code <= 0x01ECB4) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01ED01 <= code && code <= 0x01ED3D) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (code <= 0x01EE03) return EastAsianWidth.Neutral;
                else
                {
                    if (code < 0x01F3E0)
                    {
                        if (code < 0x01F005)
                        {
                            if (code < 0x01EE5B)
                            {
                                if (code < 0x01EE42)
                                {
                                    if (code < 0x01EE27)
                                    {
                                        if (code < 0x01EE21)
                                        {
                                            if (0x01EE05 <= code && code <= 0x01EE1F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EE22) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01EE24 <= code && code <= 0x01EE24) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01EE27) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01EE34)
                                        {
                                            if (0x01EE29 <= code && code <= 0x01EE32) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EE37) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01EE39)
                                            {
                                            }
                                            else if (code <= 0x01EE39) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01EE3B <= code && code <= 0x01EE3B) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01EE42) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01EE4D)
                                    {
                                        if (code < 0x01EE49)
                                        {
                                            if (0x01EE47 <= code && code <= 0x01EE47) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EE49) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01EE4B <= code && code <= 0x01EE4B) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01EE4F) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01EE54)
                                        {
                                            if (0x01EE51 <= code && code <= 0x01EE52) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EE54) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01EE57)
                                            {
                                            }
                                            else if (code <= 0x01EE57) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01EE59 <= code && code <= 0x01EE59) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x01EE5B) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x01EE7E)
                                {
                                    if (code < 0x01EE64)
                                    {
                                        if (code < 0x01EE5F)
                                        {
                                            if (0x01EE5D <= code && code <= 0x01EE5D) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EE5F) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01EE61 <= code && code <= 0x01EE62) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01EE64) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01EE6C)
                                        {
                                            if (0x01EE67 <= code && code <= 0x01EE6A) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EE72) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01EE74)
                                            {
                                            }
                                            else if (code <= 0x01EE77) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01EE79 <= code && code <= 0x01EE7C) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01EE7E) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01EEA5)
                                    {
                                        if (code < 0x01EE8B)
                                        {
                                            if (0x01EE80 <= code && code <= 0x01EE89) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EE9B) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01EEA1 <= code && code <= 0x01EEA3) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01EEA9) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01EEF0)
                                        {
                                            if (0x01EEAB <= code && code <= 0x01EEBB) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01EEF1) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01F000)
                                            {
                                            }
                                            else if (code <= 0x01F003) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01F004 <= code && code <= 0x01F004) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x01F02B) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x01F1AD)
                            {
                                if (code < 0x01F110)
                                {
                                    if (code < 0x01F0C1)
                                    {
                                        if (code < 0x01F0A0)
                                        {
                                            if (0x01F030 <= code && code <= 0x01F093) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F0AE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01F0B1 <= code && code <= 0x01F0BF) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01F0CE) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01F0D1)
                                        {
                                            if (0x01F0CF <= code && code <= 0x01F0CF) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01F0F5) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01F100)
                                            {
                                            }
                                            else if (code <= 0x01F10A) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x01F10B <= code && code <= 0x01F10F) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01F12D) return EastAsianWidth.Ambiguous;
                                else
                                {
                                    if (code < 0x01F170)
                                    {
                                        if (code < 0x01F130)
                                        {
                                            if (0x01F12E <= code && code <= 0x01F12F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F169) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (0x01F16A <= code && code <= 0x01F16F) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01F18D) return EastAsianWidth.Ambiguous;
                                    else
                                    {
                                        if (code < 0x01F18F)
                                        {
                                            if (0x01F18E <= code && code <= 0x01F18E) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01F190) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x01F191)
                                            {
                                            }
                                            else if (code <= 0x01F19A) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x01F19B <= code && code <= 0x01F1AC) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x01F1AD) return EastAsianWidth.Neutral;
                            else
                            {
                                if (code < 0x01F32D)
                                {
                                    if (code < 0x01F240)
                                    {
                                        if (code < 0x01F200)
                                        {
                                            if (0x01F1E6 <= code && code <= 0x01F1FF) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F202) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x01F210 <= code && code <= 0x01F23B) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x01F248) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x01F260)
                                        {
                                            if (0x01F250 <= code && code <= 0x01F251) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01F265) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01F300)
                                            {
                                            }
                                            else if (code <= 0x01F320) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x01F321 <= code && code <= 0x01F32C) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01F335) return EastAsianWidth.Wide;
                                else
                                {
                                    if (code < 0x01F394)
                                    {
                                        if (code < 0x01F337)
                                        {
                                            if (0x01F336 <= code && code <= 0x01F336) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F37C) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01F37D)
                                            {
                                            }
                                            else if (code <= 0x01F37D) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01F37E <= code && code <= 0x01F393) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                    else if (code <= 0x01F39F) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01F3CB)
                                        {
                                            if (0x01F3A0 <= code && code <= 0x01F3CA) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01F3CE) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01F3CF)
                                            {
                                            }
                                            else if (code <= 0x01F3D3) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x01F3D4 <= code && code <= 0x01F3DF) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (code <= 0x01F3F0) return EastAsianWidth.Wide;
                    else
                    {
                        if (code < 0x01F700)
                        {
                            if (code < 0x01F595)
                            {
                                if (code < 0x01F4FD)
                                {
                                    if (code < 0x01F3F8)
                                    {
                                        if (code < 0x01F3F4)
                                        {
                                            if (0x01F3F1 <= code && code <= 0x01F3F3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F3F4) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x01F3F5 <= code && code <= 0x01F3F7) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01F43E) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x01F440)
                                        {
                                            if (0x01F43F <= code && code <= 0x01F43F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F440) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01F441)
                                            {
                                            }
                                            else if (code <= 0x01F441) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01F442 <= code && code <= 0x01F4FC) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01F4FE) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01F54F)
                                    {
                                        if (code < 0x01F53E)
                                        {
                                            if (0x01F4FF <= code && code <= 0x01F53D) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01F54A) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01F54B <= code && code <= 0x01F54E) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x01F54F) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01F568)
                                        {
                                            if (0x01F550 <= code && code <= 0x01F567) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01F579) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01F57A)
                                            {
                                            }
                                            else if (code <= 0x01F57A) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x01F57B <= code && code <= 0x01F594) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x01F596) return EastAsianWidth.Wide;
                            else
                            {
                                if (code < 0x01F6CD)
                                {
                                    if (code < 0x01F5FB)
                                    {
                                        if (code < 0x01F5A4)
                                        {
                                            if (0x01F597 <= code && code <= 0x01F5A3) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F5A4) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x01F5A5 <= code && code <= 0x01F5FA) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01F64F) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x01F680)
                                        {
                                            if (0x01F650 <= code && code <= 0x01F67F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F6C5) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01F6C6)
                                            {
                                            }
                                            else if (code <= 0x01F6CB) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01F6CC <= code && code <= 0x01F6CC) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01F6CF) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01F6DD)
                                    {
                                        if (code < 0x01F6D3)
                                        {
                                            if (0x01F6D0 <= code && code <= 0x01F6D2) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01F6D4) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (0x01F6D5 <= code && code <= 0x01F6D7) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x01F6DF) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x01F6EB)
                                        {
                                            if (0x01F6E0 <= code && code <= 0x01F6EA) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F6EC) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01F6F0)
                                            {
                                            }
                                            else if (code <= 0x01F6F3) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01F6F4 <= code && code <= 0x01F6FC) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (code <= 0x01F773) return EastAsianWidth.Neutral;
                        else
                        {
                            if (code < 0x01FA70)
                            {
                                if (code < 0x01F8B0)
                                {
                                    if (code < 0x01F800)
                                    {
                                        if (code < 0x01F7E0)
                                        {
                                            if (0x01F780 <= code && code <= 0x01F7D8) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F7EB) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x01F7F0 <= code && code <= 0x01F7F0) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x01F80B) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x01F850)
                                        {
                                            if (0x01F810 <= code && code <= 0x01F847) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F859) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x01F860)
                                            {
                                            }
                                            else if (code <= 0x01F887) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01F890 <= code && code <= 0x01F8AD) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01F8B1) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x01F93C)
                                    {
                                        if (code < 0x01F90C)
                                        {
                                            if (0x01F900 <= code && code <= 0x01F90B) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F93A) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x01F93B <= code && code <= 0x01F93B) return EastAsianWidth.Neutral;
                                        }
                                    }
                                    else if (code <= 0x01F945) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x01F947)
                                        {
                                            if (0x01F946 <= code && code <= 0x01F946) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01F9FF) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01FA00)
                                            {
                                            }
                                            else if (code <= 0x01FA53) return EastAsianWidth.Neutral;
                                            else
                                            {
                                                if (0x01FA60 <= code && code <= 0x01FA6D) return EastAsianWidth.Neutral;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (code <= 0x01FA74) return EastAsianWidth.Wide;
                            else
                            {
                                if (code < 0x01FB00)
                                {
                                    if (code < 0x01FAB0)
                                    {
                                        if (code < 0x01FA80)
                                        {
                                            if (0x01FA78 <= code && code <= 0x01FA7C) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01FA86) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (0x01FA90 <= code && code <= 0x01FAAC) return EastAsianWidth.Wide;
                                        }
                                    }
                                    else if (code <= 0x01FABA) return EastAsianWidth.Wide;
                                    else
                                    {
                                        if (code < 0x01FAD0)
                                        {
                                            if (0x01FAC0 <= code && code <= 0x01FAC5) return EastAsianWidth.Wide;
                                        }
                                        else if (code <= 0x01FAD9) return EastAsianWidth.Wide;
                                        else
                                        {
                                            if (code < 0x01FAE0)
                                            {
                                            }
                                            else if (code <= 0x01FAE7) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x01FAF0 <= code && code <= 0x01FAF6) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                }
                                else if (code <= 0x01FB92) return EastAsianWidth.Neutral;
                                else
                                {
                                    if (code < 0x0E0001)
                                    {
                                        if (code < 0x01FBF0)
                                        {
                                            if (0x01FB94 <= code && code <= 0x01FBCA) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x01FBF9) return EastAsianWidth.Neutral;
                                        else
                                        {
                                            if (code < 0x020000)
                                            {
                                            }
                                            else if (code <= 0x02FFFD) return EastAsianWidth.Wide;
                                            else
                                            {
                                                if (0x030000 <= code && code <= 0x03FFFD) return EastAsianWidth.Wide;
                                            }
                                        }
                                    }
                                    else if (code <= 0x0E0001) return EastAsianWidth.Neutral;
                                    else
                                    {
                                        if (code < 0x0E0100)
                                        {
                                            if (0x0E0020 <= code && code <= 0x0E007F) return EastAsianWidth.Neutral;
                                        }
                                        else if (code <= 0x0E01EF) return EastAsianWidth.Ambiguous;
                                        else
                                        {
                                            if (code < 0x0F0000)
                                            {
                                            }
                                            else if (code <= 0x0FFFFD) return EastAsianWidth.Ambiguous;
                                            else
                                            {
                                                if (0x100000 <= code && code <= 0x10FFFD) return EastAsianWidth.Ambiguous;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return EastAsianWidth.Unknown;
    }
}
