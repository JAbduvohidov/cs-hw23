using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace homework23.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [MinLength(5)] [NotNull] public string Text { get; set; }
        [MinLength(2)] public string? Author { get; set; }
        [NotNull] public DateTime InsertDate { get; set; } = DateTime.Now;
    }
}