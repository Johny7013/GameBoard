﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GameBoard.Configuration;
using GameBoard.Errors;
using GameBoard.LogicLayer.GameEventParticipations;
using GameBoard.LogicLayer.GameEventParticipations.Dtos;
using GameBoard.LogicLayer.GameEvents;
using GameBoard.LogicLayer.GameEvents.Dtos;
using GameBoard.LogicLayer.Groups.Dtos;
using GameBoard.Models.GameEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GameBoard.Controllers
{
    [Authorize]
    public class GameEventParticipation : Controller
    {
        private readonly IGameEventParticipationService _gameEventParticipationService;
        private readonly IGameEventService _gameEventService;
        private readonly HostConfiguration _hostConfiguration;

        public GameEventParticipation(
            IGameEventParticipationService gameEventParticipationService,
            IGameEventService gameEventService,
            IOptions<HostConfiguration> hostConfiguration)
        {
            _gameEventParticipationService = gameEventParticipationService;
            _gameEventService = gameEventService;
            _hostConfiguration = hostConfiguration.Value;
        }

        [HttpGet]
        public async Task<IActionResult> ExitGameEvent(int id)
        {
            var gameEvent = await _gameEventService.GetGameEventAsync(id);

            return View("ExitGameEvent", gameEvent.ToExitViewModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ExitGameEvent(ExitGameEventViewModel exitGameEventViewModel)
        {
            await _gameEventParticipationService.ExitGameEventAsync(exitGameEventViewModel.Id, User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AcceptGameEventInvite(int gameEventId)
        {
            await _gameEventParticipationService.AcceptGameEventInvitationAsync(gameEventId, User.Identity.Name);

            return RedirectToAction("GameEvent", "GameEvent", new {id = gameEventId});
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RejectGameEventInvite(int gameEventId)
        {
            await _gameEventParticipationService.RejectGameEventInvitationAsync(gameEventId, User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SendGameEventInvite(int gameEventId, string userName)
        {
            GameEventDto gameEvent;

            var sendGameEventInvitationDto = new SendGameEventInvitationDto(
                gameEventId,
                userName,
                eventId => _hostConfiguration.HostAddress + Url.Action(
                    "GameEvent",
                    "gameEvent",
                    new {id = eventId}));

            try
            {
                gameEvent = await _gameEventService.GetGameEventAsync(gameEventId);
            }
            catch (ApplicationException exception)
            {
                return Error.FromController(this).ErrorJson("Error!", exception.Message, HttpStatusCode.BadRequest);
            }
            catch
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "An unexpected error has occured while processing your request.",
                    HttpStatusCode.InternalServerError);
            }

            if (gameEvent == null)
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "Specified game event does not exist.",
                    HttpStatusCode.NotFound);
            }

            if (gameEvent.Creator.UserName != User.Identity.Name)
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "You're unauthorized to perform this action.",
                    HttpStatusCode.Unauthorized);
            }

            return await SendGameEventInvitation(gameEvent, userName, sendGameEventInvitationDto);
        }

        private async Task<IActionResult> SendGameEventInvitation(
            GameEventDto gameEvent,
            string userName,
            SendGameEventInvitationDto sendGameEventInvitationDto)
        {
            try
            {
                await _gameEventParticipationService.SendGameEventInvitationAsync(sendGameEventInvitationDto);
            }
            catch (ApplicationException exception)
            {
                return Error.FromController(this).ErrorJson("Error!", exception.Message, HttpStatusCode.BadRequest);
            }
            catch
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "An unexpected error has occured while processing your request.",
                    HttpStatusCode.InternalServerError);
            }

            return Ok(
                new
                {
                    title = "Invite sent.",
                    message =
                        $"An email with your invitation to the {gameEvent.Name} event has been sent to {userName}."
                });
        }

        //group Invite

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SendGameEventInviteToGroup(int gameEventId, GroupDto group)
        {
            GameEventDto gameEvent;

            var sendGameEventInvitationDtos = group.Users.Select(
                u => new SendGameEventInvitationDto(
                    gameEventId,
                    u.UserName,
                    eventId => _hostConfiguration.HostAddress + Url.Action(
                        "GameEvent",
                        "gameEvent",
                        new { id = eventId })));
            try
            {
                gameEvent = await _gameEventService.GetGameEventAsync(gameEventId);
            }
            catch (ApplicationException exception)
            {
                return Error.FromController(this).ErrorJson("Error!", exception.Message, HttpStatusCode.BadRequest);
            }
            catch
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "An unexpected error has occured while processing your request.",
                    HttpStatusCode.InternalServerError);
            }

            if (gameEvent == null)
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "Specified game event does not exist.",
                    HttpStatusCode.NotFound);
            }

            if (gameEvent.Creator.UserName != User.Identity.Name)
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "You're unauthorized to perform this action.",
                    HttpStatusCode.Unauthorized);
            }

            return await SendGameEventInvitationToGroup(gameEvent, group.GroupName, sendGameEventInvitationDtos);
        }

        private async Task<IActionResult> SendGameEventInvitationToGroup(
            GameEventDto gameEvent,
            string groupName,
            IEnumerable<SendGameEventInvitationDto> sendGameEventInvitationDtos)
        {
            try
            {
                await _gameEventParticipationService.SendGameEventInvitationAsync(sendGameEventInvitationDtos);
            }
            catch (ApplicationException exception)
            {
                return Error.FromController(this).ErrorJson("Error!", exception.Message, HttpStatusCode.BadRequest);
            }
            catch
            {
                return Error.FromController(this).ErrorJson(
                    "Error!",
                    "An unexpected error has occured while processing your request.",
                    HttpStatusCode.InternalServerError);
            }

            return Ok(
                new
                {
                    title = "Invite sent.",
                    message =
                        $"An email with your invitation to the {gameEvent.Name} event has been sent to {groupName}."
                });
        }
    }
}