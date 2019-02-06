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

public class AttenuationProps : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal AttenuationProps(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  public static global::System.Runtime.InteropServices.HandleRef getCPtr(AttenuationProps obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~AttenuationProps() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          Audio360CSharpPINVOKE.delete_AttenuationProps(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public float minimumDistance {
    set {
      Audio360CSharpPINVOKE.AttenuationProps_minimumDistance_set(swigCPtr, value);
    } 
    get {
      float ret = Audio360CSharpPINVOKE.AttenuationProps_minimumDistance_get(swigCPtr);
      return ret;
    } 
  }

  public float maximumDistance {
    set {
      Audio360CSharpPINVOKE.AttenuationProps_maximumDistance_set(swigCPtr, value);
    } 
    get {
      float ret = Audio360CSharpPINVOKE.AttenuationProps_maximumDistance_get(swigCPtr);
      return ret;
    } 
  }

  public float factor {
    set {
      Audio360CSharpPINVOKE.AttenuationProps_factor_set(swigCPtr, value);
    } 
    get {
      float ret = Audio360CSharpPINVOKE.AttenuationProps_factor_get(swigCPtr);
      return ret;
    } 
  }

  public bool maxDistanceMute {
    set {
      Audio360CSharpPINVOKE.AttenuationProps_maxDistanceMute_set(swigCPtr, value);
    } 
    get {
      bool ret = Audio360CSharpPINVOKE.AttenuationProps_maxDistanceMute_get(swigCPtr);
      return ret;
    } 
  }

  public AttenuationProps() : this(Audio360CSharpPINVOKE.new_AttenuationProps__SWIG_0(), true) {
  }

  public AttenuationProps(float minDistanceValue, float maxDistanceValue, float factorValue, bool maxDistMute) : this(Audio360CSharpPINVOKE.new_AttenuationProps__SWIG_1(minDistanceValue, maxDistanceValue, factorValue, maxDistMute), true) {
  }

  public AttenuationProps(float minDistanceValue, float maxDistanceValue, float factorValue) : this(Audio360CSharpPINVOKE.new_AttenuationProps__SWIG_2(minDistanceValue, maxDistanceValue, factorValue), true) {
  }

}

}
