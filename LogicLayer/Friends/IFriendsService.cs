﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GameBoard.LogicLayer.Friends.Dtos;
using GameBoard.LogicLayer.UserSearch.Dtos;
using JetBrains.Annotations;

namespace GameBoard.LogicLayer.Friends
{
    public interface IFriendsService
    {
        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<UserDto>> GetFriendsByUserNameAsync([NotNull] string userName);

        // Throws FriendRequestAlreadyPending if there exists a request between the two users
        // that was not accepted or rejected.
        Task SendFriendRequestAsync([NotNull] SendFriendRequestDto friendRequest);

        // Returns null if friend request does not exist.
        [ItemCanBeNull]
        Task<FriendRequestDto> GetFriendRequestAsync(int friendRequestId);

        // Throws FriendRequestAlreadyFinalized if the request was already accepted or rejected.
        Task AcceptFriendRequestAsync(int friendRequestId);

        // Throws FriendRequestAlreadyFinalized if the request was already accepted or rejected.
        Task RejectFriendRequestAsync(int friendRequestId);
    }
}