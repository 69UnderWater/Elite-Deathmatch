var browser = null;

let carJsonList = ""

mp.events.add("Client:Garage:Open", (...data) => {
	if(browser == null) {
		mp.events.call("client:createGarageCam")
		browser = mp.browsers.new("package://cef/Garage/index.html");
		mp.gui.cursor.show(true, true);
		mp.gui.chat.show(false);
		mp.gui.chat.activate(false);

		setTimeout(() => {
			if (carJsonList.length == 0) {
				carJsonList = data[0]
			}

			browser.execute(`LoadVehicles(${JSON.stringify(carJsonList)})`)
		}, 20)
	}
});

let currentVehicle = null

mp.events.add("client:showcaseVehicle", hash => {
	if (currentVehicle === null) {
		currentVehicle = mp.vehicles.new(mp.game.joaat(hash), new mp.Vector3(-178.14297, -166.18355, 44.032265), {
			heading: 101.59156,
			alpha: 255,
			engine: true,
			locked: true,
			numberPlate: "MG13",
			color: [255, 255, 255]
		})
	} else {
		currentVehicle.destroy()
		currentVehicle = null

		currentVehicle = mp.vehicles.new(mp.game.joaat(hash), new mp.Vector3(-178.14297, -166.18355, 44.032265), {
			heading: 101.59156,
			alpha: 255,
			engine: true,
			locked: true,
			numberPlate: "MG13",
			color: [255, 255, 255]
		})
	}

})

mp.events.add("Client:Garage:ParkOut", (hash, level) => {
	if(browser == null) return;

	mp.events.callRemote("Server:Garage:ParkOut", hash, level);
});

mp.events.add("Client:Garage:Close", () => {
	if(browser == null) return;
	mp.gui.cursor.show(false, false);
	mp.gui.chat.show(true);
	mp.gui.chat.activate(true);
	browser.destroy();
	browser = null;
	mp.events.call("client:deleteGarageCam")
});