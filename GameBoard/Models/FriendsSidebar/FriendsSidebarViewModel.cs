using System;
using System.Collections.Generic;
using GameBoard.Models.Groups;

namespace GameBoard.Models.FriendsSidebar
{
    public class FriendsSidebarViewModel
    {
        public IEnumerable<GroupViewModel> Groups { get; }

        public bool Toggled { get; }

        public bool GroupInviteEnabled { get; }

        public int GameEventId { get; }

        public string GameEventName { get; }

        public int GroupId { get; }

        public string SubComponentName { get; }

        public Func<string, int, object> SubComponentArguments { get; }

        public FriendsSidebarViewModel(
            IEnumerable<GroupViewModel> groups,
            bool toggled,
            bool groupInviteEnabled,
            int gameEventId,
            string gameEventName,
            int groupId,
            string subComponentName,
            Func<string, int, object> subComponentArguments)
        {
            Groups = groups;
            Toggled = toggled;
            GroupInviteEnabled = groupInviteEnabled;
            GameEventId = gameEventId;
            GameEventName = gameEventName;
            GroupId = groupId;
            SubComponentName = subComponentName;
            SubComponentArguments = subComponentArguments;
        }
    }
}

