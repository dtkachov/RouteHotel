﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MapUtilsTest.HotelRouteAPI {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RouteParams", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class RouteParams : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private bool OptimizeRouteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.Location[] LocationsField;
        
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
        public bool OptimizeRoute {
            get {
                return this.OptimizeRouteField;
            }
            set {
                if ((this.OptimizeRouteField.Equals(value) != true)) {
                    this.OptimizeRouteField = value;
                    this.RaisePropertyChanged("OptimizeRoute");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public MapUtilsTest.HotelRouteAPI.Location[] Locations {
            get {
                return this.LocationsField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationsField, value) != true)) {
                    this.LocationsField = value;
                    this.RaisePropertyChanged("Locations");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Location", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Location : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.LatLng LatLngField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public MapUtilsTest.HotelRouteAPI.LatLng LatLng {
            get {
                return this.LatLngField;
            }
            set {
                if ((object.ReferenceEquals(this.LatLngField, value) != true)) {
                    this.LatLngField = value;
                    this.RaisePropertyChanged("LatLng");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string LocationName {
            get {
                return this.LocationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationNameField, value) != true)) {
                    this.LocationNameField = value;
                    this.RaisePropertyChanged("LocationName");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LatLng", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class LatLng : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private double LatitudeField;
        
        private double LongitudeField;
        
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
        public double Latitude {
            get {
                return this.LatitudeField;
            }
            set {
                if ((this.LatitudeField.Equals(value) != true)) {
                    this.LatitudeField = value;
                    this.RaisePropertyChanged("Latitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public double Longitude {
            get {
                return this.LongitudeField;
            }
            set {
                if ((this.LongitudeField.Equals(value) != true)) {
                    this.LongitudeField = value;
                    this.RaisePropertyChanged("Longitude");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Route", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Route : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RouteIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SummaryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.RouteLeg[] LegsField;
        
        private int DurationField;
        
        private int DistanceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string RouteID {
            get {
                return this.RouteIDField;
            }
            set {
                if ((object.ReferenceEquals(this.RouteIDField, value) != true)) {
                    this.RouteIDField = value;
                    this.RaisePropertyChanged("RouteID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Summary {
            get {
                return this.SummaryField;
            }
            set {
                if ((object.ReferenceEquals(this.SummaryField, value) != true)) {
                    this.SummaryField = value;
                    this.RaisePropertyChanged("Summary");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public MapUtilsTest.HotelRouteAPI.RouteLeg[] Legs {
            get {
                return this.LegsField;
            }
            set {
                if ((object.ReferenceEquals(this.LegsField, value) != true)) {
                    this.LegsField = value;
                    this.RaisePropertyChanged("Legs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public int Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public int Distance {
            get {
                return this.DistanceField;
            }
            set {
                if ((this.DistanceField.Equals(value) != true)) {
                    this.DistanceField = value;
                    this.RaisePropertyChanged("Distance");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RouteLeg", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class RouteLeg : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StartAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EndAddressField;
        
        private int DurationField;
        
        private int DistanceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.RouteStep[] StepsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.LatLng StartLocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.LatLng EndLocationField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string StartAddress {
            get {
                return this.StartAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.StartAddressField, value) != true)) {
                    this.StartAddressField = value;
                    this.RaisePropertyChanged("StartAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string EndAddress {
            get {
                return this.EndAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.EndAddressField, value) != true)) {
                    this.EndAddressField = value;
                    this.RaisePropertyChanged("EndAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public int Distance {
            get {
                return this.DistanceField;
            }
            set {
                if ((this.DistanceField.Equals(value) != true)) {
                    this.DistanceField = value;
                    this.RaisePropertyChanged("Distance");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public MapUtilsTest.HotelRouteAPI.RouteStep[] Steps {
            get {
                return this.StepsField;
            }
            set {
                if ((object.ReferenceEquals(this.StepsField, value) != true)) {
                    this.StepsField = value;
                    this.RaisePropertyChanged("Steps");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public MapUtilsTest.HotelRouteAPI.LatLng StartLocation {
            get {
                return this.StartLocationField;
            }
            set {
                if ((object.ReferenceEquals(this.StartLocationField, value) != true)) {
                    this.StartLocationField = value;
                    this.RaisePropertyChanged("StartLocation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public MapUtilsTest.HotelRouteAPI.LatLng EndLocation {
            get {
                return this.EndLocationField;
            }
            set {
                if ((object.ReferenceEquals(this.EndLocationField, value) != true)) {
                    this.EndLocationField = value;
                    this.RaisePropertyChanged("EndLocation");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RouteStep", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class RouteStep : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int DurationField;
        
        private int DistanceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.LatLng StartLocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.LatLng EndLocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MapUtilsTest.HotelRouteAPI.LatLng[] PointsField;
        
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
        public int Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public int Distance {
            get {
                return this.DistanceField;
            }
            set {
                if ((this.DistanceField.Equals(value) != true)) {
                    this.DistanceField = value;
                    this.RaisePropertyChanged("Distance");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public MapUtilsTest.HotelRouteAPI.LatLng StartLocation {
            get {
                return this.StartLocationField;
            }
            set {
                if ((object.ReferenceEquals(this.StartLocationField, value) != true)) {
                    this.StartLocationField = value;
                    this.RaisePropertyChanged("StartLocation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public MapUtilsTest.HotelRouteAPI.LatLng EndLocation {
            get {
                return this.EndLocationField;
            }
            set {
                if ((object.ReferenceEquals(this.EndLocationField, value) != true)) {
                    this.EndLocationField = value;
                    this.RaisePropertyChanged("EndLocation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public MapUtilsTest.HotelRouteAPI.LatLng[] Points {
            get {
                return this.PointsField;
            }
            set {
                if ((object.ReferenceEquals(this.PointsField, value) != true)) {
                    this.PointsField = value;
                    this.RaisePropertyChanged("Points");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HotelRouteAPI.RouteAPISoap")]
    public interface RouteAPISoap {
        
        // CODEGEN: Generating message contract since element name routeParams from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetRoute", ReplyAction="*")]
        MapUtilsTest.HotelRouteAPI.GetRouteResponse GetRoute(MapUtilsTest.HotelRouteAPI.GetRouteRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetRoute", ReplyAction="*")]
        System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetRouteResponse> GetRouteAsync(MapUtilsTest.HotelRouteAPI.GetRouteRequest request);
        
        // CODEGEN: Generating message contract since element name GetTOResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetTO", ReplyAction="*")]
        MapUtilsTest.HotelRouteAPI.GetTOResponse GetTO(MapUtilsTest.HotelRouteAPI.GetTORequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetTO", ReplyAction="*")]
        System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetTOResponse> GetTOAsync(MapUtilsTest.HotelRouteAPI.GetTORequest request);
        
        // CODEGEN: Generating message contract since element name GetUserLocationByIPResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetUserLocationByIP", ReplyAction="*")]
        MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponse GetUserLocationByIP(MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetUserLocationByIP", ReplyAction="*")]
        System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponse> GetUserLocationByIPAsync(MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetRouteRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetRoute", Namespace="http://tempuri.org/", Order=0)]
        public MapUtilsTest.HotelRouteAPI.GetRouteRequestBody Body;
        
        public GetRouteRequest() {
        }
        
        public GetRouteRequest(MapUtilsTest.HotelRouteAPI.GetRouteRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetRouteRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public MapUtilsTest.HotelRouteAPI.RouteParams routeParams;
        
        public GetRouteRequestBody() {
        }
        
        public GetRouteRequestBody(MapUtilsTest.HotelRouteAPI.RouteParams routeParams) {
            this.routeParams = routeParams;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetRouteResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetRouteResponse", Namespace="http://tempuri.org/", Order=0)]
        public MapUtilsTest.HotelRouteAPI.GetRouteResponseBody Body;
        
        public GetRouteResponse() {
        }
        
        public GetRouteResponse(MapUtilsTest.HotelRouteAPI.GetRouteResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetRouteResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public MapUtilsTest.HotelRouteAPI.Route GetRouteResult;
        
        public GetRouteResponseBody() {
        }
        
        public GetRouteResponseBody(MapUtilsTest.HotelRouteAPI.Route GetRouteResult) {
            this.GetRouteResult = GetRouteResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetTORequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetTO", Namespace="http://tempuri.org/", Order=0)]
        public MapUtilsTest.HotelRouteAPI.GetTORequestBody Body;
        
        public GetTORequest() {
        }
        
        public GetTORequest(MapUtilsTest.HotelRouteAPI.GetTORequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetTORequestBody {
        
        public GetTORequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetTOResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetTOResponse", Namespace="http://tempuri.org/", Order=0)]
        public MapUtilsTest.HotelRouteAPI.GetTOResponseBody Body;
        
        public GetTOResponse() {
        }
        
        public GetTOResponse(MapUtilsTest.HotelRouteAPI.GetTOResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetTOResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public MapUtilsTest.HotelRouteAPI.RouteParams GetTOResult;
        
        public GetTOResponseBody() {
        }
        
        public GetTOResponseBody(MapUtilsTest.HotelRouteAPI.RouteParams GetTOResult) {
            this.GetTOResult = GetTOResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetUserLocationByIPRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetUserLocationByIP", Namespace="http://tempuri.org/", Order=0)]
        public MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequestBody Body;
        
        public GetUserLocationByIPRequest() {
        }
        
        public GetUserLocationByIPRequest(MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetUserLocationByIPRequestBody {
        
        public GetUserLocationByIPRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetUserLocationByIPResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetUserLocationByIPResponse", Namespace="http://tempuri.org/", Order=0)]
        public MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponseBody Body;
        
        public GetUserLocationByIPResponse() {
        }
        
        public GetUserLocationByIPResponse(MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetUserLocationByIPResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public MapUtilsTest.HotelRouteAPI.LatLng GetUserLocationByIPResult;
        
        public GetUserLocationByIPResponseBody() {
        }
        
        public GetUserLocationByIPResponseBody(MapUtilsTest.HotelRouteAPI.LatLng GetUserLocationByIPResult) {
            this.GetUserLocationByIPResult = GetUserLocationByIPResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RouteAPISoapChannel : MapUtilsTest.HotelRouteAPI.RouteAPISoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RouteAPISoapClient : System.ServiceModel.ClientBase<MapUtilsTest.HotelRouteAPI.RouteAPISoap>, MapUtilsTest.HotelRouteAPI.RouteAPISoap {
        
        public RouteAPISoapClient() {
        }
        
        public RouteAPISoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RouteAPISoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RouteAPISoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RouteAPISoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MapUtilsTest.HotelRouteAPI.GetRouteResponse MapUtilsTest.HotelRouteAPI.RouteAPISoap.GetRoute(MapUtilsTest.HotelRouteAPI.GetRouteRequest request) {
            return base.Channel.GetRoute(request);
        }
        
        public MapUtilsTest.HotelRouteAPI.Route GetRoute(MapUtilsTest.HotelRouteAPI.RouteParams routeParams) {
            MapUtilsTest.HotelRouteAPI.GetRouteRequest inValue = new MapUtilsTest.HotelRouteAPI.GetRouteRequest();
            inValue.Body = new MapUtilsTest.HotelRouteAPI.GetRouteRequestBody();
            inValue.Body.routeParams = routeParams;
            MapUtilsTest.HotelRouteAPI.GetRouteResponse retVal = ((MapUtilsTest.HotelRouteAPI.RouteAPISoap)(this)).GetRoute(inValue);
            return retVal.Body.GetRouteResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetRouteResponse> MapUtilsTest.HotelRouteAPI.RouteAPISoap.GetRouteAsync(MapUtilsTest.HotelRouteAPI.GetRouteRequest request) {
            return base.Channel.GetRouteAsync(request);
        }
        
        public System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetRouteResponse> GetRouteAsync(MapUtilsTest.HotelRouteAPI.RouteParams routeParams) {
            MapUtilsTest.HotelRouteAPI.GetRouteRequest inValue = new MapUtilsTest.HotelRouteAPI.GetRouteRequest();
            inValue.Body = new MapUtilsTest.HotelRouteAPI.GetRouteRequestBody();
            inValue.Body.routeParams = routeParams;
            return ((MapUtilsTest.HotelRouteAPI.RouteAPISoap)(this)).GetRouteAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MapUtilsTest.HotelRouteAPI.GetTOResponse MapUtilsTest.HotelRouteAPI.RouteAPISoap.GetTO(MapUtilsTest.HotelRouteAPI.GetTORequest request) {
            return base.Channel.GetTO(request);
        }
        
        public MapUtilsTest.HotelRouteAPI.RouteParams GetTO() {
            MapUtilsTest.HotelRouteAPI.GetTORequest inValue = new MapUtilsTest.HotelRouteAPI.GetTORequest();
            inValue.Body = new MapUtilsTest.HotelRouteAPI.GetTORequestBody();
            MapUtilsTest.HotelRouteAPI.GetTOResponse retVal = ((MapUtilsTest.HotelRouteAPI.RouteAPISoap)(this)).GetTO(inValue);
            return retVal.Body.GetTOResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetTOResponse> MapUtilsTest.HotelRouteAPI.RouteAPISoap.GetTOAsync(MapUtilsTest.HotelRouteAPI.GetTORequest request) {
            return base.Channel.GetTOAsync(request);
        }
        
        public System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetTOResponse> GetTOAsync() {
            MapUtilsTest.HotelRouteAPI.GetTORequest inValue = new MapUtilsTest.HotelRouteAPI.GetTORequest();
            inValue.Body = new MapUtilsTest.HotelRouteAPI.GetTORequestBody();
            return ((MapUtilsTest.HotelRouteAPI.RouteAPISoap)(this)).GetTOAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponse MapUtilsTest.HotelRouteAPI.RouteAPISoap.GetUserLocationByIP(MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest request) {
            return base.Channel.GetUserLocationByIP(request);
        }
        
        public MapUtilsTest.HotelRouteAPI.LatLng GetUserLocationByIP() {
            MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest inValue = new MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest();
            inValue.Body = new MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequestBody();
            MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponse retVal = ((MapUtilsTest.HotelRouteAPI.RouteAPISoap)(this)).GetUserLocationByIP(inValue);
            return retVal.Body.GetUserLocationByIPResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponse> MapUtilsTest.HotelRouteAPI.RouteAPISoap.GetUserLocationByIPAsync(MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest request) {
            return base.Channel.GetUserLocationByIPAsync(request);
        }
        
        public System.Threading.Tasks.Task<MapUtilsTest.HotelRouteAPI.GetUserLocationByIPResponse> GetUserLocationByIPAsync() {
            MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest inValue = new MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequest();
            inValue.Body = new MapUtilsTest.HotelRouteAPI.GetUserLocationByIPRequestBody();
            return ((MapUtilsTest.HotelRouteAPI.RouteAPISoap)(this)).GetUserLocationByIPAsync(inValue);
        }
    }
}
