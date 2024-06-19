using MailKit.Net.Smtp;
using MimeKit;


namespace ShopProject.Helpers.SMTPService
{
    public class SMTPController
    {
        private readonly string SenderEmailGmail = "nazar.korniychuk523@gmail.com";
        private readonly string SenderEmailUkrNet = "nazar.korniychuk@ukr.net";

        private readonly string SenderPasswordGmail = "juswaybmujtcpfuy";
        private readonly string SenderPasswordUkrNet = "MajaNVcpP96MWfyj";

        private readonly string SMTPGmail = "smtp.gmail.com";
        private readonly string SMTPUkrNet = "smtp.ukr.net";


        private readonly int[] PortGmail = { 25, 465, 587 };
        private readonly int PortUkrNet = 465;

        private MimeMessage Message;

        public SMTPController()
        {
            Message = new MimeMessage();
        }

        public void SendMessage(string emailAddres, string codeChangePassword, TypeSMPTServer typeSMPT)
        {
            Message = new MimeMessage();
            using (var smtp = new SmtpClient())
            {
                Message.To.Add(new MailboxAddress("Receiver Name", emailAddres));

                Message.Subject = "Change password";

                Message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "<b>Код підтвердження для зміни пароля " + codeChangePassword + "</b>"
                };


                switch (typeSMPT)
                {
                    case TypeSMPTServer.Gmail:
                        {
                            Message.From.Add(new MailboxAddress("ShopProject", SenderEmailGmail));

                            smtp.Connect(SMTPGmail, PortGmail[1], true);
                            smtp.Authenticate(SenderEmailGmail, SenderPasswordGmail);
                            break;
                        }
                    case TypeSMPTServer.UkrNet:
                        {
                            Message.From.Add(new MailboxAddress("ShopProject", SenderEmailUkrNet));

                            smtp.Connect(SMTPUkrNet, PortUkrNet, true);
                            smtp.Authenticate(SenderEmailUkrNet, SenderPasswordUkrNet);
                            break;
                        }
                }

                smtp.Send(Message);
                smtp.Disconnect(true);
            }

        }


    }

}