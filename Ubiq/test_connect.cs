using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;

public class test_connect : MonoBehaviour
{

    private System.Net.Sockets.Socket TCPListener = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

    private List<float[]> receivedData = new List<float[]>();
    private object syncLock = new object();

    private SkinnedMeshRenderer skinnedMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = this.GetComponent<SkinnedMeshRenderer>();

        // start listener
        Thread thread = new Thread(new ThreadStart(ThreadListen));
        thread.Start();


    }

    // Update is called once per frame
    void Update()
    {

        if (receivedData.Count > 0) {
            // optional: filtering
            // float[] dataIn = ApplyLowPassFilter(receivedData, kernelSize);
            // float[] dataIn = ApplyGaussianFilter(receivedData, kernelSize, sigma);

            float[] dataIn = receivedData[receivedData.Count - 1];

            // apply animation
            float v1 = 100f * dataIn[0];
            float v2 = 100f * dataIn[1];
            float v3 = 100f * dataIn[2];
            if (v1 < 0) { v1 = 0; }
            if (v2 < 0) { v2 = 0; }
            if (v3 < 0) { v3 = 0; }
            if (v1 > 100) { v1 = 100; }
            if (v2 > 100) { v2 = 100; }
            if (v3 > 100) { v3 = 100; }

            // smile
            skinnedMeshRenderer.SetBlendShapeWeight(39, v1);    // smile left
            skinnedMeshRenderer.SetBlendShapeWeight(40, v1);    // smile right
            skinnedMeshRenderer.SetBlendShapeWeight(41, v1);    // eye smile left
            skinnedMeshRenderer.SetBlendShapeWeight(42, v1);    // eye smile right

            // sad
            skinnedMeshRenderer.SetBlendShapeWeight(13, v2);    // frown left
            skinnedMeshRenderer.SetBlendShapeWeight(14, v2);    // frown right
            skinnedMeshRenderer.SetBlendShapeWeight(30, v2);    // mouth down

            // surprise
            skinnedMeshRenderer.SetBlendShapeWeight(33, v3);    // mouth open

            if (receivedData.Count > 15) {    // clean buffer if buffer size is too large
                lock (syncLock) {
                    while (receivedData.Count > 15) {
                        receivedData.RemoveAt(0);
                    }
                }
            }
        }

    }

    public void StartListener()
    {
        System.Net.IPEndPoint localEndPoint = new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);    // listen at localhost, port 8888
        TCPListener.Bind(localEndPoint);
        TCPListener.Listen(10);

        Debug.Log("Listener started at 8888");

    }

    public void ThreadListen() {
        
        StartListener();

        while (true) {    // listening tcp connection

            byte[] bytes;
            System.Net.Sockets.Socket handler = TCPListener.Accept();
            byte[] poseData = new byte[3];

            bytes = new byte[1024];
            int bytesRec = handler.Receive(bytes); 
            if (bytesRec > 0)
            {
                for (int i = 0; i < 3; i++) {    // smile, sad and surprise values
                    poseData[i] = bytes[i];
                }
            }
            else
            {
            }

            handler.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            handler.Close();
            handler.Dispose();

            if (poseData[0] != 0)
            {
                // send back to original thread
                float[] content = new float[3];
                for (int i = 0; i < 3; i++)
                {
                    content[i] = Convert.ToSingle(poseData[i]);
                }

                lock(syncLock)
                {
                    receivedData.Add(content);
                }
            }

        }


    }


}
