﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectBlow : MonoBehaviour {

    public static float MicLoudness;
    public float BlowForce;

    private string _device;

    //mic initialization
    void InitMic() {
        if (_device == null) _device = Microphone.devices[0];
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
    }

    void StopMicrophone() {
        Microphone.End(_device);
    }


    AudioClip _clipRecord = new AudioClip();
    int _sampleWindow = 128;



    void Start() {
        Debug.Log("Start Blower");
        BlowForce = 0f;
    }

    //get data from microphone into audioclip
    float LevelMax() {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        float sum = 0f;
        for (int i = 0; i < _sampleWindow; i++) {
            float wavePeak = waveData[i] * waveData[i];
            sum += wavePeak;
            //if (levelMax < wavePeak) {
            //    levelMax = wavePeak;
            //}
        }
        //Debug.Log(sum * 10000000);
        BlowForce = sum;
        return sum;
    }



    void Update() {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = LevelMax();
    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable() {
        InitMic();
        _isInitialized = true;
    }

    //stop mic when loading a new level or quit application
    void OnDisable() {
        StopMicrophone();
    }

    void OnDestroy() {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus) {
        if (focus) {
            Debug.Log("Focus");

            if (!_isInitialized) {
                Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus) {
            Debug.Log("Pause");
            //StopMicrophone();
            //Debug.Log("Stop Mic");
            //_isInitialized = false;

        }
    }



}
