#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;
using Mona.SDK.Brains.UIEditors;

namespace Mona
{
    [InitializeOnLoad]
    public class MonaToolbar
    {
        static MonaToolbar()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            GUI.skin.button.fontSize = 12;
            GUI.skin.button.fixedHeight = 22;
            GUI.skin.button.border = new RectOffset(0, 0, 0, 0);
            GUI.skin.button.margin = new RectOffset(0, 0, 0, 0);
            GUI.skin.button.padding = new RectOffset(10, 10, 4, 4);
            GUI.skin.button.alignment = TextAnchor.MiddleCenter;
            GUI.color = Color.white * 0.75f;
            GUI.contentColor = Color.white * 1.19f;

            if (TemplateCheck.UpdateAvaliable)
            {
                if (GUILayout.Button(new GUIContent("○ Update", "Update Template")))
                {
                    TemplateHelper.ShowWindow();
                }
            }
            else if (TemplateCheck.ConfigurationIssue)
            {
                if (GUILayout.Button(new GUIContent("○ Setup Error", "Check Template Configuration")))
                {
                    TemplateHelper.ShowWindow();
                }
            }

            if (GUILayout.Button(new GUIContent("☉ Mona Brains", "Show Mona Brains")))
            {
                MonaBrainsEditorWindow.ShowWindow();
            }

            GUILayout.Space(25);

            if (GUILayout.Button(new GUIContent("☼ Mona Library", "Show Mona Library")))
            {
                MonaLibraryWindow.ShowWindow();
            }

            GUI.contentColor = QualityAssurance.SpaceErrors != null && QualityAssurance.SpaceErrors.Count != 0 ? Color.red * 20.19f : Color.white * 1.2f;

            GUILayout.Space(25);

            
            if (GUILayout.Button(new GUIContent("▶️ Playground", "Build and open playground")))
            {
                QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();

                if (QualityAssurance.SpaceErrors == null || QualityAssurance.SpaceErrors.Count == 0)
                {
                    Helpers.UpsertExportsDirectory();
                    BuildPipeline.BuildAssetBundles(Constants.PlaygroundDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
                    Helpers.OpenDirectory(Constants.ExportsDirectory);
                    Application.OpenURL(Constants.PlaygroundURL);
                }
                else
                {
                    QualityAssuranceEditor.Init();
                }
            }

            if (GUILayout.Button(new GUIContent("▲ QA", "QA")))
            {
                QualityAssuranceEditor.Init();
            }
            GUI.color = Color.white;
        }
    }
}
#endif