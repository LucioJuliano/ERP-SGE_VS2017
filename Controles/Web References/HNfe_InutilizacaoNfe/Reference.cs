﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Controles.HNfe_InutilizacaoNfe {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="NfeInutilizacao", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao")]
    public partial class NfeInutilizacao : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback nfeInutilizacaoNFOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public NfeInutilizacao() {
            this.Url = global::Controles.Properties.Settings.Default.Controles_HNfe_InutilizacaoNfe_NfeInutilizacao;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event nfeInutilizacaoNFCompletedEventHandler nfeInutilizacaoNFCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao/nfeInutilizacaoNF", RequestNamespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao", ResponseNamespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string nfeInutilizacaoNF(string nfeCabecMsg, string nfeDadosMsg) {
            object[] results = this.Invoke("nfeInutilizacaoNF", new object[] {
                        nfeCabecMsg,
                        nfeDadosMsg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void nfeInutilizacaoNFAsync(string nfeCabecMsg, string nfeDadosMsg) {
            this.nfeInutilizacaoNFAsync(nfeCabecMsg, nfeDadosMsg, null);
        }
        
        /// <remarks/>
        public void nfeInutilizacaoNFAsync(string nfeCabecMsg, string nfeDadosMsg, object userState) {
            if ((this.nfeInutilizacaoNFOperationCompleted == null)) {
                this.nfeInutilizacaoNFOperationCompleted = new System.Threading.SendOrPostCallback(this.OnnfeInutilizacaoNFOperationCompleted);
            }
            this.InvokeAsync("nfeInutilizacaoNF", new object[] {
                        nfeCabecMsg,
                        nfeDadosMsg}, this.nfeInutilizacaoNFOperationCompleted, userState);
        }
        
        private void OnnfeInutilizacaoNFOperationCompleted(object arg) {
            if ((this.nfeInutilizacaoNFCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.nfeInutilizacaoNFCompleted(this, new nfeInutilizacaoNFCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void nfeInutilizacaoNFCompletedEventHandler(object sender, nfeInutilizacaoNFCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class nfeInutilizacaoNFCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal nfeInutilizacaoNFCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591