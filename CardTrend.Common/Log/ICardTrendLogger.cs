﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Common.Log
{
    public interface ICardTrendLogger
    {
        void Info(string message);

        void Warn(string message);

        void Debug(string message);

        void Error(string message);
        void Error(string message, Exception x);
        void Error(Exception x);

        void Fatal(string message);
        void Fatal(Exception x);
    }
}
