﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MesaDePrueba.ServiceReference2 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Task", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MesaDePrueba.ServiceReference2.TaskOfIHttpActionResult))]
    public partial class Task : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskOfIHttpActionResult", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class TaskOfIHttpActionResult : MesaDePrueba.ServiceReference2.Task {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.WebService1Soap")]
    public interface WebService1Soap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento HelloWorldResult del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        MesaDePrueba.ServiceReference2.HelloWorldResponse HelloWorld(MesaDePrueba.ServiceReference2.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<MesaDePrueba.ServiceReference2.HelloWorldResponse> HelloWorldAsync(MesaDePrueba.ServiceReference2.HelloWorldRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento TransaccionResult del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Transaccion", ReplyAction="*")]
        MesaDePrueba.ServiceReference2.TransaccionResponse Transaccion(MesaDePrueba.ServiceReference2.TransaccionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Transaccion", ReplyAction="*")]
        System.Threading.Tasks.Task<MesaDePrueba.ServiceReference2.TransaccionResponse> TransaccionAsync(MesaDePrueba.ServiceReference2.TransaccionRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public MesaDePrueba.ServiceReference2.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(MesaDePrueba.ServiceReference2.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int a;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int b;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public decimal c;
        
        public HelloWorldRequestBody() {
        }
        
        public HelloWorldRequestBody(int a, int b, decimal c) {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public MesaDePrueba.ServiceReference2.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(MesaDePrueba.ServiceReference2.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class TransaccionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Transaccion", Namespace="http://tempuri.org/", Order=0)]
        public MesaDePrueba.ServiceReference2.TransaccionRequestBody Body;
        
        public TransaccionRequest() {
        }
        
        public TransaccionRequest(MesaDePrueba.ServiceReference2.TransaccionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class TransaccionRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int A;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int B;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public decimal C;
        
        public TransaccionRequestBody() {
        }
        
        public TransaccionRequestBody(int A, int B, decimal C) {
            this.A = A;
            this.B = B;
            this.C = C;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class TransaccionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="TransaccionResponse", Namespace="http://tempuri.org/", Order=0)]
        public MesaDePrueba.ServiceReference2.TransaccionResponseBody Body;
        
        public TransaccionResponse() {
        }
        
        public TransaccionResponse(MesaDePrueba.ServiceReference2.TransaccionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class TransaccionResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public MesaDePrueba.ServiceReference2.TaskOfIHttpActionResult TransaccionResult;
        
        public TransaccionResponseBody() {
        }
        
        public TransaccionResponseBody(MesaDePrueba.ServiceReference2.TaskOfIHttpActionResult TransaccionResult) {
            this.TransaccionResult = TransaccionResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebService1SoapChannel : MesaDePrueba.ServiceReference2.WebService1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebService1SoapClient : System.ServiceModel.ClientBase<MesaDePrueba.ServiceReference2.WebService1Soap>, MesaDePrueba.ServiceReference2.WebService1Soap {
        
        public WebService1SoapClient() {
        }
        
        public WebService1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MesaDePrueba.ServiceReference2.HelloWorldResponse MesaDePrueba.ServiceReference2.WebService1Soap.HelloWorld(MesaDePrueba.ServiceReference2.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld(int a, int b, decimal c) {
            MesaDePrueba.ServiceReference2.HelloWorldRequest inValue = new MesaDePrueba.ServiceReference2.HelloWorldRequest();
            inValue.Body = new MesaDePrueba.ServiceReference2.HelloWorldRequestBody();
            inValue.Body.a = a;
            inValue.Body.b = b;
            inValue.Body.c = c;
            MesaDePrueba.ServiceReference2.HelloWorldResponse retVal = ((MesaDePrueba.ServiceReference2.WebService1Soap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MesaDePrueba.ServiceReference2.HelloWorldResponse> MesaDePrueba.ServiceReference2.WebService1Soap.HelloWorldAsync(MesaDePrueba.ServiceReference2.HelloWorldRequest request) {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<MesaDePrueba.ServiceReference2.HelloWorldResponse> HelloWorldAsync(int a, int b, decimal c) {
            MesaDePrueba.ServiceReference2.HelloWorldRequest inValue = new MesaDePrueba.ServiceReference2.HelloWorldRequest();
            inValue.Body = new MesaDePrueba.ServiceReference2.HelloWorldRequestBody();
            inValue.Body.a = a;
            inValue.Body.b = b;
            inValue.Body.c = c;
            return ((MesaDePrueba.ServiceReference2.WebService1Soap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MesaDePrueba.ServiceReference2.TransaccionResponse MesaDePrueba.ServiceReference2.WebService1Soap.Transaccion(MesaDePrueba.ServiceReference2.TransaccionRequest request) {
            return base.Channel.Transaccion(request);
        }
        
        public MesaDePrueba.ServiceReference2.TaskOfIHttpActionResult Transaccion(int A, int B, decimal C) {
            MesaDePrueba.ServiceReference2.TransaccionRequest inValue = new MesaDePrueba.ServiceReference2.TransaccionRequest();
            inValue.Body = new MesaDePrueba.ServiceReference2.TransaccionRequestBody();
            inValue.Body.A = A;
            inValue.Body.B = B;
            inValue.Body.C = C;
            MesaDePrueba.ServiceReference2.TransaccionResponse retVal = ((MesaDePrueba.ServiceReference2.WebService1Soap)(this)).Transaccion(inValue);
            return retVal.Body.TransaccionResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MesaDePrueba.ServiceReference2.TransaccionResponse> MesaDePrueba.ServiceReference2.WebService1Soap.TransaccionAsync(MesaDePrueba.ServiceReference2.TransaccionRequest request) {
            return base.Channel.TransaccionAsync(request);
        }
        
        public System.Threading.Tasks.Task<MesaDePrueba.ServiceReference2.TransaccionResponse> TransaccionAsync(int A, int B, decimal C) {
            MesaDePrueba.ServiceReference2.TransaccionRequest inValue = new MesaDePrueba.ServiceReference2.TransaccionRequest();
            inValue.Body = new MesaDePrueba.ServiceReference2.TransaccionRequestBody();
            inValue.Body.A = A;
            inValue.Body.B = B;
            inValue.Body.C = C;
            return ((MesaDePrueba.ServiceReference2.WebService1Soap)(this)).TransaccionAsync(inValue);
        }
    }
}
