using Newtonsoft.Json;
using System.Text;

namespace ContactosApp {
	public class ContactApiClient {
		private readonly HttpClient httpClient;
		private readonly string url = "contact";

		public ContactApiClient(string urlBase) {
			httpClient = new HttpClient();
			url = $"{urlBase}/{url}";
		}

		private string buildUrl(string endpoint) {
			return url + $"/{endpoint}";
		}

		public async Task<List<Contact>> getContacts(User user) {
			try {
				string jsonData = JsonConvert.SerializeObject(user);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				response.EnsureSuccessStatusCode();

				string jsonResponse = await response.Content.ReadAsStringAsync();

				List<Contact> result = JsonConvert.DeserializeObject<List<Contact>>(jsonResponse);

				return result;
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}

		public async Task deleteContact(User user, int id) {
			try {
				string url = buildUrl("delete");

				var requestData = new {
					userId = user.id,
					contactId = id
				};

				string jsonData = JsonConvert.SerializeObject(requestData);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}

		public async Task addContact(User user, int id) {
			try {
				string url = buildUrl("add");

				var requestData = new {
					userId = user.id,
					contactId = id
				};

				string jsonData = JsonConvert.SerializeObject(requestData);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}
		}

		public async Task<List<Contact>> getContactsNoAdded(User user) {
			try {
				string url = buildUrl("noAdded");

				var requestData = new {
					userId = user.id
				};

				string jsonData = JsonConvert.SerializeObject(requestData);

				HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				response.EnsureSuccessStatusCode();

				string jsonResponse = await response.Content.ReadAsStringAsync();

				List<Contact> result = JsonConvert.DeserializeObject<List<Contact>>(jsonResponse);

				return result;
			}
			catch (Exception ex) {
				throw new Exception("Error POST: " + ex.Message);
			}


		}
	}
}
