using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Extensions
{
    public static class ClubExtensions
    {
        public static ClubDto MapToClubDto(this Club club)
        {
            return new ClubDto
            {
                Id = club.Id,
                Name = club.Name,
                City = club.City,
                Postcode = club.Postcode
            };
        }
    }
}