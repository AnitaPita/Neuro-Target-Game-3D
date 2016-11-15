using UnityEngine;
using LSL;
using Assets.LSL4Unity.Scripts;
using Assets.LSL4Unity.Scripts.Common;

namespace Assets.LSL4Unity.Demo
{
    /// <summary>
    /// An reusable example of an outlet which provides the orientation of an entity to LSL
    /// </summary>
    public class LSLDemoInlet : MonoBehaviour
    {
        private const string unique_source_id = "D256CFBDBA3145978CFA641403219531";

        private liblsl.StreamInlet inlet;
        private liblsl.StreamInfo streamInfo;
        public liblsl.StreamInfo GetStreamInfo()
        {
            return streamInfo;
        }


        private float[] currentSample;

        private double dataRate;

        public double GetDataRate()
        {
            return dataRate;
        }

        /*public bool HasConsumer()
        {
            if (inlet != null)
                return inlet.have_consumers();

            return false;
        }*/

        public string StreamName = "BeMoBI.Unity.Orientation.thing";
        public string StreamType = "Unity.Quaternion";
        public int ChannelCount = 4;

        public MomentForSampling sampling;

        public Transform sampleSource;

        void Start()
        {
            // initialize the array once
            currentSample = new float[ChannelCount];

            dataRate = LSLUtils.GetSamplingRateFor(sampling);

            streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, dataRate, liblsl.channel_format_t.cf_float32, unique_source_id);

            inlet = new liblsl.StreamInlet(streamInfo);
        }

        private void pull_sample()
        {
            if (inlet == null)
                return;
            var rotation = sampleSource.rotation;

            // reuse the array for each sample to reduce allocation costs
            currentSample[0] = rotation.x;
            currentSample[1] = rotation.y;
            currentSample[2] = rotation.z;
            currentSample[3] = rotation.w;

            inlet.pull_sample(currentSample, liblsl.local_clock());
        }

        void FixedUpdate()
        {
            if (sampling == MomentForSampling.FixedUpdate)
                pull_sample();
        }

        void Update()
        {
            if (sampling == MomentForSampling.Update)
                pull_sample();
        }

        void LateUpdate()
        {
            if (sampling == MomentForSampling.LateUpdate)
                pull_sample();
        }
    }
}
