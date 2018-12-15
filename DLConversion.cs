using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DLConversion
{
    public static uint[] AllAddresses;
    public static void ConvertDL(ROM SM64ROM, uint SegAddr, bool ForceROMAddr)
    {
        if (SegAddr >> 24 == 0x02)
        {
            SM64ROM.setSegment(2, 0x803156); //Force segment 2 to US addr, sometimes it gets misplaced in level script parsing
        }
        uint Offset;
        if (!ForceROMAddr) Offset = SM64ROM.readSegmentAddr(SegAddr); //If not forcing a ROM addr, read seg addr
        else Offset = SegAddr;
        for (uint i = 0; i < AllAddresses.Length; i++)
        {
            if (Offset == AllAddresses[i]) return; //If already converted certain DL, skip
        }
        Array.Resize(ref AllAddresses, AllAddresses.Length + 1); //If not converted yet, create new index for mem
        AllAddresses[AllAddresses.Length - 1] = Offset; //Last index is new offset (record dl as converted)
        for (UInt32 i = Offset; i < SM64ROM.getEndROMAddr(); i+=8)
        {
            byte[] CMD = UInt64toByteArray(SM64ROM.ReadEightBytes(i));
            switch (CMD[0])
            {
                case 04: //G_VTX
                    byte vertexcount = (byte)((CMD[1] >> 4) + 1);
                    byte index = (byte)(CMD[1] & 0x0F);
                    SM64ROM.changeByte(i+1, (byte)(index * 2)); //Vertex count * 2
                    UInt16 Params = (UInt16)((vertexcount << 10) | ((vertexcount * 0x10) - 1));
                    SM64ROM.WriteTwoBytes(i + 2, Params);
                    break;
                case 06: //G_DL
                    ConvertDL(SM64ROM, returnSegmentAddr(CMD), false);
                    if (CMD[1] > 0) return;
                    break;
                case 0xB2: //G_RDPHalf_Cont
                    SM64ROM.changeByte(i, 0xB3); //rdphalf_cont not used in newer microcodes; use rdphalf_2
                    break;
                case 0xB3: //G_RDPHalf_2
                    SM64ROM.changeByte(i, 0xB4); //rdphalf_2 change to rdphalf_1 in newer microcodes
                    break;
                case 0xB4: //G_RDPHalf_1
                    SM64ROM.WriteFourBytes(i, 0xBC00000E); //replace G_RDPHalf_1 with G_Moveword in newer microcodes
                    break;
                case 0xBF: //G_TRI1
                    for (uint j = 5; j < 8; j++)
                    {
                        uint TriAddr = i + j;
                        CMD[j] /= 5;
                        SM64ROM.changeByte(TriAddr, CMD[j]);
                    }
                    break;
                case 0xB8: //G_ENDDL
                    return;
            }
        }
    }

    public static UInt32 returnSegmentAddr(byte[] cmd)
    {
        UInt32 value = 0;
        for (int i = 4; i < 8; i++)
        {
            value = (value << 8) | cmd[i];
        }
        return value;
    }

    private static byte[] UInt64toByteArray(UInt64 Word)
    {
        byte[] cmd = new byte[8];
        cmd[0] = (byte)(Word >> 56);
        cmd[1] = (byte)((Word >> 48) & 0xFF);
        cmd[2] = (byte)((Word >> 40) & 0xFF);
        cmd[3] = (byte)((Word >> 32) & 0xFF);
        cmd[4] = (byte)((Word >> 24) & 0xFF);
        cmd[5] = (byte)((Word >> 16) & 0xFF);
        cmd[6] = (byte)((Word >> 8) & 0xFF);
        cmd[7] = (byte)(Word & 0xFF);
        return cmd;
    }

    public static void SpecificConversions(String[] Sås, ROM SM64ROM) //Sås = source file
    {
        bool Geo = false;
        for (uint i = 0; i < Sås.Length; i++)
        {
            string CMD = Sås[i].ToLower();
            if (CMD.StartsWith("geo")) Geo = true;
            else if (CMD.StartsWith("dl") || CMD.StartsWith("f3d")) Geo = false;
            else if (CMD.StartsWith("#"))
            {
                string[] portions = Sås[i].Split(' ');
                StringBuilder removehash = new StringBuilder(portions[0]); //remove the # for raw segment number
                removehash.Remove(0, 1);
                portions[0] = removehash.ToString();
                byte Segment = byte.Parse(portions[0], System.Globalization.NumberStyles.HexNumber);
                uint StartAddr = UInt32.Parse(portions[1], System.Globalization.NumberStyles.HexNumber);
                SM64ROM.setSegment(Segment, StartAddr);
            }
            else if (CMD.StartsWith("addr"))
            {
                string[] portions = Sås[i].Split(' ');
                uint Addr = UInt32.Parse(portions[1], System.Globalization.NumberStyles.HexNumber);
                if (!Geo) ConvertDL(SM64ROM, Addr, true); //Force ROM addr here for raw DL conversion
                else GeoLayouts.DecodeGeoLayout(SM64ROM, Addr);
            }
            else if (CMD.StartsWith("segaddr"))
            {
                string[] portions = Sås[i].Split(' ');
                uint SegAddr = UInt32.Parse(portions[1], System.Globalization.NumberStyles.HexNumber);
                if (!Geo) ConvertDL(SM64ROM, SegAddr, false); //Force ROM addr here for raw DL conversion
                else GeoLayouts.DecodeGeoLayout(SM64ROM, SM64ROM.readSegmentAddr(SegAddr));
            }
        }
    }
    
}
