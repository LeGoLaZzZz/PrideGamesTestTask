using System;
using Throwing.Trajectory;
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

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var trajectoryFormula = _aimer.trajectoryFormula;
            
            var maxiHigh = trajectoryFormula.GetMaxHigh(_aimer.GetCurrentDirection(),_aimer.GetSpawnPosition() );
            UnityEditor.EditorGUILayout.LabelField($"Max high: {maxiHigh}");
        
            var maxiRange = trajectoryFormula.GetMaxRange(_aimer.GetCurrentDirection(),_aimer.GetSpawnPosition());
            UnityEditor.EditorGUILayout.LabelField($"Max range: {maxiRange}");
        }

        public void OnSceneGUI()
        {
            EditorGUI.BeginChangeCheck();
            var newRotation =
                UnityEditor.Handles.RotationHandle(_aimer.directionPoint.localRotation, _aimer.directionPoint.position);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_aimer.directionPoint, "aim rotation");
                _aimer.directionPoint.localRotation = newRotation;
            }
        }
    }
}