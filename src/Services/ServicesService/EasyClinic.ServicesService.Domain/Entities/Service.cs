using EasyClinic.ServicesService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ServicesService.Domain.Entities;

/// <summary>
/// Medical Service model class
/// </summary>
public class Service
{
    /// <summary>
    /// Service unique Id
    /// </summary>
    public Guid Id { get;set; }

    /// <summary>
    /// Service Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Optional Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Full price
    /// </summary>
    public decimal Price { get; set; }

    // Status
    /// <summary>
    /// Status: Active or Inactive
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Service category
    /// </summary>
    public ServiceCategory Category { get; set; } = null!;
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Service Specialization
    /// </summary>
    public Specialization Specialization { get; set; } = null!;
    public Guid SpecializationId { get; set; }

}
