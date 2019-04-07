
class AttentionHub {
	constructor() {
		this.onAlertFritz = null;
<<<<<<< HEAD
		this.onSummonScott = null;
=======
		this.onPlaySound = null;
>>>>>>> Add a play sound command and a stop command to stop currently playing sounds.
		this.debug = true;
		this._hub = null;
	}

	start(groups) {
		let url = groups ? "/attentionhub?groups=" + groups : "/attentionhub";
		this._hub = new signalR.HubConnectionBuilder()
			.withUrl(url)
			.withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
			.build();

		this._hub.onclose(() => {
			if (this.debug) console.debug("hub connection closed");

			// Hub connection was closed for some reason
			let interval = setInterval(() => {
				this.start(groups).then(() => {
					clearInterval(interval);
					if (this.debug) console.debug("hub reconnected");
				});
			}, 5000);
		});

		this._hub.on("ClientConnected", connectionId => {
			if (this.debug) console.debug(`Client connected: ${connectionId}`);
		});

		this._hub.on("AlertFritz", () => {
			if (this.debug) console.debug("AlertFritz");
			if (this.onAlertFritz) this.onAlertFritz();
		});

<<<<<<< HEAD
		this._hub.on("SummonScott", () => {
			if (this.debug) console.debug("Summoning Scott!");
			if (this.onSummonScott) this.onSummonScott();
		});

=======
		this._hub.on("PlaySound", (soundName) => {
			if (this.debug) console.debug(`PlaySound: ${soundName}`);
			if (this.onPlaySound) this.onPlaySound(soundName);
		});

		this._hub.on("StopSound", () => {
			if (this.debug) console.debug("StopSound");
			if (this.onStopSound) this.onStopSound();
		})

>>>>>>> Add a play sound command and a stop command to stop currently playing sounds.
		return this._hub.start();
	}

	sendTest() {
	  if (this.debug) console.debug("sending AlertFritz");
	  this._hub.invoke("AlertFritz");
	}
}
