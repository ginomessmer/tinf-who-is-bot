using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LecturerLookup.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LecturerLookup.DiscordBot
{
    public class CalculateScoreWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CalculateScoreWorker> _logger;

        public CalculateScoreWorker(IServiceProvider serviceProvider, ILogger<CalculateScoreWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                await using var dbContext = scope.ServiceProvider.GetRequiredService<WhoIsDbContext>();
                
                var allTeachers = await dbContext.Teachers
                    .Include(x => x.Tags)
                    .ThenInclude(x => x.Votes)
                    .ToListAsync(stoppingToken);

                foreach (var tag in allTeachers.SelectMany(teacher => teacher.Tags))
                {
                    tag.Evaluation.TotalUpVotes = tag.Votes.Count(x => x.Score > 0);
                    tag.Evaluation.TotalDownVotes = tag.Votes.Count(x => x.Score < 0);
                    tag.Evaluation.TotalVotes = tag.Evaluation.TotalUpVotes + tag.Evaluation.TotalDownVotes;
                    tag.Evaluation.CalculatedScore = tag.Evaluation.TotalUpVotes / tag.Evaluation.TotalVotes;
                }

                await dbContext.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Updated all scores");

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}