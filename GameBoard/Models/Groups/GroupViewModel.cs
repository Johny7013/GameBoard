﻿using GameBoard.Models.User;
using System;
using System.Collections.Generic;

namespace GameBoard.Models.Groups
{
    public class GroupViewModel
    {
        public int GroupId { get; }

        public string GroupName { get; }

        public IEnumerable<UserViewModel> Users { get; }

        public bool GroupInviteEnabled { get; }

        public int GameEventId { get; }

        public string GameEventName { get; }

        public string SubComponentName { get; }

        public Func<string, int, object> SubComponentArguments { get; }

        public GroupViewModel(int groupId, string groupName, IEnumerable<UserViewModel> users, bool groupInviteEnabled, int gameEventId, 
            string gameEventName,
            string subComponentName,
            Func<string, int, object> subComponentArguments)
        {
            GroupId = groupId;
            GroupName = groupName;
            Users = users;
            GroupInviteEnabled = groupInviteEnabled;
            GameEventId = gameEventId;
            GameEventName = gameEventName;
            SubComponentName = subComponentName;
            SubComponentArguments = subComponentArguments;
        }

        public GroupViewModel(int groupId, string groupName, IEnumerable<UserViewModel> users)
        {
            GroupId = groupId;
            GroupName = groupName;
            Users = users;
        }
    }
}
