namespace email_test_app
{
    public static class HtmlBinder
    {
        public static string Create(string password)
        {
            string body = string.Empty;
            var templatePath = "otptemplate.html";
            using (StreamReader reader = new StreamReader(templatePath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{digit01}", password.Substring(0, 1));
            body = body.Replace("{digit02}", password.Substring(1, 1));
            body = body.Replace("{digit03}", password.Substring(2, 1));
            body = body.Replace("{digit04}", password.Substring(3, 1));
            body = body.Replace("{digit05}", password.Substring(4, 1));
            body = body.Replace("{digit06}", password.Substring(5, 1));
            body = body.Replace("{digit07}", password.Substring(6, 1));
            body = body.Replace("{digit08}", password.Substring(7, 1));
            return body;
        }
    }
}