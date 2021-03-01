using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Controles
{
    public class ImprimirEtqCodBarra
    {
        public bool VersaoDistr = false;

        const uint IMAGE_BITMAP = 0;
        const uint LR_LOADFROMFILE = 16;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
           int cxDesired, int cyDesired, uint fuLoad);
        [DllImport("Gdi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int DeleteObject(IntPtr ho);
        const string szSavePath = "C:\\Argox";
        const string szSaveFile = "C:\\Argox\\PPLA_Example.Prn";
        const string sznop1 = "nop_front\r\n";
        const string sznop2 = "nop_middle\r\n";
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_Maxi(int x, int y, int primary, int secondary,
            int country, int service, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_Maxi_Ori(int x, int y, int ori, int primary,
            int secondary, int country, int service, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_PDF417(int x, int y, int narrow, int width, char normal,
            int security, int aspect, int row, int column, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_PDF417_Ori(int x, int y, int ori, int narrow, int width,
            char normal, int security, int aspect, int row, int column, char mode, int numeric,
            string data);
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_DataMatrix(int x, int y, int rotation, int hor_mul,
            int ver_mul, int ECC, int data_format, int num_rows, int num_col, char mode,
            int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern void A_Clear_Memory();
        [DllImport("Winppla.dll")]
        private static extern void A_ClosePrn();
        [DllImport("Winppla.dll")]
        private static extern int A_CreatePrn(int selection, string filename);
        [DllImport("Winppla.dll")]
        private static extern int A_Del_Graphic(int mem_mode, string graphic);
        [DllImport("Winppla.dll")]
        private static extern int A_Draw_Box(char mode, int x, int y, int width, int height,
            int top, int side);
        [DllImport("Winppla.dll")]
        private static extern int A_Draw_Line(char mode, int x, int y, int width, int height);
        [DllImport("Winppla.dll")]
        private static extern void A_Feed_Label();
        [DllImport("Winppla.dll")]
        private static extern IntPtr A_Get_DLL_Version(int nShowMessage);
        [DllImport("Winppla.dll")]
        private static extern int A_Get_DLL_VersionA(int nShowMessage);
        [DllImport("Winppla.dll")]
        private static extern int A_Get_Graphic(int x, int y, int mem_mode, char format,
            string filename);
        [DllImport("Winppla.dll")]
        private static extern int A_Get_Graphic_ColorBMP(int x, int y, int mem_mode, char format,
            string filename);
        [DllImport("Winppla.dll")]
        private static extern int A_Get_Graphic_ColorBMPEx(int x, int y, int nWidth, int nHeight,
            int rotate, int mem_mode, char format, string id_name, string filename);
        [DllImport("Winppla.dll")]
        private static extern int A_Get_Graphic_ColorBMP_HBitmap(int x, int y, int nWidth, int nHeight,
           int rotate, int mem_mode, char format, string id_name, IntPtr hbm);
        [DllImport("Winppla.dll")]
        private static extern int A_Initial_Setting(int Type, string Source);
        [DllImport("Winppla.dll")]
        private static extern int A_WriteData(int IsImmediate, byte[] pbuf, int length);
        [DllImport("Winppla.dll")]
        private static extern int A_ReadData(byte[] pbuf, int length, int dwTimeoutms);
        [DllImport("Winppla.dll")]
        private static extern int A_Load_Graphic(int x, int y, string graphic_name);
        [DllImport("Winppla.dll")]
        private static extern int A_Open_ChineseFont(string path);
        [DllImport("Winppla.dll")]
        private static extern int A_Print_Form(int width, int height, int copies, int amount,
            string form_name);
        [DllImport("Winppla.dll")]
        private static extern int A_Print_Out(int width, int height, int copies, int amount);
        [DllImport("Winppla.dll")]
        private static extern int A_Prn_Barcode(int x, int y, int ori, char type, int narrow,
            int width, int height, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern int A_Prn_Text(int x, int y, int ori, int font, int type,
            int hor_factor, int ver_factor, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern int A_Prn_Text_Chinese(int x, int y, int fonttype, string id_name,
            string data, int mem_mode);
        [DllImport("Winppla.dll")]
        private static extern int A_Prn_Text_TrueType(int x, int y, int FSize, string FType,
            int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name,
            string data, int mem_mode);
        [DllImport("Winppla.dll")]
        private static extern int A_Prn_Text_TrueType_W(int x, int y, int FHeight, int FWidth,
            string FType, int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut,
            string id_name, string data, int mem_mode);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Backfeed(int back);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_BMPSave(int nSave, string pstrBMPFName);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Cutting(int cutting);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Darkness(int heat);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_DebugDialog(int nEnable);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Feed(char rate);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Form(string formfile, string form_name, int mem_mode);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Margin(int position, int margin);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Prncomport(int baud, int parity, int data, int stop);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Prncomport_PC(int nBaudRate, int nByteSize, int nParity,
            int nStopBits, int nDsr, int nCts, int nXonXoff);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Sensor_Mode(char type, int continuous);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Speed(char speed);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Syssetting(int transfer, int cut_peel, int length,
            int zero, int pause);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Unit(char unit);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Gap(int gap);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_Logic(int logic);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_ProcessDlg(int nShow);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_ErrorDlg(int nShow);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_LabelVer(int centiInch);
        [DllImport("Winppla.dll")]
        private static extern int A_GetUSBBufferLen();
        [DllImport("Winppla.dll")]
        private static extern int A_EnumUSB(byte[] buf);
        [DllImport("Winppla.dll")]
        private static extern int A_CreateUSBPort(int nPort);
        [DllImport("Winppla.dll")]
        private static extern int A_CreatePort(int nPortType, int nPort, string filename);
        [DllImport("Winppla.dll")]
        private static extern int A_Clear_MemoryEx(int nMode);
        [DllImport("Winppla.dll")]
        private static extern void A_Set_Mirror();
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_RSS(int x, int y, int ori, int ratio, int height,
            char rtype, int mult, int seg, string data1, string data2);
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_QR_M(int x, int y, int ori, char mult, int value,
            int model, char error, int mask, char dinput, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern int A_Bar2d_QR_A(int x, int y, int ori, char mult, int value,
            char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        private static extern int A_GetNetPrinterBufferLen();
        [DllImport("Winppla.dll")]
        private static extern int A_EnumNetPrinter(byte[] buf);
        [DllImport("Winppla.dll")]
        private static extern int A_CreateNetPort(int nPort);
        [DllImport("Winppla.dll")]
        private static extern int A_Prn_Text_TrueType_Uni(int x, int y, int FSize, string FType,
            int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name,
            byte[] data, int format, int mem_mode);
        [DllImport("Winppla.dll")]
        private static extern int A_Prn_Text_TrueType_UniB(int x, int y, int FSize, string FType,
            int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name,
            byte[] data, int format, int mem_mode);
        [DllImport("Winppla.dll")]
        private static extern int A_GetUSBDeviceInfo(int nPort, byte[] pDeviceName,
            out int pDeviceNameLen, byte[] pDevicePath, out int pDevicePathLen);
        [DllImport("Winppla.dll")]
        private static extern int A_Set_EncryptionKey(string encryptionKey);
        [DllImport("Winppla.dll")]
        private static extern int A_Check_EncryptionKey(string decodeKey, string encryptionKey,
            int dwTimeoutms);

        public void ImprimirEtq(DataTable TabEtq)
        {
            int nLen, ret, sw;
            byte[] pbuf = new byte[128];
            string strmsg;
            IntPtr ver;
            System.Text.Encoding encAscII = System.Text.Encoding.ASCII;
            System.Text.Encoding encUnicode = System.Text.Encoding.Unicode;

            // dll version.
            ver = A_Get_DLL_Version(0);

            // search port.
            nLen = A_GetUSBBufferLen() + 1;
            strmsg = "DLL ";
            strmsg += Marshal.PtrToStringAnsi(ver);
            strmsg += "\r\n";
            if (nLen > 1)
            {
                byte[] buf1, buf2;
                int len1 = 128, len2 = 128;
                buf1 = new byte[len1];
                buf2 = new byte[len2];
                A_EnumUSB(pbuf);
                A_GetUSBDeviceInfo(1, buf1, out len1, buf2, out len2);
                sw = 1;
                if (1 == sw)
                {
                    ret = A_CreatePrn(12, encAscII.GetString(buf2, 0, len2));// open usb.
                }
                else
                {
                    ret = A_CreateUSBPort(1);// must call A_GetUSBBufferLen() function fisrt.
                }
                if (0 != ret)
                {
                    strmsg += "Falha na comunicação com a Impressora";
                }
                else
                {
                    strmsg += "Open USB:\r\nDevice name: ";
                    strmsg += encAscII.GetString(buf1, 0, len1);
                    strmsg += "\r\nDevice path: ";
                    strmsg += encAscII.GetString(buf2, 0, len2);
                    //sw = 2;
                    if (2 == sw)
                    {
                        //get printer status.
                        pbuf[0] = 0x01;
                        pbuf[1] = 0x46;
                        pbuf[2] = 0x0D;
                        pbuf[3] = 0x0A;
                        A_WriteData(1, pbuf, 4);//<SOH>F
                        ret = A_ReadData(pbuf, 2, 1000);
                    }
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(szSavePath);
                ret = A_CreatePrn(0, szSaveFile);// open file.
                strmsg += "Open ";
                strmsg += szSaveFile;
                if (0 != ret)
                {
                    strmsg += " Erro no Arquivo de Impressão!";
                }
                else
                {
                    strmsg += " file succeed!";
                }
            }

            if (0 != ret)
                return;


            for (int I = 0; I <= TabEtq.Rows.Count - 1; I++)
            {
                A_Set_DebugDialog(1);
                A_Set_Unit('n');
                A_Set_Syssetting(1, 0, 0, 0, 0);
                A_Set_Darkness(8);
                A_Del_Graphic(1, "*");// delete all picture.
                A_Clear_Memory();// clear memory.
                A_WriteData(0, encAscII.GetBytes(sznop2), sznop2.Length);
                A_WriteData(1, encAscII.GetBytes(sznop1), sznop1.Length);


                if (VersaoDistr)
                {
                    A_Prn_Text(40, 0, 1, 3, 1, 1, 1, 'N', 2, "  Ref:" + TabEtq.Rows[I]["Referencia"].ToString() + "  Unidade:" + TabEtq.Rows[I]["UND"].ToString());

                    if (TabEtq.Rows[I]["CodBarra"].ToString().Trim() != "")
                        A_Prn_Barcode(50, 20, 1, 'F', 0, 0, 40, 'N', 0, TabEtq.Rows[I]["CodBarra"].ToString().Trim().Substring(0, 12));
                                        
                    A_Prn_Text(220, 20, 1, 4, 0, 1, 1, 'N', 2, "R$");

                    A_Prn_Text(255, 20, 1, 6, 0, 1, 1, 'N', 2,  string.Format("{0:N2}", decimal.Parse(TabEtq.Rows[I]["Preco"].ToString())));

                    if (TabEtq.Rows[I]["Descricao"].ToString().Trim().Length <= 37)
                        A_Prn_Text(50, 85, 1, 3, 0, 1, 1, 'N', 2, TabEtq.Rows[I]["Descricao"].ToString().Trim());
                    else
                    {
                        int tamcmp = TabEtq.Rows[I]["Descricao"].ToString().Trim().Length - 37;
                        A_Prn_Text(50, 70, 1, 3, 0, 1, 1, 'N', 2, TabEtq.Rows[I]["Descricao"].ToString().Trim().Substring(37, tamcmp));
                        A_Prn_Text(50, 85, 1, 3, 0, 1, 1, 'N', 2, TabEtq.Rows[I]["Descricao"].ToString().Trim().Substring(0, 36));
                    }
                }
                else
                {
                    if (TabEtq.Rows[I]["CodBarra"].ToString().Trim() != "")
                        A_Prn_Barcode(20, 10, 1, 'F', 0, 0, 20, 'N', 0, TabEtq.Rows[I]["CodBarra"].ToString().Trim().Substring(0, 12));

                    A_Prn_Text(15, 50, 1, 3, 0, 1, 1, 'N', 2, "PRECO:  " + string.Format("{0:N2}", decimal.Parse(TabEtq.Rows[I]["Preco"].ToString())) + " Ref:" + TabEtq.Rows[I]["Referencia"].ToString());

                    if (TabEtq.Rows[I]["Descricao"].ToString().Trim().Length <= 37)
                        A_Prn_Text(20, 82, 1, 2, 0, 1, 1, 'N', 2, TabEtq.Rows[I]["Descricao"].ToString().Trim());
                    else
                    {
                        int tamcmp = TabEtq.Rows[I]["Descricao"].ToString().Trim().Length - 37;
                        A_Prn_Text(15, 70, 1, 2, 0, 1, 1, 'N', 2, TabEtq.Rows[I]["Descricao"].ToString().Trim().Substring(37, tamcmp));
                        A_Prn_Text(15, 82, 1, 2, 0, 1, 1, 'N', 2, TabEtq.Rows[I]["Descricao"].ToString().Trim().Substring(0, 36));
                    }
                }
                A_Print_Out(1, 1, int.Parse(TabEtq.Rows[I]["Qtde"].ToString()), 1);// copy 2. 
            }
            A_ClosePrn();
        }

    }
}
