﻿using System.ComponentModel.DataAnnotations;

namespace API_asp_start_project.Domain.Dtos
{
    public class CreateOwnerDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage= "Name can't be longer than 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        public string? Address;
    }
}
