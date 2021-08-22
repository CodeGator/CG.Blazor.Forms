using CG.Blazor.Forms.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CG.Blazor.Forms.QuickStart.ViewModels
{
    /// <summary>
    /// This class is a view-model for rendering HTML elements.
    /// </summary>
    [RenderValidationSummary()]
    [RenderDataAnnotationsValidator]
    public class HtmlVM
    {
        [RenderInputText]
        [Required]
        public string A1 { get; set; } = "A1 value";

        [RenderInputText(Options = "A,B,C,D")]
        public string A2 { get; set; } = "A";

        [RenderRadioGroup(Options = "A,B,C,D")]
        public string A3 { get; set; } = "B";

        [RenderSelect(Options = "A,B,C,D")]
        public string A4 { get; set; } = "C";
    }
}
