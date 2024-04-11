using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskAppMono.Models;

public partial class TaskItem
{
    public int TaskItemId { get; set; }
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string UserEmail { get; set; }

    public required string Description { get; set; }

    public string? Details { get; set; }

    public required DateTime DueDate { get; set; }

    public required string FrequencyType { get; set; }

    public int? FrequencyNumber { get; set; }

    public required bool Sensative { get; set; }

    public DateTime? LastCompleted { get; set; }
}
