using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FrenteLoja
{
    public class CFeSatDll
    {        
        [DllImport("mfe.dll", EntryPoint = "ConsultarMFe", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarMFe(int numSessao);

        [DllImport("mfe.dll", EntryPoint = "ConsultarStatusOperacional", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarStatusOperacional(int numSessao, string codAtivacao);

        [DllImport("mfe.dll", EntryPoint = "EnviarDadosVenda", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnviarDadosVenda(int numSessao, string codAtivacao, string dadosVenda);

        [DllImport("mfe.dll", EntryPoint = "CancelarUltimaVenda", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CancelarUltimaVenda(int numSessao, string codAtivacao, string chave, string dadosCancelamento);

        [DllImport("mfe.dll", EntryPoint = "TesteFimAFim", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TesteFimAFim(int numSessao, string codAtivacao, string dados);

        [DllImport("mfe.dll", EntryPoint = "ExtrairLogs", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ExtrairLogs(int numSessao, string codAtivacao);
    }
}
