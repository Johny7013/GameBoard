﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameBoard.DataLayer.Repositories;
using GameBoard.LogicLayer.GameEvents.Dtos;
using JetBrains.Annotations;

namespace GameBoard.LogicLayer.GameEvents
{
    class GameEventService : IGameEventService
    {
        private readonly IGameBoardRepository _repository;

        public GameEventService(IGameBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateGameEventAsync(CreateGameEventDto requestedGameEvent, IEnumerable<string> games) => throw new NotImplementedException();

        public async Task DeleteGameEventAsync(string gameEventId) => throw new NotImplementedException();

        public async Task EditGameEventAsync(EditGameEventDto editedEvent, IEnumerable<string> newGames) => throw new NotImplementedException();

        public async Task<GameEventDto> GetGameEventAsync(GameEventListItemDto gameEventListItemDto) => throw new NotImplementedException();

        public Task<IEnumerable<GameEventListDto>> GetAccessibleGameEventsAsync([NotNull] string userId) => throw new NotImplementedException();

        public Task<GameEventPermission> GetGameEventPermissionByUserAsync([NotNull] string gameEventId, [NotNull] string userId) => throw new NotImplementedException();

        public async Task SendGameEventInvitationAsync(string gameEventId, string userId) => throw new NotImplementedException();

        public async Task AcceptGameEventInvitationAsync(string gameEventId) => throw new NotImplementedException();

        public async Task RejectGameEventInvitationAsync(string gameEventId) => throw new NotImplementedException();

    }
}