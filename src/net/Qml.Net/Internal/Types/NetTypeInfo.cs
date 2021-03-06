﻿using System;
using System.Runtime.InteropServices;
using AdvancedDLSupport;
using Qml.Net.Internal;

namespace Qml.Net.Internal.Types
{
    internal class NetTypeInfo : BaseDisposable
    {
        public NetTypeInfo(string fullTypeName)
            :this(Interop.NetTypeInfo.Create(fullTypeName))
        {
        }

        public NetTypeInfo(IntPtr handle, bool ownsHandle = true)
            :base(handle, ownsHandle)
        {
            
        }

        public string FullTypeName => Utilities.ContainerToString(Interop.NetTypeInfo.GetFullTypeName(Handle));

        public string ClassName
        {
            get => Utilities.ContainerToString(Interop.NetTypeInfo.GetClassName(Handle));
            set => Interop.NetTypeInfo.SetClassName(Handle, value);
        }

        public NetVariantType PrefVariantType
        {
            get => Interop.NetTypeInfo.GetPrefVariantType(Handle);
            set => Interop.NetTypeInfo.SetPrefVariantType(Handle, value);
        }

        public void AddMethod(NetMethodInfo methodInfo)
        {
            Interop.NetTypeInfo.AddMethod(Handle, methodInfo.Handle);
        }

        public int MethodCount => Interop.NetTypeInfo.GetMethodCount(Handle);

        public NetMethodInfo GetMethod(int index)
        {
            var result = Interop.NetTypeInfo.GetMethodInfo(Handle, index);
            if (result == IntPtr.Zero) return null;
            return new NetMethodInfo(result);
        }
        
        public void AddProperty(NetPropertyInfo property)
        {
            Interop.NetTypeInfo.AddProperty(Handle, property.Handle);
        }

        public int PropertyCount => Interop.NetTypeInfo.GetPropertyCount(Handle);

        public NetPropertyInfo GetProperty(int index)
        {
            var result = Interop.NetTypeInfo.GetProperty(Handle, index);
            if (result == IntPtr.Zero) return null;
            return new NetPropertyInfo(result);
        }
        
        public void AddSignal(NetSignalInfo signal)
        {
            Interop.NetTypeInfo.AddSignal(Handle, signal.Handle);
        }

        public int SignalCount => Interop.NetTypeInfo.GetSignalCount(Handle);

        public NetSignalInfo GetSignal(int index)
        {
            var result = Interop.NetTypeInfo.GetSignal(Handle, index);
            if (result == IntPtr.Zero) return null;
            return new NetSignalInfo(result);
        }

        public bool IsLoaded => Interop.NetTypeInfo.IsLoaded(Handle);

        public bool IsLoading => Interop.NetTypeInfo.IsLoading(Handle);

        public void EnsureLoaded()
        {
            Interop.NetTypeInfo.EnsureLoaded(Handle);
        }
      
        protected override void DisposeUnmanaged(IntPtr ptr)
        {
            Interop.NetTypeInfo.Destroy(ptr);
        }
    }

    internal interface INetTypeInfoInterop
    {
        [NativeSymbol(Entrypoint = "type_info_create")]
        IntPtr Create([MarshalAs(UnmanagedType.LPWStr), CallerFree]string fullTypeName);
        [NativeSymbol(Entrypoint = "type_info_destroy")]
        void Destroy(IntPtr netTypeInfo);
        
        [NativeSymbol(Entrypoint = "type_info_getFullTypeName")]
        IntPtr GetFullTypeName(IntPtr netTypeInfo);
        
        [NativeSymbol(Entrypoint = "type_info_setClassName")]
        void SetClassName(IntPtr netTypeInfo, [MarshalAs(UnmanagedType.LPWStr), CallerFree]string className);
        [NativeSymbol(Entrypoint = "type_info_getClassName")]
        IntPtr GetClassName(IntPtr netTypeInfo);
        
        [NativeSymbol(Entrypoint = "type_info_setPrefVariantType")]
        void SetPrefVariantType(IntPtr netTypeInfo, NetVariantType variantType);
        [NativeSymbol(Entrypoint = "type_info_getPrefVariantType")]
        NetVariantType GetPrefVariantType(IntPtr netTypeInfo);

        [NativeSymbol(Entrypoint = "type_info_addMethod")]
        void AddMethod(IntPtr typeInfo, IntPtr methodInfo);
        [NativeSymbol(Entrypoint = "type_info_getMethodCount")]
        int GetMethodCount(IntPtr typeInfo);
        [NativeSymbol(Entrypoint = "type_info_getMethodInfo")]
        IntPtr GetMethodInfo(IntPtr typeInfo, int index);
        
        [NativeSymbol(Entrypoint = "type_info_addProperty")]
        void AddProperty(IntPtr typeInfo, IntPtr property);
        [NativeSymbol(Entrypoint = "type_info_getPropertyCount")]
        int GetPropertyCount(IntPtr typeInfo);
        [NativeSymbol(Entrypoint = "type_info_getProperty")]
        IntPtr GetProperty(IntPtr typeInfo, int index);
        
        [NativeSymbol(Entrypoint = "type_info_addSignal")]
        void AddSignal(IntPtr typeInfo, IntPtr signal);
        [NativeSymbol(Entrypoint = "type_info_getSignalCount")]
        int GetSignalCount(IntPtr typeInfo);
        [NativeSymbol(Entrypoint = "type_info_getSignal")]
        IntPtr GetSignal(IntPtr typeInfo, int index);

        [NativeSymbol(Entrypoint = "type_info_isLoaded")]
        bool IsLoaded(IntPtr typeInfo);
        [NativeSymbol(Entrypoint = "type_info_isLoading")]
        bool IsLoading(IntPtr typeInfo);
        [NativeSymbol(Entrypoint = "type_info_ensureLoaded")]
        void EnsureLoaded(IntPtr typeInfo);
    }
}