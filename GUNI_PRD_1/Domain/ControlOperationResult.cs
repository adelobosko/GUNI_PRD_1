using System;
using System.Collections.Generic;
using System.Linq;

namespace GUNI_PRD_1
{
    public enum ControlOperationStatus
    {
        EXECUTED,
        DECLINED
    }

    public class ControlOperationResult
    {
        public ControlOperationStatus Status { get; set; }
        public List<string> Messages { get; set; }

        public ControlOperationResult()
        {
            Messages = new List<string>();
        }

        public override string ToString()
        {
            var result = $"Status: {Enum.GetName(typeof(ControlOperationStatus), Status)};";
            if (!Messages.Any())
                return result;

            result += "\r\nMessages:";
            return Messages.Aggregate(result, (current, message) => current + $"\r\n{message}");
        }
    }
}