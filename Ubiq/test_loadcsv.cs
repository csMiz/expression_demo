using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test_loadcsv : MonoBehaviour
{

    private List<float[]> receivedData = new List<float[]>();
    private int playing = 0;
    private int nowFrame = 0;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        FileStream file = new FileStream("C:/Users/asdfg/Desktop/P104C/vdata_lp.csv", FileMode.Open);
        StreamReader sr = new StreamReader(file);
        while (!sr.EndOfStream) {
            string s = sr.ReadLine();
            string[] segs = s.Split(',');
            if (segs.Length > 1) {
                float v1 = 100f * float.Parse(segs[0]);
                float v2 = 100f * float.Parse(segs[1]);
                float v3 = 100f * float.Parse(segs[2]);
                if (v1 < 0) { v1 = 0; }
                if (v2 < 0) { v2 = 0; }
                if (v3 < 0) { v3 = 0; }
                if (v1 > 100) { v1 = 100; }
                if (v2 > 100) { v2 = 100; }
                if (v3 > 100) { v3 = 100; }

                float[] frame = new float[] { v1, v2, v3 };
                receivedData.Add(frame);
            }
        }
        sr.Dispose();
        file.Close();
        file.Dispose();

        skinnedMeshRenderer = this.GetComponent<SkinnedMeshRenderer>();

    }

    private void FixedUpdate()
    {
        if (playing == 1) {
            if (nowFrame >= receivedData.Count) {
                playing = 0;
                nowFrame = 0;
            }
            float[] nowdata = receivedData[nowFrame];
            //Debug.Log(nowdata[0] + "," + nowdata[1] + "," + nowdata[2]);

            skinnedMeshRenderer.SetBlendShapeWeight(39, nowdata[0]);    // smile left
            skinnedMeshRenderer.SetBlendShapeWeight(40, nowdata[0]);    // smile right
            skinnedMeshRenderer.SetBlendShapeWeight(41, nowdata[0]);    // eye smile left
            skinnedMeshRenderer.SetBlendShapeWeight(42, nowdata[0]);    // eye smile right

            skinnedMeshRenderer.SetBlendShapeWeight(13, nowdata[1]);    // frown left
            skinnedMeshRenderer.SetBlendShapeWeight(14, nowdata[1]);    // frown right
            skinnedMeshRenderer.SetBlendShapeWeight(30, nowdata[1]);    // mouth down

            skinnedMeshRenderer.SetBlendShapeWeight(33, nowdata[2]);    // mouth open

            nowFrame += 1;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            playing = 1;
            Debug.Log("receiving data...");
        }
    }
}
