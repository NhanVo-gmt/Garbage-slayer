using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR
public class BehaviourTreeDrawingGizmos : MonoBehaviour
{
    static int index = 0;
    static Transform selectedTransform;
    static float drawRadius;
    static Vector2[] drawPointWays;

    public static void DrawLine()
    {
        index = 0;
    }

    public static void DrawPatrolLine(Vector2[] pointWays)
    {
        index = 2;
        drawPointWays = pointWays;
    }

    public static void DrawWireSphere(float radius)
    {
        index = 1;
        drawRadius = radius;
    }

    private void OnDrawGizmos() {
        switch (index)
        {
            case 0:
                break;

            case 1:
                Gizmos.color = Color.red;
            
                if (Selection.transforms != null)
                Gizmos.DrawWireSphere(Selection.transforms[0].position, drawRadius);
                break;
            case 2:
                Gizmos.color = Color.green;
                
                for (int i = 0; i < drawPointWays.Length; i++)
                {
                    if (i == drawPointWays.Length - 1)
                    {
                        Gizmos.DrawLine(drawPointWays[i], drawPointWays[0]);
                        continue;
                    }
                    Gizmos.DrawLine(drawPointWays[i], drawPointWays[i + 1]);
                }
                break;
            default:
                break;
        }

    }

}
#endif
