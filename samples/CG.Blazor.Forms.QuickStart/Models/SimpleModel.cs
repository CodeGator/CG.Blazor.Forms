using MudBlazor;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Blazor.Forms.QuickStart.Models
{
    /// <summary>
    /// This class demonstrates a simple model with a handfull of bound properties.
    /// </summary>
    public class SimpleModel
    {
        /// <summary>
        /// This property demonstrates binding a string to a mudtextfield
        /// </summary>
        [RenderMudTextField]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// This property demonstrates binding a string to a mudtextfield
        /// </summary>
        [RenderMudTextField]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// This property demonstrates binding a datetime to a muddatepicker.
        /// </summary>
        [RenderMudDatePicker]
        public DateTime? DateOfBirth { get; set; }

        public SimpleModel()
        {
            FirstName = "Code";
            LastName = "Gator";
            DateOfBirth = new DateTime(2005, 4, 21);
        }
    }
}
