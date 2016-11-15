using UnityEngine;
using LSL;
using Assets.LSL4Unity.Scripts;
using Assets.LSL4Unity.Scripts.Common;

public class Cube_Translation : MonoBehaviour {

    private liblsl.StreamInlet inlet;
    private liblsl.StreamInfo streamInfo;
    public liblsl.StreamInfo GetStreamInfo()
    {
        return streamInfo;
    }

    private float[] currentSample;
    public string StreamName = "BioControlSignal";
    public string StreamType = "Sine";
    public string UniqueID = "uid98765";
    public int ChannelCount = 1;


    private float movement = 1f;

    void Start () {
        currentSample = new float[1];
        streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, 100, liblsl.channel_format_t.cf_float32, UniqueID);
        inlet = new liblsl.StreamInlet(streamInfo);
    }

    // Update is called once per frame
    void Update () {
        inlet.pull_sample(currentSample);
        //if (Input.GetKey("o"))
        //    leftSpeed += 1;
        //if (Input.GetKey("p"))
        //    rightSpeed += 1;
        if (currentSample[0] < 0)
            transform.Translate(Vector3.forward * Time.deltaTime);
        if (currentSample[0] > 0)
            transform.Translate(Vector3.forward * -1*Time.deltaTime);
    }
}
