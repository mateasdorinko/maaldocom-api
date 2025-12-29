using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Dtos;

public class KnowledgeDto : BaseDto
{
    public string? Title { get; set; }
    public string? Quote { get; set; }
}