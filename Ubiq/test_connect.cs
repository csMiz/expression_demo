using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;

public class test_connect : MonoBehaviour
{

    private System.Net.Sockets.Socket TCPListener = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

    private List<float> receivedData = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        //StartListener();

        Thread thread = new Thread(new ThreadStart(ThreadListen));
        thread.Start();


    }

    // Update is called once per frame
    void Update()
    {
        //    byte[] bytes;
        //    System.Net.Sockets.Socket handler = TCPListener.Accept();
        //    byte[] poseData = new byte[4];

        //    bytes = new byte[1024];
        //    int bytesRec = handler.Receive(bytes);    //可能会卡在这
        //    if (bytesRec > 0) {
        //        poseData[0] = bytes[0];
        //        poseData[1] = bytes[1];
        //        poseData[2] = bytes[2];
        //        poseData[3] = bytes[3];
        //    }
        //    else{
        //    }

        //    handler.Shutdown(System.Net.Sockets.SocketShutdown.Both);
        //    handler.Close();
        //    handler.Dispose();

        //    if (poseData[0] == 0)
        //    {
        //        gameObject.transform.localScale = new Vector3(1, 1, 1);

        //    }
        //    else {
        //        gameObject.transform.localScale = new Vector3(0.1f * Convert.ToSingle(poseData[0]), 1, 1);
        //    }

        if (receivedData.Count > 0) {
            gameObject.transform.localScale = new Vector3(receivedData[0], 1, 1);
        }

    }

    public void StartListener()
    {
        System.Net.IPEndPoint localEndPoint = new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        TCPListener.Bind(localEndPoint);
        TCPListener.Listen(10);

        Debug.Log("Listener started at 8888");

    }

    public void ThreadListen() {
        //  程序结束时不会终止此线程
        int count = 0;

        StartListener();


        while (count < 10) {

            DateTime st = DateTime.Now;

            byte[] bytes;
            System.Net.Sockets.Socket handler = TCPListener.Accept();
            byte[] poseData = new byte[4];

            bytes = new byte[1024];
            int bytesRec = handler.Receive(bytes); 
            if (bytesRec > 0)
            {
                poseData[0] = bytes[0];
                poseData[1] = bytes[1];
                poseData[2] = bytes[2];
                poseData[3] = bytes[3];
                Debug.Log(poseData[0]);
            }
            else
            {
            }

            handler.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            handler.Close();
            handler.Dispose();

            if (poseData[0] == 0)
            {
            }
            else
            {
                //gameObject.transform.localScale = new Vector3(0.1f * Convert.ToSingle(poseData[0]), 1, 1);   //需要回到原始线程
                receivedData.Clear();
                receivedData.Add(0.1f * Convert.ToSingle(poseData[0]));

            }


            DateTime ed = DateTime.Now;
            while ((ed - st).TotalMilliseconds < 1000)
            {
                ed = DateTime.Now;
            }

            count++;
        }


    }


}
