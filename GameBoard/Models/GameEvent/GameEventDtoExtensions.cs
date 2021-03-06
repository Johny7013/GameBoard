﻿using System.Linq;
using GameBoard.LogicLayer.GameEvents.Dtos;
using GameBoard.Models.DescriptionTab;
using GameBoard.Models.User;

namespace GameBoard.Models.GameEvent
{
    internal static class GameEventDtoExtensions
    {
        public static GameEventViewModel ToViewModel(this GameEventDto gameEventDto, GameEventPermission permission) =>
            new GameEventViewModel(
                gameEventDto.Id,
                gameEventDto.Name,
                gameEventDto.Place,
                gameEventDto.Date,
                permission,
                gameEventDto.Games,
                gameEventDto.Creator.ToViewModel(),
                gameEventDto.Invitees.Select(u => u.ToViewModel()),
                gameEventDto.Participants.Select(u => u.ToViewModel()),
                gameEventDto.DescriptionTab.ToViewModel());

        public static EditGameEventViewModel ToEditViewModel(this GameEventDto gameEventDto) =>
            new EditGameEventViewModel
            {
                Id = gameEventDto.Id,
                Name = gameEventDto.Name,
                Date = gameEventDto.Date,
                Place = gameEventDto.Place,
                Games = string.Join('\n', gameEventDto.Games)
            };

        public static ExitGameEventViewModel ToExitViewModel(this GameEventDto gameEventDto) =>
            new ExitGameEventViewModel
            {
                Id = gameEventDto.Id,
                Name = gameEventDto.Name
            };

        public static RemoveFromGameEventViewModel ToRemoveFromGameEventViewModel(
            this GameEventDto gameEventDto,
            string userName) =>
            new RemoveFromGameEventViewModel
            {
                Id = gameEventDto.Id,
                Name = gameEventDto.Name,
                UserName = userName
            };
    }
}