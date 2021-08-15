using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using System;

namespace CG.Blazor.Forms.Components
{
    /// <summary>
    /// This class contains the code-behind for the <see cref="MuddyGroupBox"/>
    /// component.
    /// </summary>
    public partial class MuddyGroupBox : MudComponentBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the class name for the component.
        /// </summary>
        protected string Classname => new CssBuilder("mud-group-box")
            .AddClass(Class)
            .Build();

        /// <summary>
        /// This parameter contains the child content of the component.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// This parameter indicates the elevation for the component. The higher 
        /// the number, the heavier the drop-shadow. 0 for no shadow.
        /// </summary>
        [Parameter] 
        public int Elevation { set; get; }

        /// <summary>
        /// This parameter contains the label for the component.
        /// </summary>
        [Parameter]
        public string Label { get; set; }

        /// <summary>
        /// This parmeter contains the color for the label.
        /// </summary>
        [Parameter]
        public Color LabelColor { get; set; }

        /// <summary>
        /// This parmeter contains the typography for the label.
        /// </summary>
        [Parameter]
        public Typo LabelTypo { get; set; }

        /// <summary>
        /// This parameter, if true, indicates the group box will be outlined.
        /// </summary>
        [Parameter] 
        public bool Outlined { get; set; }

        /// <summary>
        /// This property, if true, indicates the border-radius will be set to 0.
        /// </summary>
        [Parameter]
        public bool Square { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="MuddyGroupBox"/>
        /// class.
        /// </summary>
        public MuddyGroupBox()
        {
            // Set default values
            Elevation = 1;
            Label = string.Empty;
            LabelColor = Color.Default;
            LabelTypo = Typo.h6;
            Outlined = false;
            Square = false;
        }

        #endregion
    }
}
