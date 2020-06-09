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
    public class DeleteUserMessageHandler : IMessageHandler<DeleteUserMessage>
    {
        private readonly IDAppRepository _dAppRepository;
        private readonly IDelegateRepository _delegateRepository;
        public DeleteUserMessageHandler(IDAppRepository dAppRepository, IDelegateRepository delegateRepository)
        {
            _dAppRepository = dAppRepository;
            _delegateRepository = delegateRepository;
        }
        public Task HandleMessageAsync(string messageType, DeleteUserMessage sendable)
        {
            _dAppRepository.RemoveDAppOffersWithUser(sendable.Id);
            _dAppRepository.RemoveDelegateFromAllOffersWithUser(sendable.Id);
            _delegateRepository.RemoveDelegateOffersWithUser(sendable.Id);
            return Task.CompletedTask;
        }

        public Task HandleMessageAsync(string messageType, byte[] obj)
        {
            return HandleMessageAsync(messageType, JsonSerializer.Deserialize<DeleteUserMessage>((ReadOnlySpan<byte>)obj, (JsonSerializerOptions)null));

        }


    }
}
