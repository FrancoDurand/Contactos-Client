using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactosApp {
	public class Menu {
		ApiClient apiClient;
		User user;
		List<Contact> contacts;
		
		public Menu() { 
			apiClient = new ApiClient();
			user = new User();
			contacts = new List<Contact>();
		}

		public async Task menu() {
			int opc;

			do {
				Console.Clear();
				Console.WriteLine("Bienvenido a la app de contactos");
				Console.WriteLine("1.Registrarse");
				Console.WriteLine("2.Iniciar sesión");
				Console.WriteLine("0.Salir");

				opc = int.Parse(Console.ReadLine());

				switch (opc) {
					case 1:
						await menuRegister();
						break;

					case 2:
						await menuLogin();
						break;

					default:
						break;
				}
			} while (opc != 0);
		}

		private async Task menuRegister() {
			string name, email, password;

			Console.Clear();
			Console.WriteLine("REGISTRO");

			do {
				Console.Write("Nombre: ");
				name = Console.ReadLine();
			} while (name == "");

			do {
				Console.Write("Email: ");
				email = Console.ReadLine();
			} while (email == "");

			do {
				Console.Write("Password: ");
				password = Console.ReadLine();
			} while (password == "");

			user.name = name;
			user.email = email;
			user.password = password;

			await apiClient.registerUser(user);

			Console.WriteLine("Registro exitoso");
			Console.WriteLine("Presione cualquier tecla para continuar...");
			Console.ReadKey();
		}

		private async Task menuLogin() {
			do {
				Console.Clear();
				
				Console.Write("Ingrese su email: ");
				user.email = Console.ReadLine();

				Console.Write("Ingrese su password: ");
				user.password = Console.ReadLine();

				try {
					dynamic response = await apiClient.login(user);
					
					if (response.id != null) {
						user.id = response.id;
						user.name = response.name;
						break;
					}
					else {
						// Si el inicio de sesión no fue exitoso, mostramos un mensaje y volvemos a repetir el bucle.
						Console.WriteLine("Email o password incorrecto. Intente nuevamente.");
						Console.WriteLine("Presione cualquier tecla para continuar...");
						Console.ReadKey();
					}
				}
				catch (Exception ex) {
					// Manejo de excepciones
					Console.WriteLine("Error en el inicio de sesión: " + ex.Message);
					Console.WriteLine("Presione cualquier tecla para continuar...");
					Console.ReadKey();
				}

			} while (true);

			

			Console.WriteLine("Sesión iniciada");
			Console.WriteLine("Presione cualquier tecla para continuar..."); 
			Console.ReadKey();

			await menuUser();
		}

		private async Task menuUser() {
			int opc;

			await getContacts();

			do {
				Console.Clear();
				Console.WriteLine($"Bienvenido {user.name}");
				Console.WriteLine("1.Ver mis contactos");
				Console.WriteLine("2.Actualizar datos");
				Console.WriteLine("3.Añadir contacto");
				Console.WriteLine("4.Eliminar contacto");
				Console.WriteLine("0.Cerrar sesión");
				opc = int.Parse(Console.ReadLine());

				switch (opc) {
					case 1:
						showContatcs();
						break;

					case 2:
						await updateData();
						break;

					case 3:
						await addContact();
						break;

					case 4:
						await deleteContact();
						break;

					default:
						break;
				}

				if (opc != 0)
					Console.ReadKey();
			} while (opc != 0);

			user = new User();
		}

		private async Task getContacts() {
			try {
				contacts = await apiClient.getContacts(user);
			}
			catch (Exception ex) { Console.WriteLine(ex.Message); }
		}

		private void showContatcs() {
			Console.Clear();

			foreach (Contact c in contacts) {
				Console.WriteLine($"ID: {c.id}");
				Console.WriteLine($"Nombre: {c.name}");
				Console.WriteLine($"Email: {c.email}\n");
			}
		}

		private async Task updateData() {
			int opc;

			do {
				Console.Clear();
				Console.WriteLine("1.Nombre");
				Console.WriteLine("2.Email");
				Console.WriteLine("3.Password");
				opc = int.Parse(Console.ReadLine());
			} while (opc < 1 && opc > 3);

			switch (opc) {
				case 1:
					await updateName();
					break;

				case 2:
					await updateEmail();
					break;

				case 3:
					await updatePassword();
					break;
			}

		}
		
		private async Task updateName() {
			do {
				Console.Clear();
				Console.Write("Nuevo nombre: ");
				user.name = Console.ReadLine();
			} while (user.name == "");

			await apiClient.updateName(user);
		}
		
		private async Task updateEmail() {
			do {
				Console.Clear();
				Console.Write("Nuevo email: ");
				user.email = Console.ReadLine();
			} while (user.email == "");

			await apiClient.updateEmail(user);
		}
		
		private async Task updatePassword() {
			do {
				Console.Clear();
				Console.Write("Nuevo password: ");
				user.password = Console.ReadLine();
			} while (user.password == "");

			await apiClient.updatePassword(user);
		}

		private async Task deleteContact() {
			showContatcs();

			int opc;

			do {
				Console.Write("ID: ");
				opc = int.Parse(Console.ReadLine());
			} while (!isContact(opc));

			await apiClient.deleteContact(user, opc);
			await getContacts();
		}

		private async Task addContact() {
			List<Contact> noAdded = await getContactsNoAdded();

			Console.Clear();

			foreach (Contact c in noAdded) {
				Console.WriteLine($"ID: {c.id}");
				Console.WriteLine($"Nombre: {c.name}");
				Console.WriteLine($"Email: {c.email}\n");
			}

			int opc;

			do {
				Console.Write("ID: ");
				opc = int.Parse(Console.ReadLine());
			} while (!isContact(noAdded, opc));

			await apiClient.addContact(user, opc);
			await getContacts();
		}

		private bool isContact(int id) {
			foreach (Contact contact in contacts) {
				if (contact.id == id) return true;
			}

			return false;
		}

		private bool isContact(List<Contact> contacts, int id) {
			foreach (Contact contact in contacts) {
				if (contact.id == id) return true;
			}

			return false;
		}

		private async Task<List<Contact>> getContactsNoAdded() {
			return await apiClient.getContactsNoAdded(user);
		}
	}
}
