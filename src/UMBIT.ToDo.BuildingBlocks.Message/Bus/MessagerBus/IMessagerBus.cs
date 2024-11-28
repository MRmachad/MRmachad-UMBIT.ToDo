﻿using UMBIT.ToDo.Core.Messages.Messagem;

namespace TSE.Nexus.SDK.Messages.Bus.MessagerBus
{
    public interface IMessagerBus
    {
        bool EhValido();
        void Publish<T>(T message, string nomeQueue = "", string exchange = "") where T : UMBITMensagem, new();
        void Subscribe<T>(Action<T> onMessage, string nomeQueue = "", string exchange = "") where T : UMBITMensagem, new();

    }
}
