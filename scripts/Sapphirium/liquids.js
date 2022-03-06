const charge = 1;
const surgeMass = extend(Liquid, "surge-mass", {
	setStats(){
		this.super$setStats();
	    this.stats.addPercent(Stat.charge, charge);
	},
	temperature: 1,
	viscosity: 0.8,
	effect: StatusEffects.shocked,
	color: Pal.surge,
	lightColor: Color.valueOf("fff8a5"),
});

const statuses = require("Sapphirium/statuses/statuses");

const ledonite = extend(Liquid, "ledonite-liquid", {
effect: statuses.superFreezing,
});

module.exports = {
	surgeMass: surgeMass,
	ledonite: ledonite
	}