using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Extensions
{
    public static class SportExtensions
    {
        public static SportDto MapToSportDto(this Sport sport)
        {
            return new SportDto
            {
                Id = sport.Id,
                Name = sport.Name
            };
        }

        public static void WrapIn(this SportDto dto, Sport sport)
        {
            sport.Name = dto.Name;
        }
    }
}
