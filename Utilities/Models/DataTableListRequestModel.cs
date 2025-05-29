using System.ComponentModel.DataAnnotations;

namespace Utilities.Models;

public class DataTableRequestModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    [MaxLength(255)]
    public string? SearchTerm { get; set; } 
    public int SortColumn { get; set; }
    [MaxLength(5)]
    public string SortDirection { get; set; } = "asc";
}