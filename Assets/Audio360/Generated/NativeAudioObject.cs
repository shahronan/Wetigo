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

public class NativeAudioObject : SpatDecoderInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal NativeAudioObject(global::System.IntPtr cPtr, bool cMemoryOwn) : base(Audio360CSharpPINVOKE.NativeAudioObject_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  public static global::System.Runtime.InteropServices.HandleRef getCPtr(NativeAudioObject obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public override EngineError stop() {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_stop(swigCPtr);
    return ret;
  }

  public override EngineError stopScheduled(float millisecondsFromNow) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_stopScheduled(swigCPtr, millisecondsFromNow);
    return ret;
  }

  public override EngineError stopWithFade(float fadeDurationInMs) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_stopWithFade(swigCPtr, fadeDurationInMs);
    return ret;
  }

  public virtual EngineError open(string nameAndPath) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_open__SWIG_0(swigCPtr, nameAndPath);
    return ret;
  }

  public virtual EngineError open(string nameAndPath, AssetDescriptor ad) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_open__SWIG_1(swigCPtr, nameAndPath, AssetDescriptor.getCPtr(ad));
    if (Audio360CSharpPINVOKE.SWIGPendingException.Pending) throw Audio360CSharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void close() {
    Audio360CSharpPINVOKE.NativeAudioObject_close(swigCPtr);
  }

  public virtual bool isOpen() {
    bool ret = Audio360CSharpPINVOKE.NativeAudioObject_isOpen(swigCPtr);
    return ret;
  }

  public virtual EngineError seekToSample(uint timeInSamples) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_seekToSample(swigCPtr, timeInSamples);
    return ret;
  }

  public virtual EngineError seekToMs(float timeInMs) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_seekToMs(swigCPtr, timeInMs);
    return ret;
  }

  public virtual uint getElapsedTimeInSamples() {
    uint ret = Audio360CSharpPINVOKE.NativeAudioObject_getElapsedTimeInSamples(swigCPtr);
    return ret;
  }

  public virtual double getElapsedTimeInMs() {
    double ret = Audio360CSharpPINVOKE.NativeAudioObject_getElapsedTimeInMs(swigCPtr);
    return ret;
  }

  public virtual uint getAssetDurationInSamples() {
    uint ret = Audio360CSharpPINVOKE.NativeAudioObject_getAssetDurationInSamples(swigCPtr);
    return ret;
  }

  public virtual float getAssetDurationInMs() {
    float ret = Audio360CSharpPINVOKE.NativeAudioObject_getAssetDurationInMs(swigCPtr);
    return ret;
  }

  public override EngineError setEventCallback(EventCallback callback, global::System.IntPtr userData) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_setEventCallback(swigCPtr, callback, userData);
    return ret;
  }

  public virtual void shouldSpatialise(bool spatialise) {
    Audio360CSharpPINVOKE.NativeAudioObject_shouldSpatialise(swigCPtr, spatialise);
  }

  public virtual bool isSpatialised() {
    bool ret = Audio360CSharpPINVOKE.NativeAudioObject_isSpatialised(swigCPtr);
    return ret;
  }

  public virtual EngineError setSpatializationType(SpatializationType spatType) {
    EngineError ret = (EngineError)Audio360CSharpPINVOKE.NativeAudioObject_setSpatializationType(swigCPtr, (int)spatType);
    return ret;
  }

  public virtual SpatializationType getSpatializationType() {
    SpatializationType ret = (SpatializationType)Audio360CSharpPINVOKE.NativeAudioObject_getSpatializationType(swigCPtr);
    return ret;
  }

  public virtual bool enableLooping(bool loop) {
    bool ret = Audio360CSharpPINVOKE.NativeAudioObject_enableLooping(swigCPtr, loop);
    return ret;
  }

  public virtual bool loopingEnabled() {
    bool ret = Audio360CSharpPINVOKE.NativeAudioObject_loopingEnabled(swigCPtr);
    return ret;
  }

  public virtual void setAttenuationMode(AttenuationMode mode) {
    Audio360CSharpPINVOKE.NativeAudioObject_setAttenuationMode(swigCPtr, (int)mode);
  }

  public virtual AttenuationMode getAttenuationMode() {
    AttenuationMode ret = (AttenuationMode)Audio360CSharpPINVOKE.NativeAudioObject_getAttenuationMode(swigCPtr);
    return ret;
  }

  public virtual void setAttenuationProperties(AttenuationProps props) {
    Audio360CSharpPINVOKE.NativeAudioObject_setAttenuationProperties(swigCPtr, AttenuationProps.getCPtr(props));
    if (Audio360CSharpPINVOKE.SWIGPendingException.Pending) throw Audio360CSharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual AttenuationProps getAttenuationProperties() {
    AttenuationProps ret = new AttenuationProps(Audio360CSharpPINVOKE.NativeAudioObject_getAttenuationProperties(swigCPtr), true);
    return ret;
  }

  public virtual void setDirectionalityEnabled(bool enable) {
    Audio360CSharpPINVOKE.NativeAudioObject_setDirectionalityEnabled(swigCPtr, enable);
  }

  public virtual bool isDirectionalityEnabled() {
    bool ret = Audio360CSharpPINVOKE.NativeAudioObject_isDirectionalityEnabled(swigCPtr);
    return ret;
  }

  public virtual void setDirectionalProperties(DirectionalProps props) {
    Audio360CSharpPINVOKE.NativeAudioObject_setDirectionalProperties(swigCPtr, DirectionalProps.getCPtr(props));
    if (Audio360CSharpPINVOKE.SWIGPendingException.Pending) throw Audio360CSharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual DirectionalProps getDirectionalProperties() {
    DirectionalProps ret = new DirectionalProps(Audio360CSharpPINVOKE.NativeAudioObject_getDirectionalProperties(swigCPtr), true);
    return ret;
  }

  public virtual void setPitch(float pitch) {
    Audio360CSharpPINVOKE.NativeAudioObject_setPitch(swigCPtr, pitch);
  }

  public virtual float getPitch() {
    float ret = Audio360CSharpPINVOKE.NativeAudioObject_getPitch(swigCPtr);
    return ret;
  }

}

}