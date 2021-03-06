﻿using System;
using System.Threading;
using System.Threading.Tasks;
using GameBoard.Configuration;
using GameBoard.LogicLayer.GameEventReminders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GameBoard.HostedServices.EventReminders
{
    internal sealed class DayBeforeGameEventReminderCronScheduledService : CronScheduledService
    {
        private const string CronSchedule = "0 18 * * *";   // 6pm server time is 8pm local time.

        public DayBeforeGameEventReminderCronScheduledService(IServiceScopeFactory serviceScopeFactory) : base(
            serviceScopeFactory,
            CronSchedule)
        {
        }

        protected override Task ExecuteCronJobAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var hostConfiguration = serviceProvider.GetService<IOptions<HostConfiguration>>().Value;
            var eventReminderService = serviceProvider.GetService<IGameEventReminderService>();
            var tomorrow = DateTime.Today.AddDays(1);
            // This link generation is less than ideal, but I have no other ideas at the moment.
            return eventReminderService.SendRemindersIfDateBetweenAsync(
                tomorrow,
                tomorrow.AddDays(1),
                id => hostConfiguration.HostAddress + $"/GameEvent/GameEvent/{id}");
        }
    }
}