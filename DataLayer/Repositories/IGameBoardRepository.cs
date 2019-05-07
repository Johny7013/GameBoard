﻿using System.Threading.Tasks;
using GameBoard.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameBoard.DataLayer.Repositories
{
    public interface IGameBoardRepository
    {
        DbSet<ApplicationUser> ApplicationUsers { get; }
        DbSet<Friendship> Friendships { get; }
        DbSet<Group> Groups { get; }
        DbSet<GroupUser> GroupUser { get; }

        Task SaveChangesAsync();
    }
}