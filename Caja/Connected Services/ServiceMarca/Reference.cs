﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Caja.ServiceMarca {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Marca", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Marca : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LogoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Nombre {
            get {
                return this.NombreField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreField, value) != true)) {
                    this.NombreField = value;
                    this.RaisePropertyChanged("Nombre");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Descripcion {
            get {
                return this.DescripcionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionField, value) != true)) {
                    this.DescripcionField = value;
                    this.RaisePropertyChanged("Descripcion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Logo {
            get {
                return this.LogoField;
            }
            set {
                if ((object.ReferenceEquals(this.LogoField, value) != true)) {
                    this.LogoField = value;
                    this.RaisePropertyChanged("Logo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceMarca.ServicioMarcaSoap")]
    public interface ServicioMarcaSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento marca del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AgregarMarca", ReplyAction="*")]
        Caja.ServiceMarca.AgregarMarcaResponse AgregarMarca(Caja.ServiceMarca.AgregarMarcaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AgregarMarca", ReplyAction="*")]
        System.Threading.Tasks.Task<Caja.ServiceMarca.AgregarMarcaResponse> AgregarMarcaAsync(Caja.ServiceMarca.AgregarMarcaRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento Remitente del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenerMarca", ReplyAction="*")]
        Caja.ServiceMarca.ObtenerMarcaResponse ObtenerMarca(Caja.ServiceMarca.ObtenerMarcaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenerMarca", ReplyAction="*")]
        System.Threading.Tasks.Task<Caja.ServiceMarca.ObtenerMarcaResponse> ObtenerMarcaAsync(Caja.ServiceMarca.ObtenerMarcaRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento marca del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ActualizarMarca", ReplyAction="*")]
        Caja.ServiceMarca.ActualizarMarcaResponse ActualizarMarca(Caja.ServiceMarca.ActualizarMarcaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ActualizarMarca", ReplyAction="*")]
        System.Threading.Tasks.Task<Caja.ServiceMarca.ActualizarMarcaResponse> ActualizarMarcaAsync(Caja.ServiceMarca.ActualizarMarcaRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento Remitente del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EliminarMarca", ReplyAction="*")]
        Caja.ServiceMarca.EliminarMarcaResponse EliminarMarca(Caja.ServiceMarca.EliminarMarcaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EliminarMarca", ReplyAction="*")]
        System.Threading.Tasks.Task<Caja.ServiceMarca.EliminarMarcaResponse> EliminarMarcaAsync(Caja.ServiceMarca.EliminarMarcaRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AgregarMarcaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AgregarMarca", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.AgregarMarcaRequestBody Body;
        
        public AgregarMarcaRequest() {
        }
        
        public AgregarMarcaRequest(Caja.ServiceMarca.AgregarMarcaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AgregarMarcaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Caja.ServiceMarca.Marca marca;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Remitente;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int Origen;
        
        public AgregarMarcaRequestBody() {
        }
        
        public AgregarMarcaRequestBody(Caja.ServiceMarca.Marca marca, string Remitente, int Origen) {
            this.marca = marca;
            this.Remitente = Remitente;
            this.Origen = Origen;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AgregarMarcaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AgregarMarcaResponse", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.AgregarMarcaResponseBody Body;
        
        public AgregarMarcaResponse() {
        }
        
        public AgregarMarcaResponse(Caja.ServiceMarca.AgregarMarcaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AgregarMarcaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool AgregarMarcaResult;
        
        public AgregarMarcaResponseBody() {
        }
        
        public AgregarMarcaResponseBody(bool AgregarMarcaResult) {
            this.AgregarMarcaResult = AgregarMarcaResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenerMarcaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenerMarca", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.ObtenerMarcaRequestBody Body;
        
        public ObtenerMarcaRequest() {
        }
        
        public ObtenerMarcaRequest(Caja.ServiceMarca.ObtenerMarcaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenerMarcaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Remitente;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int Origen;
        
        public ObtenerMarcaRequestBody() {
        }
        
        public ObtenerMarcaRequestBody(string Remitente, int Origen) {
            this.Remitente = Remitente;
            this.Origen = Origen;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenerMarcaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenerMarcaResponse", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.ObtenerMarcaResponseBody Body;
        
        public ObtenerMarcaResponse() {
        }
        
        public ObtenerMarcaResponse(Caja.ServiceMarca.ObtenerMarcaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenerMarcaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ObtenerMarcaResult;
        
        public ObtenerMarcaResponseBody() {
        }
        
        public ObtenerMarcaResponseBody(string ObtenerMarcaResult) {
            this.ObtenerMarcaResult = ObtenerMarcaResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ActualizarMarcaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ActualizarMarca", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.ActualizarMarcaRequestBody Body;
        
        public ActualizarMarcaRequest() {
        }
        
        public ActualizarMarcaRequest(Caja.ServiceMarca.ActualizarMarcaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ActualizarMarcaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Caja.ServiceMarca.Marca marca;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Remitente;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int Origen;
        
        public ActualizarMarcaRequestBody() {
        }
        
        public ActualizarMarcaRequestBody(Caja.ServiceMarca.Marca marca, string Remitente, int Origen) {
            this.marca = marca;
            this.Remitente = Remitente;
            this.Origen = Origen;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ActualizarMarcaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ActualizarMarcaResponse", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.ActualizarMarcaResponseBody Body;
        
        public ActualizarMarcaResponse() {
        }
        
        public ActualizarMarcaResponse(Caja.ServiceMarca.ActualizarMarcaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ActualizarMarcaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool ActualizarMarcaResult;
        
        public ActualizarMarcaResponseBody() {
        }
        
        public ActualizarMarcaResponseBody(bool ActualizarMarcaResult) {
            this.ActualizarMarcaResult = ActualizarMarcaResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EliminarMarcaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EliminarMarca", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.EliminarMarcaRequestBody Body;
        
        public EliminarMarcaRequest() {
        }
        
        public EliminarMarcaRequest(Caja.ServiceMarca.EliminarMarcaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EliminarMarcaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int Id;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Remitente;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int Origen;
        
        public EliminarMarcaRequestBody() {
        }
        
        public EliminarMarcaRequestBody(int Id, string Remitente, int Origen) {
            this.Id = Id;
            this.Remitente = Remitente;
            this.Origen = Origen;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EliminarMarcaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EliminarMarcaResponse", Namespace="http://tempuri.org/", Order=0)]
        public Caja.ServiceMarca.EliminarMarcaResponseBody Body;
        
        public EliminarMarcaResponse() {
        }
        
        public EliminarMarcaResponse(Caja.ServiceMarca.EliminarMarcaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EliminarMarcaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool EliminarMarcaResult;
        
        public EliminarMarcaResponseBody() {
        }
        
        public EliminarMarcaResponseBody(bool EliminarMarcaResult) {
            this.EliminarMarcaResult = EliminarMarcaResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServicioMarcaSoapChannel : Caja.ServiceMarca.ServicioMarcaSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioMarcaSoapClient : System.ServiceModel.ClientBase<Caja.ServiceMarca.ServicioMarcaSoap>, Caja.ServiceMarca.ServicioMarcaSoap {
        
        public ServicioMarcaSoapClient() {
        }
        
        public ServicioMarcaSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicioMarcaSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioMarcaSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioMarcaSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Caja.ServiceMarca.AgregarMarcaResponse Caja.ServiceMarca.ServicioMarcaSoap.AgregarMarca(Caja.ServiceMarca.AgregarMarcaRequest request) {
            return base.Channel.AgregarMarca(request);
        }
        
        public bool AgregarMarca(Caja.ServiceMarca.Marca marca, string Remitente, int Origen) {
            Caja.ServiceMarca.AgregarMarcaRequest inValue = new Caja.ServiceMarca.AgregarMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.AgregarMarcaRequestBody();
            inValue.Body.marca = marca;
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            Caja.ServiceMarca.AgregarMarcaResponse retVal = ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).AgregarMarca(inValue);
            return retVal.Body.AgregarMarcaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Caja.ServiceMarca.AgregarMarcaResponse> Caja.ServiceMarca.ServicioMarcaSoap.AgregarMarcaAsync(Caja.ServiceMarca.AgregarMarcaRequest request) {
            return base.Channel.AgregarMarcaAsync(request);
        }
        
        public System.Threading.Tasks.Task<Caja.ServiceMarca.AgregarMarcaResponse> AgregarMarcaAsync(Caja.ServiceMarca.Marca marca, string Remitente, int Origen) {
            Caja.ServiceMarca.AgregarMarcaRequest inValue = new Caja.ServiceMarca.AgregarMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.AgregarMarcaRequestBody();
            inValue.Body.marca = marca;
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            return ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).AgregarMarcaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Caja.ServiceMarca.ObtenerMarcaResponse Caja.ServiceMarca.ServicioMarcaSoap.ObtenerMarca(Caja.ServiceMarca.ObtenerMarcaRequest request) {
            return base.Channel.ObtenerMarca(request);
        }
        
        public string ObtenerMarca(string Remitente, int Origen) {
            Caja.ServiceMarca.ObtenerMarcaRequest inValue = new Caja.ServiceMarca.ObtenerMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.ObtenerMarcaRequestBody();
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            Caja.ServiceMarca.ObtenerMarcaResponse retVal = ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).ObtenerMarca(inValue);
            return retVal.Body.ObtenerMarcaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Caja.ServiceMarca.ObtenerMarcaResponse> Caja.ServiceMarca.ServicioMarcaSoap.ObtenerMarcaAsync(Caja.ServiceMarca.ObtenerMarcaRequest request) {
            return base.Channel.ObtenerMarcaAsync(request);
        }
        
        public System.Threading.Tasks.Task<Caja.ServiceMarca.ObtenerMarcaResponse> ObtenerMarcaAsync(string Remitente, int Origen) {
            Caja.ServiceMarca.ObtenerMarcaRequest inValue = new Caja.ServiceMarca.ObtenerMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.ObtenerMarcaRequestBody();
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            return ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).ObtenerMarcaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Caja.ServiceMarca.ActualizarMarcaResponse Caja.ServiceMarca.ServicioMarcaSoap.ActualizarMarca(Caja.ServiceMarca.ActualizarMarcaRequest request) {
            return base.Channel.ActualizarMarca(request);
        }
        
        public bool ActualizarMarca(Caja.ServiceMarca.Marca marca, string Remitente, int Origen) {
            Caja.ServiceMarca.ActualizarMarcaRequest inValue = new Caja.ServiceMarca.ActualizarMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.ActualizarMarcaRequestBody();
            inValue.Body.marca = marca;
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            Caja.ServiceMarca.ActualizarMarcaResponse retVal = ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).ActualizarMarca(inValue);
            return retVal.Body.ActualizarMarcaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Caja.ServiceMarca.ActualizarMarcaResponse> Caja.ServiceMarca.ServicioMarcaSoap.ActualizarMarcaAsync(Caja.ServiceMarca.ActualizarMarcaRequest request) {
            return base.Channel.ActualizarMarcaAsync(request);
        }
        
        public System.Threading.Tasks.Task<Caja.ServiceMarca.ActualizarMarcaResponse> ActualizarMarcaAsync(Caja.ServiceMarca.Marca marca, string Remitente, int Origen) {
            Caja.ServiceMarca.ActualizarMarcaRequest inValue = new Caja.ServiceMarca.ActualizarMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.ActualizarMarcaRequestBody();
            inValue.Body.marca = marca;
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            return ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).ActualizarMarcaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Caja.ServiceMarca.EliminarMarcaResponse Caja.ServiceMarca.ServicioMarcaSoap.EliminarMarca(Caja.ServiceMarca.EliminarMarcaRequest request) {
            return base.Channel.EliminarMarca(request);
        }
        
        public bool EliminarMarca(int Id, string Remitente, int Origen) {
            Caja.ServiceMarca.EliminarMarcaRequest inValue = new Caja.ServiceMarca.EliminarMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.EliminarMarcaRequestBody();
            inValue.Body.Id = Id;
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            Caja.ServiceMarca.EliminarMarcaResponse retVal = ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).EliminarMarca(inValue);
            return retVal.Body.EliminarMarcaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Caja.ServiceMarca.EliminarMarcaResponse> Caja.ServiceMarca.ServicioMarcaSoap.EliminarMarcaAsync(Caja.ServiceMarca.EliminarMarcaRequest request) {
            return base.Channel.EliminarMarcaAsync(request);
        }
        
        public System.Threading.Tasks.Task<Caja.ServiceMarca.EliminarMarcaResponse> EliminarMarcaAsync(int Id, string Remitente, int Origen) {
            Caja.ServiceMarca.EliminarMarcaRequest inValue = new Caja.ServiceMarca.EliminarMarcaRequest();
            inValue.Body = new Caja.ServiceMarca.EliminarMarcaRequestBody();
            inValue.Body.Id = Id;
            inValue.Body.Remitente = Remitente;
            inValue.Body.Origen = Origen;
            return ((Caja.ServiceMarca.ServicioMarcaSoap)(this)).EliminarMarcaAsync(inValue);
        }
    }
}
