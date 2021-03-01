using System;
using System.Net; // importe o namespace .Net
using System.Net.Mail; // importe o namespace .Net.Mail
using System.Collections;
using System.Data;
using Controle_Dados;
using System.IO;
using System.Windows.Forms;

namespace Controles
{    

    public class EnviarEmail
    {
        public Funcoes Controle = new Funcoes();
        public string Smtp = "";
        public int    Porta = 0;
        public string Email = "";
        public string Senha = "";

        public void MontarEmail(string Assunto, string texto)
        {
            DataSet ListaEmail = new DataSet();
            ListaEmail = Controle.ConsultaTabela("SELECT USUARIO,EMAIL FROM USUARIOS WHERE EMAILALTPRD=1 AND RTRIM(EMAIL)<>''");
            if (ListaEmail.Tables[0].Rows.Count > 0)
            {
                Controles.EnviarEmail Enviar = new Controles.EnviarEmail();
                string Destino = ListaEmail.Tables[0].Rows[0]["Email"].ToString();
                ArrayList ComCopia = new ArrayList();
                for (int I = 1; I <= ListaEmail.Tables[0].Rows.Count - 1; I++)
                    ComCopia.Add(ListaEmail.Tables[0].Rows[I]["Email"].ToString());
                Enviar_Email(Assunto, Destino, ComCopia, texto);                
            }   
        }
        public void MontarEmailDist(string Assunto, string texto)
        {
            DataSet ListaEmail = new DataSet();            
            ListaEmail = Controle.ConsultaTabela("SELECT RAZAOSOCIAL,EMAIL FROM PESSOAS WHERE NOTIFICAALTPRC=1 AND RTRIM(EMAIL)<>''");
            if (ListaEmail.Tables[0].Rows.Count > 0)
            {
                Controles.EnviarEmail Enviar = new Controles.EnviarEmail();
                //string Destino = "lucio@talimpoce.com.br";
                string Destino = ListaEmail.Tables[0].Rows[0]["Email"].ToString();
                ArrayList ComCopia = new ArrayList();
                for (int I = 1; I <= ListaEmail.Tables[0].Rows.Count - 1; I++)
                    ComCopia.Add(ListaEmail.Tables[0].Rows[I]["Email"].ToString());
                Enviar_Email(Assunto, Destino, ComCopia, texto);
            }
        }
        private bool Enviar_Email(string assunto,string destino,ArrayList ComCopia,string texto)
        {
            SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);
            cliente.EnableSsl = true;
            MailAddress remetente = new MailAddress("admin@talimpoce.com.br", assunto);
            NetworkCredential credenciais = new NetworkCredential("admin@talimpoce.com.br", "@dminTAL68", "");
            cliente.Credentials = credenciais;            

            MailAddress destinatario = new MailAddress(destino);
            MailMessage mensagem = new MailMessage(remetente, destinatario);
            for (int I = 0; I <= ComCopia.Count - 1; I++)
                mensagem.CC.Add(ComCopia[I].ToString());
            mensagem.Body = texto;
            mensagem.Subject = assunto;            
            try
            {
                cliente.Send(mensagem);
                return true;
            }
            catch (Exception e)
            {
                return true;
            }
        }
        public bool Enviar_EmailXmlNFE(string assunto, string destino, int NumNota, int IdFilial, DateTime DtEmissao)
        {
            FileInfo XMLNFE = new FileInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeProc\\" + string.Format("{0:D3}", IdFilial) + "\\" + string.Format("{0:D4}", DtEmissao.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DtEmissao.Date.Month.ToString())) + "\\Nfe-" + string.Format("{0:D8}", NumNota) + "-procNFE.xml");
            FileInfo PDFNFE = new FileInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfePDF\\" + string.Format("{0:D3}", IdFilial) + "\\" + string.Format("{0:D4}", DtEmissao.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DtEmissao.Date.Month.ToString())) + "\\Nfe-" + string.Format("{0:D8}", NumNota) + ".PDF");

            if (XMLNFE.Exists)
            {
                /*SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);
                cliente.EnableSsl = true;
                MailAddress remetente = new MailAddress("nfe.saida@talimpoce.com.br", "NOTA FISCAL ELETRONICA");
                NetworkCredential credenciais = new NetworkCredential("nfe.saida@talimpoce.com.br", "nfetalimpo", "");
                cliente.Credentials = credenciais;*/
                
                SmtpClient cliente = new SmtpClient(Smtp, Porta);                
                cliente.EnableSsl = true;
                MailAddress remetente = new MailAddress(Email, "NOTA FISCAL ELETRONICA");
                NetworkCredential credenciais = new NetworkCredential(Email, Senha, "");
                cliente.Credentials = credenciais;
                
                MailAddress destinatario = new MailAddress(destino);
                MailMessage mensagem     = new MailMessage(remetente, destinatario);
                

                mensagem.Body = "XML DA NOTA ELETRONICA No.: " + NumNota.ToString() + " em Anexo \n \n \n \n \nAtenção: Esta é uma mensagem automática, não é necessário responder.";
                mensagem.Subject = "XML DA NOTA ELETRONICA";
                Attachment Anexo = new Attachment(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfeProc\\" + string.Format("{0:D3}", IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())) + "\\Nfe-" + string.Format("{0:D8}", NumNota) + "-procNFE.xml");                
                mensagem.Attachments.Add(Anexo);

                if (PDFNFE.Exists)
                {
                    Attachment AnexoPdf = new Attachment(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NfePDF\\" + string.Format("{0:D3}", IdFilial) + "\\" + string.Format("{0:D4}", DateTime.Now.Date.Year.ToString()) + "\\" + string.Format("{0:D2}", int.Parse(DateTime.Now.Date.Month.ToString())) + "\\Nfe-" + string.Format("{0:D8}", NumNota) + ".PDF");
                    mensagem.Attachments.Add(AnexoPdf);
                }

                try
                {
                    cliente.Send(mensagem);
                    MessageBox.Show("Email enviado");
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erro ao enviar o XML da Nota Eletronica de No. " + NumNota.ToString());
                    return false;
                }
            }
            else
            {
                MessageBox.Show("XML da Nota Eletronica de No. " + NumNota.ToString() + " não localizado");
                return false;
            }
        }

        public bool Enviar_EmailCobranca(string assunto, string destino, string ComCopia, string texto, string anexo)
        {
            
            SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);
            cliente.EnableSsl = true;
            MailAddress remetente = new MailAddress("financeiro@talimpoce.com.br", assunto);
            NetworkCredential credenciais = new NetworkCredential("financeiro@talimpoce.com.br", "cleansystem", "");
            cliente.Credentials = credenciais;

            MailAddress destinatario = new MailAddress(destino);
            MailMessage mensagem = new MailMessage(remetente, destinatario);

            if (anexo != "")
            {
                Attachment Anexo = new Attachment(anexo);
                mensagem.Attachments.Add(Anexo);                
            }

            if (ComCopia!="")
                mensagem.CC.Add(ComCopia);

            mensagem.Body = texto;
            mensagem.Subject = assunto;
            try
            {
                cliente.Send(mensagem);
                return true;
            }
            catch (Exception e)
            {
                return true;
            }
        }
    }
}
