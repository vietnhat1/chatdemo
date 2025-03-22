using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace chatdemo
{
    public partial class Chatbot : System.Web.UI.Page
    {
        // Thay thế bằng API Key của bạn
        private const string OPENAI_API_KEY = "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";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();

            if (!string.IsNullOrEmpty(userInput))
            {
                string response = await GetChatbotResponse(userInput);
                lblResponse.Text = response;
            }
            else
            {
                lblResponse.Text = "Vui lòng nhập câu hỏi trước khi gửi.";
            }
        }

        private async Task<string> GetChatbotResponse(string userMessage)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OPENAI_API_KEY);

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                new { role = "system", content = "Bạn là một trợ lý thông minh." },
                new { role = "user", content = userMessage }
            },
                    max_tokens = 200
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Response from API: " + responseString); // Kiểm tra phản hồi API

                dynamic result = JsonConvert.DeserializeObject(responseString);

                // Kiểm tra nếu kết quả không hợp lệ
                if (result == null || result.error != null)
                {
                    return $"Lỗi từ API: {result?.error?.message ?? "Không thể kết nối tới máy chủ."}";
                }

                if (result?.choices == null || result.choices.Count == 0)
                {
                    return "Không nhận được phản hồi từ chatbot. Vui lòng thử lại.";
                }

                return result.choices[0].message.content.ToString().Trim();
            }
        }

    }
}
