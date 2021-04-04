using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using homework23.Models;

namespace homework23.DataContext
{
    public static class DbSeed
    {
        private static QuotesDbContext _ctx;

        public static async Task Seed(QuotesDbContext context)
        {
            _ctx = context;
            if (!context.Quotes.Any())
            {
                var startupCategories = new List<Quote>
                {
                    new() {Text = "random quote 1", Author = "random guy 1"},
                    new() {Text = "random quote 2", Author = "random guy 2"},
                    new() {Text = "random quote 3", Author = "random guy 3"},
                    new() {Text = "random quote 4", Author = "random guy 4"},
                    new() {Text = "random quote 5", Author = "random guy 5"},
                };

                await context.Quotes.AddRangeAsync(startupCategories);
            }

            await context.SaveChangesAsync();

            var timer = new Timer(1_000 * 60 * 60 * 24); //1 day
            timer.Elapsed += RemoveOldQuotes;
            timer.Start();
        }

        private static void RemoveOldQuotes(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Checked");
            _ctx?.Quotes.RemoveRange(_ctx?.Quotes.ToList()
                .Where(quote => (DateTime.Now - quote.InsertDate).TotalDays > 30));
        }
    }
}