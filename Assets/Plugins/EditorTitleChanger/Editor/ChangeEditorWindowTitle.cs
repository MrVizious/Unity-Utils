#if UNITY_EDITOR_WIN
using System;
using System.Linq;
using UnityEditor;
using System.Reflection;

namespace MainWindowTitleModifier
{
    [InitializeOnLoad]
    public class ChangeEditorWindowTitle
    {
        static ChangeEditorWindowTitle()
        {
            EditorApplication.update += Update;
        }
        //[MenuItem("MainWindowTitleModifier/Set Title to Unity Editor", priority = 1)]
        static void Update()
        {
            // ApplicationTitleDescriptorのTypeを取得します.
            Type tEditorApplication = typeof(EditorApplication);
            Type tApplicationTitleDescriptor = tEditorApplication.Assembly.GetTypes()
                .First(x => x.FullName == "UnityEditor.ApplicationTitleDescriptor");

            // 関係するイベントとメソッドのInfoを取得します.
            EventInfo eiUpdateMainWindowTitle = tEditorApplication.GetEvent("updateMainWindowTitle", BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo miUpdateMainWindowTitle = tEditorApplication.GetMethod("UpdateMainWindowTitle", BindingFlags.Static | BindingFlags.NonPublic);

            // Action<object>をAction<ApplicationTitleDescriptor>に変換します.
            Type delegateType = typeof(Action<>).MakeGenericType(tApplicationTitleDescriptor);
            MethodInfo methodInfo = ((Action<object>)UpdateMainWindowTitle).Method;
            Delegate del = Delegate.CreateDelegate(delegateType, null, methodInfo);

            // UpdateMainWindowTitleを呼び出す前後にイベントの追加と削除.
            eiUpdateMainWindowTitle.GetAddMethod(true).Invoke(null, new object[] { del });
            miUpdateMainWindowTitle.Invoke(null, new object[0]);
            eiUpdateMainWindowTitle.GetRemoveMethod(true).Invoke(null, new object[] { del });
        }

        static void UpdateMainWindowTitle(object desc)
        {
            // UnityEditor.ApplicationTitleDescriptor.title = "Solution1_Reflection"; と同様
            typeof(EditorApplication).Assembly.GetTypes()
                .First(x => x.FullName == "UnityEditor.ApplicationTitleDescriptor")
                .GetField("title", BindingFlags.Instance | BindingFlags.Public)
                .SetValue(desc, "Unity Editor");
        }
    }
}
#endif