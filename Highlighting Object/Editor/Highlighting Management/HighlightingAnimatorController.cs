using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using Proekt.HighlightingManagement;

namespace ProektEditor.HighlightingManagement
{
    public sealed class HighlightingAnimatorController
    {
        private const float m_DefaultDuration = 0.25f;


        public static void TryCreateControllerFromPanel(HighlightingAnimator highlightingAnimator)
        {
            var title = "Create new controller";
            var extension = "controller";
            var path = EditorUtility.SaveFilePanel(title, "", highlightingAnimator.name, extension);
            path = FileUtil.GetProjectRelativePath(path);

            if (path.Length > 1)
                highlightingAnimator.animator.runtimeAnimatorController = CreateController(highlightingAnimator, path);
        }

        public static AnimatorController CreateController(HighlightingAnimator highlightingAnimator, string path)
        {
            var controller = AnimatorController.CreateAnimatorControllerAtPath(path);

            controller.AddParameter(highlightingAnimator.normal, AnimatorControllerParameterType.Trigger);
            controller.AddParameter(highlightingAnimator.highlighted, AnimatorControllerParameterType.Trigger);

            var rootStateMachine = controller.layers[0].stateMachine;

            var normalMotion = CreateAnimationClip(highlightingAnimator.normal);
            AddClipToAnimatorController(normalMotion, controller);

            var highlightedMotion = CreateAnimationClip(highlightingAnimator.highlighted);
            AddClipToAnimatorController(highlightedMotion, controller);

            var normalState = rootStateMachine.AddState(highlightingAnimator.normal);
            normalState.motion = normalMotion;

            var highlightedState = rootStateMachine.AddState(highlightingAnimator.highlighted);
            highlightedState.motion = highlightedMotion;

            var normalTransition = CreateAnyStateTransition(rootStateMachine, normalState, highlightingAnimator.normal);
            var highlightedTransition = CreateAnyStateTransition(rootStateMachine, highlightedState, highlightingAnimator.highlighted);

            return controller;
        }

        private static void AddClipToAnimatorController(AnimationClip animationClip, AnimatorController animatorController)
        {
            AssetDatabase.AddObjectToAsset(animationClip, animatorController);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(animationClip));
        }

        private static AnimatorStateTransition CreateAnyStateTransition(AnimatorStateMachine stateMachine, AnimatorState state, string parameter)
        {
            var transition = stateMachine.AddAnyStateTransition(state);
            transition.AddCondition(AnimatorConditionMode.If, 0, parameter);
            transition.duration = m_DefaultDuration;

            return transition;
        }

        private static AnimationClip CreateAnimationClip(string name)
        {
            var clip = new AnimationClip();
            clip.name = name;

            return clip;
        }
    }
}
