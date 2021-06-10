using System;
using UnityEditor;
using UnityEngine;

namespace Throwing.Editor
{
    [CustomEditor(typeof(Aimer))]
    public class AimerCustomEditor : UnityEditor.Editor
    {
        private Aimer _aimer;

        private void OnEnable()
        {
            _aimer = target as Aimer;
        }

        public void OnSceneGUI()
        {
            
            EditorGUI.BeginChangeCheck();
            var newRotation=UnityEditor.Handles.RotationHandle(_aimer.directionPoint.localRotation,_aimer.directionPoint.position);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_aimer.directionPoint, "aim rotation");
                _aimer.directionPoint.localRotation = newRotation;
            }
            
            
        }
    }
}