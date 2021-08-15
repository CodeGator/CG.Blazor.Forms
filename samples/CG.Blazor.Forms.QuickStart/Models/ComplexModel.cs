using MudBlazor.Utilities;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CG.Blazor.Forms.Attributes;

namespace CG.Blazor.Forms.QuickStart.Models
{

    // *******************************************************************

    /// <summary>
    /// This class demonstrates a complex model with properties that are
    /// themselves models with properties that are themselves models, etc.
    /// </summary>
    public class ComplexModel
    {
        /// <summary>
        /// To demonstrate a child model that will be rendered.
        /// </summary>
        [RenderObject]
        public PartAModel PartAModel { get; set; }
        
        public ComplexModel()
        {
            PartAModel = new PartAModel();
        }

        /// <summary>
        /// Just to demonstrate that the VM can have a property with an
        /// associated search function too.
        /// </summary>
        [RenderMudAutocomplete(SearchFunc = "Search1", Label = "Auto Complete")]
        public string Z { get; set; }

        /// <summary>
        /// The generation process ignores this because its not decorated.
        /// </summary>
        public string[] _blah = new string[] { "A", "B", "C", "D", "E", "F" };

        /// <summary>
        /// This method is called wire up to the rendered autocomplete control
        /// by the form generator, and will be called, dynamically, at runtime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> Search1(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // If text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _blah;
            // Otherwise, show the filter results.
            return _blah.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    // *******************************************************************

    /// <summary>
    /// This model highlights all the ways string properties can be 
    /// rendered when decorated with an appropriate attribute.
    /// </summary>
    public class PartAModel
    {
        /// <summary>
        /// To demonstrate a string property bound to a text field.
        /// </summary>
        [RenderMudTextField(Label = "PartA:A")]
        [Required]
        public string A { get; set; }

        /// <summary>
        /// To demonstrate a string property bound to a radio group.
        /// </summary>
        [RenderMudRadioGroup(Options = "A, B, C")]
        public string B { get; set; }

        /// <summary>
        /// To demonstrate a string property bound to an auto complete field.
        /// </summary>
        [RenderMudAutocomplete(SearchFunc = "Search1", Label = "PartA:C")]
        [Required]
        public string C { get; set; }

        /// <summary>
        /// To demonstrate a child model that will be rendered.
        /// </summary>
        [RenderObject]
        public PartBModel PartBModel { get; set; }

        public PartAModel()
        {
            PartBModel = new PartBModel();
            A = "1";
            B = "2";
            C = "3";
        }
    }

    // *******************************************************************

    /// <summary>
    /// This model highlights all the ways numeric properties can be 
    /// rendered when decorated with the appropriate attribute.
    /// </summary>
    public class PartBModel
    {
        /// <summary>
        /// To demonstrate an int property bound to a numeric field.
        /// </summary>
        [RenderMudNumericField(Label = "PartB:D")]
        public int D { get; set; }

        /// <summary>
        /// To demonstrate an long property bound to a numeric field.
        /// </summary>
        [RenderMudNumericField(Label = "PartB:E")]
        public long E { get; set; }

        /// <summary>
        /// To demonstrate an decimal property bound to a numeric field.
        /// </summary>
        [RenderMudNumericField(Label = "PartB:F")]
        public decimal F { get; set; }

        /// <summary>
        /// To demonstrate a float property bound to a numeric field.
        /// </summary>
        [RenderMudNumericField(Label = "PartB:G")]
        public float G { get; set; }

        /// <summary>
        /// To demonstrate a double property bound to a numeric field.
        /// </summary>
        [RenderMudNumericField(Label = "PartB:H")]
        public double H { get; set; }

        /// <summary>
        /// To demonstrate a byte property bound to a numeric field.
        /// </summary>
        [RenderMudNumericField(Label = "PartB:I")]
        public byte I { get; set; }

        /// <summary>
        /// To demonstrate a child model that will be rendered.
        /// </summary>
        //[RenderMudPaper()]
        [RenderMuddyGroupBox()]
        public PartCModel PartCModel { get; set; }

        public PartBModel()
        {
            PartCModel = new PartCModel();
            D = 4;
            E = 5;
            F = 6;
            G = 7;
            H = 8;
            I = 9;
        }
    }

    // *******************************************************************

    /// <summary>
    /// This model highlights all the ways specific object types can be 
    /// rendered when decorated with the appropriate attribute.
    /// </summary>
    public class PartCModel
    {
        /// <summary>
        /// To demonstrate a datetime property bound to a date picker.
        /// </summary>
        [RenderMudDatePicker(Label = "PartC:J")]
        public DateTime J { get; set; }

        /// <summary>
        /// To demonstrate a timespan property bound to a time picker.
        /// </summary>
        [RenderMudTimePicker(Label = "PartC:K")]
        public TimeSpan K { get; set; }

        /// <summary>
        /// To demonstrate a bool property bound to a switch.
        /// </summary>
        [RenderMudSwitch(Label = "PartC:L")]
        public bool L { get; set; }

        /// <summary>
        /// To demonstrate a bool property bound to a checkbox.
        /// </summary>
        [RenderMudCheckBox(Label = "PartC:M")]
        public bool M { get; set; }

        /// <summary>
        /// To demonstrate a mudcolor property bound to a color picker.
        /// </summary>
        [RenderMudColorPicker(Label = "PartC:N")]
        public MudColor N { get; set; }

        public PartCModel()
        {
            J = DateTime.Now;
            K = TimeSpan.FromMinutes(1);
            L = true;
            M = true;
            N = new MudColor("#FAFBFC");
        }
    }

}
