using Newtonsoft.Json;
using System.Text;

namespace ContactosApp {
	public class UserApiClient {
		private readonly HttpClient httpClient;
		private readonly string url = "user";

		public UserApiClient(string urlBase) {
			httpClient = new HttpClient();
			url = $"{urlBase}/{url}";
		}

		private string buildUrl(string endpoint) {
			return url + $"/{endpoint}";
		}

		public async Task<List<User>?> getUsers() {
			try {
				HttpResponseMessage response = await httpClient.GetAsync(url);
				
				response.EnsureSuccessStatusCode();

				string jsonResponse = await response.Content.ReadAsStringAsync();
				List<User>? result = JsonConvert.DeserializeObject<List<User>>(jsonResponse);
				
				return result;
			}
			catch (Exception ex) {
				throw new Exception("Error Get: " + ex.Message);
			}
		}

		public async Task<dynamic?> login(User user) {
			try {
				string url = buildUrl("login");
				string jsonData = JsonConvert.SerializeObject(user);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				response.EnsureSuccessStatusCode();

				string jsonResponse = await response.Content.ReadAsStringAsync();

				dynamic? result = JsonConvert.DeserializeAnonymousType(jsonResponse, new { id = 0, name = "" });
				
				return result;
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}

		public async Task updateName(User user) {
			try {
				string url = buildUrl("updateName");
				string jsonData = JsonConvert.SerializeObject(user);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PutAsync(url, content);

				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}

		public async Task updateEmail(User user) {
			try {
				string url = buildUrl("updateEmail");
				string jsonData = JsonConvert.SerializeObject(user);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PutAsync(url, content);

				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}

		public async Task updatePassword(User user) {
			try {
				string url = buildUrl("updatePass");
				string jsonData = JsonConvert.SerializeObject(user);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PutAsync(url, content);

				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}

		public async Task registerUser(User user) {
			try {
				string url = buildUrl("register");
				string jsonData = JsonConvert.SerializeObject(user);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}
	}
}
