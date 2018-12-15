using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GeoLayouts
{
    static uint JumpAddr;
    static uint increment = 0;


    public static void DecodeGeoLayout(ROM SM64ROM, uint offset)
    {
        for (uint i = offset; i < SM64ROM.getEndROMAddr();)
        {
            if (SM64ROM.getByte(i) > 0x20) return;
            uint segaddr;
            switch (SM64ROM.getByte(i))
            {
                case 0:
                    increment = 8;
                    break;
                case 1:
                    return;
                case 2:
                    JumpAddr = SM64ROM.readSegmentAddr(SM64ROM.ReadFourBytes(i + 4));
                    GeoLayouts.DecodeGeoLayout(SM64ROM, JumpAddr);
                    if (SM64ROM.getByte(i + 1) == 0) return;
                    increment = 8;
                    break;
                case 3:
                    return;
                case 4:
                    //open node
                    increment = 4;
                    break;
                case 5:
                    //close node
                    increment = 4;
                    break;
                case 8:
                    increment = 12;
                    break;
                case 9:
                    increment = 4;
                    break;
                case 0x0A:
                    if (SM64ROM.getByte(i + 1) > 0) increment = 12;
                    else increment = 8;
                    break;
                case 0x0B:
                    increment = 4;
                    break;
                case 0x0C:
                    increment = 4;
                    break;
                case 0x0D:
                    increment = 8;
                    break;
                case 0x0E:
                    increment = 8;
                    break;
                case 0x0F:
                    increment = 0x14;
                    break;
                case 0x10:
                    //rotate
                    increment = 16;
                    break;
                case 0x13:
                    //Load DL with rotation
                    increment = 12;
                    if (SM64ROM.getByte(i + 8) == 0) break;
                    segaddr = SM64ROM.ReadFourBytes(i + 8);
                    DLConversion.ConvertDL(SM64ROM, segaddr, false);
                    break;
                case 0x14:
                    //billboard
                    increment = 8;
                    break;
                case 0x15:
                    //Load DL
                    increment = 8;
                    segaddr = SM64ROM.ReadFourBytes(i + 4);
                    DLConversion.ConvertDL(SM64ROM, segaddr, false);
                    break;
                case 0x16:
                    increment = 8;
                    break;
                case 0x17:
                    increment = 4;
                    break;
                case 0x18:
                    increment = 8;
                    break;
                case 0x19:
                    increment = 8;
                    break;
                case 0x1A:
                    increment = 8;
                    break;
                case 0x1D:
                    //scale, can be 12 length in rare occurence
                    increment = 8;
                    break;
                case 0x1E:
                    increment = 8;
                    break;
                case 0x1F:
                    increment = 16;
                    break;
                case 0x20:
                    increment = 4;
                    break;
            }
            //Debug text for geo layouts
            /*Array.Resize(ref LevelScripts.DebugScript, LevelScripts.DebugScript.Length + 1);
            LevelScripts.DebugScript[LevelScripts.DebugScript.Length - 1] = i.ToString("x") + ": ";
            for (uint j = i; j < i + increment; j++)
            {
                LevelScripts.DebugScript[LevelScripts.DebugScript.Length - 1] += SM64ROM.getByte(j).ToString("x2") + " ";
            }*/
            i += increment;
        }
    }
}
