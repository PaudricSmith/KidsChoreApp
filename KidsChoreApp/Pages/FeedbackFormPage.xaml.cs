

namespace KidsChoreApp.Pages
{
    public partial class FeedbackFormPage : ContentPage
    {

        private List<FileResult> _selectedFiles = new List<FileResult>();
        private List<Attachment> _attachments = new List<Attachment>();


        public FeedbackFormPage()
        {
            InitializeComponent();
        }


        private async void OnSendClicked(object sender, EventArgs e)
        {

            string subject = "KidsChoreApp Feedback";
            string body = MessageEditor.Text;
            string[] recipients = { "ooberutuber@gmail.com" };
            string[] ccRecipients = { "" }; // Copy of email to someone
            string[] bccRecipients = { "" }; // Blind copy to someone that the sender can not see

            if (string.IsNullOrWhiteSpace(body))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            var emailMessage = new EmailMessage
            {
                Subject = subject,
                Body = body,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(recipients),
                Cc = new List<string>(ccRecipients),
                Bcc = new List<string>(bccRecipients)
            };

            // Add attachments if any
            foreach (var file in _selectedFiles)
            {
                if (file != null && !string.IsNullOrEmpty(file.FullPath))
                {
                    var emailAttachment = new EmailAttachment(file.FullPath);
                    emailMessage.Attachments?.Add(emailAttachment);
                }
            }

            try
            {
                await Email.ComposeAsync(emailMessage).ConfigureAwait(false);
                await Shell.Current.GoToAsync(".."); // Clear and go back to the 'FeedbackPage'
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Error", "Email is not supported on this device.", "OK");
                Console.WriteLine(fnsEx);
                return;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An error occurred while sending your feedback. Please try again later.", "OK");
                Console.WriteLine(ex);
                return;
            }
        }


        private async void OnAttachFilesClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickMultipleAsync();
                if (result != null)
                {
                    _selectedFiles.AddRange(result);

                    _attachments = _selectedFiles.Select(f => new Attachment { FileName = f.FileName }).ToList();
                    AttachmentsCollectionView.ItemsSource = _attachments;
                    AttachmentsCollectionView.IsVisible = _attachments.Count > 0;

                    await DisplayAlert("Files Attached", $"{result.Count()} files selected.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An error occurred while picking the files. Please try again later.", "OK");
                Console.WriteLine(ex);
            }
        }
    }


    public class Attachment
    {
        public string? FileName { get; set; }
    }
}
