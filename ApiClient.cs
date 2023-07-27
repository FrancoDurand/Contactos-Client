using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactosApp {
	public class ApiClient {
		string urlBase = "http://contactosapi.somee.com/api";
		UserApiClient userApiClient;
		ContactApiClient contactApiClient;

		public ApiClient() {
			userApiClient = new UserApiClient(urlBase);
			contactApiClient = new ContactApiClient(urlBase);
		}

		public async Task<List<User>?> getUsers() {
			return await userApiClient.getUsers();
		}

		public async Task<dynamic?> login(User user) {
			return await userApiClient.login(user);
		}

		public async Task updateName(User user) {
			await userApiClient.updateName(user);
		}

		public async Task updateEmail(User user) {
			await userApiClient.updateEmail(user);
		}

		public async Task updatePassword(User user) {
			await userApiClient.updatePassword(user);
		}

		public async Task<List<Contact>> getContacts(User u) {
			return await contactApiClient.getContacts(u);
		}

		public async Task deleteContact(User u, int id) {
			await contactApiClient.deleteContact(u, id);
		}

		public async Task addContact(User u, int id) {
			await contactApiClient.addContact(u, id);
		}

		public async Task<List<Contact>> getContactsNoAdded(User user) {
			return await contactApiClient.getContactsNoAdded(user);
		}

		public async Task registerUser(User user) {
			await userApiClient.registerUser(user);
		}
	}
}
