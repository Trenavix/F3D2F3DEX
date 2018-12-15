using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LevelScripts
{

    public static bool ExitDecode = false;
    static uint NewSegment;
    static uint NewSegAddr;
    static UInt32 JumpSegAddr;
    static uint BeginMIO0;
    static byte LevelID;
    public static String[] DebugScript;
    static uint[] JumpList;
    static uint stackcounter;
    static bool skipCMD;
    static uint[] allCMDaddresses = new uint[0];
    public static void ParseLevelScripts(ROM SM64ROM, UInt32 offset)
    {
        JumpList = new uint[0];
        allCMDaddresses = new uint[0];
        stackcounter = 0;
        SM64ROM.setSegment(2, 0x803156);
        ExitDecode = false;
        DebugScript = new String[0];
        DecodeLevelScripts(SM64ROM, offset);
        //File.WriteAllLines("D:/ConversionTest.txt", DebugScript); //Filename for debug dump
    }

    public static void DecodeLevelScripts(ROM SM64ROM, UInt32 offset)
    {
        for (UInt32 i = offset; i < SM64ROM.getEndROMAddr();)
        {
            if (DebugScript.Length > 10000) ExitDecode = true;
            if (ExitDecode) return;
            uint increment = SM64ROM.getByte(i + 1);
            if (increment == 0 || SM64ROM.getByte(i) > 0x3C) return;
            Array.Resize(ref LevelScripts.DebugScript, LevelScripts.DebugScript.Length + 1);
            uint newindex = (uint)(LevelScripts.DebugScript.Length - 1);
            for (uint j = 0; j < stackcounter; j++) LevelScripts.DebugScript[newindex] += "    "; //tab further each stack increase for debug txt
            LevelScripts.DebugScript[newindex] += i.ToString("x") + ": ";
            for (uint j = i; j < i + increment; j++)
            {
                LevelScripts.DebugScript[newindex] += SM64ROM.getByte(j).ToString("x2") + " ";
            }
            SkipCMDDetermination(i); //see if the command has already been processed before
            if (!skipCMD) switch (SM64ROM.getByte(i)) //if not skipping command, process it
            {
                case 0x00: //Load raw data and jump
                    NewSegment = SM64ROM.getByte(i + 3);
                    NewSegAddr = SM64ROM.ReadFourBytes(i + 4);
                    SM64ROM.setSegment(NewSegment, NewSegAddr);
                    JumpSegAddr = SM64ROM.ReadFourBytes(i + 12);
                    if (CheckExistingJumps(SM64ROM.readSegmentAddr(JumpSegAddr))) break; //Don't jump to a spot previously jumped to
                    stackcounter++;
                    LevelScripts.DecodeLevelScripts(SM64ROM, SM64ROM.readSegmentAddr(JumpSegAddr));
                    break;
                case 0x01: //Load raw data and jump
                    NewSegment = SM64ROM.getByte(i + 3);
                    NewSegAddr = SM64ROM.ReadFourBytes(i + 4);
                    SM64ROM.setSegment(NewSegment, NewSegAddr);
                    JumpSegAddr = SM64ROM.ReadFourBytes(i + 12);
                    if (CheckExistingJumps(SM64ROM.readSegmentAddr(JumpSegAddr))) break; //Don't jump to a spot previously jumped to
                    LevelScripts.DecodeLevelScripts(SM64ROM, SM64ROM.readSegmentAddr(JumpSegAddr));
                    break;
                case 0x02: //End level data
                    return;
                case 0x03:
                    //delayframes
                    break;
                case 0x04:
                    //delayframes
                    break;
                case 0x05: //Jump to addr
                    JumpSegAddr = SM64ROM.ReadFourBytes(i + 4);
                    if (SM64ROM.readSegmentAddr(JumpSegAddr) == (i - 4)) return; //The cake is a fucking lie
                    if (CheckExistingJumps(SM64ROM.readSegmentAddr(JumpSegAddr))) break; //Don't jump to a spot previously jumped to
                    LevelScripts.DecodeLevelScripts(SM64ROM, SM64ROM.readSegmentAddr(JumpSegAddr));
                    break;
                case 0x06: //Push stack
                    JumpSegAddr = SM64ROM.ReadFourBytes(i + 4);
                    if (CheckExistingJumps(SM64ROM.readSegmentAddr(JumpSegAddr))) break; //Don't jump to a spot previously jumped to
                    stackcounter++;
                    LevelScripts.DecodeLevelScripts(SM64ROM, SM64ROM.readSegmentAddr(JumpSegAddr));
                    break;
                case 0x07: //Pop stack
                    stackcounter--;
                    return;
                case 0x0B: //Conditional Pop
                    break;
                case 0x0C: //Conditional Jump
                    LevelID = SM64ROM.getByte(i + 7);
                    if (LevelID == 0xFF) break;
                    //if (LevelID == 0x00) throw new Exception(SM64ROM.getSegmentStart(0x14).ToString("x"));
                    if (LevelID < 5) SM64ROM.BackupAllSegments();
                    SM64ROM.LoadBackupSegments();
                    JumpSegAddr = SM64ROM.ReadFourBytes(i + 8);
                    if (CheckExistingJumps(SM64ROM.readSegmentAddr(JumpSegAddr))) break; //Don't jump to a spot previously jumped to
                    LevelScripts.DecodeLevelScripts(SM64ROM, SM64ROM.readSegmentAddr(JumpSegAddr));
                    break;
                case 0x0D: //Conditional Push
                    break;
                case 0x0E: //Conditional Skip
                    break;
                case 0x0F: //Skip next
                    break;
                case 0x10: //No-op
                    break;
                case 0x11: //Set Accumulator From ASM
                    break;
                case 0x12: //Actively set accumulator
                    break;
                case 0x13: //Set accumulator
                    break;
                case 0x16:
                    //Loads directly to ram, no segment
                    break;
                case 0x17:
                    NewSegment = SM64ROM.getByte(i + 3);
                    NewSegAddr = SM64ROM.ReadFourBytes(i + 4);
                    SM64ROM.setSegment(NewSegment, NewSegAddr);
                    break;
                case 0x18:
                    //MIO0 segment
                case 0x1A:
                    //MIO0 segment
                    NewSegment = SM64ROM.getByte(i + 3);
                    BeginMIO0 = SM64ROM.ReadFourBytes(i + 4);
                    NewSegAddr = SM64ROM.ReadFourBytes(BeginMIO0 + 12) + BeginMIO0;
                    SM64ROM.setSegment(NewSegment, NewSegAddr);
                    break;
                case 0x1F: //LEVEL MODEL
                    uint SegAddr = SM64ROM.ReadFourBytes(i + 4);
                    GeoLayouts.DecodeGeoLayout(SM64ROM, SM64ROM.readSegmentAddr(SegAddr));
                    break;
                case 0x21:
                    uint DLSegAddr = SM64ROM.ReadFourBytes(i + 4);
                    DLConversion.ConvertDL(SM64ROM, DLSegAddr, false);
                    break;
                case 0x22:
                    uint ModelSegAddr = SM64ROM.ReadFourBytes(i + 4);
                    GeoLayouts.DecodeGeoLayout(SM64ROM, SM64ROM.readSegmentAddr(ModelSegAddr));
                    break;
                case 0x2B:
                    //Default Mario Position
                    break;
                case 0x2E:
                    //Collision
                    break;
                default:
                    break;
            }

            
            i += increment;
        }
    }

    private static bool CheckExistingJumps(uint addr)
    {
        for (uint i = 0; i < JumpList.Length; i++)
        {
            if (JumpList[i] == addr) return true;
        }
        Array.Resize(ref JumpList, JumpList.Length + 1);
        JumpList[JumpList.Length - 1] = addr;
        return false;
    }
    private static void SkipCMDDetermination(uint addr)
    {
        skipCMD = false;
        for (uint j = 0; j < allCMDaddresses.Length; j++)
        { if (addr == allCMDaddresses[j]) skipCMD = true; }
        if (!skipCMD) //Add new entry to all cmds
        {
            Array.Resize(ref allCMDaddresses, allCMDaddresses.Length + 1);
            allCMDaddresses[allCMDaddresses.Length - 1] = addr;
        }
    }
}