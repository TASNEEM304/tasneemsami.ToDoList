﻿namespace TasneemSami.ToDoList.Api.Logger
{
    public interface ILoggerManger
    {
        void LogInfo(string message);

        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
