using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Artexacta.App.Utilities.Email;

public partial class Test_TestMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ResultsLabel.Text = "";
    }

    protected void SendMailButton_Click(object sender, EventArgs e)
    {
        try
        {
            string from = ConfigurationManager.AppSettings.Get("AdminEmailAddress");

            System.Net.Mail.MailMessage emailMessage = new System.Net.Mail.MailMessage();
            emailMessage.From = new System.Net.Mail.MailAddress(from, Artexacta.App.Configuration.Configuration.GetReturnEmailName());
            emailMessage.IsBodyHtml = true;
            emailMessage.Subject = "Test Message";
            emailMessage.To.Add(AddressTextBox.Text);

            //Deal with accents in subject
            emailMessage.Subject = HttpUtility.HtmlDecode(emailMessage.Subject);

            //Normalizing body in UTF8 encode
            emailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            emailMessage.Body = emailMessage.Body.Normalize();

            //Sending Email
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Send(emailMessage);

            ResultsLabel.Text = "TEST OK";
        }
        catch (Exception q)
        {
            ResultsLabel.Text = "Error sending email: " + q.Message;
            if (q.InnerException != null)
            {
                ResultsLabel.Text = ResultsLabel.Text + " - " + q.InnerException.Message;
            }
        }
    }    
}
