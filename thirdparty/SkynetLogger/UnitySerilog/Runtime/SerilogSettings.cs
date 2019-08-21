using Serilog.Core;
using Serilog.Events;
using UnityEngine;

namespace Serilog
{
    public class SerilogSettings : ScriptableObject
    {
        public static SerilogSettings Instance;

        public LogEventLevel LogLevel { get => m_LogLevel; set { m_LogLevel = value; m_LevelSwitch.MinimumLevel = value; } }

        [SerializeField]
        private LogEventLevel m_LogLevel = LogEventLevel.Debug;
        
        LoggingLevelSwitch m_LevelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);

        public void OnEnable()
        {
            Debug.Log("Enable Serilog");

            m_LevelSwitch = new LoggingLevelSwitch(LogLevel);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.UnityConsole()
                .MinimumLevel.ControlledBy(m_LevelSwitch)
                .CreateLogger();

            Instance = this;
        }
    }
}