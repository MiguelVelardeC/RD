using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;


public enum TestCalendarioAdvancedFormMode
{
    Insert, Edit
}

public partial class TestCalendarioAdvancedForm : System.Web.UI.UserControl 
{
    #region Private members

        private bool FormInitialized
        {
            get
            {
                return (bool) (ViewState["FormInitialized"] ?? false);
            }

            set
            {
                ViewState["FormInitialized"] = value;
            }
        }

        private TestCalendarioAdvancedFormMode mode = TestCalendarioAdvancedFormMode.Insert;

        #endregion

        #region Protected properties

        protected RadScheduler Owner
        {
            get
            {
                return Appointment.Owner;
            }
        }

        protected Appointment Appointment
        {
            get
            {
                SchedulerFormContainer container = (SchedulerFormContainer) BindingContainer;
                return container.Appointment;
            }
        }

        #endregion

        #region Attributes and resources

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string Description
        {
            get
            {
                return DescriptionText.Text;
            }

            set
            {
                DescriptionText.Text = value;
            }
        }

        #endregion

        #region Public properties

        public TestCalendarioAdvancedFormMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public string Subject
        {
            get
            {
                return SubjectText.Text;
            }

            set
            {
                SubjectText.Text = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateButton.ValidationGroup = Owner.ValidationGroup;
            UpdateButton.CommandName = Mode == TestCalendarioAdvancedFormMode.Edit ? "Update" : "Insert";

            InitializeStrings();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!FormInitialized)
            {
                
                FormInitialized = true;
            }
        }

        protected void BasicControlsPanel_DataBinding(object sender, EventArgs e)
        {
            ;
        }

        #region Private methods

        private void InitializeStrings()
        {
            SubjectValidator.ErrorMessage = Owner.Localization.AdvancedSubjectRequired;
            SubjectValidator.ValidationGroup = Owner.ValidationGroup;
        }

        #endregion
}
