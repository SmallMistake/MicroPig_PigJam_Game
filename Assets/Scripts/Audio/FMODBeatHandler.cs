using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Text;

public class FMODBeatHandler : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;
    FMOD.Studio.EVENT_CALLBACK cb;

    FMODUnity.StudioEventEmitter emitter;

    void Start()
    {
        instance = emitter.EventInstance;


        cb = new FMOD.Studio.EVENT_CALLBACK(StudioEventCallback);
        instance.setCallback(cb, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT);
        instance.start();
    }

    public FMOD.RESULT StudioEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters)
    {
        if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
        {
            /*
            FMOD.Studio.TIMELINE_MARKER_PROPERTIES marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
            IntPtr namePtr = marker.name;
            int nameLen = 0;
            while (Marshal.ReadByte(namePtr, nameLen) != 0) ++nameLen;
            byte[] buffer = new byte[nameLen];
            Marshal.Copy(namePtr, buffer, 0, buffer.Length);
            string name = Encoding.UTF8.GetString(buffer, 0, nameLen);
            if (name == "HIGH")
            {
                UnityEngine.Debug.Log("Reached high intensity marker");
            }
            */
        }
        if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT)
        {
            FMOD.Studio.TIMELINE_BEAT_PROPERTIES beat = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
        }
        return FMOD.RESULT.OK;
    }
}