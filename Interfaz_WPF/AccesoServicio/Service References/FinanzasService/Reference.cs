﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoServicio.FinanzasService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FinanzasService.IContabilidadService")]
    public interface IContabilidadService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContabilidadService/Saludar", ReplyAction="http://tempuri.org/IContabilidadService/SaludarResponse")]
        string Saludar();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContabilidadService/ObtenerEmpresas", ReplyAction="http://tempuri.org/IContabilidadService/ObtenerEmpresasResponse")]
        string ObtenerEmpresas();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContabilidadService/ObtenerMonedas", ReplyAction="http://tempuri.org/IContabilidadService/ObtenerMonedasResponse")]
        string ObtenerMonedas();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContabilidadService/AutenticarUsuario", ReplyAction="http://tempuri.org/IContabilidadService/AutenticarUsuarioResponse")]
        bool AutenticarUsuario(SIA.Libreria.Usuario pUsuario, string pNombreEmpresa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContabilidadService/InsertarNuevoUsuario", ReplyAction="http://tempuri.org/IContabilidadService/InsertarNuevoUsuarioResponse")]
        bool InsertarNuevoUsuario(SIA.Libreria.Usuario pUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContabilidadService/InsertarNuevaEmpresa", ReplyAction="http://tempuri.org/IContabilidadService/InsertarNuevaEmpresaResponse")]
        bool InsertarNuevaEmpresa(SIA.Libreria.Empresa pEmpresa, byte[] pLogo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContabilidadService/CrearCuenta", ReplyAction="http://tempuri.org/IContabilidadService/CrearCuentaResponse")]
        bool CrearCuenta(SIA.Libreria.Cuenta pCuenta);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IContabilidadServiceChannel : AccesoServicio.FinanzasService.IContabilidadService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ContabilidadServiceClient : System.ServiceModel.ClientBase<AccesoServicio.FinanzasService.IContabilidadService>, AccesoServicio.FinanzasService.IContabilidadService {
        
        public ContabilidadServiceClient() {
        }
        
        public ContabilidadServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ContabilidadServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContabilidadServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContabilidadServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Saludar() {
            return base.Channel.Saludar();
        }
        
        public string ObtenerEmpresas() {
            return base.Channel.ObtenerEmpresas();
        }
        
        public string ObtenerMonedas() {
            return base.Channel.ObtenerMonedas();
        }
        
        public bool AutenticarUsuario(SIA.Libreria.Usuario pUsuario, string pNombreEmpresa) {
            return base.Channel.AutenticarUsuario(pUsuario, pNombreEmpresa);
        }
        
        public bool InsertarNuevoUsuario(SIA.Libreria.Usuario pUsuario) {
            return base.Channel.InsertarNuevoUsuario(pUsuario);
        }
        
        public bool InsertarNuevaEmpresa(SIA.Libreria.Empresa pEmpresa, byte[] pLogo) {
            return base.Channel.InsertarNuevaEmpresa(pEmpresa, pLogo);
        }
        
        public bool CrearCuenta(SIA.Libreria.Cuenta pCuenta) {
            return base.Channel.CrearCuenta(pCuenta);
        }
    }
}
