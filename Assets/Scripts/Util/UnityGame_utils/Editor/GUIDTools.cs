using System.Windows;
using NUnit;
using UnityEngine;
using UnityEditor;

namespace UnityUtils.Editor
{
    public class GUIDTools : EditorWindow
    {
        static int guiMode;
        string guidToSearch = "";
        bool noRes = false;
        bool autoFind = false;
        bool priv = false;

        [MenuItem("Assets/Copy GUID")]
        public static void CopyGUID()
        {
            string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));
            Debug.Log(guid + " copied");
            GUIUtility.systemCopyBuffer = guid;
        }

        [MenuItem("Tools/GUID/Find by GUID")]
        static void FindByGUID()
        {
            guiMode = 0;
            EditorWindow.GetWindow<GUIDTools>("Finder");
        }
        [MenuItem("Hierarchy/test")]
        static void ChangeObjSceneGui()
        {
            guiMode = 1;
            EditorWindow.GetWindow<GUIDTools>("Finder");
        }

        [ContextMenu("Do Something")]
        void DoSomething()
        {
            Debug.Log("Perform operation");
        }

        private void Reset()
        {
            priv = false;
            noRes = true;
        }

        private void OnGUI()
        {
            if (guiMode == 0)
            {
                
                string lastSearch = guidToSearch;
                guidToSearch = GUILayout.TextField(guidToSearch);
                if (guidToSearch != lastSearch) Reset();
                GUILayout.Space(2f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Find") || autoFind)
                {
                    priv = true;
                    if (ValidateGUIDAsset(guidToSearch))
                        noRes = false;
                    else
                        noRes = true;
                }
                //GUILayout.FlexibleSpace();
                autoFind = EditorGUILayout.Toggle(autoFind);
                GUILayout.EndHorizontal();
                GUILayout.Space(2f);
                if (!noRes && priv)
                {
                    GUILayout.Label("Location of asset: " + GetAssetPath(guidToSearch));
                }
                else if (noRes && priv)
                {
                    GUILayout.Label("No asset found with that GUID");
                }
                unsafe
                {
                    //Define int var a
                    int a = int.MaxValue;
                    Debug.Log(a);
                    //Set int* b to address of a
                    int* b = &a;
                    Debug.Log(*b);
                    //Remove the type of b --UNSAFE (Breaks language convention)--
                    void* c = b;
                    //! From this point onward c is just a memory address
                    Debug.Log(*(char*)c); ;

                    //Set it back to an int*
                    int* d = (int*)c;

                    //The compiler is fine with these conversions but it's complete goggely goo
                    char* e = (char*)c;
                    long* f = (long*)c;
                    short* g = (short*)c;
                }
            }
        }
        public bool ValidateGUIDAsset(string guid)
        {
            bool assetFound;
            assetFound = AssetDatabase.GUIDToAssetPath(guid) != "";
            return assetFound;
        }

        public string GetAssetPath(string guid)
        { return AssetDatabase.GUIDToAssetPath(guid);  }
    }
}
