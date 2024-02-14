using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.GetRadius());

        Vector3 viewAngleA = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.GetAngle() / 2);
        Vector3 viewAngleB = DirectionFromAngle(fov.transform.eulerAngles.y, fov.GetAngle() / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.GetRadius());
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.GetRadius());

        if (fov.GetCanSeeTarget())
        {
            Handles.color = Color.green;
            if (fov.GetTargetTransform() != null)
            {
                Handles.DrawLine(fov.transform.position, new Vector3(fov.GetTargetTransform().position.x, fov.GetTargetTransform().position.y + fov.GetTargetCenterY(), fov.GetTargetTransform().position.z));
            }
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
