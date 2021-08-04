﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMarket.Common
{
    public static class Logging
    {
        public static void LogInfo<T>(
            this ILogger<T> logger,
            string className,
            string methodName,
            string customMessage)
        {
            logger.LogInformation($"[{className} | {methodName}]\n{customMessage}.");
        }

        public static void LogErrorByTemplate<T>(
            this ILogger<T> logger,
            string className,
            string methodName,
            string customMessage,
            Exception exception)
        {
            logger.LogError($"[{className} | {methodName}]\n{customMessage};\n" +
                $"ex.Message == {exception.Message}\n" +
                $"ex.InnerException = {exception.InnerException}",
                exception);
        }
    }
}
