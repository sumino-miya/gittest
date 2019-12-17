using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 u3 = new Vector3(0f, 0f, 0f);
    private Vector4 A4 = new Vector4(0f, 0f, 0f, 0f);
    private Vector4 a4 = new Vector4(0f, 0f, 0f, 0f);
    public Vector4 x4 = new Vector4(0f, 0f, 0f, 0f);
    public Vector4 X4 = new Vector4(0f, 0f, 0f, 0f);
    public Vector4 u4 = new Vector4(0f, 0f, 0f, 0f);
    public float fAcceleration = 0.0001f;
    public Matrix4x4 L;
    public Matrix4x4 Linv;
    public float t;
    public float dt;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = x4;
        t = 0;
        dt = 0.016f;
    }

    // Update is called once per frame
    void Update()
    {
        t += dt;
        L = Matrix4x4.identity;
        u3 = new Vector3(u4.x, u4.y, u4.z);
        u4.w = Mathf.Sqrt(1 + Mathf.Pow(u3.magnitude, 2));

        L.m00 = u4.w;
        L.m03 = -u4.z;
        L.m30 = -u4.z;
        L.m33 = u4.w;

        Linv = Matrix4x4.identity;
        Linv.m00 = u4.w;
        Linv.m03 = u4.z;
        Linv.m30 = u4.z;
        Linv.m33 = u4.w;

        x4 += u4 * dt;

        A4.x = Input.GetAxis("Horizontal") * fAcceleration;
        A4.z = Input.GetAxis("Vertical") * fAcceleration;

        a4 = Linv * A4;

        u4 += a4 * dt;

        X4 = L * x4;


        transform.position = new Vector3(X4.x, X4.y, X4.z);
    }
}
