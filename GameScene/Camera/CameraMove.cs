using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    public Vector3 offestPos;
    public float bodyHeight;
    public float moveSpeed = 10;
    public float rotaSpeed = 10;
    private Transform UPtarget;
    private Transform target;
    public Transform npctarget;
    public bool Isnpc;
    private Vector3 targetPos;
    private Quaternion targetRot;

    private void Update()
    {
        if (target == null)
            return;

        targetPos = target.position + target.forward * offestPos.z;
        targetPos += Vector3.up * offestPos.y;
        targetPos += Vector3.right * offestPos.x;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);
        if (!Isnpc)
            UPtarget = target;
        else
            UPtarget = npctarget;

        targetRot = Quaternion.LookRotation(UPtarget.position + Vector3.up * bodyHeight - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, rotaSpeed * Time.deltaTime);
    }

    public void SetTargetPos(Transform target)
    {
        this.target = target;
    }
}
