﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Core.Events
{
    public abstract class Message<T> : IRequest<T>
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
