using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.SpeechRecognition;
using System.Diagnostics;

namespace IoT_Mirror
{
    class SpeechManager
    {
        public delegate void Login();
        public Login Login_Start;

        public delegate void Logout();
        public Logout Logout_Start;

        public async void Init()
        {
            var speechRecognizer = new SpeechRecognizer();
            var keyWord = "" + "";
            speechRecognizer.Constraints.Add(
                new SpeechRecognitionListConstraint(new List<string>() { keyWord + "hello", keyWord + "log in", keyWord + "login" }, "login"));

            speechRecognizer.Constraints.Add(
                new SpeechRecognitionListConstraint(new List<string>() { keyWord + "logout", keyWord + "log out", keyWord + "quit" }, "logout"));

            var compilationResult = await speechRecognizer.CompileConstraintsAsync();

            speechRecognizer.ContinuousRecognitionSession.ResultGenerated +=
                ContinuousRecognitionSession_ResultGenerated;

            await speechRecognizer.ContinuousRecognitionSession.StartAsync();
        }

        private void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            if (args.Result.Constraint.Tag == "login")
                Login_Start();
            else if (args.Result.Constraint.Tag == "logout")
                Logout_Start();
        }
    }
}
