using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
namespace AtualizarERP_SGE
{
    public partial class FrmAtualizar : Form
    {
        private string PathExe = "";
        private bool UsaGerenciadorImp = false;
        public FrmAtualizar()
        {
            InitializeComponent();
        }

        private void FrmAtualizar_Load(object sender, EventArgs e)
        {
            ArrayList Parametros = new ArrayList();
            StreamReader LerParam = new StreamReader("ERP-SGE.ini");
            while (!LerParam.EndOfStream)
                Parametros.Add(LerParam.ReadLine());            
            PathExe = Parametros[7].ToString();
            LblArq.Text = "";
            Application.DoEvents();
            LerParam.Close();
        }

        private void Atualizar()
        {
            Application.DoEvents();
            if (PathExe == "")
            {
                MessageBox.Show("Pasta de Atualização do Sistema não Informada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }

            if (!Directory.Exists(@PathExe))
            {
                MessageBox.Show("Pasta de Atualização do Sistema não Localizada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }

            if (MessageBox.Show("Atenção:Nova Versão do Sistema Disponível, Confirma Atualização do Sistema ?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Process[] processo = Process.GetProcessesByName("GerenciarImpResumida");
                    foreach (Process proc in processo)
                    {                        
                        proc.Kill();
                        UsaGerenciadorImp = true;
                    }   
                    

                    string[] ListArquivos = Directory.GetFiles(@PathExe);
                    BarProc.Maximum = Directory.GetFiles(@PathExe).Length;
                    BarProc.Value = 0;
                    foreach (string Arq in ListArquivos)
                    {
                        LblArq.Text = Path.GetFileName(Arq);
                        Application.DoEvents();
                        System.IO.File.Copy(Arq, Path.GetFileName(Arq), true);
                        BarProc.Value = BarProc.Value + 1;
                    }
                    MessageBox.Show("Atualização Concluida");

                    if (UsaGerenciadorImp)
                        System.Diagnostics.Process.Start("GerenciarImpResumida.exe");

                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro na Atualização:" + erro.ToString());
                }
                
            }
            Close();
        }

        private void FrmAtualizar_Shown(object sender, EventArgs e)
        {            
            Atualizar();
        }
    }
}
