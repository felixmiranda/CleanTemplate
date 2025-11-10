using System;

namespace CleanTemplate.Application.Dtos.Customer;

public class CustomerByIdResponseDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    public int State { get; set; }
}
