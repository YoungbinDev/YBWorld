using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ReadOnlyAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

public class KinematicEquationMovement : MonoBehaviour
{
    [SerializeField] private float displacement;
    [ReadOnly, SerializeField] private float initialVelocity;
    [ReadOnly, SerializeField] private float finalVelocity = 0;
    [ReadOnly, SerializeField] private float acceleration = Physics.gravity.y;
    [ReadOnly, SerializeField] private float time;

    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();

        time = (finalVelocity - Mathf.Sqrt(Mathf.Pow(finalVelocity, 2) - 2 * acceleration * displacement)) / acceleration;
        initialVelocity = ((displacement / time) - ((acceleration * time) / 2));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            time = (finalVelocity - Mathf.Sqrt(Mathf.Pow(finalVelocity, 2) - 2 * acceleration * displacement)) / acceleration;
            initialVelocity = ((displacement / time) - ((acceleration * time) / 2));

            rigid.velocity = Vector3.zero;
            rigid.AddForce(new Vector3(0, initialVelocity, 0), ForceMode.Impulse);
        }
    }
}
