using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    GameObject cameraObj;
    Camera script;
    private Vector4 camera_u4 = new Vector4(0f, 0f, 0f, 0f);
    private Vector4 camera_x4 = new Vector4(0f, 0f, 0f, 0f);
    private Vector4 kyori = new Vector4(0f, 0f, 0f, 0f);
    private Vector3 u3 = new Vector3(0f, 0f, 0f);
    private Vector4 A4 = new Vector4(0f, 0f, 0.1f, 0f);
    private Vector4 a4 = new Vector4(0f, 0f, 0f, 0f);
    public Vector4 x4 = new Vector4(0f, 0f, 0f, 0f);
    public Vector4 X4 = new Vector4(0f, 0f, 0f, 0f);
    public Vector4 u4 = new Vector4(0f, 0f, 0f, 0f);
    private Matrix4x4 L;
    private Matrix4x4 Linv;
    public float t;
    public float dt;
    // Start is called before the first frame update
    void Start()
    {
        cameraObj = GameObject.Find("MainCamera");
        script = cameraObj.GetComponent<Camera>();
        x4 = transform.position;
        t = 0;
        dt = 0.016f;
    }

    // Update is called once per frame
    void Update()
    {
        camera_x4 = script.x4;
        kyori = x4 - camera_x4;
        //dt = 2 * (kyori.x * u4.x + kyori.y * u4.y + kyori.z * u4.z + kyori.w * u4.w);
        t += dt;
        L = Matrix4x4.identity;
        u3 = new Vector3(u4.x, u4.y, u4.z);
        u4.w = Mathf.Sqrt(1 + Mathf.Pow(u3.magnitude, 2));

        camera_u4 = script.u4;

        L.m00 = camera_u4.w;
        L.m03 = -camera_u4.z;
        L.m30 = -camera_u4.z;
        L.m33 = camera_u4.w;

        Linv = Matrix4x4.identity;
        Linv.m00 = camera_u4.w;
        Linv.m03 = camera_u4.z;
        Linv.m30 = camera_u4.z;
        Linv.m33 = camera_u4.w;

        x4 += u4 * dt;

        a4 = Linv * A4;

        u4 += a4 * dt;

        X4 = L * x4;


        transform.position = new Vector4(X4.x, X4.y, X4.z);
    }
}
