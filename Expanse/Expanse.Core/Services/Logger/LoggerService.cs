#region Using namespaces...

using System.Diagnostics;

#endregion


namespace Expanse.Core.Services.Logger
{
    public abstract class LoggerService
    {
        #region Public Methods

        public abstract void Trace(string message, params object[] args);

        [DebuggerStepThrough]
        public void TraceIf(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                Trace(message, args);
            }
        }

        public abstract void Debug(string message, params object[] args);

        [DebuggerStepThrough]
        public void DebugIf(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                Debug(message, args);
            }
        }

        public abstract void Info(string message, params object[] args);

        [DebuggerStepThrough]
        public void InfoIf(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                Info(message, args);
            }
        }

        public abstract void Warn(string message, params object[] args);

        [DebuggerStepThrough]
        public void WarnIf(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                Warn(message, args);
            }
        }

        public abstract void Error(string message, params object[] args);

        [DebuggerStepThrough]
        public void ErrorIf(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                Error(message, args);
            }
        }

        public abstract void Fatal(string message, params object[] args);

        [DebuggerStepThrough]
        public void FatalIf(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                Fatal(message, args);
            }
        }

        #endregion

    }
}