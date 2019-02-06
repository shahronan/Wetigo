// Copyright (c) 2018-present, Facebook, Inc. 
// @generated
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace TBE {

public class AssetDescriptor : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal AssetDescriptor(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  public static global::System.Runtime.InteropServices.HandleRef getCPtr(AssetDescriptor obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~AssetDescriptor() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          Audio360CSharpPINVOKE.delete_AssetDescriptor(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public uint offsetInBytes {
    set {
      Audio360CSharpPINVOKE.AssetDescriptor_offsetInBytes_set(swigCPtr, value);
    } 
    get {
      uint ret = Audio360CSharpPINVOKE.AssetDescriptor_offsetInBytes_get(swigCPtr);
      return ret;
    } 
  }

  public uint lengthInBytes {
    set {
      Audio360CSharpPINVOKE.AssetDescriptor_lengthInBytes_set(swigCPtr, value);
    } 
    get {
      uint ret = Audio360CSharpPINVOKE.AssetDescriptor_lengthInBytes_get(swigCPtr);
      return ret;
    } 
  }

  public AssetDescriptor() : this(Audio360CSharpPINVOKE.new_AssetDescriptor__SWIG_0(), true) {
  }

  public AssetDescriptor(uint offset, uint length) : this(Audio360CSharpPINVOKE.new_AssetDescriptor__SWIG_1(offset, length), true) {
  }

}

}
