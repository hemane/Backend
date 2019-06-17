﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Service
{
    public interface ISportService
    {
        Task<IEnumerable<SportDto>> GetAsync();
        Task<SportDto> GetAsync(int id);
        Task SaveAsync(SportDto sport);
        Task DeleteAsync(int id);
    }
}