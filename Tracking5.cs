using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracking5 : MonoBehaviour
{

    public Transform OtherTransform2;
    private List<RectTransform> mRectTransforms = new List<RectTransform>();
    private AndroidJavaClass mAjc;
    private float mTimeElapsed;
    string mJavaMessage = "";
    private Dictionary<string, RectTransform> mRtDict = new Dictionary<string, RectTransform>();
    private string[] NAMES = {"aaa", "bbb", "ccc" };

    // Use this for initialization
    void Start () {
        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("status");
        for (int i=0; i< tagobjs.Length; i++)
        {
            RectTransform rt = tagobjs[i].GetComponent<RectTransform>();
            rt.position = new Vector3((float)999, (float)999, (float)999);
            mRtDict.Add(NAMES[i], rt);
        }
        mAjc = new AndroidJavaClass("aar.sample.abe.com.broadcastlib.MyReceiver");
        mAjc.CallStatic("createInstance");
    }
	
	// Update is called once per frame
	void Update () {

        mTimeElapsed += Time.deltaTime;

        if (mTimeElapsed > 0.5)
        {
            mTimeElapsed = 0;
            Vector3 angles = OtherTransform2.rotation.eulerAngles;

            string javaMessage = mAjc.GetStatic<string>("mText");
            if (javaMessage.Equals(mJavaMessage))
            {
                return;
            }
            mJavaMessage = javaMessage;

            string[] messages = javaMessage.Split('\n');

            for (int i=0; i < messages.Length; i++)
            {
                string[] items = messages[i].Split(',');
                string name = items[0];
                double angDx = double.Parse(items[1]);
                double angDy = double.Parse(items[2]);
                string hp = items[3];

                double convAngleX = angles[1] - 12.5 + (35 * angDx);
                double dx = 5 * Math.Sin(Math.PI * convAngleX / 180);
                double dz = 5 * Math.Cos(Math.PI * convAngleX / 180);

                RectTransform rt = mRtDict[name];
                rt.position = new Vector3((float)dx, GetComponent<RectTransform>().position[1], (float)dz);
                rt.eulerAngles = new Vector3(0, angles[1], 0);

                RectTransform[] children = rt.GetComponentsInChildren<RectTransform>();
                foreach (RectTransform child in children)
                {
                    if (child.CompareTag("disp_text"))
                    {
                        child.GetComponent<Text>().text = name + " " + hp;
                    }
                }
                if (mRectTransforms.Count < i) break;
            }
        }
    }
}
