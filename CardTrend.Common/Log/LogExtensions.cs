using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Common.Log
{
    public static class LogExtensions
    {
        public static string BuildExceptionMessage(this Exception ex)
        {
            Exception logException = ex;
            if (ex.InnerException != null)
            {
                logException = ex.InnerException;
            }

            StringBuilder errorMessage = new StringBuilder();

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("Message :");
            errorMessage.Append(logException.Message);

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("Source :");
            errorMessage.Append(logException.Source);

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("Stack Trace :");
            errorMessage.Append(logException.StackTrace);

            errorMessage.Append(Environment.NewLine);
            errorMessage.Append("TargetSite :");
            errorMessage.Append(logException.TargetSite);

            return errorMessage.ToString();
        }

        /// <summary>
        /// Formats the stack trace.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="builder">The builder.</param>
        private static void Format(Exception exception, StringBuilder builder)
        {
            builder.AppendLine(exception.Message);
            builder.AppendLine(exception.StackTrace);
            if (exception.InnerException != null)
            {
                builder.AppendLine();
                builder.AppendLine("Inner Exception:");
                Format(exception.InnerException, builder);
            }
        }

    }
}
