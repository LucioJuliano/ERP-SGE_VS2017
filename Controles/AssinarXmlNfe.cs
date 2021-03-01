using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;

namespace Controles
{
    /*public class AssinarXmlNfe
    {
        AssinaturaDigital AD = new AssinaturaDigital();
        //
        // cria cert
        //
        X509Certificate2 cert = new X509Certificate2();
        //
        // seleciona certificado do repositório MY do windows
        //
        Certificado certificado = new Certificado();

        /*static void Main(string[] args)
        {
            Console.WriteLine("Exemplo de Assinatura Digital XML\r\r");
            Console.Write("Arquivo xml a ser assinado :");
            string _arquivo = Console.ReadLine();

            if (_arquivo == null)
            {
                Console.WriteLine("\rNome de arquivo não informado…");
            }
            else if (!File.Exists(_arquivo))
            {
                Console.WriteLine("\rArquivo {0} inexistente…", _arquivo);
            }
            else
            {
                Console.Write("URI a ser assinada (Ex.: infCanc, infNFe, infInut, etc.) :");
                string _uri = Console.ReadLine();
                if (_uri == null)
                {
                    Console.WriteLine("\rURI não informada…");
                }
                else
                {
                    //
                    // le o arquivo xml
                    //
                    StreamReader SR;
                    string _stringXml;
                    SR = File.OpenText(_arquivo);
                    _stringXml = SR.ReadToEnd();
                    SR.Close();
                    //
                    // realiza assinatura
                    //
                    
                    cert = certificado.BuscaNome("");
                    int resultado = AD.Assinar(_stringXml, _uri, cert);
                    if (resultado == 0)
                    {
                        //
                        // grava arquivo assinado
                        //
                        StreamWriter SW;
                        SW = File.CreateText(_arquivo.Trim() + "_assinado");
                        SW.Write(AD.XMLStringAssinado);
                        SW.Close();
                    }
                    Console.WriteLine(AD.mensagemResultado);
                }
            }
        }

    }*/

    public class AssinarXmlNfe
    {        

        public int Assinar(string XMLString, string RefUri, X509Certificate2 X509Cert)
        /*
        * Entradas:
        * XMLString: string XML a ser assinada
        * RefUri : Referência da URI a ser assinada (Ex. infNFe
        * X509Cert : certificado digital a ser utilizado na assinatura digital
        *
        * Retornos:
        * Assinar : 0 - Assinatura realizada com sucesso
        * 1 - Erro: Problema ao acessar o certificado digital - %exceção%
        * 2 - Problemas no certificado digital
        * 3 - XML mal formado + exceção
        * 4 - A tag de assinatura %RefUri% inexiste
        * 5 - A tag de assinatura %RefUri% não é unica
        * 6 - Erro Ao assinar o documento - ID deve ser string %RefUri(Atributo)%
        * 7 - Erro: Ao assinar o documento - %exceção%
        *
        * XMLStringAssinado : string XML assinada
        *
        * XMLDocAssinado : XMLDocument do XML assinado
        */
        {
            int resultado = 0;
            msgResultado = "Assinatura realizada com sucesso";
          //  try
            {
                // certificado para ser utilizado na assinatura
                //
                string _xnome = "";
                if (X509Cert != null)
                {
                    _xnome = X509Cert.Subject.ToString();
                }
                X509Certificate2 _X509Cert = new X509Certificate2();
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, _xnome, false);
                if (collection1.Count == 0)
                {
                    resultado = 2;
                    msgResultado = "Problemas no certificado digital";
                }
                else
                {
                    // certificado ok
                    _X509Cert = collection1[0];
                    string x;
                    x = _X509Cert.GetKeyAlgorithm().ToString();
                    // Create a new XML document.
                    XmlDocument doc = new XmlDocument();

                    // Format the document to ignore white spaces.
                    doc.PreserveWhitespace = false;

                    // Load the passed XML file using it’s name.
                    try
                    {
                        doc.LoadXml(XMLString);

                        // Verifica se a tag a ser assinada existe é única
                        int qtdeRefUri = doc.GetElementsByTagName(RefUri).Count;

                        if (qtdeRefUri == 0)
                        {
                            // a URI indicada não existe
                            resultado = 4;
                            msgResultado = "A tag de assinatura " + RefUri.Trim() + " inexiste";
                        }
                        // Exsiste mais de uma tag a ser assinada
                        else
                        {
                            if (qtdeRefUri > 1)
                            {
                                // existe mais de uma URI indicada
                                resultado = 5;
                                msgResultado = "A tag de assinatura " + RefUri.Trim() + " não é unica";

                            }
                            //else if (_listaNum.IndexOf(doc.GetElementsByTagName(RefUri).Item(0).Attributes.ToString().Substring(1,1))>0)
                            //{
                            // resultado = 6;
                            // msgResultado = "Erro: Ao assinar o documento - ID deve ser string (" + doc.GetElementsByTagName(RefUri).Item(0).Attributes + ")";
                            //}
                            else
                            {
                                try
                                {

                                    // Create a SignedXml object.
                                    SignedXml signedXml = new SignedXml(doc);

                                    // Add the key to the SignedXml document
                                    signedXml.SigningKey = _X509Cert.PrivateKey;

                                    // Create a reference to be signed
                                    Reference reference = new Reference();
                                    // pega o uri que deve ser assinada
                                    XmlAttributeCollection _Uri = doc.GetElementsByTagName(RefUri).Item(0).Attributes;
                                    foreach (XmlAttribute _atributo in _Uri)
                                    {
                                        if (_atributo.Name == "Id")
                                        {
                                            reference.Uri = "#" + _atributo.InnerText;
                                        }
                                    }

                                    // Add an enveloped transformation to the reference.
                                    XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                    reference.AddTransform(env);

                                    XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                    reference.AddTransform(c14);

                                    // Add the reference to the SignedXml object.
                                    signedXml.AddReference(reference);

                                    // Create a new KeyInfo object
                                    KeyInfo keyInfo = new KeyInfo();

                                    // Load the certificate into a KeyInfoX509Data object
                                    // and add it to the KeyInfo object.
                                    keyInfo.AddClause(new KeyInfoX509Data(_X509Cert));

                                    // Add the KeyInfo object to the SignedXml object.
                                    signedXml.KeyInfo = keyInfo;

                                    signedXml.ComputeSignature();
                                                                        

                                    // Get the XML representation of the signature and save
                                    // it to an XmlElement object.
                                    XmlElement xmlDigitalSignature = signedXml.GetXml();

                                    // Append the element to the XML document.
                                    doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                    
                                    XMLDoc = new XmlDocument();
                                    XMLDoc.PreserveWhitespace = false;
                                    XMLDoc = doc;
                                }
                                catch (Exception caught)
                                {
                                    resultado = 7;
                                    msgResultado = "Erro: Ao assinar o documento - " + caught.Message;
                                }
                            }
                        }
                    }
                    catch (Exception caught)
                    {
                        resultado = 3;
                        msgResultado = "Erro: XML mal formado - " + caught.Message;
                    }
                }
            }
            //catch (Exception caught)
            //{
            //    resultado = 1;
           //     msgResultado = "Erro: Problema ao acessar o certificado digital" + caught.Message;
            //}

