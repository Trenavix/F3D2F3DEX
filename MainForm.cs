using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F3D2F3DEX
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static OpenFileDialog OFD() => new OpenFileDialog();
        private static SaveFileDialog SFD() => new SaveFileDialog();
        private static ROM SM64ROM;
        private static String currentROMPath;
        private static String[] OpenTXTFile;

        private void OpenROM_Button_Click(object sender, EventArgs e)
        {
            OpenROM();
            tabControl1.Enabled = true;
        }

        private void OpenROM()
        {
            // Displays an OpenFileDialog so the user can select a Cursor.  
            OpenFileDialog OpenROM = OFD();
            OpenROM.Filter = "ROM Files|*.z64;*.rom;*.v64;*.n64";
            OpenROM.Title = "Select a SM64 ROM File";
            if (OpenROM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool isSM64ROM = false;
                using (FileStream fs = new FileStream(OpenROM.FileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] Header = new byte[0x40]; fs.Read(Header, 0, 0x40); //Load header into bytearray
                    if (Header[0x3c] == 0x53 && Header[0x3d] == 0x4D && Header[0x3e] == 0x45 && Header[0x3f] == 0) isSM64ROM = true; //Check if header is correct
                    fs.Close();
                }
                if (isSM64ROM)
                {
                    LoadROM(OpenROM.FileName);
                    currentROMPath = OpenROM.FileName;
                    textBox4.Text = currentROMPath;
                }
                else { MessageBox.Show("File is not a SM64 US ROM! Please try again.", "Invalid File!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void OpenTXT()
        {
            OpenFileDialog OpenTXT = OFD();
            OpenTXT.Filter = "TXT Files|*.txt";
            OpenTXT.Title = "Select a TXT File";
            if (OpenTXT.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenTXTFile = File.ReadAllLines(OpenTXT.FileName);
            }
            TxtFileTextBox.Text = OpenTXT.FileName;
        }

        public static void LoadROM(String BinDirectory)
        {
            using (FileStream fs = new FileStream(BinDirectory, FileMode.Open, FileAccess.Read))
            {
                byte[] ROMFile = new byte[fs.Length];
                fs.Read(ROMFile, 0, (int)fs.Length);
                ROM NewSM64ROM = new ROM(ROMFile);
                SM64ROM = NewSM64ROM;
                fs.Close();
            }
        }

        private void ConvertALLDLs_Button_Click(object sender, EventArgs e)
        {
            if (SM64ROM == null) { MessageBox.Show("No ROM loaded"); return; }
            UInt32 LevelAddr = UInt32.Parse(textBox1.Text, System.Globalization.NumberStyles.HexNumber);
            DLConversion.AllAddresses = new uint[0];
            LevelScripts.ParseLevelScripts(SM64ROM, LevelAddr);
            MessageBox.Show("Every Display List traceable from level scripts has been converted! Be sure to only to do this once before saving your ROM.", "Conversion complete!");
        }

        private void SaveROM_Button_Click(object sender, EventArgs e)
        {
            if (SM64ROM == null) { MessageBox.Show("No ROM loaded"); return; }
            // Displays a SaveFileDialog so the user can save the bin  
            SaveFileDialog SaveROM = SFD();
            SaveROM.Filter = "SM64 ROM File|*.z64;*.rom;*.v64;*.n64";
            SaveROM.Title = "Save a SM64 ROM file.";
            if (SaveROM.ShowDialog() == System.Windows.Forms.DialogResult.OK && SaveROM.FileName != "") // If the file name is not an empty string open it for saving. 
            {
                File.WriteAllBytes(SaveROM.FileName, SM64ROM.getCurrentROM());
            }
        }

        private void OpenTXT_Button_Click(object sender, EventArgs e)
        {
            OpenTXT();
        }

        private void CustomDL_Button_Click(object sender, EventArgs e)
        {
            if (OpenTXTFile == null) { MessageBox.Show("Please open a text file with all of the custom display lists to convert.", "No .txt opened"); return; }
            DLConversion.AllAddresses = new uint[0];
            DLConversion.SpecificConversions(OpenTXTFile, SM64ROM);
            MessageBox.Show("All custom DLs converted and parsed. Be sure to only do this once before saving your ROM.", "Conversions complete!");
        }
    }
}
