﻿using System;
using System.Collections.Generic;
using GameBoard.Models.User;

namespace GameBoard.Models.Groups
{
    public class GroupViewModel
    {
        public int Id { get; }

        public string Name { get; }

        public IEnumerable<UserViewModel> Users { get; }

        public bool GroupInviteEnabled { get; }

        public int GameEventId { get; }

        public string GameEventName { get; }

        public string SubComponentName { get; }

        public Func<string, object> SubComponentArguments { get; }

        public GroupViewModel(
            int id,
            string name,
            IEnumerable<UserViewModel> users,
            bool groupInviteEnabled,
            int gameEventId,
            string gameEventName,
            string subComponentName,
            Func<string, object> subComponentArguments)
        {
            Id = id;
            Name = name;
            Users = users;
            GroupInviteEnabled = groupInviteEnabled;
            GameEventId = gameEventId;
            GameEventName = gameEventName;
            SubComponentName = subComponentName;
            SubComponentArguments = subComponentArguments;
        }

        public GroupViewModel(int id, string name, IEnumerable<UserViewModel> users)
        {
            Id = id;
            Name = name;
            Users = users;
        }
    }
}