            return resultado;
        }

        //
        // mensagem de Retorno
        //
        private string msgResultado;
        public  XmlElement xmlDigitalSignature;
        private XmlDocument XMLDoc;

        public XmlDocument XMLDocAssinado
        {
            get { return XMLDoc; }
        }
        public string XMLStringAssinado
        {
            get { return XMLDoc.OuterXml; }
        }
        public string mensagemResultado
        {
            get { return msgResultado; }
        }
    }
    public class Certificado
    {
        public X509Certificate2 BuscaNome(string Nome)
        {
            X509Certificate2 _X509Cert = new X509Certificate2();
            try
            {

                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
                if (Nome == "")
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                    if (scollection.Count == 0)
                    {
                        _X509Cert.Reset();
                        Console.WriteLine("Nenhum certificado escolhido", "Atenção");
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                else
                {
                    X509Certificate2Collection scollection = (X509Certificate2Collection)collection2.Find(X509FindType.FindBySubjectDistinguishedName, Nome, false);
                    if (scollection.Count == 0)
                    {
                        Console.WriteLine("Nenhum certificado válido foi encontrado com o nome informado: " + Nome, "Atenção");
                        _X509Cert.Reset();
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                store.Close();
                return _X509Cert;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return _X509Cert;
            }
        }
        public X509Certificate2 BuscaNroSerie(string NroSerie)
        {
            X509Certificate2 _X509Cert = new X509Certificate2();
            try
            {

                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, true);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection1.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);
                if (NroSerie == "")
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificados Digitais", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                    if (scollection.Count == 0)
                    {
                        _X509Cert.Reset();
                        Console.WriteLine("Nenhum certificado válido foi encontrado com o número de série informado: " + NroSerie, "Atenção");
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                else
                {
                    X509Certificate2Collection scollection = (X509Certificate2Collection)collection2.Find(X509FindType.FindBySerialNumber, NroSerie, true);
                    if (scollection.Count == 0)
                    {
                        _X509Cert.Reset();
                        Console.WriteLine("Nenhum certificado válido foi encontrado com o número de série informado: " + NroSerie, "Atenção");
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                store.Close();
                return _X509Cert;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return _X509Cert;
            }
        }
    }
}
