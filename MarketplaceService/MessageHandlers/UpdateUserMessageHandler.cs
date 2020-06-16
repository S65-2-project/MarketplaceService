using marketplaceservice.MqMessages;
using MarketplaceService.Repositories;
using MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace marketplaceservice.MessageHandlers
{
    public class UpdateUserMessageHandler : IMessageHandler<UpdateUserMessage>
    {
        private readonly IDAppRepository _dAppRepository;
        private readonly IDelegateRepository _delegateRepository;
        public UpdateUserMessageHandler(IDAppRepository dAppRepository, IDelegateRepository delegateRepository)
        {
            _dAppRepository = dAppRepository;
            _delegateRepository = delegateRepository;
        }
        public Task HandleMessageAsync(string messageType, UpdateUserMessage sendable)
        {
            _dAppRepository.UpdateUserEmail(sendable.Id, sendable.NewEmail);
            _delegateRepository.UpdateUserEmail(sendable.Id, sendable.NewEmail);
            return Task.CompletedTask;
        }

        public Task HandleMessageAsync(string messageType, byte[] obj)
        {
            return HandleMessageAsync(messageType, JsonSerializer.Deserialize<UpdateUserMessage>((ReadOnlySpan<byte>)obj, (JsonSerializerOptions)null));

        }


    }
}
