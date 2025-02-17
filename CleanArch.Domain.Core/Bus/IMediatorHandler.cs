﻿using CleanArch.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TRequest, TResponse>(TRequest command) where TRequest : Command<TResponse>;
    }
}
