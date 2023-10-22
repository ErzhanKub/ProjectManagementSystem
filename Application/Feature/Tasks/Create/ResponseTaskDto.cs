﻿using Domain.Enums;

namespace Application.Feature.Tasks.Create
{
    public record ResponseTaskDto
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public string? Comment { get; init; }
        public StatusTask Status { get; init; }
        public int Priority { get; init; }
        public Guid AuthorId { get; init; }
    }
}
