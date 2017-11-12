#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class AkMarkerCallbackInfo : AkEventCallbackInfo {
  private IntPtr swigCPtr;

  internal AkMarkerCallbackInfo(IntPtr cPtr, bool cMemoryOwn) : base(AkSoundEnginePINVOKE.CSharp_AkMarkerCallbackInfo_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = cPtr;
  }

  internal static IntPtr getCPtr(AkMarkerCallbackInfo obj) {
    return (obj == null) ? IntPtr.Zero : obj.swigCPtr;
  }

  ~AkMarkerCallbackInfo() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkMarkerCallbackInfo(swigCPtr);
        }
        swigCPtr = IntPtr.Zero;
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public uint uIdentifier {
    get {
      uint ret = AkSoundEnginePINVOKE.CSharp_AkMarkerCallbackInfo_uIdentifier_get(swigCPtr);

      return ret;
    } 
  }

  public uint uPosition {
    get {
      uint ret = AkSoundEnginePINVOKE.CSharp_AkMarkerCallbackInfo_uPosition_get(swigCPtr);

      return ret;
    } 
  }

  public string strLabel { get { return AkSoundEngine.StringFromIntPtrString(AkSoundEnginePINVOKE.CSharp_AkMarkerCallbackInfo_strLabel_get(swigCPtr));
 } 
  }

  public AkMarkerCallbackInfo() : this(AkSoundEnginePINVOKE.CSharp_new_AkMarkerCallbackInfo(), true) {

  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.