using Serilog;
using UnityEditor;
using UnityEngine;

namespace Serilog.Editor
{
    [InitializeOnLoad]
    public class InitSerilogOnLoad : MonoBehaviour
    {
        static InitSerilogOnLoad()
        {
            SerilogSettingsProvider.GetSettings();
        }
    }
}