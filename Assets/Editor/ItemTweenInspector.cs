using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(ItemTween))]
public class ItemTweenInspector : Editor
{
    ItemTween itemTween;

    public override void OnInspectorGUI()
    {
        DrawComponents();
    }

    private void DrawComponents()
    {
        itemTween = (ItemTween)target;

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Script");
        EditorGUILayout.ObjectField(MonoScript.FromMonoBehaviour((ItemTween)target), typeof(ItemTween), false, GUILayout.Width(250));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Tween Type");
        itemTween.tweenType = (TweenType)EditorGUILayout.EnumPopup(itemTween.tweenType, GUILayout.Width(250));
        GUILayout.EndHorizontal();

        switch(itemTween.tweenType)
        {
            case TweenType.Move:
                DrawMoveComponents();
                break;

            case TweenType.Scale:
                DrawScaleComponents();
                break;

            case TweenType.Fade:
                DrawFadeComponents();
                break;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Tween Time");
        itemTween.tweenTime = EditorGUILayout.FloatField(itemTween.tweenTime, GUILayout.Width(250));
        GUILayout.EndHorizontal();
    }

    private void DrawMoveComponents()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Move Direction");
        itemTween.direction = (MoveDirection)EditorGUILayout.EnumPopup(itemTween.direction, GUILayout.Width(250));
        GUILayout.EndHorizontal();
    }

    private void DrawScaleComponents()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Scale");
        itemTween.scale = (Scale)EditorGUILayout.EnumPopup(itemTween.scale, GUILayout.Width(250));
        GUILayout.EndHorizontal();
    }

    private void DrawFadeComponents()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Fade");
        itemTween.fade = (Fade)EditorGUILayout.EnumPopup(itemTween.fade, GUILayout.Width(250));
        GUILayout.EndHorizontal();
    }

}
