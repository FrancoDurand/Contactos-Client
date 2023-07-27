using ContactosApp;
using Newtonsoft.Json;

class Program {
	static async Task Main() {
		Menu menu = new Menu();
		
		await menu.menu();
	}
}
