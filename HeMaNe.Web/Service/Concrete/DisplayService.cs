using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HeMaNe.Web.Service.Concrete
{
    public class DisplayService : IDisplayService
    {
        private readonly HemaneContext _context;

        public DisplayService(HemaneContext context)
        {
            _context = context;
        }

        public async Task<DisplayDto> DisplayLeaguesAsync()
        {
            var days = new Dictionary<DateTime, List<Day>>();
            foreach (var day in await this._context.Days.ToListAsync())
            {
                var date = day.DateTimeOffset.Date;
                if (!days.ContainsKey(date))
                {
                    days.Add(date, new List<Day>());
                }

                days[date].Add(day);
            }

            var display = new DisplayDto
            {
                Days = days.OrderBy(d => d.Key).Select(e =>
                {
                    var (key, value) = e;
                    var sports = new Dictionary<Sport, Dictionary<League, List<Match>>>();
                    foreach (var day in value)
                    {
                        var sport = day.League.Sport;
                        var league = day.League;

                        if (!sports.ContainsKey(sport))
                        {
                            sports.Add(sport, new Dictionary<League, List<Match>>());
                        }

                        if (!sports[sport].ContainsKey(league))
                        {
                            sports[sport].Add(league, new List<Match>());
                        }
                        
                        sports[sport][league].AddRange(day.Matches);
                    }

                    return new DisplayDayDto
                    {
                        Date = key,
                        Sports = sports.Select(s => new DisplaySportDto
                        {
                            Id = s.Key.Id,
                            Name = s.Key.Name,
                            Leagues = s.Value.Select(l => new DisplayLeagueDto
                            {
                                Id = l.Key.Id,
                                DayId = value.FirstOrDefault(d => d.League.Id == l.Key.Id)?.Id ?? 0,
                                Name = l.Key.Name,
                                Matches = l.Value.Select(m => new DisplayMatchDto
                                {
                                    Id = m.Id,
                                    Teams = m.Teams.Select(t => new DisplayTeamDto
                                    {
                                        Id = t.Team.Id,
                                        Name = t.Team.Name,
                                        Club = t.Team.Club.Name,
                                        Score = t.Score
                                    })
                                })
                            })
                        })
                    };
                })
            };


            return display;
        }
    }
}
